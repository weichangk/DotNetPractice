using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RabbitMQ.Core.Client
{
    public class RabbitReceive
    {
        /// <summary>
        /// 接收Rabbit通知及处理
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="exchange"></param>
        /// <param name="routeKeys"></param>
        /// <param name="logHandle"></param>
        /// <param name="logErrorHandle"></param>
        /// <param name="dataHandle"></param>
        public static void ReceiveMessage(string queueName, string exchange, List<RabbitReceiveHandleModel> reciveHandles , Func<string, bool> logHandle, Func<string, bool> logErrorHandle)
        {
            try
            {
                var routeKeys = reciveHandles.Select(f => f.BindRouteKey).ToArray();
                RabbitBuilder.CreateQueryChannel(queueName, exchange, routeKeys, (obj, e) =>
                {
                    try
                    {
                        var reciveHandle = reciveHandles.FirstOrDefault(f => f.BindRouteKey == e.RoutingKey);
                        if (reciveHandle != null && reciveHandle.DataHandleFunc != null)
                        {
                            string jsonData = Encoding.UTF8.GetString(e.Body.ToArray());
                            if (!string.IsNullOrEmpty(jsonData))
                            {
                                var dataHandle = reciveHandle.DataHandleFunc;
                                dataHandle(jsonData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logErrorHandle?.Invoke($"读取推送数据失败 {ex.Message} {DateTime.Now.ToString("hh:mm:ss fff")}");
                    }
                }, logHandle);
            }
            catch (IOException ioe)
            {
                logErrorHandle?.Invoke($"推送服务，IO端口,异常信息:{ioe.Message} {DateTime.Now.ToString("hh: mm:ss fff")}");
            }
            catch (Exception ex)
            {
                logErrorHandle?.Invoke($"推送服务，等待连接,异常信息:{ex.Message} {DateTime.Now.ToString("hh:mm:ss fff")}");
            }
        }
    }
}
