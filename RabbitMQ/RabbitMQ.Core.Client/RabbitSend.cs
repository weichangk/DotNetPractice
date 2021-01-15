using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Text;

namespace RabbitMQ.Core.Client
{
    public class RabbitSend
    {
        /// <summary>
        /// 发送消息队列
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="type"></param>
        /// <param name="routingKey"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SendMsgProxy(string exchange, string type, string routingKey, object obj)
        {
            if (obj == null) return "推送的数据为空！";

            string errorMessage = string.Empty;

            try
            {
                var factory = RabbitBuilder.CurrentConnectionFactory;

                if (factory == null) return "推送尚未初始化";

                using (IConnection connection = factory.CreateConnection())
                {
                    using (IModel channel = connection.CreateModel())
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

                    }
                }

            }
            catch (IOException ioe)
            {
                errorMessage = string.Format("推送消息发生IO异常 - \t\r\nroutingKey：{0}\t\r\n异常信息：{1}", (routingKey ?? ""), (ioe.InnerException ?? ioe).Message);
            }
            catch (Exception ex)
            {
                errorMessage = string.Format("推送消息发生异常 - \t\r\nroutingKey：{0}\t\r\n异常信息：{1}", (routingKey ?? ""), (ex.InnerException ?? ex).Message);
            }

            return errorMessage;
        }
    }
}
