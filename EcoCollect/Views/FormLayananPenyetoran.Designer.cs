namespace EcoCollect.Views
{
    partial class FormLayananPenyetoran
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLayananPenyetoran));
            this.lblNamaNasabah = new System.Windows.Forms.Label();
            this.lblUsernameNasabah = new System.Windows.Forms.Label();
            this.lblTeleponNasabah = new System.Windows.Forms.Label();
            this.cmbJenisSampah = new System.Windows.Forms.ComboBox();
            this.txtBeratKg = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.lblTotalTimbangan = new System.Windows.Forms.Label();
            this.lblTotalNominal = new System.Windows.Forms.Label();
            this.btnSimpanSetoran = new System.Windows.Forms.Button();
            this.dgvDetailSetoran = new System.Windows.Forms.DataGridView();
            this.btnKembali = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailSetoran)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNamaNasabah
            // 
            this.lblNamaNasabah.AutoSize = true;
            this.lblNamaNasabah.BackColor = System.Drawing.Color.Transparent;
            this.lblNamaNasabah.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaNasabah.Location = new System.Drawing.Point(549, 109);
            this.lblNamaNasabah.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNamaNasabah.Name = "lblNamaNasabah";
            this.lblNamaNasabah.Size = new System.Drawing.Size(33, 13);
            this.lblNamaNasabah.TabIndex = 0;
            this.lblNamaNasabah.Text = "nama";
            // 
            // lblUsernameNasabah
            // 
            this.lblUsernameNasabah.AutoSize = true;
            this.lblUsernameNasabah.BackColor = System.Drawing.Color.Transparent;
            this.lblUsernameNasabah.Location = new System.Drawing.Point(657, 108);
            this.lblUsernameNasabah.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsernameNasabah.Name = "lblUsernameNasabah";
            this.lblUsernameNasabah.Size = new System.Drawing.Size(35, 13);
            this.lblUsernameNasabah.TabIndex = 1;
            this.lblUsernameNasabah.Text = "label2";
            // 
            // lblTeleponNasabah
            // 
            this.lblTeleponNasabah.AutoSize = true;
            this.lblTeleponNasabah.BackColor = System.Drawing.Color.Transparent;
            this.lblTeleponNasabah.Location = new System.Drawing.Point(764, 109);
            this.lblTeleponNasabah.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTeleponNasabah.Name = "lblTeleponNasabah";
            this.lblTeleponNasabah.Size = new System.Drawing.Size(19, 13);
            this.lblTeleponNasabah.TabIndex = 2;
            this.lblTeleponNasabah.Text = "no";
            // 
            // cmbJenisSampah
            // 
            this.cmbJenisSampah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJenisSampah.FormattingEnabled = true;
            this.cmbJenisSampah.Location = new System.Drawing.Point(539, 150);
            this.cmbJenisSampah.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbJenisSampah.Name = "cmbJenisSampah";
            this.cmbJenisSampah.Size = new System.Drawing.Size(141, 21);
            this.cmbJenisSampah.TabIndex = 3;
            // 
            // txtBeratKg
            // 
            this.txtBeratKg.Location = new System.Drawing.Point(730, 151);
            this.txtBeratKg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBeratKg.Name = "txtBeratKg";
            this.txtBeratKg.Size = new System.Drawing.Size(21, 20);
            this.txtBeratKg.TabIndex = 4;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.Transparent;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTambah.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Location = new System.Drawing.Point(767, 151);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(76, 16);
            this.btnTambah.TabIndex = 5;
            this.btnTambah.UseVisualStyleBackColor = false;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // lblTotalTimbangan
            // 
            this.lblTotalTimbangan.AutoSize = true;
            this.lblTotalTimbangan.BackColor = System.Drawing.Color.Teal;
            this.lblTotalTimbangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTimbangan.ForeColor = System.Drawing.Color.White;
            this.lblTotalTimbangan.Location = new System.Drawing.Point(549, 273);
            this.lblTotalTimbangan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalTimbangan.Name = "lblTotalTimbangan";
            this.lblTotalTimbangan.Size = new System.Drawing.Size(45, 20);
            this.lblTotalTimbangan.TabIndex = 6;
            this.lblTotalTimbangan.Text = "0 Kg";
            // 
            // lblTotalNominal
            // 
            this.lblTotalNominal.AutoSize = true;
            this.lblTotalNominal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalNominal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalNominal.ForeColor = System.Drawing.Color.White;
            this.lblTotalNominal.Location = new System.Drawing.Point(748, 272);
            this.lblTotalNominal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalNominal.Name = "lblTotalNominal";
            this.lblTotalNominal.Size = new System.Drawing.Size(47, 20);
            this.lblTotalNominal.TabIndex = 7;
            this.lblTotalNominal.Text = "Rp 0";
            // 
            // btnSimpanSetoran
            // 
            this.btnSimpanSetoran.BackColor = System.Drawing.Color.Transparent;
            this.btnSimpanSetoran.FlatAppearance.BorderSize = 0;
            this.btnSimpanSetoran.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSimpanSetoran.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSimpanSetoran.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanSetoran.Location = new System.Drawing.Point(707, 317);
            this.btnSimpanSetoran.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSimpanSetoran.Name = "btnSimpanSetoran";
            this.btnSimpanSetoran.Size = new System.Drawing.Size(141, 16);
            this.btnSimpanSetoran.TabIndex = 8;
            this.btnSimpanSetoran.UseVisualStyleBackColor = false;
            this.btnSimpanSetoran.Click += new System.EventHandler(this.btnSimpanSetoran_Click);
            // 
            // dgvDetailSetoran
            // 
            this.dgvDetailSetoran.AllowUserToAddRows = false;
            this.dgvDetailSetoran.AllowUserToDeleteRows = false;
            this.dgvDetailSetoran.AllowUserToResizeColumns = false;
            this.dgvDetailSetoran.AllowUserToResizeRows = false;
            this.dgvDetailSetoran.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetailSetoran.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDetailSetoran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetailSetoran.GridColor = System.Drawing.Color.Black;
            this.dgvDetailSetoran.Location = new System.Drawing.Point(537, 186);
            this.dgvDetailSetoran.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvDetailSetoran.Name = "dgvDetailSetoran";
            this.dgvDetailSetoran.ReadOnly = true;
            this.dgvDetailSetoran.RowHeadersVisible = false;
            this.dgvDetailSetoran.RowHeadersWidth = 62;
            this.dgvDetailSetoran.RowTemplate.Height = 28;
            this.dgvDetailSetoran.Size = new System.Drawing.Size(309, 62);
            this.dgvDetailSetoran.TabIndex = 9;
            this.dgvDetailSetoran.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetailSetoran_CellContentClick);
            // 
            // btnKembali
            // 
            this.btnKembali.BackColor = System.Drawing.Color.Transparent;
            this.btnKembali.FlatAppearance.BorderSize = 0;
            this.btnKembali.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnKembali.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembali.Location = new System.Drawing.Point(523, 57);
            this.btnKembali.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(29, 18);
            this.btnKembali.TabIndex = 10;
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // FormLayananPenyetoran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(894, 474);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.dgvDetailSetoran);
            this.Controls.Add(this.btnSimpanSetoran);
            this.Controls.Add(this.lblTotalNominal);
            this.Controls.Add(this.lblTotalTimbangan);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtBeratKg);
            this.Controls.Add(this.cmbJenisSampah);
            this.Controls.Add(this.lblTeleponNasabah);
            this.Controls.Add(this.lblUsernameNasabah);
            this.Controls.Add(this.lblNamaNasabah);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormLayananPenyetoran";
            this.Text = "FormLayananPenyetoran";
            this.Load += new System.EventHandler(this.FormLayananPenyetoran_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailSetoran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNamaNasabah;
        private System.Windows.Forms.Label lblUsernameNasabah;
        private System.Windows.Forms.Label lblTeleponNasabah;
        private System.Windows.Forms.ComboBox cmbJenisSampah;
        private System.Windows.Forms.TextBox txtBeratKg;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label lblTotalTimbangan;
        private System.Windows.Forms.Label lblTotalNominal;
        private System.Windows.Forms.Button btnSimpanSetoran;
        private System.Windows.Forms.DataGridView dgvDetailSetoran;
        private System.Windows.Forms.Button btnKembali;
    }
}