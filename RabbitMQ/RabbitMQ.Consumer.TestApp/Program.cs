using RabbitMQ.Core.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer.TestApp
{
    class Program
    {
        private static string ConsumerName = "";
        private static string RabbitPushServer = "localhost";
        private static string PushServerUserName = "guest";
        private static string PushServerPassword = "guest";

        static void Main(string[] args)
        {
            Console.WriteLine("请输入信息通道名称");
            ConsumerName = Console.ReadLine();
            ExcuteFunGetPushMsg();
            Console.WriteLine("信息通道开启成功");
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }

        /// <summary>
        /// 接收推送消息
        /// </summary>
        /// <param name="completeEvent"></param>
        /// <param name="refreshSaleItemInfoHandle"></param>
        public static void ReceiveMessage(EventHandler<ClientRabbitArgs> completeEvent)
        {

            RabbitBuilder.CreateConnectionFactory(RabbitPushServer, PushServerUserName, PushServerPassword, () => {
                Console.WriteLine($"Create Rabbit Connection:ServerIP({RabbitPushServer}) Name({PushServerUserName}) Password({PushServerPassword})");
            });

            //队列名称（保证终端唯一性）
            string queueName = $"STARCLIENT.QUERY_{ConsumerName}";

            //绑定数据处理的方法
            var reciveHandles = new List<RabbitReceiveHandleModel>()
            {

                new RabbitReceiveHandleModel()
                {
                    BindRouteKey = $"weixinKey_{ConsumerName}",
                    DataHandleFunc = (jsonData) =>
                    {
                        if (string.IsNullOrEmpty(jsonData))
                            return false;

                        completeEvent(null, new ClientRabbitArgs() { Data = jsonData, PushDataType = "weixin" });
                        return true;
                    }
                },

                new RabbitReceiveHandleModel()
                {
                    BindRouteKey = $"qqKey_{ConsumerName}",
                    DataHandleFunc = (jsonData) =>
                    {
                        if (string.IsNullOrEmpty(jsonData))
                            return false;

                        completeEvent(null, new ClientRabbitArgs() { Data = jsonData, PushDataType = "qq" });
                        return true;
                    }
                },
                new RabbitReceiveHandleModel()
                {
                    BindRouteKey = $"weiboKey_{ConsumerName}",
                    DataHandleFunc = (jsonData) =>
                    {
                        if (string.IsNullOrEmpty(jsonData))
                            return false;

                        completeEvent(null, new ClientRabbitArgs() { Data = jsonData, PushDataType = "weibo" });
                        return true;
                    }
                },
                new RabbitReceiveHandleModel()
                {
                    BindRouteKey = $"dingdingKey_{ConsumerName}",
                    DataHandleFunc = (jsonData) =>
                    {
                        if (string.IsNullOrEmpty(jsonData))
                            return false;

                        completeEvent(null, new ClientRabbitArgs() { Data = jsonData, PushDataType = "dingding" });
                        return true;
                    }
                },
            };
            RabbitReceive.ReceiveMessage(queueName
                    , "amq.direct"
                    , reciveHandles
                    , (s) => { Console.WriteLine($"info:{s}"); return true; }
                    , (s) => { Console.WriteLine($"error:{s}"); return true; });
        }
        private static bool ExcuteFunGetPushMsg()
        {
            try
            {
                ReceiveMessage(Client_CompleteEvent);

            }
            catch (Exception ex)
            {
                Console.WriteLine("接收推送消息发生异常，10秒后继续，异常信息：{0}", ex.Message);
            }

            return true;
        }
        private static void Client_CompleteEvent(object sender, ClientRabbitArgs e)
        {
            Console.WriteLine($"接受到信息 ==》 信息类型：{e.PushDataType} 信息内容：{e.Data}");
        }

    }
}
