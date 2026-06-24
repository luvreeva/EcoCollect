using Npgsql;
using System.Data;

namespace EcoCollect.Views
{
    partial class FormDashboardNasabah
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDashboardNasabah));
            this.btnLogoutNasabah = new System.Windows.Forms.Button();
            this.btnFiturTarikSaldo = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panelRiwayatPenyetoranDashboard = new System.Windows.Forms.Panel();
            this.dgvRiwayatPenyetoranDashboard = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvRiwayatPenarikan = new System.Windows.Forms.DataGridView();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.lblTotalSetor = new System.Windows.Forms.Label();
            this.lblTotalTarik = new System.Windows.Forms.Label();
            this.btnRiwayatKeuangan = new System.Windows.Forms.Button();
            this.btnProfil = new System.Windows.Forms.Button();
            this.panelRiwayatPenyetoranDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayatPenyetoranDashboard)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayatPenarikan)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogoutNasabah
            // 
            this.btnLogoutNasabah.BackColor = System.Drawing.Color.Transparent;
            this.btnLogoutNasabah.FlatAppearance.BorderSize = 0;
            this.btnLogoutNasabah.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLogoutNasabah.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLogoutNasabah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogoutNasabah.Location = new System.Drawing.Point(19, 428);
            this.btnLogoutNasabah.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogoutNasabah.Name = "btnLogoutNasabah";
            this.btnLogoutNasabah.Size = new System.Drawing.Size(229, 25);
            this.btnLogoutNasabah.TabIndex = 0;
            this.btnLogoutNasabah.UseVisualStyleBackColor = false;
            this.btnLogoutNasabah.Click += new System.EventHandler(this.btnLogoutNasabah_Click);
            // 
            // btnFiturTarikSaldo
            // 
            this.btnFiturTarikSaldo.BackColor = System.Drawing.Color.Transparent;
            this.btnFiturTarikSaldo.FlatAppearance.BorderSize = 0;
            this.btnFiturTarikSaldo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFiturTarikSaldo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFiturTarikSaldo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiturTarikSaldo.Location = new System.Drawing.Point(19, 169);
            this.btnFiturTarikSaldo.Margin = new System.Windows.Forms.Padding(2);
            this.btnFiturTarikSaldo.Name = "btnFiturTarikSaldo";
            this.btnFiturTarikSaldo.Size = new System.Drawing.Size(229, 24);
            this.btnFiturTarikSaldo.TabIndex = 2;
            this.btnFiturTarikSaldo.UseVisualStyleBackColor = false;
            this.btnFiturTarikSaldo.Click += new System.EventHandler(this.btnFiturTarikSaldo_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(63, 251);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(185, 24);
            this.button4.TabIndex = 4;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // panelRiwayatPenyetoranDashboard
            // 
            this.panelRiwayatPenyetoranDashboard.AutoScroll = true;
            this.panelRiwayatPenyetoranDashboard.AutoSize = true;
            this.panelRiwayatPenyetoranDashboard.BackColor = System.Drawing.Color.Transparent;
            this.panelRiwayatPenyetoranDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelRiwayatPenyetoranDashboard.Controls.Add(this.dgvRiwayatPenyetoranDashboard);
            this.panelRiwayatPenyetoranDashboard.Location = new System.Drawing.Point(303, 244);
            this.panelRiwayatPenyetoranDashboard.Margin = new System.Windows.Forms.Padding(2);
            this.panelRiwayatPenyetoranDashboard.Name = "panelRiwayatPenyetoranDashboard";
            this.panelRiwayatPenyetoranDashboard.Size = new System.Drawing.Size(269, 179);
            this.panelRiwayatPenyetoranDashboard.TabIndex = 5;
            // 
            // dgvRiwayatPenyetoranDashboard
            // 
            this.dgvRiwayatPenyetoranDashboard.AllowUserToAddRows = false;
            this.dgvRiwayatPenyetoranDashboard.AllowUserToDeleteRows = false;
            this.dgvRiwayatPenyetoranDashboard.AllowUserToResizeColumns = false;
            this.dgvRiwayatPenyetoranDashboard.AllowUserToResizeRows = false;
            this.dgvRiwayatPenyetoranDashboard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRiwayatPenyetoranDashboard.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvRiwayatPenyetoranDashboard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRiwayatPenyetoranDashboard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRiwayatPenyetoranDashboard.GridColor = System.Drawing.Color.Black;
            this.dgvRiwayatPenyetoranDashboard.Location = new System.Drawing.Point(0, 1);
            this.dgvRiwayatPenyetoranDashboard.Margin = new System.Windows.Forms.Padding(2);
            this.dgvRiwayatPenyetoranDashboard.Name = "dgvRiwayatPenyetoranDashboard";
            this.dgvRiwayatPenyetoranDashboard.ReadOnly = true;
            this.dgvRiwayatPenyetoranDashboard.RowHeadersWidth = 62;
            this.dgvRiwayatPenyetoranDashboard.RowTemplate.Height = 28;
            this.dgvRiwayatPenyetoranDashboard.ShowCellErrors = false;
            this.dgvRiwayatPenyetoranDashboard.ShowEditingIcon = false;
            this.dgvRiwayatPenyetoranDashboard.ShowRowErrors = false;
            this.dgvRiwayatPenyetoranDashboard.Size = new System.Drawing.Size(265, 176);
            this.dgvRiwayatPenyetoranDashboard.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.dgvRiwayatPenarikan);
            this.panel1.Location = new System.Drawing.Point(597, 242);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 176);
            this.panel1.TabIndex = 6;
            // 
            // dgvRiwayatPenarikan
            // 
            this.dgvRiwayatPenarikan.AllowUserToAddRows = false;
            this.dgvRiwayatPenarikan.AllowUserToDeleteRows = false;
            this.dgvRiwayatPenarikan.AllowUserToResizeColumns = false;
            this.dgvRiwayatPenarikan.AllowUserToResizeRows = false;
            this.dgvRiwayatPenarikan.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvRiwayatPenarikan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRiwayatPenarikan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRiwayatPenarikan.GridColor = System.Drawing.Color.Black;
            this.dgvRiwayatPenarikan.Location = new System.Drawing.Point(1, 1);
            this.dgvRiwayatPenarikan.Margin = new System.Windows.Forms.Padding(2);
            this.dgvRiwayatPenarikan.Name = "dgvRiwayatPenarikan";
            this.dgvRiwayatPenarikan.ReadOnly = true;
            this.dgvRiwayatPenarikan.RowHeadersWidth = 62;
            this.dgvRiwayatPenarikan.RowTemplate.Height = 28;
            this.dgvRiwayatPenarikan.Size = new System.Drawing.Size(263, 177);
            this.dgvRiwayatPenarikan.TabIndex = 0;
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.BackColor = System.Drawing.Color.Transparent;
            this.lblSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldo.ForeColor = System.Drawing.Color.White;
            this.lblSaldo.Location = new System.Drawing.Point(343, 170);
            this.lblSaldo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(47, 20);
            this.lblSaldo.TabIndex = 7;
            this.lblSaldo.Text = "Rp 0";
            // 
            // lblTotalSetor
            // 
            this.lblTotalSetor.AutoSize = true;
            this.lblTotalSetor.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalSetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSetor.Location = new System.Drawing.Point(537, 170);
            this.lblTotalSetor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalSetor.Name = "lblTotalSetor";
            this.lblTotalSetor.Size = new System.Drawing.Size(47, 20);
            this.lblTotalSetor.TabIndex = 8;
            this.lblTotalSetor.Text = "Rp 0";
            // 
            // lblTotalTarik
            // 
            this.lblTotalTarik.AutoSize = true;
            this.lblTotalTarik.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalTarik.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTarik.Location = new System.Drawing.Point(735, 169);
            this.lblTotalTarik.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalTarik.Name = "lblTotalTarik";
            this.lblTotalTarik.Size = new System.Drawing.Size(47, 20);
            this.lblTotalTarik.TabIndex = 9;
            this.lblTotalTarik.Text = "Rp 0";
            // 
            // btnRiwayatKeuangan
            // 
            this.btnRiwayatKeuangan.BackColor = System.Drawing.Color.Transparent;
            this.btnRiwayatKeuangan.FlatAppearance.BorderSize = 0;
            this.btnRiwayatKeuangan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRiwayatKeuangan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRiwayatKeuangan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRiwayatKeuangan.Location = new System.Drawing.Point(19, 213);
            this.btnRiwayatKeuangan.Margin = new System.Windows.Forms.Padding(2);
            this.btnRiwayatKeuangan.Name = "btnRiwayatKeuangan";
            this.btnRiwayatKeuangan.Size = new System.Drawing.Size(231, 20);
            this.btnRiwayatKeuangan.TabIndex = 10;
            this.btnRiwayatKeuangan.UseVisualStyleBackColor = false;
            this.btnRiwayatKeuangan.Click += new System.EventHandler(this.btnRiwayatKeuangan_Click);
            // 
            // btnProfil
            // 
            this.btnProfil.BackColor = System.Drawing.Color.Transparent;
            this.btnProfil.FlatAppearance.BorderSize = 0;
            this.btnProfil.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnProfil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnProfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfil.Location = new System.Drawing.Point(19, 251);
            this.btnProfil.Name = "btnProfil";
            this.btnProfil.Size = new System.Drawing.Size(231, 24);
            this.btnProfil.TabIndex = 11;
            this.btnProfil.UseVisualStyleBackColor = false;
            this.btnProfil.Click += new System.EventHandler(this.btnProfil_Click);
            // 
            // FormDashboardNasabah
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(894, 474);
            this.Controls.Add(this.btnProfil);
            this.Controls.Add(this.btnRiwayatKeuangan);
            this.Controls.Add(this.lblTotalTarik);
            this.Controls.Add(this.lblTotalSetor);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelRiwayatPenyetoranDashboard);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnFiturTarikSaldo);
            this.Controls.Add(this.btnLogoutNasabah);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormDashboardNasabah";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormDashboardNasabah";
            this.Load += new System.EventHandler(this.FormDashboardNasabah_Load);
            this.panelRiwayatPenyetoranDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayatPenyetoranDashboard)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayatPenarikan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogoutNasabah;
        private System.Windows.Forms.Button btnFiturTarikSaldo;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panelRiwayatPenyetoranDashboard;
        private System.Windows.Forms.DataGridView dgvRiwayatPenyetoranDashboard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvRiwayatPenarikan;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.Label lblTotalSetor;
        private System.Windows.Forms.Label lblTotalTarik;
        private System.Windows.Forms.Button btnRiwayatKeuangan;
        private System.Windows.Forms.Button btnProfil;
    }
}