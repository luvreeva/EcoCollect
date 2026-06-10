namespace EcoCollect.Views
{
    partial class FormBeranda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBeranda));
            this.btnAksesPetugas = new System.Windows.Forms.Button();
            this.btnAksesNasabah = new System.Windows.Forms.Button();
            this.btnAkses = new System.Windows.Forms.Button();
            this.btnAksesN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAksesPetugas
            // 
            this.btnAksesPetugas.BackColor = System.Drawing.Color.Transparent;
            this.btnAksesPetugas.FlatAppearance.BorderSize = 0;
            this.btnAksesPetugas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAksesPetugas.Location = new System.Drawing.Point(456, 310);
            this.btnAksesPetugas.Name = "btnAksesPetugas";
            this.btnAksesPetugas.Size = new System.Drawing.Size(149, 17);
            this.btnAksesPetugas.TabIndex = 0;
            this.btnAksesPetugas.UseVisualStyleBackColor = false;
            this.btnAksesPetugas.Click += new System.EventHandler(this.btnAksesPetugas_Click);
            // 
            // btnAksesNasabah
            // 
            this.btnAksesNasabah.BackColor = System.Drawing.Color.Transparent;
            this.btnAksesNasabah.FlatAppearance.BorderSize = 0;
            this.btnAksesNasabah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAksesNasabah.Location = new System.Drawing.Point(456, 414);
            this.btnAksesNasabah.Name = "btnAksesNasabah";
            this.btnAksesNasabah.Size = new System.Drawing.Size(149, 16);
            this.btnAksesNasabah.TabIndex = 1;
            this.btnAksesNasabah.UseVisualStyleBackColor = false;
            // 
            // btnAkses
            // 
            this.btnAkses.BackColor = System.Drawing.Color.Transparent;
            this.btnAkses.FlatAppearance.BorderSize = 0;
            this.btnAkses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAkses.Location = new System.Drawing.Point(456, 295);
            this.btnAkses.Name = "btnAkses";
            this.btnAkses.Size = new System.Drawing.Size(149, 19);
            this.btnAkses.TabIndex = 2;
            this.btnAkses.UseVisualStyleBackColor = false;
            this.btnAkses.Click += new System.EventHandler(this.btnAkses_Click);
            // 
            // btnAksesN
            // 
            this.btnAksesN.BackColor = System.Drawing.Color.Transparent;
            this.btnAksesN.FlatAppearance.BorderSize = 0;
            this.btnAksesN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAksesN.Location = new System.Drawing.Point(456, 394);
            this.btnAksesN.Name = "btnAksesN";
            this.btnAksesN.Size = new System.Drawing.Size(149, 23);
            this.btnAksesN.TabIndex = 3;
            this.btnAksesN.UseVisualStyleBackColor = false;
            this.btnAksesN.Click += new System.EventHandler(this.btnAksesN_Click);
            // 
            // FormBeranda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(894, 474);
            this.Controls.Add(this.btnAksesN);
            this.Controls.Add(this.btnAkses);
            this.Controls.Add(this.btnAksesNasabah);
            this.Controls.Add(this.btnAksesPetugas);
            this.Name = "FormBeranda";
            this.Text = "FormBeranda";
            this.Load += new System.EventHandler(this.FormBeranda_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAksesPetugas;
        private System.Windows.Forms.Button btnAksesNasabah;
        private System.Windows.Forms.Button btnAkses;
        private System.Windows.Forms.Button btnAksesN;
    }
}