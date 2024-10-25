namespace WindowsFormsApp1
{
    partial class WorkFlowPage
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.tarih = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAsama = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProje = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGorevTanimi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.girisSaati = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cikisSaati = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbxEmployee = new System.Windows.Forms.ComboBox();
            this.btnEkle = new Guna.UI2.WinForms.Guna2Button();
            this.btnSil = new Guna.UI2.WinForms.Guna2Button();
            this.btnGuncelle = new Guna.UI2.WinForms.Guna2Button();
            this.btnFiltrele = new Guna.UI2.WinForms.Guna2Button();
            this.btnExcel = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tarih
            // 
            this.tarih.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tarih.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tarih.Location = new System.Drawing.Point(226, 38);
            this.tarih.Name = "tarih";
            this.tarih.Size = new System.Drawing.Size(256, 28);
            this.tarih.TabIndex = 1;
            this.tarih.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(41, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tarih";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(41, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Görevlendirilen";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtAsama
            // 
            this.txtAsama.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtAsama.Location = new System.Drawing.Point(226, 135);
            this.txtAsama.Name = "txtAsama";
            this.txtAsama.Size = new System.Drawing.Size(256, 28);
            this.txtAsama.TabIndex = 0;
            this.txtAsama.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(41, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Aşama";
            this.label3.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtProje
            // 
            this.txtProje.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtProje.Location = new System.Drawing.Point(664, 37);
            this.txtProje.Name = "txtProje";
            this.txtProje.Size = new System.Drawing.Size(256, 28);
            this.txtProje.TabIndex = 0;
            this.txtProje.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(512, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "Proje";
            this.label4.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtGorevTanimi
            // 
            this.txtGorevTanimi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtGorevTanimi.Location = new System.Drawing.Point(664, 82);
            this.txtGorevTanimi.Name = "txtGorevTanimi";
            this.txtGorevTanimi.Size = new System.Drawing.Size(256, 28);
            this.txtGorevTanimi.TabIndex = 0;
            this.txtGorevTanimi.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(512, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "Görev Tanımı";
            this.label5.Click += new System.EventHandler(this.label1_Click);
            // 
            // girisSaati
            // 
            this.girisSaati.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.girisSaati.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.girisSaati.Location = new System.Drawing.Point(664, 133);
            this.girisSaati.Name = "girisSaati";
            this.girisSaati.ShowUpDown = true;
            this.girisSaati.Size = new System.Drawing.Size(116, 28);
            this.girisSaati.TabIndex = 3;
            this.girisSaati.ValueChanged += new System.EventHandler(this.girisSaati_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(512, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 25);
            this.label6.TabIndex = 2;
            this.label6.Text = "Giriş Saati";
            this.label6.Click += new System.EventHandler(this.label1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(512, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 25);
            this.label8.TabIndex = 2;
            this.label8.Text = "Çıkış Saati";
            this.label8.Click += new System.EventHandler(this.label1_Click);
            // 
            // cikisSaati
            // 
            this.cikisSaati.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cikisSaati.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.cikisSaati.Location = new System.Drawing.Point(664, 182);
            this.cikisSaati.Name = "cikisSaati";
            this.cikisSaati.ShowUpDown = true;
            this.cikisSaati.Size = new System.Drawing.Size(114, 28);
            this.cikisSaati.TabIndex = 3;
            this.cikisSaati.ValueChanged += new System.EventHandler(this.cikisSaati_ValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(35, 253);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1095, 585);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            // 
            // cmbxEmployee
            // 
            this.cmbxEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbxEmployee.FormattingEnabled = true;
            this.cmbxEmployee.Location = new System.Drawing.Point(226, 85);
            this.cmbxEmployee.Name = "cmbxEmployee";
            this.cmbxEmployee.Size = new System.Drawing.Size(256, 30);
            this.cmbxEmployee.TabIndex = 6;
            this.cmbxEmployee.SelectedIndexChanged += new System.EventHandler(this.cmbxEmployee_SelectedIndexChanged);
            // 
            // btnEkle
            // 
            this.btnEkle.BackColor = System.Drawing.Color.White;
            this.btnEkle.BorderColor = System.Drawing.Color.Transparent;
            this.btnEkle.BorderRadius = 10;
            this.btnEkle.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEkle.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEkle.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEkle.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEkle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnEkle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEkle.ForeColor = System.Drawing.Color.White;
            this.btnEkle.Location = new System.Drawing.Point(950, 17);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(180, 45);
            this.btnEkle.TabIndex = 8;
            this.btnEkle.Text = "EKLE";
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnSil
            // 
            this.btnSil.BorderRadius = 10;
            this.btnSil.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSil.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSil.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSil.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSil.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnSil.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.Location = new System.Drawing.Point(950, 70);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(180, 45);
            this.btnSil.TabIndex = 8;
            this.btnSil.Text = "SİL";
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.BorderRadius = 10;
            this.btnGuncelle.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGuncelle.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGuncelle.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGuncelle.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGuncelle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnGuncelle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnGuncelle.ForeColor = System.Drawing.Color.White;
            this.btnGuncelle.Location = new System.Drawing.Point(950, 121);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(180, 45);
            this.btnGuncelle.TabIndex = 8;
            this.btnGuncelle.Text = "GÜNCELLE";
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // btnFiltrele
            // 
            this.btnFiltrele.BorderRadius = 10;
            this.btnFiltrele.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFiltrele.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFiltrele.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFiltrele.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFiltrele.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnFiltrele.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFiltrele.ForeColor = System.Drawing.Color.White;
            this.btnFiltrele.Location = new System.Drawing.Point(950, 172);
            this.btnFiltrele.Name = "btnFiltrele";
            this.btnFiltrele.Size = new System.Drawing.Size(180, 45);
            this.btnFiltrele.TabIndex = 9;
            this.btnFiltrele.Text = "FİLTRELE";
            this.btnFiltrele.Click += new System.EventHandler(this.btnFiltrele_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.BorderRadius = 10;
            this.btnExcel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExcel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExcel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExcel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExcel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(111)))), ((int)(((byte)(66)))));
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(35, 185);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(180, 45);
            this.btnExcel.TabIndex = 10;
            this.btnExcel.Text = "EXCEL DOSYASI";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // WorkFlowPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 892);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnFiltrele);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.cmbxEmployee);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cikisSaati);
            this.Controls.Add(this.girisSaati);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tarih);
            this.Controls.Add(this.txtGorevTanimi);
            this.Controls.Add(this.txtProje);
            this.Controls.Add(this.txtAsama);
            this.Name = "WorkFlowPage";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker tarih;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAsama;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGorevTanimi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker girisSaati;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker cikisSaati;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbxEmployee;
        private Guna.UI2.WinForms.Guna2Button btnEkle;
        private Guna.UI2.WinForms.Guna2Button btnSil;
        private Guna.UI2.WinForms.Guna2Button btnGuncelle;
        private Guna.UI2.WinForms.Guna2Button btnFiltrele;
        private Guna.UI2.WinForms.Guna2Button btnExcel;
    }
}

