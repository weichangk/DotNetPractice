using Microsoft.Office.Interop.Excel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Xls;
using NPOI.XSSF.UserModel;
using NPOI.SS.Formula.Functions;

namespace Excel20201025
{
    public class ExcelHelper
    {

        /// <summary>
        /// 根据Excel文件获取所有的sheet名称
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> GetExcelSheetNames(string filePath)
        {
            OleDbConnection connection = null;
            System.Data.DataTable dt = null;
            try
            {
                String connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'", filePath);
                //String connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'", filePath);
                connection = new OleDbConnection(connectionString);
                connection.Open();
                dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return new List<string>();
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString().Split('$')[0];
                    i++;
                }
                return excelSheets.Distinct().ToList();
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\log.txt", true, Encoding.UTF8))
                {
                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine("#################################");
                    sw.WriteLine(ex.Message + "/r/n" + ex.StackTrace);
                    sw.WriteLine("#################################");
                }
                return new List<string>();
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// 获取每一个Sheet的内容组装dataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetExcelContent(String filePath, string sheetName)
        {
            if (sheetName == "_xlnm#_FilterDatabase")
                return null;
            DataSet dateSet = new DataSet();
            String connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=NO;IMEX=1;'", filePath);
            String commandString = string.Format("SELECT * FROM [{0}$]", sheetName);
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(commandString, connection))
                {
                    OleDbCommand objCmd = new OleDbCommand(commandString, connection);
                    OleDbDataAdapter myData = new OleDbDataAdapter(commandString, connection);
                    myData.Fill(dateSet, sheetName);
                    System.Data.DataTable table = dateSet.Tables[sheetName];
                    for (int i = 0; i < table.Rows[0].ItemArray.Length; i++)
                    {
                        var cloumnName = table.Rows[0].ItemArray[i].ToString();
                        if (!string.IsNullOrEmpty(cloumnName))
                            table.Columns[i].ColumnName = cloumnName;
                    }


                    table.Rows.RemoveAt(0);
                    return table;
                }
            }
        }


        //public object ExcelToJson(string filePath)
        //{
        //    string localPath = Server.MapPath(filePath);
        //    List<string> tableNames = GetExcelSheetNames(localPath);
        //    var json = new JObject();
        //    tableNames.ForEach(tableName =>
        //    {
        //        var table = new JArray() as dynamic;
        //        DataTable dataTable = GetExcelContent(localPath, tableName);
        //        foreach (DataRow dataRow in dataTable.Rows)
        //        {
        //            dynamic row = new JObject();
        //            foreach (DataColumn column in dataTable.Columns)
        //            {
        //                row.Add(column.ColumnName, dataRow[column.ColumnName].ToString());
        //            }
        //            table.Add(row);
        //        }
        //        json.Add(tableName, table);
        //    });
        //    return json.ToString();
        //}


        /// <summary>
        /// Datable导出成Excel(xlsx)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public static void TableToExcel(System.Data.DataTable dt)
        {
            Debug.WriteLine("开始生成Excel");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string timeStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            string fileName = path + "\\" + timeStr + ".xlsx";
            FileStream fs = new FileStream(fileName, FileMode.Create);

            //使用NPOI开发包生成Excel
            //HSSFWorkbook workbook = new HSSFWorkbook();//创建excel对象 xls
            IWorkbook workbook = new XSSFWorkbook();//xlsx
            ICellStyle cellStyle = workbook.CreateCellStyle();//这里是定义单元格样式，注意：样式要指定Workbook来创建的，类似于html中的css。
            cellStyle.FillPattern = FillPattern.SolidForeground;//设置样式
            cellStyle.FillForegroundColor = HSSFColor.Red.Index;//设置背景色

            ISheet sheet = workbook.CreateSheet("sheet1");//在当前excel文件中创建一个sheet
            IRow rowColumns = sheet.CreateRow(0);//创建第一行
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = rowColumns.CreateCell(i, CellType.String);
                cell.SetCellValue(dt.Columns[i].ToString());
                //cell.CellStyle = cellStyle;//将本Workbook定义过的样式应用到这个单元格

            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow rowData = sheet.CreateRow(i+1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = rowData.CreateCell(j, CellType.String);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            //保存文件
            workbook.Write(fs);
            fs.Close();
            fs.Dispose();

            Debug.WriteLine("生成Excel成功");
        }
    }
}
