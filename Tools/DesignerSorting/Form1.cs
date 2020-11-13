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
        private Dictionary<string, List<string>> DataLoadToDictionary(string path)
        {
            List<string> fileList = new List<string>();
            List<string> linesList = new List<string>();
            StringBuilder lineTemp = new StringBuilder();
            string objectName = string.Empty;
            bool begin = false;
            int lines;
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
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
                if (fileList[i].Trim() == @"private void InitializeComponent()")
                {
                    begin = true;
                    objectName = "newObject";
                    i += 1;
                    continue;
                }

                if (begin && fileList[i].Trim() == "}")
                {
                    dictionary.Add(objectName, linesList);
                    //linesList.Clear();
                    return dictionary;
                }

                if (!begin)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(fileList[i]))
                {
                    continue;
                }

                lineTemp.Append(fileList[i]);
                if (lineTemp.ToString().Contains(";") || lineTemp.ToString().Contains(@"//"))
                {
                    linesList.Add(lineTemp.ToString());
                    lineTemp.Clear();
                }

                if (!fileList[i].Contains(@"//") && fileList[i + 1].Contains(@"//") && fileList[i + 2].Contains(@"//") && fileList[i + 3].Contains(@"//"))
                {
                    dictionary.Add(objectName, linesList);
                    linesList.Clear();
                    objectName = fileList[i + 2].Replace(@"//", "").Trim();
                }
            }
            return dictionary;
        }
        private List<string> DataSortingToList(Dictionary<string, List<string>> data)
        {
            List<string> list = new List<string>();
            Dictionary<string, List<string>> dic1Asc = data.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
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


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TBSelectFilePath.Text) || !File.Exists(TBSelectFilePath.Text))
            {
                MessageBox.Show("未选择正确文件路径");
                return;
            }

            Task.Factory.StartNew(() =>
            {
                Dictionary<string, List<string>> keyValuePairs = DataLoadToDictionary(TBSelectFilePath.Text);
                if (keyValuePairs.Count > 0)
                {
                    List<string> list = DataSortingToList(keyValuePairs);
                    foreach (var item in list)
                    {
                        richTextBox2.AppendText($"{item}{Environment.NewLine}");
                    }
                }
            });


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
