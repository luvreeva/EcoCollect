namespace EcoCollect.Views
{
    partial class FormLoginPetugas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoginPetugas));
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnMasuk = new System.Windows.Forms.Button();
            this.btnKeBeranda = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(349, 221);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(501, 28);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(349, 306);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(501, 28);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnMasuk
            // 
            this.btnMasuk.BackColor = System.Drawing.Color.Transparent;
            this.btnMasuk.FlatAppearance.BorderSize = 0;
            this.btnMasuk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasuk.Location = new System.Drawing.Point(469, 364);
            this.btnMasuk.Margin = new System.Windows.Forms.Padding(4);
            this.btnMasuk.Name = "btnMasuk";
            this.btnMasuk.Size = new System.Drawing.Size(244, 34);
            this.btnMasuk.TabIndex = 2;
            this.btnMasuk.UseVisualStyleBackColor = false;
            this.btnMasuk.Click += new System.EventHandler(this.btnMasuk_Click);
            // 
            // btnKeBeranda
            // 
            this.btnKeBeranda.BackColor = System.Drawing.Color.Transparent;
            this.btnKeBeranda.FlatAppearance.BorderSize = 0;
            this.btnKeBeranda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeBeranda.Location = new System.Drawing.Point(493, 444);
            this.btnKeBeranda.Margin = new System.Windows.Forms.Padding(4);
            this.btnKeBeranda.Name = "btnKeBeranda";
            this.btnKeBeranda.Size = new System.Drawing.Size(200, 20);
            this.btnKeBeranda.TabIndex = 3;
            this.btnKeBeranda.UseVisualStyleBackColor = false;
            this.btnKeBeranda.Click += new System.EventHandler(this.btnKeBeranda_Click);
            // 
            // FormLoginPetugas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1192, 583);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnKeBeranda);
            this.Controls.Add(this.btnMasuk);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormLoginPetugas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLoginPetugas";
            this.Load += new System.EventHandler(this.FormLoginPetugas_Load);
            this.Resize += new System.EventHandler(this.FormLoginPetugas_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnMasuk;
        private System.Windows.Forms.Button btnKeBeranda;
    }
}