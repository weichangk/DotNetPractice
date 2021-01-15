using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Client
{
    public class RabbitReceiveHandleModel
    {
        /// <summary>
        /// 绑定的路由地址
        /// </summary>
        public string BindRouteKey { get; set; }

        /// <summary>
        /// 接收消息的委托
        /// </summary>
        public Func<string, bool> DataHandleFunc { get; set; }
    }
}
