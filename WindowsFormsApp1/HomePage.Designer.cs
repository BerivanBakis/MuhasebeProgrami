namespace WindowsFormsApp1
{
    partial class HomePage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomePage));
            this.btnIsAkisi = new System.Windows.Forms.Button();
            this.btnPuantaj = new System.Windows.Forms.Button();
            this.btnAvans = new System.Windows.Forms.Button();
            this.btnPersonel = new System.Windows.Forms.Button();
            this.btnHakedis = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnIsAkisi
            // 
            this.btnIsAkisi.BackColor = System.Drawing.Color.Brown;
            this.btnIsAkisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIsAkisi.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnIsAkisi.Location = new System.Drawing.Point(128, 61);
            this.btnIsAkisi.Name = "btnIsAkisi";
            this.btnIsAkisi.Size = new System.Drawing.Size(146, 144);
            this.btnIsAkisi.TabIndex = 0;
            this.btnIsAkisi.Text = "İş Akışı";
            this.btnIsAkisi.UseVisualStyleBackColor = false;
            this.btnIsAkisi.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPuantaj
            // 
            this.btnPuantaj.BackColor = System.Drawing.Color.Chocolate;
            this.btnPuantaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnPuantaj.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPuantaj.Location = new System.Drawing.Point(316, 61);
            this.btnPuantaj.Name = "btnPuantaj";
            this.btnPuantaj.Size = new System.Drawing.Size(146, 144);
            this.btnPuantaj.TabIndex = 0;
            this.btnPuantaj.Text = "Puantaj";
            this.btnPuantaj.UseVisualStyleBackColor = false;
            this.btnPuantaj.Click += new System.EventHandler(this.btnPuantaj_Click);
            // 
            // btnAvans
            // 
            this.btnAvans.BackColor = System.Drawing.Color.Goldenrod;
            this.btnAvans.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAvans.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAvans.Location = new System.Drawing.Point(503, 61);
            this.btnAvans.Name = "btnAvans";
            this.btnAvans.Size = new System.Drawing.Size(146, 144);
            this.btnAvans.TabIndex = 0;
            this.btnAvans.Text = "Avans";
            this.btnAvans.UseVisualStyleBackColor = false;
            this.btnAvans.Click += new System.EventHandler(this.btnAvans_Click);
            // 
            // btnPersonel
            // 
            this.btnPersonel.BackColor = System.Drawing.Color.OliveDrab;
            this.btnPersonel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnPersonel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPersonel.Location = new System.Drawing.Point(686, 61);
            this.btnPersonel.Name = "btnPersonel";
            this.btnPersonel.Size = new System.Drawing.Size(146, 144);
            this.btnPersonel.TabIndex = 0;
            this.btnPersonel.Text = "Personel Bilgileri";
            this.btnPersonel.UseVisualStyleBackColor = false;
            this.btnPersonel.Click += new System.EventHandler(this.btnPersonel_Click);
            // 
            // btnHakedis
            // 
            this.btnHakedis.BackColor = System.Drawing.Color.SeaGreen;
            this.btnHakedis.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnHakedis.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHakedis.Location = new System.Drawing.Point(864, 61);
            this.btnHakedis.Name = "btnHakedis";
            this.btnHakedis.Size = new System.Drawing.Size(146, 144);
            this.btnHakedis.TabIndex = 0;
            this.btnHakedis.Text = "Hakediş";
            this.btnHakedis.UseVisualStyleBackColor = false;
            this.btnHakedis.Click += new System.EventHandler(this.btnHakedis_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.LightSeaGreen;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button6.Location = new System.Drawing.Point(1046, 61);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(146, 144);
            this.button6.TabIndex = 0;
            this.button6.Text = "İş Akışı";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1497, 892);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.btnHakedis);
            this.Controls.Add(this.btnPersonel);
            this.Controls.Add(this.btnAvans);
            this.Controls.Add(this.btnPuantaj);
            this.Controls.Add(this.btnIsAkisi);
            this.Name = "HomePage";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnIsAkisi;
        private System.Windows.Forms.Button btnPuantaj;
        private System.Windows.Forms.Button btnAvans;
        private System.Windows.Forms.Button btnPersonel;
        private System.Windows.Forms.Button btnHakedis;
        private System.Windows.Forms.Button button6;
    }
}