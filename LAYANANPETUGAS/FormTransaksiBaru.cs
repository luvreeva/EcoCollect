using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace LAYANANPETUGAS
{
    public partial class FormTransaksiBaru : Form
    {
        private int _idNasabah;
        private DataTable dtKeranjang;
        public FormTransaksiBaru(int idNasabah)
        {
            InitializeComponent();
            this._idNasabah = idNasabah;
        }

        private void FormTransaksiBaru_Load(object sender, EventArgs e)
        {
            LoadKategoriSampah();
            BuatStrukturTabel();
        }
        private void BuatStrukturTabel()
        {
            dtKeranjang = new DataTable();
            dtKeranjang.Columns.Add("ID Kategori", typeof(int));
            dtKeranjang.Columns.Add("Jenis Sampah", typeof(string));
            dtKeranjang.Columns.Add("Berat (Kg)", typeof(double));
            dtKeranjang.Columns.Add("Subtotal (Rp)", typeof(double));

            dgvKeranjang.DataSource = dtKeranjang;
            dgvKeranjang.Columns["ID Kategori"].Visible = false; // Sembunyikan ID rahasia
        }
        private void LoadKategoriSampah()
        {
            try
            {
                using (NpgsqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id_kategori, jenis_sampah FROM kategori_sampah";
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbKategori.DataSource = dt;
                        cmbKategori.DisplayMember = "jenis_sampah";
                        cmbKategori.ValueMember = "id_kategori";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat kategori: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBerat.Text))
            {
                MessageBox.Show("Isi berat sampah dulu, bro!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idKategori = Convert.ToInt32(cmbKategori.SelectedValue);
            string jenisSampah = cmbKategori.Text;
            double berat = Convert.ToDouble(txtBerat.Text);

            // Hitung subtotal kasaran (Misal Rp 3.000 / Kg, sesuaikan harga databasemu jika ada)
            double subtotal = berat * 3000;

            // Tambahkan baris ke grid
            dtKeranjang.Rows.Add(idKategori, jenisSampah, berat, subtotal);

            // Hitung ulang total akumulasi di bawah tabel
            HitungUlangTotal();

            // Reset input berat biar bisa ngetik sampah berikutnya
            txtBerat.Clear();
            txtBerat.Focus();
        }
        private void HitungUlangTotal()
        {
            double totalBerat = 0;
            double totalHarga = 0;

            foreach (DataRow row in dtKeranjang.Rows)
            {
                totalBerat += Convert.ToDouble(row["Berat (Kg)"]);
                totalHarga += Convert.ToDouble(row["Subtotal (Rp)"]);
            }

            lblTotalBerat.Text = $"Total Berat: {totalBerat} Kg";
            lblTotalHarga.Text = $"Total Harga: Rp {totalHarga:N0}";
        }
        private void btnSimpanUtama_Click(object sender, EventArgs e)
        {
            if (dtKeranjang.Rows.Count == 0)
            {
                MessageBox.Show("Keranjang masih kosong, tambahkan sampah dulu, bro!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (NpgsqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    string queryTx = @"INSERT INTO transaksi_setor (id_nasabah, kode_transaksi, tanggal) 
                                      VALUES (@idNasabah, @kode, @tanggal) RETURNING id_setor";

                    int idSetorBaru = 0;
                    string kodeOtomatis = "TX-" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryTx, conn))
                    {
                        cmd.Parameters.AddWithValue("@idNasabah", _idNasabah);
                        cmd.Parameters.AddWithValue("@kode", kodeOtomatis);
                        cmd.Parameters.AddWithValue("@tanggal", DateTime.Now);
                        idSetorBaru = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    string queryDetail = @"INSERT INTO detail_setor (id_setor, id_kategori, berat_kg, subtotal) 
                                          VALUES (@idSetor, @idKategori, @berat, @subtotal)";

                    foreach (DataRow row in dtKeranjang.Rows)
                    {
                        using (NpgsqlCommand cmdDetail = new NpgsqlCommand(queryDetail, conn))
                        {
                            cmdDetail.Parameters.AddWithValue("@idSetor", idSetorBaru);
                            cmdDetail.Parameters.AddWithValue("@idKategori", Convert.ToInt32(row["ID Kategori"]));
                            cmdDetail.Parameters.AddWithValue("@berat", Convert.ToDouble(row["Berat (Kg)"]));
                            cmdDetail.Parameters.AddWithValue("@subtotal", Convert.ToDouble(row["Subtotal (Rp)"]));

                            cmdDetail.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Semua daftar setoran sukses disimpan ke database!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan transaksi massal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
