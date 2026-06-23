namespace EcoCollect.Views
{
    partial class FormStrukSetoran
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
            this.lblTanggalTransaksi = new System.Windows.Forms.Label();
            this.lblNamaNasabah = new System.Windows.Forms.Label();
            this.lblNoTelepon = new System.Windows.Forms.Label();
            this.btnSelesai = new System.Windows.Forms.Button();
            this.flpRincianSetoran = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTotalNominal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTanggalTransaksi
            // 
            this.lblTanggalTransaksi.AutoSize = true;
            this.lblTanggalTransaksi.BackColor = System.Drawing.Color.Transparent;
            this.lblTanggalTransaksi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTanggalTransaksi.Location = new System.Drawing.Point(707, 264);
            this.lblTanggalTransaksi.Name = "lblTanggalTransaksi";
            this.lblTanggalTransaksi.Size = new System.Drawing.Size(92, 29);
            this.lblTanggalTransaksi.TabIndex = 0;
            this.lblTanggalTransaksi.Text = "tanggal";
            // 
            // lblNamaNasabah
            // 
            this.lblNamaNasabah.AutoSize = true;
            this.lblNamaNasabah.BackColor = System.Drawing.Color.Transparent;
            this.lblNamaNasabah.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaNasabah.Location = new System.Drawing.Point(707, 293);
            this.lblNamaNasabah.Name = "lblNamaNasabah";
            this.lblNamaNasabah.Size = new System.Drawing.Size(168, 29);
            this.lblNamaNasabah.TabIndex = 1;
            this.lblNamaNasabah.Text = "namaNasabah";
            // 
            // lblNoTelepon
            // 
            this.lblNoTelepon.AutoSize = true;
            this.lblNoTelepon.BackColor = System.Drawing.Color.Transparent;
            this.lblNoTelepon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoTelepon.Location = new System.Drawing.Point(708, 325);
            this.lblNoTelepon.Name = "lblNoTelepon";
            this.lblNoTelepon.Size = new System.Drawing.Size(80, 29);
            this.lblNoTelepon.TabIndex = 2;
            this.lblNoTelepon.Text = "notelp";
            // 
            // btnSelesai
            // 
            this.btnSelesai.BackColor = System.Drawing.Color.Transparent;
            this.btnSelesai.FlatAppearance.BorderSize = 0;
            this.btnSelesai.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelesai.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelesai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelesai.Location = new System.Drawing.Point(539, 606);
            this.btnSelesai.Name = "btnSelesai";
            this.btnSelesai.Size = new System.Drawing.Size(259, 31);
            this.btnSelesai.TabIndex = 3;
            this.btnSelesai.UseVisualStyleBackColor = false;
            this.btnSelesai.Click += new System.EventHandler(this.btnSelesai_Click);
            // 
            // flpRincianSetoran
            // 
            this.flpRincianSetoran.AutoScroll = true;
            this.flpRincianSetoran.BackColor = System.Drawing.Color.Transparent;
            this.flpRincianSetoran.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpRincianSetoran.Location = new System.Drawing.Point(384, 405);
            this.flpRincianSetoran.Name = "flpRincianSetoran";
            this.flpRincianSetoran.Size = new System.Drawing.Size(568, 148);
            this.flpRincianSetoran.TabIndex = 4;
            this.flpRincianSetoran.WrapContents = false;
            // 
            // lblTotalNominal
            // 
            this.lblTotalNominal.AutoSize = true;
            this.lblTotalNominal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalNominal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalNominal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalNominal.Location = new System.Drawing.Point(836, 373);
            this.lblTotalNominal.Name = "lblTotalNominal";
            this.lblTotalNominal.Size = new System.Drawing.Size(63, 29);
            this.lblTotalNominal.TabIndex = 5;
            this.lblTotalNominal.Text = "Rp 0";
            // 
            // FormStrukSetoran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EcoCollect.Properties.Resources.nasabah_R__2_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1341, 729);
            this.Controls.Add(this.lblTotalNominal);
            this.Controls.Add(this.flpRincianSetoran);
            this.Controls.Add(this.btnSelesai);
            this.Controls.Add(this.lblNoTelepon);
            this.Controls.Add(this.lblNamaNasabah);
            this.Controls.Add(this.lblTanggalTransaksi);
            this.Name = "FormStrukSetoran";
            this.Text = "FormStrukSetoran";
            this.Load += new System.EventHandler(this.FormStrukSetoran_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTanggalTransaksi;
        private System.Windows.Forms.Label lblNamaNasabah;
        private System.Windows.Forms.Label lblNoTelepon;
        private System.Windows.Forms.Button btnSelesai;
        private System.Windows.Forms.FlowLayoutPanel flpRincianSetoran;
        private System.Windows.Forms.Label lblTotalNominal;
    }
}