using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using EcoCollect.Models;

namespace EcoCollect.Views
{
    public partial class CardKategoriControl : UserControl
    {
        private readonly CultureInfo cultureId = new CultureInfo("id-ID");

        public KategoriSampahModel Kategori { get; private set; }

        public event Action<KategoriSampahModel> EditClicked;
        public event Action<KategoriSampahModel> HapusClicked;

        public CardKategoriControl()
        {
            InitializeComponent();

            this.Height = 95;
            this.MinimumSize = new Size(380, 95);

            btnHapus.Enabled = true;
            btnHapus.Visible = true;
            btnHapus.Cursor = Cursors.Hand;
            btnHapus.BringToFront();

            btnEdit.Enabled = true;
            btnEdit.Visible = true;
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.BringToFront();

            btnHapus.Click -= BtnHapus_Click;
            btnHapus.Click += BtnHapus_Click;

            btnEdit.Click -= BtnEdit_Click;
            btnEdit.Click += BtnEdit_Click;
        }

        public void SetData(KategoriSampahModel kategori)
        {
            Kategori = kategori;

            lblNama.Text = string.IsNullOrWhiteSpace(kategori.NamaJenis)
                ? "-"
                : kategori.NamaJenis;

            lblDeskripsi.Text = string.IsNullOrWhiteSpace(kategori.Deskripsi)
                ? "-"
                : kategori.Deskripsi;

            lblHarga.Text = kategori.HargaPerKg.ToString("C0", cultureId) + "\n/ kg";

            LoadGambarKategori(kategori.FotoThumbnail);

            picKategori.BringToFront();
            lblNama.BringToFront();
            lblDeskripsi.BringToFront();
            lblHarga.BringToFront();
            btnHapus.BringToFront();
            btnEdit.BringToFront();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (Kategori == null)
            {
                MessageBox.Show("Data kategori belum masuk ke card.");
                return;
            }

            EditClicked?.Invoke(Kategori);
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (Kategori == null)
            {
                MessageBox.Show("Data kategori belum masuk ke card.");
                return;
            }

            HapusClicked?.Invoke(Kategori);
        }

        private void LoadGambarKategori(string fotoThumbnail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fotoThumbnail))
                {
                    picKategori.Image = null;
                    return;
                }

                if (fotoThumbnail.StartsWith("http://") || fotoThumbnail.StartsWith("https://"))
                {
                    picKategori.LoadAsync(fotoThumbnail);
                    return;
                }

                string pathGambar = Path.Combine(Application.StartupPath, "Resources", fotoThumbnail);

                if (File.Exists(pathGambar))
                {
                    picKategori.Image = Image.FromFile(pathGambar);
                }
                else
                {
                    picKategori.Image = null;
                }
            }
            catch
            {
                picKategori.Image = null;
            }
        }
    }
}