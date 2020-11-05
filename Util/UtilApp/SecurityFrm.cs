using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

namespace UtilApp
{
    public partial class SecurityFrm : Form
    {
        public SecurityFrm()
        {
            InitializeComponent();
        }

        private void BtnDESEncrypt_Click(object sender, EventArgs e)
        {
            DESEncrypt.Key = this.TxbDESKey.Text;
            this.TxbDESEncryptOut.Text = DESEncrypt.Encrypt(this.TxbDESEncryptInput.Text);
        }

        private void BtnDESDecrypt_Click(object sender, EventArgs e)
        {
            DESEncrypt.Key = this.TxbDESKey.Text;
            this.TxbDESEncryptInput.Text = DESEncrypt.Decrypt(this.TxbDESEncryptOut.Text);
        }

        private void BtnMD5Encrypt_Click(object sender, EventArgs e)
        {
            this.TxbMD5EncryptOut.Text = Md5Helper.Encrypt(this.TxbMD5EncryptInput.Text, Convert.ToInt32(this.TxbMD5Key.Text));
        }
    }
}
