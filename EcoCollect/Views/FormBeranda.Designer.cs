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
            this.btnAkses = new System.Windows.Forms.Button();
            this.btnAksesN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAkses
            // 
            this.btnAkses.BackColor = System.Drawing.Color.Transparent;
            this.btnAkses.FlatAppearance.BorderSize = 0;
            this.btnAkses.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAkses.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAkses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAkses.Location = new System.Drawing.Point(456, 295);
            this.btnAkses.Name = "btnAkses";
            this.btnAkses.Size = new System.Drawing.Size(149, 16);
            this.btnAkses.TabIndex = 2;
            this.btnAkses.UseVisualStyleBackColor = false;
            this.btnAkses.Click += new System.EventHandler(this.btnAkses_Click);
            // 
            // btnAksesN
            // 
            this.btnAksesN.BackColor = System.Drawing.Color.Transparent;
            this.btnAksesN.FlatAppearance.BorderSize = 0;
            this.btnAksesN.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAksesN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAksesN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAksesN.Location = new System.Drawing.Point(456, 394);
            this.btnAksesN.Name = "btnAksesN";
            this.btnAksesN.Size = new System.Drawing.Size(149, 14);
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
            this.Name = "FormBeranda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormBeranda";
            this.Load += new System.EventHandler(this.FormBeranda_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAkses;
        private System.Windows.Forms.Button btnAksesN;
    }
}