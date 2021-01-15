using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;

namespace RabbitMQ.Core.Client
{
    public class RabbitBuilder
    {
        public static ConnectionFactory CurrentConnectionFactory { get; private set; }
        private static IConnection _rabbitConnection;
        private static Dictionary<string, IModel> _rabbitQueryChannels = new Dictionary<string, IModel>();

        /// <summary>
        /// 是否已初始化
        /// </summary>
        public static bool HasInitialized { get { return CurrentConnectionFactory != null; } }
        /// <summary>
        /// CreateConnectionFactory
        /// </summary>
        /// <returns></returns>
        public static ConnectionFactory CreateConnectionFactory(string hostName, string userName, string password, Action logerHandler)
        {
            if (CurrentConnectionFactory == null)
            {
                CurrentConnectionFactory = new ConnectionFactory
                {
                    HostName = hostName
                };
                var array = hostName.Split(':');
                if (array.Length == 2)
                {
                    if (!int.TryParse(array[1], out int port))
                    {
                        port = 5672;
                    }
                    CurrentConnectionFactory.Port = port;
                }
                CurrentConnectionFactory.UserName = userName;
                CurrentConnectionFactory.Password = password;
                CurrentConnectionFactory.VirtualHost = "/";
                CurrentConnectionFactory.RequestedHeartbeat = TimeSpan.FromSeconds(300);//心跳检测
                CurrentConnectionFactory.AutomaticRecoveryEnabled = true;//自动重连
                CurrentConnectionFactory.NetworkRecoveryInterval = TimeSpan.FromSeconds(5);

                logerHandler?.Invoke();
            }

            return CurrentConnectionFactory;
        }

        /// <summary>
        /// 创建查询的交换机
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="exchange"></param>
        /// <param name="bindRoutingKeys"></param>
        /// <param name="receivedHandler">接收消息的委托</param>
        /// <param name="logHandler">日志写入的委托</param>
        public static void CreateQueryChannel(string queueName, string exchange, string[] bindRoutingKeys, EventHandler<BasicDeliverEventArgs> receivedHandler, Func<string, bool> logHandler)
        {
            var channelKey = string.Format("{0}_{1}", queueName, exchange);

            // 必须要先创建连接才能调用
            if (CurrentConnectionFactory == null)
                return;

            if (_rabbitConnection == null)
                _rabbitConnection = CurrentConnectionFactory.CreateConnection();
            var channel = _rabbitQueryChannels.ContainsKey(channelKey) ? _rabbitQueryChannels[channelKey] : _rabbitConnection.CreateModel();

            // 在MQ上定义一个队列，如果名称相同不会重复创建
            channel.QueueDeclare(queueName, true, false, false, null);
            if (logHandler != null) logHandler(string.Format("已成功在exchange {0}上定义查询{1}", exchange ?? "", queueName ?? ""));
            if (bindRoutingKeys != null)
            {
                foreach (var k in bindRoutingKeys)
                {
                    channel.QueueBind(queueName, exchange, k);
                    logHandler?.Invoke(string.Format("已成功在exchange - {0},queueName - {1}的channel上绑定路由key {2}", exchange ?? "", queueName ?? "", k ?? ""));
                }

            }

            //在队列上定义一个消费者
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += receivedHandler;
            consumer.ConsumerCancelled += (obj, e) =>
            {
                //channel.Dispose();
                //channel = null;
                //_rabbitQueryChannels.Clear();
                //_rabbitConnectionFactory = null;
                logHandler?.Invoke(string.Format("Rabbit-Channel-Key:{0}已被释放！", channelKey));
            };

            channel.BasicConsume(queueName, true, consumer);

            if (!_rabbitQueryChannels.ContainsKey(channelKey)) _rabbitQueryChannels.Add(channelKey, channel);
        }

        public static void Dispose()
        {
            try
            {
                if (_rabbitQueryChannels != null)
                {
                    foreach (var channelPair in _rabbitQueryChannels)
                    {
                        if (channelPair.Value != null) channelPair.Value.Dispose();

                    }

                    _rabbitQueryChannels.Clear();
                }

                CurrentConnectionFactory = null;
            }
            catch { }
        }
    }
}
