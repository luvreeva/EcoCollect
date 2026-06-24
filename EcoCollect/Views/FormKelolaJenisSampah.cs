using EcoCollect.Controllers;
using EcoCollect.Helpers;
using EcoCollect.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.IO;

namespace EcoCollect.Views
{
    public partial class FormKelolaJenisSampah : Form
    {
        private readonly KategoriSampahController kategoriController = new KategoriSampahController();

        private int idKategoriDipilih = 0;
        private readonly CultureInfo cultureId = new CultureInfo("id-ID");

        public FormKelolaJenisSampah()
        {
            InitializeComponent();
        }

        private void FormKelolaJenisSampah_Load(object sender, EventArgs e)
        {
            flpKategoriSampah.AutoScroll = true;
            flpKategoriSampah.FlowDirection = FlowDirection.TopDown;
            flpKategoriSampah.WrapContents = false;

            ResetForm();
            LoadKategori();
        }

        private void txtCariKategori_TextChanged(object sender, EventArgs e)
        {
            LoadKategori(txtCariKategori.Text.Trim());
        }

        private void LoadKategori(string keyword = "")
        {
            flpKategoriSampah.Controls.Clear();

            try
            {
                List<KategoriSampahModel> daftarKategori = kategoriController.GetAllKategori(keyword);

                foreach (KategoriSampahModel kategori in daftarKategori)
                {
                    Panel card = BuatCardKategori(kategori);
                    flpKategoriSampah.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan kategori sampah: " + ex.Message);
            }
        }

        private Panel BuatCardKategori(KategoriSampahModel kategori)
        {
            int cardWidth = flpKategoriSampah.ClientSize.Width - 35;

            if (cardWidth < 380)
                cardWidth = 380;

            Panel card = new Panel();
            card.Width = cardWidth;
            card.Height = 95;
            card.BackColor = Color.FromArgb(220, 245, 247);
            card.Margin = new Padding(5, 5, 5, 10);
            card.Tag = kategori;

            int marginLeft = 12;
            int gambarWidth = 55;

            int buttonWidth = 58;
            int buttonHeight = 30;
            int gap = 8;

            int xEdit = card.Width - buttonWidth - 12;
            int xHapus = xEdit - buttonWidth - gap;

            int hargaWidth = 70;
            int xHarga = xHapus - hargaWidth - gap;

            int xInfo = marginLeft + gambarWidth + 12;
            int infoWidth = xHarga - xInfo - 10;

            if (infoWidth < 110)
                infoWidth = 110;

            PictureBox pic = new PictureBox();
            pic.Width = gambarWidth;
            pic.Height = 55;
            pic.Location = new Point(marginLeft, 20);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.BackColor = Color.Transparent;
            LoadGambarKategori(pic, kategori.FotoThumbnail);

            Label lblNama = new Label();
            lblNama.Text = string.IsNullOrWhiteSpace(kategori.NamaJenis) ? "-" : kategori.NamaJenis;
            lblNama.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblNama.ForeColor = Color.FromArgb(0, 84, 96);
            lblNama.BackColor = Color.Transparent;
            lblNama.Location = new Point(xInfo, 20);
            lblNama.Width = infoWidth;
            lblNama.Height = 22;
            lblNama.AutoSize = false;
            lblNama.TextAlign = ContentAlignment.MiddleLeft;

            Label lblDeskripsi = new Label();
            lblDeskripsi.Text = string.IsNullOrWhiteSpace(kategori.Deskripsi) ? "-" : kategori.Deskripsi;
            lblDeskripsi.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblDeskripsi.ForeColor = Color.Gray;
            lblDeskripsi.BackColor = Color.Transparent;
            lblDeskripsi.Location = new Point(xInfo, 46);
            lblDeskripsi.Width = infoWidth;
            lblDeskripsi.Height = 35;
            lblDeskripsi.AutoSize = false;
            lblDeskripsi.TextAlign = ContentAlignment.TopLeft;

            Label lblHarga = new Label();
            lblHarga.Text = kategori.HargaPerKg.ToString("C0", cultureId) + "\n/ kg";
            lblHarga.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            lblHarga.ForeColor = Color.FromArgb(0, 84, 96);
            lblHarga.BackColor = Color.Transparent;
            lblHarga.Location = new Point(xHarga, 27);
            lblHarga.Width = hargaWidth;
            lblHarga.Height = 42;
            lblHarga.AutoSize = false;
            lblHarga.TextAlign = ContentAlignment.MiddleCenter;

            Button btnHapus = new Button();
            btnHapus.Text = "Hapus";
            btnHapus.Width = buttonWidth;
            btnHapus.Height = buttonHeight;
            btnHapus.Location = new Point(xHapus, 32);
            btnHapus.Tag = kategori;
            btnHapus.Cursor = Cursors.Hand;
            btnHapus.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            btnHapus.TextAlign = ContentAlignment.MiddleCenter;
            btnHapus.BackColor = Color.White;
            btnHapus.ForeColor = Color.FromArgb(0, 84, 96);
            btnHapus.FlatStyle = FlatStyle.Flat;
            btnHapus.FlatAppearance.BorderSize = 0;
            btnHapus.Click += BtnHapus_Click;

            Button btnEdit = new Button();
            btnEdit.Text = "Edit";
            btnEdit.Width = buttonWidth;
            btnEdit.Height = buttonHeight;
            btnEdit.Location = new Point(xEdit, 32);
            btnEdit.Tag = kategori;
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            btnEdit.TextAlign = ContentAlignment.MiddleCenter;
            btnEdit.BackColor = Color.White;
            btnEdit.ForeColor = Color.FromArgb(0, 84, 96);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += BtnEdit_Click;

            card.Controls.Add(pic);
            card.Controls.Add(lblNama);
            card.Controls.Add(lblDeskripsi);
            card.Controls.Add(lblHarga);
            card.Controls.Add(btnHapus);
            card.Controls.Add(btnEdit);

            pic.BringToFront();
            lblNama.BringToFront();
            lblDeskripsi.BringToFront();
            lblHarga.BringToFront();
            btnHapus.BringToFront();
            btnEdit.BringToFront();

            return card;
        }

        private void LoadGambarKategori(PictureBox pic, string fotoThumbnail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fotoThumbnail))
                {
                    pic.Image = null;
                    return;
                }

                if (fotoThumbnail.StartsWith("http://") || fotoThumbnail.StartsWith("https://"))
                {
                    pic.LoadAsync(fotoThumbnail);
                    return;
                }

                string pathGambar = System.IO.Path.Combine(Application.StartupPath, "Resources", fotoThumbnail);

                if (System.IO.File.Exists(pathGambar))
                {
                    pic.Image = Image.FromFile(pathGambar);
                }
                else
                {
                    pic.Image = null;
                }
            }
            catch
            {
                pic.Image = null;
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == null || !(btn.Tag is KategoriSampahModel kategori))
                return;

            idKategoriDipilih = kategori.IdKategori;

            txtNamaJenis.Text = kategori.NamaJenis;
            txtHargaPerKg.Text = kategori.HargaPerKg.ToString("N0", cultureId);
            txtUrlThumbnail.Text = kategori.FotoThumbnail;
            txtDeskripsi.Text = kategori.Deskripsi;

            btnSimpanJenisSampah.Text = "Update Jenis Sampah";
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == null || !(btn.Tag is KategoriSampahModel kategori))
                return;

            DialogResult result = MessageBox.Show(
                "Yakin ingin menghapus kategori '" + kategori.NamaJenis + "'?",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                bool berhasil = kategoriController.HapusKategori(kategori.IdKategori);

                if (berhasil)
                {
                    MessageBox.Show("Kategori berhasil dihapus.");
                    ResetForm();
                    LoadKategori(txtCariKategori.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Kategori gagal dihapus.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus kategori: " + ex.Message);
            }
        }

        private void btnSimpanJenisSampah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaJenis.Text))
            {
                MessageBox.Show("Nama jenis sampah wajib diisi.");
                return;
            }

            if (!TryParseHarga(txtHargaPerKg.Text.Trim(), out decimal hargaPerKg))
            {
                MessageBox.Show("Harga per kg tidak valid.");
                return;
            }

            if (hargaPerKg <= 0)
            {
                MessageBox.Show("Harga per kg harus lebih dari 0.");
                return;
            }

            try
            {
                KategoriSampahModel kategori = new KategoriSampahModel
                {
                    IdKategori = idKategoriDipilih,
                    NamaJenis = txtNamaJenis.Text.Trim(),
                    HargaPerKg = hargaPerKg,
                    FotoThumbnail = txtUrlThumbnail.Text.Trim(),
                    Deskripsi = txtDeskripsi.Text.Trim()
                };

                bool berhasil;

                if (idKategoriDipilih == 0)
                {
                    berhasil = kategoriController.TambahKategori(kategori);
                }
                else
                {
                    berhasil = kategoriController.UpdateKategori(kategori);
                }

                if (berhasil)
                {
                    MessageBox.Show(idKategoriDipilih == 0
                        ? "Kategori sampah berhasil ditambahkan."
                        : "Kategori sampah berhasil diperbarui.");

                    ResetForm();
                    LoadKategori(txtCariKategori.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Data kategori gagal disimpan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan kategori: " + ex.Message);
            }
        }

        private bool TryParseHarga(string input, out decimal harga)
        {
            input = input.Replace("Rp", "")
                         .Replace("rp", "")
                         .Replace(".", "")
                         .Replace(" ", "")
                         .Trim();

            input = input.Replace(",", ".");

            return decimal.TryParse(
                input,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out harga
            );
        }

        private void ResetForm()
        {
            idKategoriDipilih = 0;

            txtNamaJenis.Clear();
            txtHargaPerKg.Clear();
            txtUrlThumbnail.Clear();
            txtDeskripsi.Clear();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FormDashboardPetugas form = new FormDashboardPetugas();
            form.Show();
            this.Close();
        }

        private void btnTambahJenis_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtNamaJenis.Focus();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Yakin ingin logout?",
        "Konfirmasi",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Session.IdPetugas = 0;
                Session.NamaPetugas = "";

                FormLoginPetugas login = new FormLoginPetugas();
                login.Show();
                this.Close();
            }
        }

        private void btnRiwayatSetorSampah_Click(object sender, EventArgs e)
        {
            FormRiwayatSetorSampah form = new FormRiwayatSetorSampah();
            form.Show();
            this.Close();
        }

        private void btnLayananPenyetoran_Click(object sender, EventArgs e)
        {
            FormBuatSetoran form = new FormBuatSetoran();
            form.Show();
            this.Close();
        }
    }
}