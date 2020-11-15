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

namespace DesignerSorting
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private Dictionary<string, List<string>> DataLoadToDictionary(string path, out List<string> headList, out List<string> bottomList)
        {
            headList = null;
            bottomList = null;
            List<string> fileList = new List<string>();
            List<string> linesList = new List<string>();
            StringBuilder lineTemp = new StringBuilder();
            string objectName = string.Empty;
            bool begin = false;
            int lines;
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    lines = 1;
                    while (!sr.EndOfStream)
                    {
                        lines++;
                        fileList.Add(sr.ReadLine());
                    }
                }
                //Debug.WriteLine(lines);
                for (int i = 0; i < lines; i++)
                {

                    //Debug.WriteLine(fileList[i]);
                    if (fileList[i].Trim() == @"private void InitializeComponent()")//开始标志
                    {
                        begin = true;
                        objectName = "newObject";

                        headList = new List<string>();
                        for (int j = 0; j <= i + 2; j++)
                        {
                            headList.Add(fileList[j]);
                        }

                        i += 1;
                        continue;
                    }

                    if (begin && fileList[i].Trim() == "}")//结束标志
                    {
                        List<string> tempList = new List<string>();
                        tempList.AddRange(linesList);
                        linesList.Clear();
                        if (dictionary.ContainsKey(objectName))
                        {
                            dictionary[objectName].AddRange(tempList);
                        }
                        else
                        {
                            dictionary.Add(objectName, tempList);
                        }

                        bottomList = new List<string>();
                        for (int j = i - 1; j < lines - 1; j++)
                        {
                            bottomList.Add(fileList[j]);
                        }

                        return dictionary;
                    }

                    if (!begin)
                    {
                        continue;//没有开始跳过
                    }

                    if (string.IsNullOrEmpty(fileList[i]))//空行跳过
                    {
                        continue;
                    }

                    if (fileList[i].Substring(fileList[i].Length - 1) == "," || fileList[i].Substring(fileList[i].Length - 3) == "});" || fileList[i].Substring(fileList[i].Length - 2) == "};")//合并没有结束语句并删除空值};
                    {
                        lineTemp.Append(fileList[i].Trim());
                    }
                    else
                    {
                        lineTemp.Append(fileList[i]);
                    }
                    if (lineTemp.ToString().Contains(";") || lineTemp.ToString().Contains(@"//"))
                    {
                        linesList.Add(lineTemp.ToString());//有效数据追加
                        lineTemp.Clear();
                    }

                    //if (fileList[i].Trim() == @"//" && fileList[i + 1].Trim() == @"//" && fileList[i + 2].Trim() == @"//")
                    //{
                    //    i += 2;
                    //    continue;
                    //}//排除连续三行//
                    if (!fileList[i].Contains(@"//") && fileList[i + 1].Contains(@"//") && fileList[i + 2].Contains(@"//") && fileList[i + 3].Contains(@"//"))//分类
                    {
                        List<string> tempList = new List<string>();
                        tempList.AddRange(linesList);
                        linesList.Clear();
                        if (string.IsNullOrEmpty(objectName))
                        {
                            string subStr = tempList[3].Trim().Substring(5);
                            objectName = subStr.Substring(0, subStr.IndexOf("."));
                        }
                        if (dictionary.ContainsKey(objectName))
                        {
                            dictionary[objectName].AddRange(tempList);
                        }
                        else
                        {
                            dictionary.Add(objectName, tempList);
                        }
                        objectName = fileList[i + 2].Replace(@"//", "").Trim();
                    }
                }
                if (dictionary.Count == 0)
                {
                    using (System.IO.StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true, Encoding.UTF8))
                    {
                        sw.WriteLine(DateTime.Now + Environment.NewLine + "文件无效，无数据导出");
                    }
                    return null;
                }
                return dictionary;
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true, Encoding.UTF8))
                {
                    sw.WriteLine(DateTime.Now + "   :" + objectName + Environment.NewLine + ex.Message);
                }
                headList = null;
                bottomList = null;
                return null;
            }
        }
        private Dictionary<string, List<string>> RichTextDataLoadToDictionary(RichTextBox richTextBox, out List<string> headList, out List<string> bottomList)
        {
            headList = null;
            bottomList = null;
            List<string> fileList = new List<string>();
            List<string> linesList = new List<string>();
            StringBuilder lineTemp = new StringBuilder();
            string objectName = string.Empty;
            bool begin = false;
            int lines = 1;
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            try
            {
                foreach (var item in richTextBox.Lines)
                {
                    lines++;
                    fileList.Add(item);
                }

                //Debug.WriteLine(lines);
                for (int i = 0; i < lines; i++)
                {

                    //Debug.WriteLine(fileList[i]);
                    if (fileList[i].Trim() == @"private void InitializeComponent()")//开始标志
                    {
                        begin = true;
                        objectName = "newObject";

                        headList = new List<string>();
                        for (int j = 0; j <= i + 2; j++)
                        {
                            headList.Add(fileList[j]);
                        }

                        i += 1;
                        continue;
                    }

                    if (begin && fileList[i].Trim() == "}")//结束标志
                    {
                        List<string> tempList = new List<string>();
                        tempList.AddRange(linesList);
                        linesList.Clear();
                        dictionary.Add(objectName, tempList);

                        bottomList = new List<string>();
                        for (int j = i - 1; j < lines - 1; j++)
                        {
                            bottomList.Add(fileList[j]);
                        }

                        return dictionary;
                    }

                    if (!begin)
                    {
                        continue;//没有开始跳过
                    }

                    if (string.IsNullOrEmpty(fileList[i]))//空行跳过
                    {
                        continue;
                    }

                    if (fileList[i].Substring(fileList[i].Length - 1) == "," || fileList[i].Substring(fileList[i].Length - 3) == "});" || fileList[i].Substring(fileList[i].Length - 2) == "};")//合并没有结束语句并删除空值};
                    {
                        lineTemp.Append(fileList[i].Trim());
                    }
                    else
                    {
                        lineTemp.Append(fileList[i]);
                    }
                    if (lineTemp.ToString().Contains(";") || lineTemp.ToString().Contains(@"//"))
                    {
                        linesList.Add(lineTemp.ToString());//有效数据追加
                        lineTemp.Clear();
                    }

                    //if (fileList[i].Trim() == @"//" && fileList[i + 1].Trim() == @"//" && fileList[i + 2].Trim() == @"//")
                    //{
                    //    i += 2;
                    //    continue;
                    //}//排除连续三行//
                    if (!fileList[i].Contains(@"//") && fileList[i + 1].Contains(@"//") && fileList[i + 2].Contains(@"//") && fileList[i + 3].Contains(@"//"))//分类
                    {
                        List<string> tempList = new List<string>();
                        tempList.AddRange(linesList);
                        linesList.Clear();
                        if (string.IsNullOrEmpty(objectName))
                        {
                            string subStr = tempList[3].Trim().Substring(5);
                            objectName = subStr.Substring(0, subStr.IndexOf("."));
                        }
                        if (dictionary.ContainsKey(objectName))
                        {
                            dictionary[objectName].AddRange(tempList);
                        }
                        else
                        {
                            dictionary.Add(objectName, tempList);
                        }
                        objectName = fileList[i + 2].Replace(@"//", "").Trim();
                    }
                }
                if (dictionary.Count == 0)
                {
                    using (System.IO.StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true, Encoding.UTF8))
                    {
                        sw.WriteLine(DateTime.Now + Environment.NewLine + "文件无效，无数据导出");
                    }
                    return null;
                }
                return dictionary;
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true, Encoding.UTF8))
                {
                    sw.WriteLine(DateTime.Now + "   :" + objectName + Environment.NewLine + ex.Message);
                }
                headList = null;
                bottomList = null;
                return null;
            }
        }
        private List<string> BodyDataSortingToList(Dictionary<string, List<string>> data)
        {
            try
            {
                List<string> list = new List<string>();
                Dictionary<string, List<string>> dic1Asc = data.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                List<string> listNewObject1 = new List<string>();
                List<string> listNewObject2 = new List<string>();
                foreach (var item in dic1Asc["newObject"])
                {
                    if (item.Contains("= new"))
                    {
                        listNewObject1.Add(item);
                    }
                    else
                    {
                        listNewObject2.Add(item);
                    }
                }
                listNewObject1.Sort();
                listNewObject2.Sort();
                list.AddRange(listNewObject1);
                list.AddRange(listNewObject2);
                dic1Asc.Remove("newObject");
                foreach (var kv in dic1Asc)
                {
                    List<string> listTemp = new List<string>();
                    List<string> listTemp1 = new List<string>();
                    List<string> listTemp2 = new List<string>();
                    for (int i = 0; i < kv.Value.Count; i++)
                    {
                        if (i < 3)
                        {
                            listTemp1.Add(kv.Value[i]);
                        }
                        else
                        {
                            listTemp2.Add(kv.Value[i]);
                        }
                    }
                    listTemp2.Sort();
                    listTemp.AddRange(listTemp1);
                    listTemp.AddRange(listTemp2);
                    list.AddRange(listTemp);
                }
                return list;
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true, Encoding.UTF8))
                {
                    sw.WriteLine(DateTime.Now + Environment.NewLine + ex.Message);
                }
                return null;
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "请选择文件",
                //Filter = "Visual C# 文件(*.Designer*)|*.Designer*"
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TBSelectFilePath.Text = dialog.FileName;
            }
        }
        private void BtnExport_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            BtnExport.Enabled = false;
            BtnTransition.Enabled = false;
            if (string.IsNullOrEmpty(TBSelectFilePath.Text) || !File.Exists(TBSelectFilePath.Text))
            {
                MessageBox.Show("未选择正确文件路径");
                BtnExport.Enabled = true;
                BtnTransition.Enabled = true;
                return;
            }

            Task.Factory.StartNew(() =>
            {
                Dictionary<string, List<string>> keyValuePairs = DataLoadToDictionary(TBSelectFilePath.Text, out List<string> headList, out List<string> bottomList);
                if (keyValuePairs == null)
                {
                    MessageBox.Show("文件导出未成功，请查看日志");
                    BtnExport.Enabled = true;
                    BtnTransition.Enabled = true;
                    return;
                }

                foreach (var hl in headList)
                {
                    richTextBox2.AppendText($"{hl}{Environment.NewLine}");
                }
                if (keyValuePairs.Count > 0)
                {
                    List<string> list = BodyDataSortingToList(keyValuePairs);
                    if (list == null)
                    {
                        MessageBox.Show("文件导出未成功，请查看日志");
                        BtnExport.Enabled = true;
                        BtnTransition.Enabled = true;
                        return;
                    }
                    foreach (var item in list)
                    {
                        richTextBox2.AppendText($"{item}{Environment.NewLine}");
                    }
                }

                bool b1Sta = false;
                List<string> b1 = new List<string>();
                List<string> b2 = new List<string>();
                List<string> b3 = new List<string>();
                for (int bi = 0; bi < bottomList.Count; bi++)
                {
                    if (!b1Sta)
                    {
                        b1.Add(bottomList[bi]);
                        if (bottomList[bi].Trim() == "#endregion")
                        {
                            b1Sta = true;
                        }
                    }
                    else
                    {
                        if (bi >= bottomList.Count - 2)
                        {
                            b3.Add(bottomList[bi]);
                        }
                        else
                        {
                            b2.Add(bottomList[bi]);
                        }
                    }

                }
                b2.Sort();
                foreach (var b1l in b1)
                {
                    richTextBox2.AppendText($"{b1l}{Environment.NewLine}");
                }
                foreach (var b2l in b2)
                {
                    richTextBox2.AppendText($"{b2l}{Environment.NewLine}");
                }
                foreach (var b3l in b3)
                {
                    richTextBox2.AppendText($"{b3l}{Environment.NewLine}");
                }

                BtnExport.Enabled = true;
                BtnTransition.Enabled = true;
                MessageBox.Show("文件导出成功");
            });


        }
        private void BtnTransition_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            BtnExport.Enabled = false;
            BtnTransition.Enabled = false;
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("转换数据不能为空");
                BtnExport.Enabled = true;
                BtnTransition.Enabled = true;
                return;
            }

            Task.Factory.StartNew(() =>
            {
                Dictionary<string, List<string>> keyValuePairs = RichTextDataLoadToDictionary(richTextBox1, out List<string> headList, out List<string> bottomList);
                if (keyValuePairs == null)
                {
                    MessageBox.Show("文件导出未成功，请查看日志");
                    BtnExport.Enabled = true;
                    BtnTransition.Enabled = true;
                    return;
                }

                foreach (var hl in headList)
                {
                    richTextBox2.AppendText($"{hl}{Environment.NewLine}");
                }
                if (keyValuePairs.Count > 0)
                {
                    List<string> list = BodyDataSortingToList(keyValuePairs);
                    if (list == null)
                    {
                        MessageBox.Show("文件导出未成功，请查看日志");
                        BtnExport.Enabled = true;
                        BtnTransition.Enabled = true;
                        return;
                    }
                    foreach (var item in list)
                    {
                        richTextBox2.AppendText($"{item}{Environment.NewLine}");
                    }
                }

                bool b1Sta = false;
                List<string> b1 = new List<string>();
                List<string> b2 = new List<string>();
                List<string> b3 = new List<string>();
                for (int bi = 0; bi < bottomList.Count; bi++)
                {
                    if (!b1Sta)
                    {
                        b1.Add(bottomList[bi]);
                        if (bottomList[bi].Trim() == "#endregion")
                        {
                            b1Sta = true;
                        }
                    }
                    else
                    {
                        if (bi >= bottomList.Count - 2)
                        {
                            b3.Add(bottomList[bi]);
                        }
                        else
                        {
                            b2.Add(bottomList[bi]);
                        }
                    }

                }
                b2.Sort();
                foreach (var b1l in b1)
                {
                    richTextBox2.AppendText($"{b1l}{Environment.NewLine}");
                }
                foreach (var b2l in b2)
                {
                    richTextBox2.AppendText($"{b2l}{Environment.NewLine}");
                }
                foreach (var b3l in b3)
                {
                    richTextBox2.AppendText($"{b3l}{Environment.NewLine}");
                }
                BtnExport.Enabled = true;
                BtnTransition.Enabled = true;
                MessageBox.Show("文件转换成功");
            });
        }
    }
}
