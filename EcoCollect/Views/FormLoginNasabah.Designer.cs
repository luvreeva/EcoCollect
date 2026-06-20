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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(257, 180);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(375, 23);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Cambria", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(257, 251);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(375, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnMasukNasabah
            // 
            this.btnMasukNasabah.BackColor = System.Drawing.Color.Transparent;
            this.btnMasukNasabah.FlatAppearance.BorderSize = 0;
            this.btnMasukNasabah.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMasukNasabah.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMasukNasabah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasukNasabah.Location = new System.Drawing.Point(352, 296);
            this.btnMasukNasabah.Name = "btnMasukNasabah";
            this.btnMasukNasabah.Size = new System.Drawing.Size(183, 30);
            this.btnMasukNasabah.TabIndex = 2;
            this.btnMasukNasabah.UseVisualStyleBackColor = false;
            this.btnMasukNasabah.Click += new System.EventHandler(this.btnMasuk_Click);
            // 
            // btnKeRegristrasi
            // 
            this.btnKeRegristrasi.BackColor = System.Drawing.Color.Transparent;
            this.btnKeRegristrasi.FlatAppearance.BorderSize = 0;
            this.btnKeRegristrasi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnKeRegristrasi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnKeRegristrasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeRegristrasi.Location = new System.Drawing.Point(466, 332);
            this.btnKeRegristrasi.Name = "btnKeRegristrasi";
            this.btnKeRegristrasi.Size = new System.Drawing.Size(114, 23);
            this.btnKeRegristrasi.TabIndex = 3;
            this.btnKeRegristrasi.UseVisualStyleBackColor = false;
            this.btnKeRegristrasi.Click += new System.EventHandler(this.btnKeRegristrasi_Click);
            // 
            // btnKeBeranda
            // 
            this.btnKeBeranda.BackColor = System.Drawing.Color.Transparent;
            this.btnKeBeranda.FlatAppearance.BorderSize = 0;
            this.btnKeBeranda.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnKeBeranda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnKeBeranda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeBeranda.Location = new System.Drawing.Point(383, 361);
            this.btnKeBeranda.Name = "btnKeBeranda";
            this.btnKeBeranda.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnKeBeranda.Size = new System.Drawing.Size(152, 18);
            this.btnKeBeranda.TabIndex = 4;
            this.btnKeBeranda.UseVisualStyleBackColor = false;
            this.btnKeBeranda.Click += new System.EventHandler(this.btnKeBeranda_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.FlatAppearance.BorderSize = 0;
            this.checkBox1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.checkBox1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.checkBox1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Location = new System.Drawing.Point(584, 254);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(50, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Show";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // FormLoginNasabah
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(894, 474);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnKeBeranda);
            this.Controls.Add(this.btnKeRegristrasi);
            this.Controls.Add(this.btnMasukNasabah);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
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
        private System.Windows.Forms.CheckBox checkBox1;
    }
}