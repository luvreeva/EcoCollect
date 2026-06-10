namespace EcoCollect.Views
{
    partial class FormLoginNasabah
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoginNasabah));
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnMasukNasabah = new System.Windows.Forms.Button();
            this.btnKeRegristrasi = new System.Windows.Forms.Button();
            this.btnKeBeranda = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(380, 277);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(579, 34);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(380, 383);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(579, 34);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnMasukNasabah
            // 
            this.btnMasukNasabah.BackColor = System.Drawing.Color.Transparent;
            this.btnMasukNasabah.FlatAppearance.BorderSize = 0;
            this.btnMasukNasabah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasukNasabah.Location = new System.Drawing.Point(528, 455);
            this.btnMasukNasabah.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMasukNasabah.Name = "btnMasukNasabah";
            this.btnMasukNasabah.Size = new System.Drawing.Size(274, 46);
            this.btnMasukNasabah.TabIndex = 2;
            this.btnMasukNasabah.UseVisualStyleBackColor = false;
            this.btnMasukNasabah.Click += new System.EventHandler(this.btnMasuk_Click);
            // 
            // btnKeRegristrasi
            // 
            this.btnKeRegristrasi.BackColor = System.Drawing.Color.Transparent;
            this.btnKeRegristrasi.FlatAppearance.BorderSize = 0;
            this.btnKeRegristrasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeRegristrasi.Location = new System.Drawing.Point(699, 511);
            this.btnKeRegristrasi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnKeRegristrasi.Name = "btnKeRegristrasi";
            this.btnKeRegristrasi.Size = new System.Drawing.Size(171, 35);
            this.btnKeRegristrasi.TabIndex = 3;
            this.btnKeRegristrasi.UseVisualStyleBackColor = false;
            this.btnKeRegristrasi.Click += new System.EventHandler(this.btnKeRegristrasi_Click);
            // 
            // btnKeBeranda
            // 
            this.btnKeBeranda.BackColor = System.Drawing.Color.Transparent;
            this.btnKeBeranda.FlatAppearance.BorderSize = 0;
            this.btnKeBeranda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeBeranda.Location = new System.Drawing.Point(574, 555);
            this.btnKeBeranda.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnKeBeranda.Name = "btnKeBeranda";
            this.btnKeBeranda.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnKeBeranda.Size = new System.Drawing.Size(228, 28);
            this.btnKeBeranda.TabIndex = 4;
            this.btnKeBeranda.UseVisualStyleBackColor = false;
            this.btnKeBeranda.Click += new System.EventHandler(this.btnKeBeranda_Click);
            // 
            // FormLoginNasabah
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1341, 729);
            this.Controls.Add(this.btnKeBeranda);
            this.Controls.Add(this.btnKeRegristrasi);
            this.Controls.Add(this.btnMasukNasabah);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormLoginNasabah";
            this.Text = "FormLoginNasabah";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnKeRegristrasi;
        private System.Windows.Forms.Button btnKeBeranda;
        public System.Windows.Forms.Button btnMasukNasabah;
    }
}