using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IrisSkinApp
{
    public partial class Form1 : Form
    {
        SkinEngineHelper skinEngineHelper = new SkinEngineHelper();
        public Form1()
        {
            InitializeComponent();
            skinEngineHelper.SetSkinFile(SkinEnum.Calmness);
            skinEngineHelper.DisableTag = 1;//排除Tag=1的控件不自动添加皮肤
            button1.Tag = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (SkinEnum s in Enum.GetValues(typeof(SkinEnum)))//遍历枚举
            {
                comboBox1.Items.Add(s);
            }  
        }


        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            skinEngineHelper.SetSkinFile((SkinEnum)Enum.Parse(typeof(SkinEnum), comboBox1.Text));//字符串转枚举
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
