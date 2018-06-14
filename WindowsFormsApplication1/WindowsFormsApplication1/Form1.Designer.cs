namespace WindowsFormsApplication1
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
            this.button1 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.comboBox_b = new System.Windows.Forms.ComboBox();
            this.comboBox_s = new System.Windows.Forms.ComboBox();
            this.go = new System.Windows.Forms.Button();
            this.strings = new System.Windows.Forms.TextBox();
            this.string_in = new System.Windows.Forms.Button();
            this.del = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.vol_b = new System.Windows.Forms.TrackBar();
            this.vol_s = new System.Windows.Forms.TrackBar();
            this.Auto = new System.Windows.Forms.Button();
            this.crrect = new System.Windows.Forms.Button();
            this.textdiv1 = new System.Windows.Forms.TextBox();
            this.textlong = new System.Windows.Forms.TextBox();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.form2 = new System.Windows.Forms.Button();
            this.auto2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vol_b)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vol_s)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "一音再生";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(158, 207);
            this.trackBar1.Maximum = 40;
            this.trackBar1.Minimum = -10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(260, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // comboBox_b
            // 
            this.comboBox_b.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_b.FormattingEnabled = true;
            this.comboBox_b.Location = new System.Drawing.Point(297, 44);
            this.comboBox_b.Name = "comboBox_b";
            this.comboBox_b.Size = new System.Drawing.Size(121, 43);
            this.comboBox_b.TabIndex = 4;
            this.comboBox_b.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox_s
            // 
            this.comboBox_s.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_s.FormattingEnabled = true;
            this.comboBox_s.Location = new System.Drawing.Point(158, 44);
            this.comboBox_s.Name = "comboBox_s";
            this.comboBox_s.Size = new System.Drawing.Size(121, 43);
            this.comboBox_s.TabIndex = 5;
            this.comboBox_s.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // go
            // 
            this.go.Location = new System.Drawing.Point(392, 258);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(26, 45);
            this.go.TabIndex = 7;
            this.go.Text = "go";
            this.go.UseVisualStyleBackColor = true;
            this.go.Click += new System.EventHandler(this.go_Click);
            // 
            // strings
            // 
            this.strings.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.strings.Location = new System.Drawing.Point(194, 272);
            this.strings.Name = "strings";
            this.strings.Size = new System.Drawing.Size(192, 31);
            this.strings.TabIndex = 9;
            // 
            // string_in
            // 
            this.string_in.Location = new System.Drawing.Point(158, 260);
            this.string_in.Name = "string_in";
            this.string_in.Size = new System.Drawing.Size(30, 43);
            this.string_in.TabIndex = 10;
            this.string_in.Text = "in";
            this.string_in.UseVisualStyleBackColor = true;
            this.string_in.Click += new System.EventHandler(this.string_in_Click);
            // 
            // del
            // 
            this.del.Location = new System.Drawing.Point(248, 243);
            this.del.Name = "del";
            this.del.Size = new System.Drawing.Size(75, 23);
            this.del.TabIndex = 11;
            this.del.Text = "delete";
            this.del.UseVisualStyleBackColor = true;
            this.del.Click += new System.EventHandler(this.del_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(312, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 29);
            this.label1.TabIndex = 13;
            this.label1.Text = "母音";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(168, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 29);
            this.label2.TabIndex = 14;
            this.label2.Text = "子音";
            // 
            // vol_b
            // 
            this.vol_b.Location = new System.Drawing.Point(438, 34);
            this.vol_b.Maximum = 0;
            this.vol_b.Minimum = -4000;
            this.vol_b.Name = "vol_b";
            this.vol_b.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.vol_b.Size = new System.Drawing.Size(45, 104);
            this.vol_b.SmallChange = 100;
            this.vol_b.TabIndex = 100;
            // 
            // vol_s
            // 
            this.vol_s.Location = new System.Drawing.Point(107, 34);
            this.vol_s.Maximum = 0;
            this.vol_s.Minimum = -4000;
            this.vol_s.Name = "vol_s";
            this.vol_s.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.vol_s.Size = new System.Drawing.Size(45, 104);
            this.vol_s.SmallChange = 100;
            this.vol_s.TabIndex = 100;
            this.vol_s.Value = -1500;
            // 
            // Auto
            // 
            this.Auto.Location = new System.Drawing.Point(98, 207);
            this.Auto.Name = "Auto";
            this.Auto.Size = new System.Drawing.Size(54, 23);
            this.Auto.TabIndex = 19;
            this.Auto.Text = "Auto→";
            this.Auto.UseVisualStyleBackColor = true;
            this.Auto.Click += new System.EventHandler(this.Auto_Click);
            // 
            // crrect
            // 
            this.crrect.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.crrect.Location = new System.Drawing.Point(551, 38);
            this.crrect.Name = "crrect";
            this.crrect.Size = new System.Drawing.Size(61, 111);
            this.crrect.TabIndex = 20;
            this.crrect.Text = "記録";
            this.crrect.UseVisualStyleBackColor = true;
            this.crrect.Click += new System.EventHandler(this.crrect_Click);
            // 
            // textdiv1
            // 
            this.textdiv1.Location = new System.Drawing.Point(261, 192);
            this.textdiv1.Name = "textdiv1";
            this.textdiv1.Size = new System.Drawing.Size(48, 19);
            this.textdiv1.TabIndex = 101;
            this.textdiv1.Text = "0";
            this.textdiv1.TextChanged += new System.EventHandler(this.textdiv1_TextChanged);
            // 
            // textlong
            // 
            this.textlong.Location = new System.Drawing.Point(263, 122);
            this.textlong.Name = "textlong";
            this.textlong.Size = new System.Drawing.Size(46, 19);
            this.textlong.TabIndex = 102;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(158, 141);
            this.trackBar2.Maximum = 1000;
            this.trackBar2.Minimum = -1000;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(286, 45);
            this.trackBar2.TabIndex = 103;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // form2
            // 
            this.form2.Location = new System.Drawing.Point(551, 206);
            this.form2.Name = "form2";
            this.form2.Size = new System.Drawing.Size(75, 23);
            this.form2.TabIndex = 104;
            this.form2.Text = "form2";
            this.form2.UseVisualStyleBackColor = true;
            this.form2.Click += new System.EventHandler(this.form2_Click);
            // 
            // auto2
            // 
            this.auto2.Location = new System.Drawing.Point(98, 144);
            this.auto2.Name = "auto2";
            this.auto2.Size = new System.Drawing.Size(54, 23);
            this.auto2.TabIndex = 105;
            this.auto2.Text = "Auto→";
            this.auto2.UseVisualStyleBackColor = true;
            this.auto2.Click += new System.EventHandler(this.auto2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 314);
            this.Controls.Add(this.auto2);
            this.Controls.Add(this.form2);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.textlong);
            this.Controls.Add(this.textdiv1);
            this.Controls.Add(this.crrect);
            this.Controls.Add(this.Auto);
            this.Controls.Add(this.vol_s);
            this.Controls.Add(this.vol_b);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.del);
            this.Controls.Add(this.string_in);
            this.Controls.Add(this.strings);
            this.Controls.Add(this.go);
            this.Controls.Add(this.comboBox_s);
            this.Controls.Add(this.comboBox_b);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vol_b)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vol_s)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar1;
        public System.Windows.Forms.ComboBox comboBox_b;
        public System.Windows.Forms.ComboBox comboBox_s;
        private System.Windows.Forms.Button go;
        private System.Windows.Forms.TextBox strings;
        private System.Windows.Forms.Button string_in;
        private System.Windows.Forms.Button del;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar vol_b;
        private System.Windows.Forms.TrackBar vol_s;
        private System.Windows.Forms.Button Auto;
        private System.Windows.Forms.Button crrect;
        private System.Windows.Forms.TextBox textdiv1;
        private System.Windows.Forms.TextBox textlong;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Button form2;
        private System.Windows.Forms.Button auto2;
    }
}

