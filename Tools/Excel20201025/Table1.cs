using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel20201025
{
    public class Table1
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
        /// 单位全称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 工程名称
        /// </summary>
        public string ItemName { get; set; }
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
    }
}
