namespace UtilApp
{
    partial class SecurityFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DESgroupBox = new System.Windows.Forms.GroupBox();
            this.MD5groupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxbDESEncryptInput = new System.Windows.Forms.TextBox();
            this.TxbDESKey = new System.Windows.Forms.TextBox();
            this.TxbDESEncryptOut = new System.Windows.Forms.TextBox();
            this.TxbMD5EncryptOut = new System.Windows.Forms.TextBox();
            this.TxbMD5Key = new System.Windows.Forms.TextBox();
            this.TxbMD5EncryptInput = new System.Windows.Forms.TextBox();
            this.BtnDESEncrypt = new System.Windows.Forms.Button();
            this.BtnMD5Encrypt = new System.Windows.Forms.Button();
            this.BtnDESDecrypt = new System.Windows.Forms.Button();
            this.DESgroupBox.SuspendLayout();
            this.MD5groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DESgroupBox
            // 
            this.DESgroupBox.Controls.Add(this.BtnDESDecrypt);
            this.DESgroupBox.Controls.Add(this.BtnDESEncrypt);
            this.DESgroupBox.Controls.Add(this.TxbDESEncryptOut);
            this.DESgroupBox.Controls.Add(this.TxbDESKey);
            this.DESgroupBox.Controls.Add(this.TxbDESEncryptInput);
            this.DESgroupBox.Controls.Add(this.label5);
            this.DESgroupBox.Controls.Add(this.label2);
            this.DESgroupBox.Controls.Add(this.label1);
            this.DESgroupBox.Location = new System.Drawing.Point(12, 12);
            this.DESgroupBox.Name = "DESgroupBox";
            this.DESgroupBox.Size = new System.Drawing.Size(565, 182);
            this.DESgroupBox.TabIndex = 0;
            this.DESgroupBox.TabStop = false;
            this.DESgroupBox.Text = "DES";
            // 
            // MD5groupBox
            // 
            this.MD5groupBox.Controls.Add(this.BtnMD5Encrypt);
            this.MD5groupBox.Controls.Add(this.TxbMD5EncryptOut);
            this.MD5groupBox.Controls.Add(this.TxbMD5Key);
            this.MD5groupBox.Controls.Add(this.TxbMD5EncryptInput);
            this.MD5groupBox.Controls.Add(this.label6);
            this.MD5groupBox.Controls.Add(this.label3);
            this.MD5groupBox.Controls.Add(this.label4);
            this.MD5groupBox.Location = new System.Drawing.Point(12, 200);
            this.MD5groupBox.Name = "MD5groupBox";
            this.MD5groupBox.Size = new System.Drawing.Size(565, 182);
            this.MD5groupBox.TabIndex = 1;
            this.MD5groupBox.TabStop = false;
            this.MD5groupBox.Text = "MD5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "加密内容";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "加密密钥";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "加密位数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "加密内容";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "密文";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "密文";
            // 
            // TxbDESEncryptInput
            // 
            this.TxbDESEncryptInput.Location = new System.Drawing.Point(96, 33);
            this.TxbDESEncryptInput.Name = "TxbDESEncryptInput";
            this.TxbDESEncryptInput.Size = new System.Drawing.Size(346, 21);
            this.TxbDESEncryptInput.TabIndex = 3;
            // 
            // TxbDESKey
            // 
            this.TxbDESKey.Location = new System.Drawing.Point(96, 68);
            this.TxbDESKey.Name = "TxbDESKey";
            this.TxbDESKey.Size = new System.Drawing.Size(346, 21);
            this.TxbDESKey.TabIndex = 4;
            // 
            // TxbDESEncryptOut
            // 
            this.TxbDESEncryptOut.Location = new System.Drawing.Point(96, 102);
            this.TxbDESEncryptOut.Name = "TxbDESEncryptOut";
            this.TxbDESEncryptOut.Size = new System.Drawing.Size(346, 21);
            this.TxbDESEncryptOut.TabIndex = 5;
            // 
            // TxbMD5EncryptOut
            // 
            this.TxbMD5EncryptOut.Location = new System.Drawing.Point(96, 105);
            this.TxbMD5EncryptOut.Name = "TxbMD5EncryptOut";
            this.TxbMD5EncryptOut.Size = new System.Drawing.Size(346, 21);
            this.TxbMD5EncryptOut.TabIndex = 8;
            // 
            // TxbMD5Key
            // 
            this.TxbMD5Key.Location = new System.Drawing.Point(96, 71);
            this.TxbMD5Key.Name = "TxbMD5Key";
            this.TxbMD5Key.Size = new System.Drawing.Size(346, 21);
            this.TxbMD5Key.TabIndex = 7;
            // 
            // TxbMD5EncryptInput
            // 
            this.TxbMD5EncryptInput.Location = new System.Drawing.Point(96, 36);
            this.TxbMD5EncryptInput.Name = "TxbMD5EncryptInput";
            this.TxbMD5EncryptInput.Size = new System.Drawing.Size(346, 21);
            this.TxbMD5EncryptInput.TabIndex = 6;
            // 
            // BtnDESEncrypt
            // 
            this.BtnDESEncrypt.Location = new System.Drawing.Point(18, 138);
            this.BtnDESEncrypt.Name = "BtnDESEncrypt";
            this.BtnDESEncrypt.Size = new System.Drawing.Size(75, 23);
            this.BtnDESEncrypt.TabIndex = 6;
            this.BtnDESEncrypt.Text = "加密";
            this.BtnDESEncrypt.UseVisualStyleBackColor = true;
            this.BtnDESEncrypt.Click += new System.EventHandler(this.BtnDESEncrypt_Click);
            // 
            // BtnMD5Encrypt
            // 
            this.BtnMD5Encrypt.Location = new System.Drawing.Point(18, 139);
            this.BtnMD5Encrypt.Name = "BtnMD5Encrypt";
            this.BtnMD5Encrypt.Size = new System.Drawing.Size(75, 23);
            this.BtnMD5Encrypt.TabIndex = 9;
            this.BtnMD5Encrypt.Text = "加密";
            this.BtnMD5Encrypt.UseVisualStyleBackColor = true;
            this.BtnMD5Encrypt.Click += new System.EventHandler(this.BtnMD5Encrypt_Click);
            // 
            // BtnDESDecrypt
            // 
            this.BtnDESDecrypt.Location = new System.Drawing.Point(95, 138);
            this.BtnDESDecrypt.Name = "BtnDESDecrypt";
            this.BtnDESDecrypt.Size = new System.Drawing.Size(75, 23);
            this.BtnDESDecrypt.TabIndex = 7;
            this.BtnDESDecrypt.Text = "解密";
            this.BtnDESDecrypt.UseVisualStyleBackColor = true;
            this.BtnDESDecrypt.Click += new System.EventHandler(this.BtnDESDecrypt_Click);
            // 
            // SecurityFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 393);
            this.Controls.Add(this.MD5groupBox);
            this.Controls.Add(this.DESgroupBox);
            this.Name = "SecurityFrm";
            this.Text = "SecurityFrm";
            this.DESgroupBox.ResumeLayout(false);
            this.DESgroupBox.PerformLayout();
            this.MD5groupBox.ResumeLayout(false);
            this.MD5groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox DESgroupBox;
        private System.Windows.Forms.GroupBox MD5groupBox;
        private System.Windows.Forms.TextBox TxbDESEncryptOut;
        private System.Windows.Forms.TextBox TxbDESKey;
        private System.Windows.Forms.TextBox TxbDESEncryptInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxbMD5EncryptOut;
        private System.Windows.Forms.TextBox TxbMD5Key;
        private System.Windows.Forms.TextBox TxbMD5EncryptInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnDESDecrypt;
        private System.Windows.Forms.Button BtnDESEncrypt;
        private System.Windows.Forms.Button BtnMD5Encrypt;
    }
}