using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Client
{
    public class ClientRabbit
    {
        //public static void ReceiveMessage(EventHandler<ClientRabbitArgs> completeEvent)
        //{
        //    var clientUser = ClientUserContext.Current.User;
        //    var clientSale = ClientUserContext.Current.Sale;
        //    string tenantCode = clientUser.TenantCode;
        //    long branchId = clientUser.BranchId;
        //    string branchCode = clientUser.BranchCode;
        //    string clientCode = clientSale.ClientCode;

        //    // 1.创建连接
        //    var host = ClientUserContext.CurrentHost;
        //    if (host == null || string.IsNullOrEmpty(host.RabbitPushServer) || string.IsNullOrEmpty(host.PushServerUserName))
        //        return;

        //    RabbitBuilder.CreateConnectionFactory(host.RabbitPushServer, host.PushServerUserName, host.PushServerPassword, () =>
        //    {
        //        try
        //        {
        //            RabbitMQLogsRequest request = new RabbitMQLogsRequest();
        //            request.AppName = "cloud.pos.star.Windows";
        //            request.TenantID = ClientUserContext.Current.User.TenantId;
        //            request.TenantCode = ClientUserContext.Current.User.TenantCode;
        //            request.BranchID = ClientUserContext.Current.User.BranchId;
        //            request.BranchID = ClientUserContext.Current.User.OperatorId;
        //            request.GUID = System.Net.Dns.GetHostName();
        //            request.Use = "LS_STAR_PC";
        //            request.ClientIP = request.GUID;//ClientAppDefine.LOGER_API_ADDRESS
        //            var result = HttpHelper.PostLoger(ClientAppDefine.LOGER_API_ADDRESS + request.GetApiName(), request);
        //        }
        //        catch (Exception ex)
        //        {
        //            ClientLogger.InfoFormat(ex.Message);
        //        }
        //    }
        //    );

        //    // 2.查询的名称（保证终端唯一性）
        //    string queueName = $"STARCLIENT.QUERY_{tenantCode}_{branchCode}_{clientCode}";

        //    // 3.绑定数据处理的方法
        //    var reciveHandles = new List<RabbitReceiveHandleViewModel>()
        //    {
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"ItemKey_{tenantCode}",
        //            DataHandleFunc = (jsonData) =>{ return HandleReceiveData<Item>(jsonData,completeEvent); }
        //        },
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"ItemMultcodesKey_{tenantCode}",
        //            DataHandleFunc = (jsonData) =>{ return HandleReceiveData<ItemMultcode>(jsonData,completeEvent); }
        //        },
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"CategoryKey_{tenantCode}",
        //            DataHandleFunc = (jsonData) =>{ return HandleReceiveData<Category>(jsonData,completeEvent); }
        //        },
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"PromotionKey_{tenantCode}",
        //            DataHandleFunc = (jsonData) =>{ return HandleReceiveData<PromotionWorking>(jsonData,completeEvent); }
        //        },
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"WinXinOrderKey_{tenantCode}",
        //            DataHandleFunc = (jsonData) =>
        //            {
        //                if (!jsonData.HasValue())
        //                    return false;

        //                return ClientFunc.ExceptionHandle(() =>
        //                {
        //                    completeEvent(null, new ClientRabbitArgs() { Data = jsonData, PushDataType = PushDataType.WinXinOrder });
        //                },"接收微订单消息");
        //            }
        //        },
        //        // 商户设置
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"Settings_{tenantCode}",
        //            DataHandleFunc = (jsonData) => HandleReceiveSettingData(jsonData,completeEvent)
        //        },
        //        // 门店设置
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"Settings_{tenantCode}_{branchCode}",
        //            DataHandleFunc = (jsonData) => HandleReceiveSettingData(jsonData,completeEvent)
        //        },
        //        // 终端设置
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"Settings_{tenantCode}_{branchCode}_{clientCode}",
        //            DataHandleFunc = (jsonData) => HandleReceiveSettingData(jsonData,completeEvent)
        //        },
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"NoticeKey",
        //            DataHandleFunc = (jsonData) =>
        //            {
        //                if (!jsonData.HasValue())
        //                    return false;

        //                return ClientFunc.ExceptionHandle(() =>
        //                {
        //                    completeEvent(null, new ClientRabbitArgs() { Data = jsonData, PushDataType = PushDataType.Notice });
        //                    return;
        //                }, "接收系统消息推送");
        //            }
        //        },
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"ClientCustom_{tenantCode}_{branchCode}_{clientCode}",
        //            DataHandleFunc = (jsonData) =>{ return HandleReceiveData<string>(jsonData,completeEvent); }
        //        },
        //        //chensb 2020-08-06 Add 添加运营平台后台更新消息推送
        //        new RabbitReceiveHandleViewModel()
        //        {
        //            BindRouteKey = $"Release_NoticeKey",
        //            DataHandleFunc = (jsonData) =>
        //            {
        //                if (!jsonData.HasValue())
        //                    return false;

        //                return ClientFunc.ExceptionHandle(() =>
        //                {
        //                    completeEvent(null, new ClientRabbitArgs() { Data = jsonData, PushDataType = PushDataType.Release });
        //                    return;
        //                }, "接收系统消息推送");
        //            }
        //        },
        //    };
        //    RabbitReceive.ReceiveMessage(queueName
        //            , "amq.direct"
        //            , reciveHandles
        //            , (s) => { return ClientLogger.InfoFormat(s); }
        //            , (s) => { return ClientLogger.ErrorFormat(s); });
        //}
    }

}
