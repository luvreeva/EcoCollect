namespace EcoCollect.Views
{
    partial class CardNasabahControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNama = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPanah = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNama
            // 
            this.lblNama.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNama.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNama.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(96)))));
            this.lblNama.Location = new System.Drawing.Point(15, 10);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(200, 22);
            this.lblNama.TabIndex = 0;
            this.lblNama.Text = "label1";
            // 
            // lblUser
            // 
            this.lblUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblUser.ForeColor = System.Drawing.Color.Gray;
            this.lblUser.Location = new System.Drawing.Point(15, 33);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(200, 20);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "label1";
            // 
            // lblPanah
            // 
            this.lblPanah.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPanah.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.lblPanah.Location = new System.Drawing.Point(300, 8);
            this.lblPanah.Name = "lblPanah";
            this.lblPanah.Size = new System.Drawing.Size(35, 45);
            this.lblPanah.TabIndex = 2;
            this.lblPanah.Text = "›";
            // 
            // CardNasabahControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.lblPanah);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblNama);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "CardNasabahControl";
            this.Size = new System.Drawing.Size(340, 65);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPanah;
    }
}
