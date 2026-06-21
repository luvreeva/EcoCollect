namespace LAYANANPETUGAS
{
    partial class FormTransaksiBaru
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTransaksiBaru));
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.txtBerat = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.dgvKeranjang = new System.Windows.Forms.DataGridView();
            this.lblTotalBerat = new System.Windows.Forms.Label();
            this.lblTotalHarga = new System.Windows.Forms.Label();
            this.btnSimpanUtama = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbKategori
            // 
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Location = new System.Drawing.Point(484, 142);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(121, 24);
            this.cmbKategori.TabIndex = 0;
            // 
            // txtBerat
            // 
            this.txtBerat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBerat.Location = new System.Drawing.Point(612, 143);
            this.txtBerat.Name = "txtBerat";
            this.txtBerat.Size = new System.Drawing.Size(67, 15);
            this.txtBerat.TabIndex = 1;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.Transparent;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Location = new System.Drawing.Point(685, 142);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(65, 24);
            this.btnTambah.TabIndex = 2;
            this.btnTambah.UseVisualStyleBackColor = false;
            // 
            // dgvKeranjang
            // 
            this.dgvKeranjang.AllowUserToAddRows = false;
            this.dgvKeranjang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKeranjang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKeranjang.Location = new System.Drawing.Point(484, 184);
            this.dgvKeranjang.Name = "dgvKeranjang";
            this.dgvKeranjang.RowHeadersVisible = false;
            this.dgvKeranjang.RowHeadersWidth = 51;
            this.dgvKeranjang.RowTemplate.Height = 24;
            this.dgvKeranjang.Size = new System.Drawing.Size(275, 51);
            this.dgvKeranjang.TabIndex = 3;
            // 
            // lblTotalBerat
            // 
            this.lblTotalBerat.AutoSize = true;
            this.lblTotalBerat.Location = new System.Drawing.Point(484, 261);
            this.lblTotalBerat.Name = "lblTotalBerat";
            this.lblTotalBerat.Size = new System.Drawing.Size(33, 16);
            this.lblTotalBerat.TabIndex = 4;
            this.lblTotalBerat.Text = "0 Kg";
            // 
            // lblTotalHarga
            // 
            this.lblTotalHarga.AutoSize = true;
            this.lblTotalHarga.Location = new System.Drawing.Point(657, 261);
            this.lblTotalHarga.Name = "lblTotalHarga";
            this.lblTotalHarga.Size = new System.Drawing.Size(35, 16);
            this.lblTotalHarga.TabIndex = 5;
            this.lblTotalHarga.Text = "Rp 0";
            // 
            // btnSimpanUtama
            // 
            this.btnSimpanUtama.BackColor = System.Drawing.Color.Transparent;
            this.btnSimpanUtama.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanUtama.Location = new System.Drawing.Point(626, 296);
            this.btnSimpanUtama.Name = "btnSimpanUtama";
            this.btnSimpanUtama.Size = new System.Drawing.Size(133, 24);
            this.btnSimpanUtama.TabIndex = 6;
            this.btnSimpanUtama.UseVisualStyleBackColor = false;
            // 
            // FormTransaksiBaru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSimpanUtama);
            this.Controls.Add(this.lblTotalHarga);
            this.Controls.Add(this.lblTotalBerat);
            this.Controls.Add(this.dgvKeranjang);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtBerat);
            this.Controls.Add(this.cmbKategori);
            this.Name = "FormTransaksiBaru";
            this.Text = "FormTransaksiBaru";
            this.Load += new System.EventHandler(this.FormTransaksiBaru_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.TextBox txtBerat;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.DataGridView dgvKeranjang;
        private System.Windows.Forms.Label lblTotalBerat;
        private System.Windows.Forms.Label lblTotalHarga;
        private System.Windows.Forms.Button btnSimpanUtama;
    }
}