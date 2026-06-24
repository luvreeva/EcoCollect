using EcoCollect.Config;
using EcoCollect.Helpers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using EcoCollect.Views;

namespace EcoCollect.Views
{
    public partial class KelolaJenisSampah : Form
    {
        private bool _isEditMode = false;
        private int _idKategoriDipilih = 0;

        public KelolaJenisSampah()
        {
            InitializeComponent();
            this.Load += new EventHandler(KelolaJenisSampah_Load);
        }

        private void KelolaJenisSampah_Load(object sender, EventArgs e)
        {
            LoadKategoriSampah();
        }

        public void LoadKategoriSampah()
        {
            flpKategoriSampah.Controls.Clear();
            string query = "SELECT id_kategori, nama_jenis, harga_per_kg, deskripsi, foto_thumbnail " +
                           "FROM kategori_sampah WHERE is_aktif = TRUE ORDER BY id_kategori DESC";

            try
            {
                using (NpgsqlConnection conn = DbConnection.GetConnection())
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemSampah item = BuatItemSampah(reader);
                            flpKategoriSampah.Controls.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data dari database: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ FIX 1: Helper method untuk menghindari duplikasi kode pembuatan ItemSampah
        private ItemSampah BuatItemSampah(NpgsqlDataReader reader)
        {
            ItemSampah item = new ItemSampah();

            item.AutoSize = false;
            int lebarPas = flpKategoriSampah.Width - 5;
            item.Size = new Size(lebarPas, 30);
            item.MinimumSize = new Size(lebarPas, 30);
            item.MaximumSize = new Size(lebarPas, 30);
            item.Margin = new Padding(0, 4, 0, 4);

            item.IdKategori = Convert.ToInt32(reader["id_kategori"]);
            item.NamaSampah = reader["nama_jenis"].ToString();
            item.Deskripsi = reader["deskripsi"].ToString();

            decimal harga = Convert.ToDecimal(reader["harga_per_kg"]);
            item.Harga = $"Rp {harga:N0} / kg";
            item.UrlGambar = reader["foto_thumbnail"].ToString();

            item.OnEditClicked += ItemSampah_OnEditClicked;
            item.OnHapusClicked += ItemSampah_OnHapusClicked;

            return item;
        }

        private void ItemSampah_OnEditClicked(object sender, EventArgs e)
        {
            ItemSampah item = (ItemSampah)sender;

            _isEditMode = true;
            _idKategoriDipilih = item.IdKategori;

            txtNamaSampah.Text = item.NamaSampah;
            txtDeskripsi.Text = item.Deskripsi;
            txtUrlThumbnail.Text = item.UrlGambar;

            string hargaMurni = item.Harga
                .Replace("Rp ", "")
                .Replace(" / kg", "")
                .Replace(".", "")
                .Trim();
            txtHargaKonversi.Text = hargaMurni;

            btnSimpan.Text = "Update Data";
            txtNamaSampah.Focus();
        }

        // ✅ FIX 2: Hapus duplikasi — hanya ada SATU method OnHapusClicked
        private void ItemSampah_OnHapusClicked(object sender, EventArgs e)
        {
            ItemSampah item = (ItemSampah)sender;

            DialogResult konfirmasi = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus kategori '{item.NamaSampah}'?",
                "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (konfirmasi == DialogResult.Yes)
            {
                string query = "UPDATE kategori_sampah SET is_aktif = FALSE WHERE id_kategori = @id";

                try
                {
                    using (NpgsqlConnection conn = DbConnection.GetConnection())
                    {
                        conn.Open();
                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", item.IdKategori);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Kategori sampah berhasil dihapus!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetFormState();
                    LoadKategoriSampah();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus data: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            string kataKunci = txtCari.Text.Trim();
            flpKategoriSampah.Controls.Clear();

            string query = "SELECT id_kategori, nama_jenis, harga_per_kg, deskripsi, foto_thumbnail " +
                           "FROM kategori_sampah WHERE is_aktif = TRUE AND nama_jenis ILIKE @keyword " +
                           "ORDER BY id_kategori DESC";

            try
            {
                using (NpgsqlConnection conn = DbConnection.GetConnection())
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@keyword", "%" + kataKunci + "%");

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ItemSampah item = BuatItemSampah(reader);
                                flpKategoriSampah.Controls.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error search: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaSampah.Text) ||
                string.IsNullOrWhiteSpace(txtHargaKonversi.Text))
            {
                MessageBox.Show("Nama Kategori dan Harga wajib diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ FIX 3: Tidak ada re-declaration 'string query' di dalam blok if/else
            string query;

            if (_isEditMode)
            {
                query = @"UPDATE kategori_sampah 
                          SET nama_jenis = @nama, harga_per_kg = @harga, 
                              deskripsi = @deskripsi, foto_thumbnail = @foto 
                          WHERE id_kategori = @id";
            }
            else
            {
                query = "INSERT INTO kategori_sampah (nama_jenis, harga_per_kg, deskripsi, foto_thumbnail, is_aktif) " +
                        "VALUES (@nama, @harga, @deskripsi, @foto, TRUE)";
            }

            try
            {
                using (NpgsqlConnection conn = DbConnection.GetConnection())
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", txtNamaSampah.Text.Trim());
                        cmd.Parameters.AddWithValue("@harga", Convert.ToDecimal(txtHargaKonversi.Text.Trim()));
                        cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text.Trim());
                        cmd.Parameters.AddWithValue("@foto", txtUrlThumbnail.Text.Trim());

                        // ✅ FIX 4: Tambahkan parameter @id saat mode edit
                        if (_isEditMode)
                        {
                            cmd.Parameters.AddWithValue("@id", _idKategoriDipilih);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                string pesanSukses = _isEditMode
                    ? "Kategori sampah berhasil diupdate!"
                    : "Kategori sampah baru berhasil disimpan!";

                MessageBox.Show(pesanSukses, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetFormState();
                LoadKategoriSampah();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ FIX 5: Implementasi ResetFormState yang hilang
        private void ResetFormState()
        {
            _isEditMode = false;
            _idKategoriDipilih = 0;

            txtNamaSampah.Clear();
            txtHargaKonversi.Clear();
            txtDeskripsi.Clear();
            txtUrlThumbnail.Clear();

            btnSimpan.Text = "Simpan Data";
        }

        private void btnLayananPenyetoran_Click(object sender, EventArgs e)
        {
            FormBuatSetoran formSetor = new FormBuatSetoran();
            formSetor.StartPosition = FormStartPosition.CenterScreen;
            formSetor.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) { }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FormDashboardPetugas dashboard = new FormDashboardPetugas();
            dashboard.StartPosition = FormStartPosition.CenterScreen;
            dashboard.Show();
            this.Close();
        }

        private void btnRiwayatSampah_Click(object sender, EventArgs e)
        {
            FormDashboardPetugas dashboard = Application.OpenForms["FormDashboardPetugas"] as FormDashboardPetugas;
            FormRiwayatSetorSampah form = new FormRiwayatSetorSampah();
            form.Show();
            this.Close();
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
                Session.ClearPetugas();

                FormLoginPetugas login = new FormLoginPetugas();
                login.Show();
                this.Close();
            }
        }
    }
}