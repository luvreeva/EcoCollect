using EcoCollect.Controllers;
using EcoCollect.Helpers;
using EcoCollect.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

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
            flpKategoriSampah.Padding = new Padding(0);

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
                    CardKategoriControl card = new CardKategoriControl();

                    card.Width = flpKategoriSampah.ClientSize.Width - 25;

                    if (card.Width < 380)
                        card.Width = 380;

                    card.Margin = new Padding(5, 5, 5, 10);

                    card.SetData(kategori);

                    card.EditClicked -= CardKategori_EditClicked;
                    card.EditClicked += CardKategori_EditClicked;

                    card.HapusClicked -= CardKategori_HapusClicked;
                    card.HapusClicked += CardKategori_HapusClicked;

                    flpKategoriSampah.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan kategori sampah: " + ex.Message);
            }
        }

        private void CardKategori_EditClicked(KategoriSampahModel kategori)
        {
            if (kategori == null)
                return;

            idKategoriDipilih = kategori.IdKategori;

            txtNamaJenis.Text = kategori.NamaJenis;
            txtHargaPerKg.Text = kategori.HargaPerKg.ToString("N0", cultureId);
            txtUrlThumbnail.Text = kategori.FotoThumbnail;
            txtDeskripsi.Text = kategori.Deskripsi;
        }

        private void CardKategori_HapusClicked(KategoriSampahModel kategori)
        {
            if (kategori == null)
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
                MessageBoxIcon.Question
            );

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