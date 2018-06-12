namespace SlicePlay
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.toruko_10 = new System.Windows.Forms.Button();
            this.toruko_2 = new System.Windows.Forms.Button();
            this.toruko_p = new System.Windows.Forms.Button();
            this.trackSlice = new System.Windows.Forms.TrackBar();
            this.anaun_p = new System.Windows.Forms.Button();
            this.anaun_10 = new System.Windows.Forms.Button();
            this.anaun_2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackPlace = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tango_p = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackSlice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPlace)).BeginInit();
            this.SuspendLayout();
            // 
            // toruko_10
            // 
            this.toruko_10.Location = new System.Drawing.Point(12, 158);
            this.toruko_10.Name = "toruko_10";
            this.toruko_10.Size = new System.Drawing.Size(75, 23);
            this.toruko_10.TabIndex = 0;
            this.toruko_10.Text = "0.1秒ごと";
            this.toruko_10.UseVisualStyleBackColor = true;
            this.toruko_10.Click += new System.EventHandler(this.toruko_10_Click);
            // 
            // toruko_2
            // 
            this.toruko_2.Location = new System.Drawing.Point(12, 206);
            this.toruko_2.Name = "toruko_2";
            this.toruko_2.Size = new System.Drawing.Size(75, 23);
            this.toruko_2.TabIndex = 1;
            this.toruko_2.Text = "0.5秒ごと";
            this.toruko_2.UseVisualStyleBackColor = true;
            this.toruko_2.Click += new System.EventHandler(this.toruko_2_Click);
            // 
            // toruko_p
            // 
            this.toruko_p.Location = new System.Drawing.Point(12, 35);
            this.toruko_p.Name = "toruko_p";
            this.toruko_p.Size = new System.Drawing.Size(104, 23);
            this.toruko_p.TabIndex = 2;
            this.toruko_p.Text = "調整再生";
            this.toruko_p.UseVisualStyleBackColor = true;
            this.toruko_p.Click += new System.EventHandler(this.toruko_p_Click);
            // 
            // trackSlice
            // 
            this.trackSlice.Location = new System.Drawing.Point(12, 78);
            this.trackSlice.Maximum = 1000;
            this.trackSlice.Minimum = 10;
            this.trackSlice.Name = "trackSlice";
            this.trackSlice.Size = new System.Drawing.Size(104, 45);
            this.trackSlice.SmallChange = 10;
            this.trackSlice.TabIndex = 3;
            this.trackSlice.Value = 10;
            this.trackSlice.Scroll += new System.EventHandler(this.trackSlice_Scroll);
            // 
            // anaun_p
            // 
            this.anaun_p.Location = new System.Drawing.Point(157, 35);
            this.anaun_p.Name = "anaun_p";
            this.anaun_p.Size = new System.Drawing.Size(104, 23);
            this.anaun_p.TabIndex = 4;
            this.anaun_p.Text = "調整再生";
            this.anaun_p.UseVisualStyleBackColor = true;
            this.anaun_p.Click += new System.EventHandler(this.anaun_p_Click);
            // 
            // anaun_10
            // 
            this.anaun_10.Location = new System.Drawing.Point(168, 158);
            this.anaun_10.Name = "anaun_10";
            this.anaun_10.Size = new System.Drawing.Size(75, 23);
            this.anaun_10.TabIndex = 6;
            this.anaun_10.Text = "0.1秒ごと";
            this.anaun_10.UseVisualStyleBackColor = true;
            this.anaun_10.Click += new System.EventHandler(this.anaun_10_Click);
            // 
            // anaun_2
            // 
            this.anaun_2.Location = new System.Drawing.Point(168, 206);
            this.anaun_2.Name = "anaun_2";
            this.anaun_2.Size = new System.Drawing.Size(75, 23);
            this.anaun_2.TabIndex = 7;
            this.anaun_2.Text = "0.5秒ごと";
            this.anaun_2.UseVisualStyleBackColor = true;
            this.anaun_2.Click += new System.EventHandler(this.anaun_2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "トルコ行進曲";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "アナウンサー";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "10m/s";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "0m";
            // 
            // trackPlace
            // 
            this.trackPlace.Location = new System.Drawing.Point(157, 80);
            this.trackPlace.Maximum = 1000;
            this.trackPlace.Minimum = -1000;
            this.trackPlace.Name = "trackPlace";
            this.trackPlace.Size = new System.Drawing.Size(104, 45);
            this.trackPlace.SmallChange = 10;
            this.trackPlace.TabIndex = 5;
            this.trackPlace.Scroll += new System.EventHandler(this.trackPlace_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "切り取り幅";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(178, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "立ち位置";
            // 
            // tango_p
            // 
            this.tango_p.Location = new System.Drawing.Point(302, 35);
            this.tango_p.Name = "tango_p";
            this.tango_p.Size = new System.Drawing.Size(75, 23);
            this.tango_p.TabIndex = 14;
            this.tango_p.Text = "調節再生";
            this.tango_p.UseVisualStyleBackColor = true;
            this.tango_p.Click += new System.EventHandler(this.tango_p_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "アナウンサー単語";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(289, 65);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 88);
            this.listBox1.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 261);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tango_p);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.anaun_2);
            this.Controls.Add(this.anaun_10);
            this.Controls.Add(this.trackPlace);
            this.Controls.Add(this.anaun_p);
            this.Controls.Add(this.trackSlice);
            this.Controls.Add(this.toruko_p);
            this.Controls.Add(this.toruko_2);
            this.Controls.Add(this.toruko_10);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackSlice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPlace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button toruko_10;
        private System.Windows.Forms.Button toruko_2;
        private System.Windows.Forms.Button toruko_p;
        private System.Windows.Forms.TrackBar trackSlice;
        private System.Windows.Forms.Button anaun_p;
        private System.Windows.Forms.Button anaun_10;
        private System.Windows.Forms.Button anaun_2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackPlace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button tango_p;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBox1;
    }
}

