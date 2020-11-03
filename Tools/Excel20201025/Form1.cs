using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel20201025
{
    public partial class Form1 : Form
    {
        readonly List<Table1> ListT1= new List<Table1>();
        readonly List<Table2> ListT2 = new List<Table2>();
        readonly List<Table3> ListT3 = new List<Table3>();
        readonly List<SummaryTable> SummaryTable = new List<SummaryTable>();
        public Form1()
        {
            InitializeComponent();
            this.button1.Visible = false;
            this.button2.Visible = false;
            this.button3.Visible = false;
            this.button4.Visible = false;
            this.label5.Text = "";
        }

        private List<Table1> LoadListTable1(string excelName)
        {
            List<string> sheetList = ExcelHelper.GetExcelSheetNames(excelName);
            sheetList.ForEach(sheetName =>
            {
                var datatable = ExcelHelper.GetExcelContent(excelName, sheetName);
                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    if (datatable.Rows[i]["发票号码"].ToString() != string.Empty)
                    {
                        ListT1.Add(
                            new Table1
                            {
                                InvoiceApplicationNo = datatable.Rows[i]["发票申请表编号"].ToString(),//发票申请表编号
                                    InvoiceNo = datatable.Rows[i]["发票号码"].ToString(),//发票号码,
                                    UnitName = datatable.Rows[i]["单位全称"].ToString(),//单位全称,
                                    ItemName = datatable.Rows[i]["工程名称"].ToString(),//工程名称
                                    MakeOutInvoiceAmount = datatable.Rows[i]["开票金额"].ToString(),//开票金额
                                    InvoiceType = datatable.Rows[i]["发票类型"].ToString(),//发票类型
                                    Rate = datatable.Rows[i]["税率"].ToString(),//税率                    
                            });
                    }
                }
            });
            //foreach (var item in ListT1)
            //{
            //    Debug.WriteLine(
            //        item.InvoiceApplicationNo.ToString() + "    " +
            //        item.InvoiceNo.ToString() + "    " +
            //        item.UnitName.ToString() + "    " +
            //        item.ItemName.ToString() + "    " +
            //        item.MakeOutInvoiceAmount.ToString() + "    " +
            //        item.InvoiceType.ToString() + "    " +
            //        item.Rate.ToString());
            //}
            return ListT1;
        }
        private List<Table2> LoadListTable2(string excelName)
        {
            var sheetList = ExcelHelper.GetExcelSheetNames(excelName);
            sheetList.ForEach(sheetName =>
            {
                var datatable = ExcelHelper.GetExcelContent(excelName, sheetName);
                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    string projectName = string.Empty;
                    string contractAmount = string.Empty;
                    string billingInfo = string.Empty;
                    var projectNameArray = datatable.Rows[i]["项目名称"].ToString().Split('、');
                    if (projectNameArray.Length > 1)
                    {
                        projectName = datatable.Rows[i]["项目名称"].ToString().Split('、')[1];//项目名称,
                    }
                    else
                    {
                        projectName = datatable.Rows[i]["项目名称"].ToString();//项目名称,
                    }
                    projectName.Replace("项目名称：", "");

                    contractAmount = datatable.Rows[i]["合同金额（元）"].ToString();//合同金额
                    billingInfo = datatable.Rows[i]["甲方开票信息"].ToString();//甲方开票信息
                    if (!string.IsNullOrEmpty(projectName) && !string.IsNullOrEmpty(contractAmount) && !string.IsNullOrEmpty(billingInfo))
                    {
                        ListT2.Add(
                            new Table2
                            {
                                ProjectName = projectName,
                                ContractAmount = contractAmount,
                                BillingInfo = billingInfo,
                            });
                    }
                }
            });

            //foreach (var item in ListT2)
            //{
            //    Debug.WriteLine(
            //        item.ProjectName.ToString() + "    " +
            //        item.ContractAmount.ToString() + "    " +
            //        item.BillingInfo.ToString());
            //}
            return ListT2;
        }
        private List<Table3> LoadListTable3(string excelName)
        {
            var sheetList = ExcelHelper.GetExcelSheetNames(excelName);
            sheetList.ForEach(sheetName =>
            {
                var datatable = ExcelHelper.GetExcelContent(excelName, sheetName);
                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    string invoiceNo = string.Empty;
                    string laborService = string.Empty;
                    string note = string.Empty;
                    string projectSite = string.Empty;
                    string projectName = string.Empty;

                    invoiceNo = datatable.Rows[i]["发票号码"].ToString();//发票号码
                    laborService = datatable.Rows[i]["开票货物或应税劳务、服务名称"].ToString();//开票货物或应税劳务、服务名称
                    note = datatable.Rows[i]["备注"].ToString();//备注

                    if (note.Contains("项目地址：") || note.Contains("项目地点：") || note.Contains("项目名称："))
                    {
                        note.Replace("项目地址：", "|项目地址：").Replace("项目地点：", "|项目地点：").Replace("项目名称：", "|项目名称：");
                        var noteArray = note.Split('|');
                        foreach (var item in noteArray)
                        {
                            if (item.Contains("项目地址：") || item.Contains("项目地点："))
                            {
                                projectSite = item.Replace("项目地址：", "").Replace("项目地点：", "");//项目地址
                            }
                            else if (item.Contains("项目名称："))
                            {
                                projectName = item.Replace("项目名称：", "");//项目名称
                            }
                        }
                    }
                    if (invoiceNo != string.Empty && laborService != string.Empty)
                    {
                        ListT3.Add(
                        new Table3
                        {
                            InvoiceNo = invoiceNo,
                            LaborService = laborService,
                            ProjectSite = projectSite,
                            ProjectName = projectName,
                        });
                    }
                    else if (invoiceNo == string.Empty && laborService == string.Empty && note != string.Empty)
                    {
                        if (note.Contains("项目地址：") || note.Contains("项目地点：") || note.Contains("项目名称："))
                        {
                            note.Replace("项目地址：", "|项目地址：").Replace("项目地点：", "|项目地点：").Replace("项目名称：", "|项目名称：");
                            var noteArray = note.Split('|');
                            foreach (var item in noteArray)
                            {
                                if (item.Contains("项目地址：") || item.Contains("项目地点："))
                                {
                                    projectSite = item.Replace("项目地址：", "").Replace("项目地点：", "");//项目地址
                                }
                                else if (item.Contains("项目名称："))
                                {
                                    projectName = item.Replace("项目名称：", "");//项目名称
                                }
                            }
                        }
                        if (projectSite != string.Empty) ListT3[ListT3.Count - 1].ProjectSite = projectSite;
                        if (projectName != string.Empty) ListT3[ListT3.Count - 1].ProjectName = projectName;
                    }
                }

                //foreach (var item in ListT3)
                //{
                //    Debug.WriteLine(
                //        item.InvoiceNo.ToString() + "    " +
                //        item.LaborService.ToString() + "    " +
                //        item.ProjectSite.ToString() + "    " +
                //        item.ProjectName.ToString());
                //}
            });
            return ListT3;
        }
        private List<SummaryTable> LoadSummaryTable()
        {
            if (ListT1.Count > 0 && ListT2.Count > 0 && ListT3.Count > 0)
            {
                var temp1 = from l1 in ListT1
                            from l3 in ListT3
                            where l1.InvoiceNo.Contains(l3.InvoiceNo)
                            select new
                            {
                                l1.InvoiceApplicationNo,
                                l1.InvoiceNo,
                                l3.ProjectName,
                                //l2.ContractAmount,
                                l1.InvoiceType,
                                l1.Rate,
                                l1.MakeOutInvoiceAmount,
                                l1.UnitName,
                                //l2.UnitEin,
                                //l2.BankName,
                                //l2.BankNo,
                                //l2.CompanyAddress,
                                //l2.CompanyPhone,
                                l3.LaborService,
                                l3.ProjectSite,
                                l1.ItemName,
                            };


                var summaryTable = from t1 in temp1
                                   join l2 in ListT2 on t1.ProjectName equals l2.ProjectName
                                   select new
                                   {
                                       t1.InvoiceApplicationNo,
                                       t1.InvoiceNo,
                                       t1.ProjectName,
                                       l2.ContractAmount,
                                       t1.InvoiceType,
                                       t1.Rate,
                                       t1.MakeOutInvoiceAmount,
                                       t1.UnitName,
                                       l2.BillingInfo,
                                       //l2.UnitEin,
                                       //l2.BankName,
                                       //l2.BankNo,
                                       //l2.CompanyAddress,
                                       //l2.CompanyPhone,
                                       t1.LaborService,
                                       t1.ProjectSite,
                                       t1.ItemName,
                                   };


                foreach (var item in summaryTable)
                {
                    SummaryTable.Add(
                    new SummaryTable
                    {
                        InvoiceApplicationNo = item.InvoiceApplicationNo,
                        InvoiceNo = item.InvoiceNo,
                        ProjectName = item.ProjectName,
                        ContractAmount = item.ContractAmount,
                        InvoiceType = item.InvoiceType,
                        Rate = item.Rate,
                        MakeOutInvoiceAmount = item.MakeOutInvoiceAmount,
                        UnitName = item.UnitName,
                        BillingInfo = item.BillingInfo,
                        //UnitEin = item.UnitEin,
                        //BankName = item.BankName,
                        //BankNo = item.BankNo,
                        //CompanyAddress = item.CompanyAddress,
                        //CompanyPhone = item.CompanyPhone,
                        LaborService = item.LaborService,
                        ProjectSite = item.ProjectSite,
                        ItemName = item.ItemName,
                    });
                }

                //foreach (var item in SummaryTable)
                //{
                //    Debug.WriteLine(
                //        item.InvoiceApplicationNo.ToString() + "    " +
                //        item.InvoiceNo.ToString() + "    " +
                //        item.ProjectName.ToString() + "    " +
                //        item.ContractAmount.ToString() + "    " +
                //        item.InvoiceType.ToString() + "    " +
                //        item.Rate.ToString() + "    " +
                //        item.MakeOutInvoiceAmount.ToString() + "    " +
                //        item.UnitName.ToString() + "    " +
                //        item.BillingInfo.ToString() + "    " +
                //        //item.UnitEin.ToString() + "    " +
                //        //item.BankName.ToString() + "    " +
                //        //item.BankNo.ToString() + "    " +
                //        //item.CompanyAddress.ToString() + "    " +
                //        //item.CompanyPhone.ToString() + "    " +
                //        item.LaborService.ToString() + "    " +
                //        item.ProjectSite.ToString() + "    " +
                //        item.ItemName.ToString());
                //}
            }
            return SummaryTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadListTable1(this.textBox1.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LoadListTable2(this.textBox2.Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            LoadListTable3(this.textBox3.Text);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            LoadSummaryTable();
        }



        private void BtnOpenExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openExcel = new OpenFileDialog();//实例化打开对话框对象
            openExcel.Filter = "Excel文件|*.xlsx";//设置打开文件筛选器
            openExcel.Multiselect = false;//设置打开对话框中不能多选
            if (openExcel.ShowDialog() == DialogResult.OK)//判断是否选择了文件
            {
                textBox1.Text = openExcel.FileName;//显示选择的Excel文件
            }
        }

        private void BtnOpenExce2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openExcel = new OpenFileDialog();//实例化打开对话框对象
            openExcel.Filter = "Excel文件|*.xlsx";//设置打开文件筛选器
            openExcel.Multiselect = false;//设置打开对话框中不能多选
            if (openExcel.ShowDialog() == DialogResult.OK)//判断是否选择了文件
            {
                textBox2.Text = openExcel.FileName;//显示选择的Excel文件
            }
        }

        private void BtnOpenExce3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openExcel = new OpenFileDialog();//实例化打开对话框对象
            openExcel.Filter = "Excel文件|*.xlsx";//设置打开文件筛选器
            openExcel.Multiselect = false;//设置打开对话框中不能多选
            if (openExcel.ShowDialog() == DialogResult.OK)//判断是否选择了文件
            {
                textBox3.Text = openExcel.FileName;//显示选择的Excel文件
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ListT1.Clear();
            ListT2.Clear();
            ListT3.Clear();
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.label5.Text = "";
            this.BtnCollect.Enabled = true;
        }

        private void BtnCollect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox1.Text =="" || this.textBox2.Text == "" || this.textBox3.Text == "")
                {
                    MessageBox.Show("请先选择对应Excel文件！！！");
                    return;
                }
                this.label5.Text = "正在生成汇总表......";

                this.BtnCollect.Enabled = false;
                LoadListTable1(this.textBox1.Text);
                LoadListTable2(this.textBox2.Text);
                LoadListTable3(this.textBox3.Text);
                LoadSummaryTable();

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("发票申请表编号", typeof(String)));
                dt.Columns.Add(new DataColumn("工程名称", typeof(String)));
                dt.Columns.Add(new DataColumn("合同金额", typeof(String)));
                dt.Columns.Add(new DataColumn("结算金额", typeof(String)));
                dt.Columns.Add(new DataColumn("发票类型", typeof(String)));
                dt.Columns.Add(new DataColumn("税率", typeof(String)));
                dt.Columns.Add(new DataColumn("开票金额", typeof(String)));
                dt.Columns.Add(new DataColumn("累计开票金额", typeof(String)));
                dt.Columns.Add(new DataColumn("单位全称", typeof(String)));
                dt.Columns.Add(new DataColumn("单位税号", typeof(String)));
                dt.Columns.Add(new DataColumn("开户行名称", typeof(String)));
                dt.Columns.Add(new DataColumn("开户行账号", typeof(String)));
                dt.Columns.Add(new DataColumn("公司地址", typeof(String)));
                dt.Columns.Add(new DataColumn("电话", typeof(String)));
                dt.Columns.Add(new DataColumn("开票货物或应税劳务服务名称", typeof(String)));
                dt.Columns.Add(new DataColumn("项目地点", typeof(String)));
                dt.Columns.Add(new DataColumn("项目名称", typeof(String)));
                dt.Columns.Add(new DataColumn("养护备注时间", typeof(String)));
                dt.Columns.Add(new DataColumn("中绿开户行名称", typeof(String)));
                dt.Columns.Add(new DataColumn("中绿开户行账号", typeof(String)));
                dt.Columns.Add(new DataColumn("合同编号", typeof(String)));

                for (int i = 0; i < SummaryTable.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["发票申请表编号"] = SummaryTable[i].InvoiceApplicationNo;
                    dr["工程名称"] = SummaryTable[i].ItemName;
                    dr["合同金额"] = SummaryTable[i].ContractAmount;
                    dr["结算金额"] = "";
                    dr["发票类型"] = SummaryTable[i].InvoiceType;
                    dr["税率"] = SummaryTable[i].Rate;
                    dr["开票金额"] = SummaryTable[i].MakeOutInvoiceAmount;
                    dr["累计开票金额"] = "";
                    dr["单位全称"] = SummaryTable[i].BillingInfo;
                    dr["单位税号"] = "";
                    dr["开户行名称"] = "";
                    dr["开户行账号"] = "";
                    dr["公司地址"] = "";
                    dr["电话"] = "";
                    dr["开票货物或应税劳务服务名称"] = SummaryTable[i].LaborService;
                    dr["项目地点"] = SummaryTable[i].ProjectSite;
                    dr["项目名称"] = SummaryTable[i].ProjectName;
                    dr["养护备注时间"] = "";
                    dr["中绿开户行名称"] = "";
                    dr["中绿开户行账号"] = "";
                    dr["合同编号"] = "";
                    dt.Rows.Add(dr);
                }
                ExcelHelper.TableToExcel(dt);
                this.label5.Text = "汇总表生成完成";
            }
            catch (Exception ex)
            {
                this.label5.Text = "汇总表生成失败！！！";
                using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\log.txt", true, Encoding.UTF8))
                {
                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine("#################################");
                    sw.WriteLine(ex.Message + "/r/n" + ex.StackTrace);
                    sw.WriteLine("#################################");
                }
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ListT1.Clear();
                ListT2.Clear();
                ListT3.Clear();
                this.BtnCollect.Enabled = true;
            }

        }
    }
}
