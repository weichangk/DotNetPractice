using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel20201025
{
    public class Table2
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 甲方开票信息
        /// </summary>
        public string BillingInfo { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string ContractAmount { get; set; }
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
    }
}
