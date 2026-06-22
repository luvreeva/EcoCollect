using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoCollect.Views
{
    public partial class ItemSampah : UserControl
    {
        public int IdKategori { get; set; }
        public event EventHandler OnEditClicked;
        public event EventHandler OnHapusClicked;

        public string NamaSampah { get; set; }
        public string Deskripsi { get; set; }
        public string HargaKonversi { get; set; }
        public string UrlThumbnail { get; set; }

        public ItemSampah()
        {
            InitializeComponent();
        }

        public string Harga
        {
            get { return lblHarga.Text; }
            set { lblHarga.Text = value; }
        }

        private string _urlGambar;
        public string UrlGambar
        {
            get { return _urlGambar; }
            set
            {
                _urlGambar = value;

                string folderPath = Path.Combine(Application.StartupPath, "Images");
                string fullPath = Path.Combine(folderPath, _urlGambar ?? "");

                if (!string.IsNullOrEmpty(_urlGambar) && File.Exists(fullPath))
                {
                    pbThumbnail.Image = Image.FromFile(fullPath);
                }
                else
                {
                    pbThumbnail.Image = null; // Kosongkan jika file gambar tidak ditemukan
                }
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            OnEditClicked?.Invoke(this, e);
        }

        private void btnHapusItem_Click(object sender, EventArgs e)
        {
            OnHapusClicked?.Invoke(this, e);
        }

        private void pbThumbnail_Click(object sender, EventArgs e)
        {

        }

        private void lblDeskripsi_Click(object sender, EventArgs e)
        {

        }

        private void lblNamaSampah_Click(object sender, EventArgs e)
        {

        }

        private void lblHarga_Click(object sender, EventArgs e)
        {

        }
    }
}
