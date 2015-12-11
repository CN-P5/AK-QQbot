namespace QQLite.Plugin
{
    partial class SettingForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Set_ApiGetMax = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Set_ApiGetMin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rbtn_Set_ApiUTF8 = new System.Windows.Forms.RadioButton();
            this.rbtn_Set_ApiGB2312 = new System.Windows.Forms.RadioButton();
            this.txt_Set_ApiKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Set_ApiUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Set_ApiPort = new System.Windows.Forms.NumericUpDown();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Set_ApiPort)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_Set_ApiGetMax);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txt_Set_ApiGetMin);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.rbtn_Set_ApiUTF8);
            this.groupBox1.Controls.Add(this.rbtn_Set_ApiGB2312);
            this.groupBox1.Controls.Add(this.txt_Set_ApiKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_Set_ApiUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(38, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 155);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "接口配置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(246, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "密匙确保接口安全，请设置英文+数字组合";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(228, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "秒，机器人定时检查web接口上的数据";
            // 
            // txt_Set_ApiGetMax
            // 
            this.txt_Set_ApiGetMax.Location = new System.Drawing.Point(180, 80);
            this.txt_Set_ApiGetMax.Name = "txt_Set_ApiGetMax";
            this.txt_Set_ApiGetMax.Size = new System.Drawing.Size(40, 21);
            this.txt_Set_ApiGetMax.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(155, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "—";
            // 
            // txt_Set_ApiGetMin
            // 
            this.txt_Set_ApiGetMin.Location = new System.Drawing.Point(107, 80);
            this.txt_Set_ApiGetMin.Name = "txt_Set_ApiGetMin";
            this.txt_Set_ApiGetMin.Size = new System.Drawing.Size(40, 21);
            this.txt_Set_ApiGetMin.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "请求接口频率：";
            // 
            // rbtn_Set_ApiUTF8
            // 
            this.rbtn_Set_ApiUTF8.AutoSize = true;
            this.rbtn_Set_ApiUTF8.Checked = true;
            this.rbtn_Set_ApiUTF8.Location = new System.Drawing.Point(190, 54);
            this.rbtn_Set_ApiUTF8.Name = "rbtn_Set_ApiUTF8";
            this.rbtn_Set_ApiUTF8.Size = new System.Drawing.Size(53, 16);
            this.rbtn_Set_ApiUTF8.TabIndex = 8;
            this.rbtn_Set_ApiUTF8.TabStop = true;
            this.rbtn_Set_ApiUTF8.Text = "UTF-8";
            this.rbtn_Set_ApiUTF8.UseVisualStyleBackColor = true;
            // 
            // rbtn_Set_ApiGB2312
            // 
            this.rbtn_Set_ApiGB2312.AutoSize = true;
            this.rbtn_Set_ApiGB2312.Location = new System.Drawing.Point(107, 54);
            this.rbtn_Set_ApiGB2312.Name = "rbtn_Set_ApiGB2312";
            this.rbtn_Set_ApiGB2312.Size = new System.Drawing.Size(59, 16);
            this.rbtn_Set_ApiGB2312.TabIndex = 7;
            this.rbtn_Set_ApiGB2312.Text = "GB2312";
            this.rbtn_Set_ApiGB2312.UseVisualStyleBackColor = true;
            // 
            // txt_Set_ApiKey
            // 
            this.txt_Set_ApiKey.Location = new System.Drawing.Point(107, 110);
            this.txt_Set_ApiKey.Name = "txt_Set_ApiKey";
            this.txt_Set_ApiKey.Size = new System.Drawing.Size(134, 21);
            this.txt_Set_ApiKey.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Api密匙：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "接口编码：";
            // 
            // txt_Set_ApiUrl
            // 
            this.txt_Set_ApiUrl.Location = new System.Drawing.Point(107, 24);
            this.txt_Set_ApiUrl.Name = "txt_Set_ApiUrl";
            this.txt_Set_ApiUrl.Size = new System.Drawing.Size(366, 21);
            this.txt_Set_ApiUrl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "接口地址：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "监听端口：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_Set_ApiPort);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(38, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(528, 106);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "本地监听";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(263, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "此功能极少使用，不清楚请勿修改，默认0即可。";
            // 
            // txt_Set_ApiPort
            // 
            this.txt_Set_ApiPort.Location = new System.Drawing.Point(107, 29);
            this.txt_Set_ApiPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.txt_Set_ApiPort.Name = "txt_Set_ApiPort";
            this.txt_Set_ApiPort.Size = new System.Drawing.Size(78, 21);
            this.txt_Set_ApiPort.TabIndex = 10;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(255, 322);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 360);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Web接口设置";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Set_ApiPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Set_ApiGetMax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_Set_ApiGetMin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbtn_Set_ApiUTF8;
        private System.Windows.Forms.RadioButton rbtn_Set_ApiGB2312;
        private System.Windows.Forms.TextBox txt_Set_ApiKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Set_ApiUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown txt_Set_ApiPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Save;
    }
}

