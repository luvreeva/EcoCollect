namespace EcoCollect.Views
{
    partial class ItemSampah
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
            this.lblNamaSampah = new System.Windows.Forms.Label();
            this.lblDeskripsi = new System.Windows.Forms.Label();
            this.lblHarga = new System.Windows.Forms.Label();
            this.btnHapusItem = new System.Windows.Forms.Button();
            this.btnEditItem = new System.Windows.Forms.Button();
            this.pbThumbnail = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNamaSampah
            // 
            this.lblNamaSampah.AutoSize = true;
            this.lblNamaSampah.BackColor = System.Drawing.Color.Transparent;
            this.lblNamaSampah.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaSampah.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(113)))), ((int)(((byte)(121)))));
            this.lblNamaSampah.Location = new System.Drawing.Point(57, 7);
            this.lblNamaSampah.Name = "lblNamaSampah";
            this.lblNamaSampah.Size = new System.Drawing.Size(0, 12);
            this.lblNamaSampah.TabIndex = 1;
            this.lblNamaSampah.Click += new System.EventHandler(this.lblNamaSampah_Click);
            // 
            // lblDeskripsi
            // 
            this.lblDeskripsi.AutoEllipsis = true;
            this.lblDeskripsi.BackColor = System.Drawing.Color.Transparent;
            this.lblDeskripsi.Font = new System.Drawing.Font("Segoe UI", 4.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeskripsi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(113)))), ((int)(((byte)(121)))));
            this.lblDeskripsi.Location = new System.Drawing.Point(57, 23);
            this.lblDeskripsi.Name = "lblDeskripsi";
            this.lblDeskripsi.Size = new System.Drawing.Size(105, 18);
            this.lblDeskripsi.TabIndex = 2;
            this.lblDeskripsi.Click += new System.EventHandler(this.lblDeskripsi_Click);
            // 
            // lblHarga
            // 
            this.lblHarga.BackColor = System.Drawing.Color.Transparent;
            this.lblHarga.Font = new System.Drawing.Font("Segoe UI", 4.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHarga.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(113)))), ((int)(((byte)(121)))));
            this.lblHarga.Location = new System.Drawing.Point(171, 8);
            this.lblHarga.Name = "lblHarga";
            this.lblHarga.Size = new System.Drawing.Size(46, 33);
            this.lblHarga.TabIndex = 3;
            this.lblHarga.Text = "Rp 0";
            this.lblHarga.Click += new System.EventHandler(this.lblHarga_Click);
            // 
            // btnHapusItem
            // 
            this.btnHapusItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHapusItem.BackColor = System.Drawing.Color.Transparent;
            this.btnHapusItem.FlatAppearance.BorderSize = 0;
            this.btnHapusItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapusItem.Font = new System.Drawing.Font("Segoe UI", 4.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHapusItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHapusItem.Location = new System.Drawing.Point(223, 10);
            this.btnHapusItem.Name = "btnHapusItem";
            this.btnHapusItem.Size = new System.Drawing.Size(42, 23);
            this.btnHapusItem.TabIndex = 4;
            this.btnHapusItem.Text = "Hapus";
            this.btnHapusItem.UseVisualStyleBackColor = false;
            this.btnHapusItem.Click += new System.EventHandler(this.btnHapusItem_Click);
            // 
            // btnEditItem
            // 
            this.btnEditItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditItem.BackColor = System.Drawing.Color.Transparent;
            this.btnEditItem.FlatAppearance.BorderSize = 0;
            this.btnEditItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditItem.Font = new System.Drawing.Font("Segoe UI", 4.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEditItem.Location = new System.Drawing.Point(269, 10);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Size = new System.Drawing.Size(44, 23);
            this.btnEditItem.TabIndex = 5;
            this.btnEditItem.Text = "Edit";
            this.btnEditItem.UseVisualStyleBackColor = false;
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // pbThumbnail
            // 
            this.pbThumbnail.BackColor = System.Drawing.Color.Gray;
            this.pbThumbnail.Location = new System.Drawing.Point(5, 7);
            this.pbThumbnail.Name = "pbThumbnail";
            this.pbThumbnail.Size = new System.Drawing.Size(38, 34);
            this.pbThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbThumbnail.TabIndex = 0;
            this.pbThumbnail.TabStop = false;
            this.pbThumbnail.Click += new System.EventHandler(this.pbThumbnail_Click);
            // 
            // ItemSampah
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnEditItem);
            this.Controls.Add(this.btnHapusItem);
            this.Controls.Add(this.lblHarga);
            this.Controls.Add(this.lblDeskripsi);
            this.Controls.Add(this.lblNamaSampah);
            this.Controls.Add(this.pbThumbnail);
            this.DoubleBuffered = true;
            this.Name = "ItemSampah";
            this.Size = new System.Drawing.Size(318, 46);
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbThumbnail;
        private System.Windows.Forms.Label lblNamaSampah;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.Label lblHarga;
        private System.Windows.Forms.Button btnHapusItem;
        private System.Windows.Forms.Button btnEditItem;
    }
}
