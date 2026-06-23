using EcoCollect.Controllers;
using EcoCollect.Helpers;
using EcoCollect.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            Panel card = new Panel();
            card.Width = flpKategoriSampah.ClientSize.Width - 25;
            card.Height = 85;
            card.BackColor = Color.FromArgb(220, 245, 247);
            card.Margin = new Padding(5, 5, 5, 8);
            card.Padding = new Padding(8);
            card.Tag = kategori;

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 5;
            layout.RowCount = 1;
            layout.BackColor = Color.Transparent;
            layout.Margin = new Padding(0);
            layout.Padding = new Padding(0);

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));   // icon/gambar
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));   // nama + deskripsi
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));   // harga
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));   // hapus
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));   // edit

            // ICON SEMENTARA, BIAR TIDAK MUNCUL X MERAH
            Label lblIcon = new Label();
            lblIcon.Text = "♻";
            lblIcon.Dock = DockStyle.Fill;
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;
            lblIcon.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblIcon.ForeColor = Color.FromArgb(0, 120, 140);
            lblIcon.BackColor = Color.Transparent;

            // PANEL NAMA + DESKRIPSI
            Panel panelInfo = new Panel();
            panelInfo.Dock = DockStyle.Fill;
            panelInfo.BackColor = Color.Transparent;

            Label lblNama = new Label();
            lblNama.Text = kategori.NamaJenis;
            lblNama.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblNama.ForeColor = Color.FromArgb(0, 84, 96);
            lblNama.Location = new Point(0, 12);
            lblNama.AutoSize = true;

            Label lblDeskripsi = new Label();
            lblDeskripsi.Text = string.IsNullOrWhiteSpace(kategori.Deskripsi) ? "-" : kategori.Deskripsi;
            lblDeskripsi.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblDeskripsi.ForeColor = Color.Gray;
            lblDeskripsi.Location = new Point(0, 38);
            lblDeskripsi.Width = 180;
            lblDeskripsi.Height = 35;

            panelInfo.Controls.Add(lblNama);
            panelInfo.Controls.Add(lblDeskripsi);

            // HARGA
            Label lblHarga = new Label();
            lblHarga.Text = kategori.HargaPerKg.ToString("C0", cultureId) + "\n/ kg";
            lblHarga.Dock = DockStyle.Fill;
            lblHarga.TextAlign = ContentAlignment.MiddleCenter;
            lblHarga.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            lblHarga.ForeColor = Color.FromArgb(0, 84, 96);
            lblHarga.BackColor = Color.Transparent;

            // BUTTON HAPUS
            Button btnHapus = new Button();
            btnHapus.Text = "Hapus";
            btnHapus.Dock = DockStyle.None;
            btnHapus.Width = 55;
            btnHapus.Height = 26;
            btnHapus.Anchor = AnchorStyles.None;
            btnHapus.Tag = kategori;
            btnHapus.Cursor = Cursors.Hand;
            btnHapus.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            btnHapus.TextAlign = ContentAlignment.MiddleCenter;
            btnHapus.FlatStyle = FlatStyle.Flat;
            btnHapus.FlatAppearance.BorderSize = 0;
            btnHapus.BackColor = Color.White;
            btnHapus.ForeColor = Color.FromArgb(0, 84, 96);
            btnHapus.Click += BtnHapus_Click;

            // BUTTON EDIT
            Button btnEdit = new Button();
            btnEdit.Text = "Edit";
            btnEdit.Dock = DockStyle.None;
            btnEdit.Width = 55;
            btnEdit.Height = 26;
            btnEdit.Anchor = AnchorStyles.None;
            btnEdit.Tag = kategori;
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            btnEdit.TextAlign = ContentAlignment.MiddleCenter;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.BackColor = Color.White;
            btnEdit.ForeColor = Color.FromArgb(0, 84, 96);
            btnEdit.Click += BtnEdit_Click;

            layout.Controls.Add(lblIcon, 0, 0);
            layout.Controls.Add(panelInfo, 1, 0);
            layout.Controls.Add(lblHarga, 2, 0);
            layout.Controls.Add(btnHapus, 3, 0);
            layout.Controls.Add(btnEdit, 4, 0);

            card.Controls.Add(layout);

            return card;
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
    }
}