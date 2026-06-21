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

namespace LAYANANPETUGAS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadKategoriSampah();
        }
        public void LoadKategoriSampah()
        {
            flpKategoriSampah.Controls.Clear();
            string query = "SELECT nama_jenis, harga_per_kg, deskripsi, foto_thumbnail FROM kategori_sampah WHERE is_aktif = TRUE ORDER BY id_kategori DESC";

            try
            {
                using (NpgsqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ItemSampah item = new ItemSampah();

                                item.AutoSize = false;
                                int lebarPas = flpKategoriSampah.Width - 5;
                                item.Size = new Size(lebarPas, 30);
                                item.MinimumSize = new Size(lebarPas, 30);
                                item.MaximumSize = new Size(lebarPas, 30);
                                item.Margin = new Padding(0, 4, 0, 4);

                                item.NamaSampah = reader["nama_jenis"].ToString();
                                item.Deskripsi = reader["deskripsi"].ToString();

                                decimal harga = Convert.ToDecimal(reader["harga_per_kg"]);
                                item.Harga = $"Rp {harga:N0} / kg";

                                item.UrlGambar = reader["foto_thumbnail"].ToString();

                                flpKategoriSampah.Controls.Add(item);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data dari database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            string kataKunci = txtCari.Text.Trim();

            // 2. Bersihkan dulu list lama sebelum memfilter hasil baru
            flpKategoriSampah.Controls.Clear();

            // 3. Query pencarian menggunakan ILIKE (PostgreSQL) agar tidak sensitif huruf besar/kecil
            string query = "SELECT nama_jenis, harga_per_kg, deskripsi, foto_thumbnail FROM kategori_sampah " +
                           "WHERE is_aktif = TRUE AND nama_jenis ILIKE @keyword ORDER BY id_kategori DESC";

            try
            {
                using (NpgsqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@keyword", "%" + kataKunci + "%");

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ItemSampah item = new ItemSampah();

                                item.AutoSize = false;
                                int lebarPas = flpKategoriSampah.Width - 5;
                                item.Size = new Size(lebarPas, 30);
                                item.MinimumSize = new Size(lebarPas, 30);
                                item.MaximumSize = new Size(lebarPas, 30);
                                item.Margin = new Padding(0, 4, 0, 4);

                                item.NamaSampah = reader["nama_jenis"].ToString();
                                item.Deskripsi = reader["deskripsi"].ToString();

                                decimal harga = Convert.ToDecimal(reader["harga_per_kg"]);
                                item.Harga = $"Rp {harga:N0} / kg";

                                item.UrlGambar = reader["foto_thumbnail"].ToString();

                                flpKategoriSampah.Controls.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNamaSampah.Text) || string.IsNullOrEmpty(txtHargaKonversi.Text))
            {
                MessageBox.Show("Nama Kategori dan Harga wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "INSERT INTO kategori_sampah (nama_jenis, harga_per_kg, deskripsi, foto_thumbnail, is_aktif) " +
                           "VALUES (@nama, @harga, @deskripsi, @foto, TRUE)";

            try
            {
                using (NpgsqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", txtNamaSampah.Text.Trim());
                        cmd.Parameters.AddWithValue("@harga", Convert.ToDecimal(txtHargaKonversi.Text.Trim()));
                        cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text.Trim());
                        cmd.Parameters.AddWithValue("@foto", txtUrlThumbnail.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Kategori sampah baru berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNamaSampah.Clear();
                txtHargaKonversi.Clear();
                txtDeskripsi.Clear();
                txtUrlThumbnail.Clear();

                LoadKategoriSampah();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnLayananPenyetoran_Click(object sender, EventArgs e)
        {
            FormBuatSetoran formSetor = new FormBuatSetoran();
            formSetor.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}

