
using System;

namespace RabbitMQ.Core.MessageQueue
{
    public class RabbitMQOptions
    {
        public RabbitMQOptions()
        {
            HostName = "localhost";
            Password = DefaultPass;
            UserName = DefaultUser;
            VirtualHost = DefaultVHost;
            TopicExchangeName = DefaultExchangeName;
            RequestedConnectionTimeout = DefaultConnectionTimeout;
            SocketReadTimeout = DefaultConnectionTimeout;
            SocketWriteTimeout = DefaultConnectionTimeout;
            Port = -1;
            QueueMessageExpires = 864000000;
        }
        /// <summary>
        /// Default value for connection attempt timeout, in milliseconds.
        /// </summary>
        public  static readonly TimeSpan DefaultConnectionTimeout = TimeSpan.FromSeconds(30);//30 * 1000;

        /// <summary>
        /// Default password (value: "guest").
        /// </summary>
        /// <remarks>PLEASE KEEP THIS MATCHING THE DOC ABOVE.</remarks>
        public const string DefaultPass = "guest";

        /// <summary>
        /// Default user name (value: "guest").
        /// </summary>
        /// <remarks>PLEASE KEEP THIS MATCHING THE DOC ABOVE.</remarks>
        public const string DefaultUser = "guest";

        /// <summary>
        /// Default virtual host (value: "/").
        /// </summary>
        /// <remarks> PLEASE KEEP THIS MATCHING THE DOC ABOVE.</remarks>
        public const string DefaultVHost = "/";

        /// <summary>
        /// Default exchange name (value: "cap.default.topic").
        /// </summary>
        public const string DefaultExchangeName = "amq.direct";

        /// <summary> The topic exchange type. </summary>
        public const string ExchangeType = "topic";

        /// <summary>The host to connect to.</summary>
        public string HostName { get; set; }

        /// <summary>
        /// Password to use when authenticating to the server.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Username to use when authenticating to the server.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Virtual host to access during this connection.
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// Topic exchange name when declare a topic exchange.
        /// </summary>
        public string TopicExchangeName { get; set; }

        /// <summary>
        /// Timeout setting for connection attempts (in milliseconds).
        /// </summary>
        public TimeSpan RequestedConnectionTimeout { get; set; }

        /// <summary>
        /// Timeout setting for socket read operations (in milliseconds).
        /// </summary>
        public TimeSpan SocketReadTimeout { get; set; }

        /// <summary>
        /// Timeout setting for socket write operations (in milliseconds).
        /// </summary>
        public TimeSpan SocketWriteTimeout { get; set; }

        /// <summary>
        /// The port to connect on.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets queue message automatic deletion time (in milliseconds). Default 864000000 ms (10 days).
        /// </summary>
        public int QueueMessageExpires { get; set; }

        /// <summary>
        /// 自动重连
        /// </summary>
        public bool AutomaticRecoveryEnabled { get; set; }
    }
}
