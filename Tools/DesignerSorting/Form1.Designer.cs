namespace DesignerSorting
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnExport = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.BtnTransition = new System.Windows.Forms.Button();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.TBSelectFilePath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(588, 28);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(75, 23);
            this.BtnExport.TabIndex = 0;
            this.BtnExport.Text = "导出";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(25, 55);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(489, 401);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(588, 55);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(489, 401);
            this.richTextBox2.TabIndex = 2;
            this.richTextBox2.Text = "";
            this.richTextBox2.WordWrap = false;
            // 
            // BtnTransition
            // 
            this.BtnTransition.Location = new System.Drawing.Point(518, 228);
            this.BtnTransition.Name = "BtnTransition";
            this.BtnTransition.Size = new System.Drawing.Size(66, 23);
            this.BtnTransition.TabIndex = 3;
            this.BtnTransition.Text = "》》》";
            this.BtnTransition.UseVisualStyleBackColor = true;
            this.BtnTransition.Click += new System.EventHandler(this.BtnTransition_Click);
            // 
            // BtnSelect
            // 
            this.BtnSelect.Location = new System.Drawing.Point(24, 26);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(76, 23);
            this.BtnSelect.TabIndex = 4;
            this.BtnSelect.Text = "选择文件";
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // TBSelectFilePath
            // 
            this.TBSelectFilePath.Location = new System.Drawing.Point(111, 28);
            this.TBSelectFilePath.Name = "TBSelectFilePath";
            this.TBSelectFilePath.Size = new System.Drawing.Size(403, 21);
            this.TBSelectFilePath.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1107, 473);
            this.Controls.Add(this.TBSelectFilePath);
            this.Controls.Add(this.BtnSelect);
            this.Controls.Add(this.BtnTransition);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.BtnExport);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Designer Sorting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button BtnTransition;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.TextBox TBSelectFilePath;
    }
}

