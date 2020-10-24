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
            ContentsDo();
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
                        //int i = 0;
                        foreach (var dir3 in dirList3)
                        {
                            DirectoryInfo[] dirList4 = dir3.GetDirectories();//四级子目录
                            foreach (var dir4 in dirList4)
                            {
                                //if (dir4.Name == ".vs" || dir4.Name == "Backup") continue;
                                if (dir4.Name == dir3.Name)
                                {
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
                                    //i++;
                                }
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


        /*
            第Ⅰ卷
                第1篇 编程基础篇
		            第1章 开发环境的使用
		            第2章 语言基础应用
			            2.1 代码的注释
				            实例013 对单行代码进行注释
         */
        /// <summary>
        /// 获取目录文件
        /// </summary>
        private void ContentsDo()
        {
            try
            {
                var rootPath = textBox2.Text;
                DirectoryInfo dirRoot = new DirectoryInfo(rootPath);//根目录
                using (StreamWriter sw = new StreamWriter(dirRoot.FullName + "\\" + "contents.txt", false, Encoding.UTF8))
                {
                    sw.WriteLine(dirRoot.Name);//第Ⅰ卷
                    var dirlist1 = dirRoot.GetDirectories();//一级目录
                    dirlist1.ToList().ForEach(dir1 =>
                    {
                        if (dir1.Name.Contains("第"))
                        {
                            sw.WriteLine("  " + dir1.Name);//第1篇 编程基础篇
                            var dirlist2 = dir1.GetDirectories();//二级目录
                            dirlist2.ToList().ForEach(dir2 =>
                            {
                                if (dir2.Name.Contains("第"))
                                {
                                    sw.WriteLine("      " + dir2.Name);//第2章 语言基础应用
                                    var dirlist3 = dir2.GetDirectories();//三级目录
                                    dirlist3.ToList().ForEach(dir3 =>
                                    {
                                        sw.WriteLine("          " + dir3.Name);//2.1 代码的注释
                                        var dirlist4 = dir3.GetDirectories();//四级目录
                                        dirlist4.ToList().ForEach(dir4 =>
                                        {
                                            string dir4Name = dir4.Name.Replace(StingGetNum(dir4.Name), StingGetNum(dir4.Name).PadLeft(4, '0'));
                                            if (dir4Name.Substring(0, 2) != "实例")
                                            {
                                                dir4Name = "实例" + dir4Name;
                                            }
                                            sw.WriteLine("              " + dir4Name);//实例013 对单行代码进行注释
                                        });
                                    });
                                }
                            });
                        }

                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
