namespace LAYANANPETUGAS
{
    partial class FormBuatSetoran
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBuatSetoran));
            this.lblFrekuensiSetor = new System.Windows.Forms.Label();
            this.lblTotalMassa = new System.Windows.Forms.Label();
            this.dgvHistoriSetoran = new System.Windows.Forms.DataGridView();
            this.btnBuatSetoran = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnKelolaJenisSampah = new System.Windows.Forms.Button();
            this.panelNasabah1 = new System.Windows.Forms.Panel();
            this.lblDetailNasabah1 = new System.Windows.Forms.Label();
            this.lblNamaNasabah1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoriSetoran)).BeginInit();
            this.panelNasabah1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFrekuensiSetor
            // 
            this.lblFrekuensiSetor.AutoSize = true;
            this.lblFrekuensiSetor.BackColor = System.Drawing.Color.Transparent;
            this.lblFrekuensiSetor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrekuensiSetor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(91)))), ((int)(((byte)(113)))));
            this.lblFrekuensiSetor.Location = new System.Drawing.Point(490, 164);
            this.lblFrekuensiSetor.Name = "lblFrekuensiSetor";
            this.lblFrekuensiSetor.Size = new System.Drawing.Size(46, 20);
            this.lblFrekuensiSetor.TabIndex = 0;
            this.lblFrekuensiSetor.Text = "0 kali";
            // 
            // lblTotalMassa
            // 
            this.lblTotalMassa.AutoSize = true;
            this.lblTotalMassa.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalMassa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalMassa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(91)))), ((int)(((byte)(113)))));
            this.lblTotalMassa.Location = new System.Drawing.Point(632, 164);
            this.lblTotalMassa.Name = "lblTotalMassa";
            this.lblTotalMassa.Size = new System.Drawing.Size(39, 20);
            this.lblTotalMassa.TabIndex = 1;
            this.lblTotalMassa.Text = "0 kg";
            // 
            // dgvHistoriSetoran
            // 
            this.dgvHistoriSetoran.AllowUserToAddRows = false;
            this.dgvHistoriSetoran.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistoriSetoran.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvHistoriSetoran.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistoriSetoran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistoriSetoran.Location = new System.Drawing.Point(481, 212);
            this.dgvHistoriSetoran.Name = "dgvHistoriSetoran";
            this.dgvHistoriSetoran.RowHeadersVisible = false;
            this.dgvHistoriSetoran.RowHeadersWidth = 51;
            this.dgvHistoriSetoran.RowTemplate.Height = 24;
            this.dgvHistoriSetoran.Size = new System.Drawing.Size(272, 73);
            this.dgvHistoriSetoran.TabIndex = 2;
            // 
            // btnBuatSetoran
            // 
            this.btnBuatSetoran.BackColor = System.Drawing.Color.Transparent;
            this.btnBuatSetoran.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuatSetoran.Location = new System.Drawing.Point(481, 296);
            this.btnBuatSetoran.Name = "btnBuatSetoran";
            this.btnBuatSetoran.Size = new System.Drawing.Size(272, 40);
            this.btnBuatSetoran.TabIndex = 3;
            this.btnBuatSetoran.UseVisualStyleBackColor = false;
            this.btnBuatSetoran.Click += new System.EventHandler(this.btnBuatSetoran_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 35);
            this.button1.TabIndex = 4;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnKelolaJenisSampah
            // 
            this.btnKelolaJenisSampah.BackColor = System.Drawing.Color.Transparent;
            this.btnKelolaJenisSampah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKelolaJenisSampah.Location = new System.Drawing.Point(12, 151);
            this.btnKelolaJenisSampah.Name = "btnKelolaJenisSampah";
            this.btnKelolaJenisSampah.Size = new System.Drawing.Size(215, 35);
            this.btnKelolaJenisSampah.TabIndex = 5;
            this.btnKelolaJenisSampah.UseVisualStyleBackColor = false;
            this.btnKelolaJenisSampah.Click += new System.EventHandler(this.btnKelolaJenisSampah_Click);
            // 
            // panelNasabah1
            // 
            this.panelNasabah1.Controls.Add(this.lblDetailNasabah1);
            this.panelNasabah1.Controls.Add(this.lblNamaNasabah1);
            this.panelNasabah1.Location = new System.Drawing.Point(256, 97);
            this.panelNasabah1.Name = "panelNasabah1";
            this.panelNasabah1.Size = new System.Drawing.Size(182, 39);
            this.panelNasabah1.TabIndex = 6;
            this.panelNasabah1.Click += new System.EventHandler(this.panelNasabah1_Click);
            // 
            // lblDetailNasabah1
            // 
            this.lblDetailNasabah1.AutoSize = true;
            this.lblDetailNasabah1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailNasabah1.Location = new System.Drawing.Point(7, 22);
            this.lblDetailNasabah1.Name = "lblDetailNasabah1";
            this.lblDetailNasabah1.Size = new System.Drawing.Size(67, 12);
            this.lblDetailNasabah1.TabIndex = 1;
            this.lblDetailNasabah1.Tag = "1";
            this.lblDetailNasabah1.Text = "@Amanda123";
            this.lblDetailNasabah1.Click += new System.EventHandler(this.panelNasabah1_Click);
            // 
            // lblNamaNasabah1
            // 
            this.lblNamaNasabah1.AutoSize = true;
            this.lblNamaNasabah1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaNasabah1.Location = new System.Drawing.Point(4, 4);
            this.lblNamaNasabah1.Name = "lblNamaNasabah1";
            this.lblNamaNasabah1.Size = new System.Drawing.Size(114, 17);
            this.lblNamaNasabah1.TabIndex = 0;
            this.lblNamaNasabah1.Tag = "1";
            this.lblNamaNasabah1.Text = "Amanda Manopo";
            this.lblNamaNasabah1.Click += new System.EventHandler(this.panelNasabah1_Click);
            // 
            // FormBuatSetoran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelNasabah1);
            this.Controls.Add(this.btnKelolaJenisSampah);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBuatSetoran);
            this.Controls.Add(this.dgvHistoriSetoran);
            this.Controls.Add(this.lblTotalMassa);
            this.Controls.Add(this.lblFrekuensiSetor);
            this.Name = "FormBuatSetoran";
            this.Text = "FormBuatSetoran";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormBuatSetoran_FormClosed);
            this.Click += new System.EventHandler(this.FormBuatSetoran_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoriSetoran)).EndInit();
            this.panelNasabah1.ResumeLayout(false);
            this.panelNasabah1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFrekuensiSetor;
        private System.Windows.Forms.Label lblTotalMassa;
        private System.Windows.Forms.DataGridView dgvHistoriSetoran;
        private System.Windows.Forms.Button btnBuatSetoran;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnKelolaJenisSampah;
        private System.Windows.Forms.Panel panelNasabah1;
        private System.Windows.Forms.Label lblDetailNasabah1;
        private System.Windows.Forms.Label lblNamaNasabah1;
    }
}