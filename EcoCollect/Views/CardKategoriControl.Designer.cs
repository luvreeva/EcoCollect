namespace EcoCollect.Views
{
    partial class CardKategoriControl
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
            this.picKategori = new System.Windows.Forms.PictureBox();
            this.lblNama = new System.Windows.Forms.Label();
            this.lblDeskripsi = new System.Windows.Forms.Label();
            this.lblHarga = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picKategori)).BeginInit();
            this.SuspendLayout();
            // 
            // picKategori
            // 
            this.picKategori.BackColor = System.Drawing.Color.Transparent;
            this.picKategori.Location = new System.Drawing.Point(12, 20);
            this.picKategori.Name = "picKategori";
            this.picKategori.Size = new System.Drawing.Size(55, 55);
            this.picKategori.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picKategori.TabIndex = 0;
            this.picKategori.TabStop = false;
            // 
            // lblNama
            // 
            this.lblNama.BackColor = System.Drawing.Color.Transparent;
            this.lblNama.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNama.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(96)))));
            this.lblNama.Location = new System.Drawing.Point(80, 20);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(145, 22);
            this.lblNama.TabIndex = 1;
            this.lblNama.Text = "label1";
            // 
            // lblDeskripsi
            // 
            this.lblDeskripsi.BackColor = System.Drawing.Color.Transparent;
            this.lblDeskripsi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDeskripsi.ForeColor = System.Drawing.Color.Gray;
            this.lblDeskripsi.Location = new System.Drawing.Point(80, 46);
            this.lblDeskripsi.Name = "lblDeskripsi";
            this.lblDeskripsi.Size = new System.Drawing.Size(145, 35);
            this.lblDeskripsi.TabIndex = 2;
            this.lblDeskripsi.Text = "label1";
            // 
            // lblHarga
            // 
            this.lblHarga.BackColor = System.Drawing.Color.Transparent;
            this.lblHarga.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHarga.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(96)))));
            this.lblHarga.Location = new System.Drawing.Point(235, 27);
            this.lblHarga.Name = "lblHarga";
            this.lblHarga.Size = new System.Drawing.Size(75, 42);
            this.lblHarga.TabIndex = 3;
            this.lblHarga.Text = "label1";
            this.lblHarga.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.White;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(375, 32);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(45, 30);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.Color.White;
            this.btnHapus.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHapus.Location = new System.Drawing.Point(315, 32);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(55, 30);
            this.btnHapus.TabIndex = 5;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = false;
            // 
            // CardKategoriControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblHarga);
            this.Controls.Add(this.lblDeskripsi);
            this.Controls.Add(this.lblNama);
            this.Controls.Add(this.picKategori);
            this.Name = "CardKategoriControl";
            this.Size = new System.Drawing.Size(430, 95);
            ((System.ComponentModel.ISupportInitialize)(this.picKategori)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picKategori;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.Label lblHarga;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnHapus;
    }
}
