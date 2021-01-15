using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.MessageQueue
{
    public class RabbitPushServer
    {
        readonly IConnectionChannelPool connectionChannelPool;
        private  RabbitMQOptions options;

        private RabbitPushServer()
        {
            InitConnectionFactory();

            this.connectionChannelPool = new ConnectionChannelPool(options);
        }

        public static RabbitPushServer Client { get; } = new RabbitPushServer();

        private void InitConnectionFactory()
        {
            try
            {
                //if (factory == null)
                //    factory = new ConnectionFactory();
                
                string hostName = ConfigurationManager.AppSettings["RabbitPushServer"].ToString().Trim();

                if (string.IsNullOrEmpty(hostName))
                {
                    //Logger.Info("获取推送服务器地址失败");
                    return;
                }

                string userName = ConfigurationManager.AppSettings["PushServerUserName"].ToString().Trim();

                if (string.IsNullOrEmpty(userName))
                {
                    //Logger.Info("获取推送服务器用户名失败");
                    return;
                }

                string password = ConfigurationManager.AppSettings["PushServerPassword"].ToString().Trim();

                if (string.IsNullOrEmpty(password))
                {
                    //Logger.Info("获取推送服务器密码失败");
                    return;
                }
                //factory.UserName = userName;
                //factory.Password = password;
                //factory.VirtualHost = "/";
                //factory.HostName = hostName;
                //factory.AutomaticRecoveryEnabled = true;//自动重连
                ////factory.HostName = "112.124.10.173:5672";
                //factory.Port = 5672;
                //connection = factory.CreateConnection();

                options = new RabbitMQOptions();
                options.UserName = userName;
                options.Password = password;
                options.HostName = hostName;
                options.VirtualHost = "/";
                options.Port = 5672;
                options.AutomaticRecoveryEnabled = true;
            }
            catch
            {

            }
        }

        /// <summary>
        /// 发送消息队列
        /// </summary>
        /// <param name="routingKey"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string SendMsgProxy(string routingKey, object obj)
        {
            return SendMsgProxy("amq.direct", ExchangeType.Direct, routingKey, obj);
        }

        /// <summary>
        /// 发送消息队列
        /// </summary>
        /// <param name="routingKey"></param>
        /// <param name="obj"></param>
        private string SendMsgProxy(string exchange, string type, string routingKey, object obj)
        {
            if (obj == null) return "推送的数据为空！";

            string errorMessage = string.Empty;

            IModel channel = connectionChannelPool.Rent();
            channel.ConfirmSelect();
            try
            {
                channel.ExchangeDeclare(exchange, type, true, false, null);

                var props = channel.CreateBasicProperties();
                props.Persistent = true;

                string json = string.Empty;
                if (obj is string)
                    json = obj as string;
                else
                    json = JsonConvert.SerializeObject(obj);

                byte[] messageBodyBytes = Encoding.UTF8.GetBytes(json);

                //指定发送的路由，通过默认的exchange直接发送到指定的队列中。
                channel.BasicPublish(exchange, routingKey, props, messageBodyBytes);

                try
                {
                    if (channel.WaitForConfirms())
                    {
                        Debug.WriteLine("RabbitMQ主动推送成功，推送信息：" + json);
                    }
                    else
                    {
                        Debug.WriteLine("RabbitMQ主动推送失败，推送信息：" + json);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("RabbitMQ主动推送失败，错误信息：" + e + "推送信息：" + json);
                }
            }
            catch (IOException ioe)
            {
                errorMessage = string.Format("IO异常，{0}", (ioe.InnerException ?? ioe).Message);
            }
            catch (Exception ex)
            {
                errorMessage = string.Format("{0}", (ex.InnerException ?? ex).Message);
            }
            finally
            {
                connectionChannelPool.Return(channel);
            }

            return errorMessage;
        }
    }
}
