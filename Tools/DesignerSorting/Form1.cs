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
        private string path;
        private long lines;
        private List<string> fileList = new List<string>();

        private List<string> ss = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "请选择文件",
                Filter = "Visual C# 文件(*.Designer*)|*.Designer*"
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = dialog.FileName;
            }
            else
            {
                MessageBox.Show("未选中txt文件");
            }

            using (var sr = new StreamReader(path))
            {
                lines = 1;
                while (sr.ReadLine() != null)
                {
                    lines++;
                    fileList.Add(sr.ReadLine());
                }
            }
            Debug.WriteLine(lines);

            bool begin =false;
            bool end = false;
            using (var sr = new StreamReader(path))
            {
                while (sr.ReadLine() != null)
                {
                    if (sr.ReadLine() == "private void InitializeComponent()")
                    {
                        begin = true;                    
                    }

                    if (begin && !end)
                    {
                        fileList.Add(sr.ReadLine());
                        if (sr.ReadLine() == @"\\")
                        {
                            end = true;
                        }
                    }
                    

                }
            }



            //int begin;
            //int end;

            //for (int i = 1; i <= fileList.Count; i++)
            //{
            //    if (fileList[i] == "private void InitializeComponent()")
            //    {
            //        begin = i+1;
            //    }
            //    if (fileList[i] == @"//")
            //    {
            //        end = i;
            //    }
            //}

        }
    }
}
