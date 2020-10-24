using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp1200
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChapterDo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(StingGetNum("asadas0123sdas2323"));
            //Console.WriteLine(StingGetNum("asdasf"));
        }

        private string StingGetNum(string input)
        {
            //字符串中首个数字字符串提取
            string pattern = @"[+-]?\d+[\.]?\d*";
            return Regex.Match(input, pattern).Value;
        }

        /// <summary>
        /// 章目录文件处理
        /// </summary>
        private void ChapterDo()
        {
            try
            {
                var rootPath = textBox1.Text;
                DirectoryInfo dirRoot = new DirectoryInfo(rootPath);//根目录

                var directoryName = StingGetNum(dirRoot.Name).ToString();
                directoryName = directoryName.Length < 2 ? $"chapter0{directoryName}" : $"chapter{directoryName}";
                DirectoryInfo DInfo = new DirectoryInfo(dirRoot.FullName + "\\" + directoryName);//创建DirectoryInfo对象
                DInfo.Create();//创建文件夹

                var dir1list1 = dirRoot.GetDirectories();//一级目录
                foreach (var dir1 in dir1list1)
                {
                    if (dir1.Name.Contains("chapter")) continue;
                    DirectoryInfo[] dirList2 = dir1.GetDirectories();//二级子目录
                    foreach (var dir2 in dirList2)
                    {
                        var num = StingGetNum(dir2.Name).ToString().PadLeft(4, '0') + "_";
                        DirectoryInfo[] dirList3 = dir2.GetDirectories();//三级子目录
                        int i = 0;
                        foreach (var dir3 in dirList3)
                        {
                            DirectoryInfo[] dirList4 = dir3.GetDirectories();//四级子目录
                            foreach (var dir4 in dirList4)
                            {
                                if (dir4.Name == ".vs" || dir4.Name == "Backup") continue;
                                //DirectoryInfo[] dirList5 = dir4.GetDirectories();//五级子目录
                                foreach (var filelist in dir4.GetFiles())
                                {
                                    if (filelist.Extension == ".csproj")
                                    {
                                        System.IO.File.Move(filelist.FullName, dir4.FullName + "\\" + num + filelist.Name);//重命名文件
                                    }
                                    if (filelist.Extension == ".user")
                                    {
                                        System.IO.File.Delete(filelist.FullName);//删除
                                    }
                                }
                                //Directory.Move(dir4.FullName, dirList3[i].FullName + "\\" + num + dir4.Name);//重命名文件夹
                                Directory.Move(dir4.FullName, DInfo.FullName + "\\" + num + dir4.Name);//移动文件
                                i++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
