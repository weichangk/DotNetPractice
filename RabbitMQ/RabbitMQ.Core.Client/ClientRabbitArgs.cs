using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Client
{
    /// <summary>
    /// 接收到消息的事件
    /// </summary>
    public class ClientRabbitArgs : EventArgs
    {
        public string PushDataType { get; set; }
        public string Data { get; set; }
    }
}
