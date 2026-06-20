namespace EcoCollect.Views
{
    partial class FormPengaturanProfil
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnDashboardNasabah = new System.Windows.Forms.Button();
            this.btnTarikSaldo = new System.Windows.Forms.Button();
            this.btnRiwayatKeuangan = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnDashboardNasabah
            // 
            this.btnDashboardNasabah.BackColor = System.Drawing.Color.Transparent;
            this.btnDashboardNasabah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboardNasabah.Location = new System.Drawing.Point(94, 195);
            this.btnDashboardNasabah.Name = "btnDashboardNasabah";
            this.btnDashboardNasabah.Size = new System.Drawing.Size(278, 37);
            this.btnDashboardNasabah.TabIndex = 1;
            this.btnDashboardNasabah.UseVisualStyleBackColor = false;
            // 
            // btnTarikSaldo
            // 
            this.btnTarikSaldo.BackColor = System.Drawing.Color.Transparent;
            this.btnTarikSaldo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTarikSaldo.Location = new System.Drawing.Point(94, 260);
            this.btnTarikSaldo.Name = "btnTarikSaldo";
            this.btnTarikSaldo.Size = new System.Drawing.Size(277, 34);
            this.btnTarikSaldo.TabIndex = 2;
            this.btnTarikSaldo.UseVisualStyleBackColor = false;
            // 
            // btnRiwayatKeuangan
            // 
            this.btnRiwayatKeuangan.BackColor = System.Drawing.Color.Transparent;
            this.btnRiwayatKeuangan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRiwayatKeuangan.Location = new System.Drawing.Point(97, 325);
            this.btnRiwayatKeuangan.Name = "btnRiwayatKeuangan";
            this.btnRiwayatKeuangan.Size = new System.Drawing.Size(273, 35);
            this.btnRiwayatKeuangan.TabIndex = 3;
            this.btnRiwayatKeuangan.UseVisualStyleBackColor = false;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Location = new System.Drawing.Point(115, 658);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(254, 41);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.UseVisualStyleBackColor = false;
            // 
            // FormPengaturanProfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EcoCollect.Properties.Resources.P_A__4_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1341, 729);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnRiwayatKeuangan);
            this.Controls.Add(this.btnTarikSaldo);
            this.Controls.Add(this.btnDashboardNasabah);
            this.Controls.Add(this.button1);
            this.Name = "FormPengaturanProfil";
            this.Text = "FormUbahProfil";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDashboardNasabah;
        private System.Windows.Forms.Button btnTarikSaldo;
        private System.Windows.Forms.Button btnRiwayatKeuangan;
        private System.Windows.Forms.Button btnLogout;
    }
}