using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAYANANPETUGAS
{
    public partial class ItemSampah : UserControl
    {
        public ItemSampah()
        {
            InitializeComponent();
        }
        public string NamaSampah
        {
            get { return lblNamaSampah.Text; }
            set { lblNamaSampah.Text = value; }
        }

        public string Deskripsi
        {
            get { return lblDeskripsi.Text; }
            set { lblDeskripsi.Text = value; }
        }

        public string Harga
        {
            get { return lblHarga.Text; }
            set { lblHarga.Text = value; }
        }

        public string UrlGambar { get; set; }

        private void btnEditItem_Click(object sender, EventArgs e)
        {

        }

        private void btnHapusItem_Click(object sender, EventArgs e)
        {

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
