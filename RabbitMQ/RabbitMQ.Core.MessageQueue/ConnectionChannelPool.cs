using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace RabbitMQ.Core.MessageQueue
{
    public class ConnectionChannelPool : IConnectionChannelPool, IDisposable
    {
        const int defaultPoolSize = 50;
        readonly Func<IConnection> connectionActivator;
        readonly ConcurrentQueue<IModel> pool = new ConcurrentQueue<IModel>();

        IConnection connection;
        int count;
        int maxSize;

        public ConnectionChannelPool(RabbitMQOptions options)
        {
            maxSize = defaultPoolSize;
            connectionActivator = CreateConnection(options);
        }


        /// <summary>
        /// 创建连接委托
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        static Func<IConnection> CreateConnection(RabbitMQOptions options)
        {
            var factory = new ConnectionFactory
            {
                HostName = options.HostName,
                UserName = options.UserName,
                Password = options.Password,
                Port = options.Port,
                VirtualHost = options.VirtualHost,
                RequestedConnectionTimeout = options.RequestedConnectionTimeout,
                SocketReadTimeout = options.SocketReadTimeout,
                SocketWriteTimeout = options.SocketWriteTimeout,
                AutomaticRecoveryEnabled = options.AutomaticRecoveryEnabled
            };

            return () => factory.CreateConnection();
        }


        /// <summary>
        /// 返回连接
        /// </summary>
        /// <returns></returns>
        public IConnection GetConnection()
        {
            if (connection != null && connection.IsOpen)
                return connection;

            connection = connectionActivator();
            connection.ConnectionShutdown += RabbitMq_ConnectionShutdown;
            return connection;
        }

        /// <summary>
        /// 连接中断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RabbitMq_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {

        }

        public IModel Rent()
        {
            IModel model;
            if (pool.TryDequeue(out model))
            {
                Interlocked.Decrement(ref count);

                return model;
            }

            model = GetConnection().CreateModel();

            return model;
        }

        public bool Return(IModel model)
        {
            if (Interlocked.Increment(ref count) <= maxSize)
            {
                pool.Enqueue(model);
                return true;
            }

            Interlocked.Decrement(ref count);

            return false;
        }


        public void Dispose()
        {
            maxSize = 0;
            IModel model;
            while (pool.TryDequeue(out model))
            {
                model.Dispose();
            }
        }
    }
}
