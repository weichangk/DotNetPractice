using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel20201025
{
    public class Table3
    {
        /// <summary>
        /// 发票号码
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// 开票货物或应税劳务、服务名称
        /// </summary>
        public string LaborService { get; set; }
        public string Note { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目地点
        /// </summary>
        public string ProjectSite { get; set; }

    }
}
