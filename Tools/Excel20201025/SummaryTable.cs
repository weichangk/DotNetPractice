using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel20201025
{
    public class SummaryTable
    {
        /// <summary>
        /// 发票申请编号
        /// </summary>
        public string InvoiceApplicationNo { get; set; }
        /// <summary>
        /// 发票号码
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string ContractAmount { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string InvoiceType { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        public string Rate { get; set; }
        /// <summary>
        /// 开票金额
        /// </summary>
        public string MakeOutInvoiceAmount { get; set; }
        /// <summary>
        /// 单位全称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 甲方开票信息
        /// </summary>
        public string BillingInfo { get; set; }
        /// <summary>
        /// 单位税号
        /// </summary>
        public string UnitEin { get; set; }
        /// <summary>
        /// 开户行名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 开户行账号
        /// </summary>
        public string BankNo { get; set; }
        //            
        //公司地址
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 公司电话
        /// </summary>
        public string CompanyPhone { get; set; }
        /// <summary>
        /// 开票货物或应税劳务、服务名称
        /// </summary>
        public string LaborService { get; set; }
        /// <summary>
        /// 项目地点
        /// </summary>
        public string ProjectSite { get; set; }
        /// <summary>
        /// 工程名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 养护备注时间
        /// </summary>
        public string CuringTime { get; set; }
          
    }
}
