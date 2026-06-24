using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EcoCollect.Config;
using EcoCollect.Helpers;
using Npgsql;

namespace EcoCollect.Views
{
    public partial class LayananPenyetoran : Form
    {
        private int _idNasabah;
        private DataTable dtKeranjang;
        public LayananPenyetoran(int idNasabah)
        {
            InitializeComponent();
            this._idNasabah = idNasabah;
        }

        private void LayananPenyetoran_Load(object sender, EventArgs e)
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

            dgvKeranjang.Columns["Harga / Kg (Rp)"].DefaultCellStyle.Format = "N0";
            dgvKeranjang.Columns["Subtotal (Rp)"].DefaultCellStyle.Format = "N0";
        }
        private void LoadKategoriSampah()
        {
            try
            {
                using (NpgsqlConnection conn = DbConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id_kategori, nama_jenis FROM kategori_sampah WHERE is_aktif = TRUE";
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbKategori.DataSource = dt;
                        cmbKategori.DisplayMember = "nama_jenis";
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
            if (!double.TryParse(txtBerat.Text, out double berat))
            {
                MessageBox.Show("Input berat harus berupa angka valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataRowView selectedRow = (DataRowView)cmbKategori.SelectedItem;
            double hargaPerKg = Convert.ToDouble(selectedRow["harga_per_kg"]);

            int idKategori = Convert.ToInt32(cmbKategori.SelectedValue);
            string jenisSampah = cmbKategori.Text;
            double TotalBerat = Convert.ToDouble(txtBerat.Text);

            // Hitung subtotal kasaran (Misal Rp 3.000 / Kg, sesuaikan harga databasemu jika ada)
            double subtotal = berat * hargaPerKg;

            bool itemSudahAda = false;
            foreach (DataRow row in dtKeranjang.Rows)
            {
                if (Convert.ToInt32(row["ID Kategori"]) == idKategori)
                {
                    double beratLama = Convert.ToDouble(row["Berat (Kg)"]);
                    row["Berat (Kg)"] = beratLama + berat;
                    row["Subtotal (Rp)"] = Convert.ToDouble(row["Berat (Kg)"]) * hargaPerKg;
                    itemSudahAda = true;
                    break;
                }
            }

            // Tambahkan baris ke grid
            if (!itemSudahAda)
            {
                dtKeranjang.Rows.Add(idKategori, jenisSampah, hargaPerKg, berat, subtotal);
            }

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
                using (NpgsqlConnection conn = DbConnection.GetConnection())
                {
                    double totalHarga = 0;

                    foreach (DataRow row in dtKeranjang.Rows)
                    {
                        totalHarga += Convert.ToDouble(row["Subtotal (Rp)"]);
                    }

                    conn.Open();
                    string queryTx = @"INSERT INTO transaksi_setor (id_nasabah, id_petugas, kode_transaksi, tanggal, total_nilai)
                                        VALUES
                                        (@idNasabah, @idPetugas, @kode, @tanggal, @total)
                                        RETURNING id_setor";

                    int idSetorBaru = 0;
                    string kodeOtomatis = "TX-" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryTx, conn))
                    {
                        cmd.Parameters.AddWithValue("@idNasabah", _idNasabah);
                        cmd.Parameters.AddWithValue("@idPetugas", Session.IdPetugas);
                        cmd.Parameters.AddWithValue("@kode", kodeOtomatis);
                        cmd.Parameters.AddWithValue("@tanggal", DateTime.Now);
                        cmd.Parameters.AddWithValue("@total", totalHarga);
                        idSetorBaru = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    string queryDetail = @"INSERT INTO detail_setor 
                                            (id_setor, id_kategori, berat_kg, harga_saat_transaksi, subtotal)
                                            VALUES 
                                            (@idSetor, @idKategori, @berat, @harga, @subtotal)";

                    foreach (DataRow row in dtKeranjang.Rows)
                    {
                        decimal hargaPerKg = 0;

                        string qHarga = "SELECT harga_per_kg FROM kategori_sampah WHERE id_kategori = @id";
                        using (var cmdHarga = new NpgsqlCommand(qHarga, conn))
                        {
                            cmdHarga.Parameters.AddWithValue("@id", Convert.ToInt32(row["ID Kategori"]));
                            hargaPerKg = Convert.ToDecimal(cmdHarga.ExecuteScalar());
                        }

                        using (NpgsqlCommand cmdDetail = new NpgsqlCommand(queryDetail, conn))
                        {
                            cmdDetail.Parameters.AddWithValue("@idSetor", idSetorBaru);
                            cmdDetail.Parameters.AddWithValue("@idKategori", Convert.ToInt32(row["ID Kategori"]));
                            cmdDetail.Parameters.AddWithValue("@berat", Convert.ToDouble(row["Berat (Kg)"]));
                            cmdDetail.Parameters.AddWithValue("@subtotal", Convert.ToDouble(row["Subtotal (Rp)"]));
                            cmdDetail.Parameters.AddWithValue("@harga", hargaPerKg);

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

        private void lblTotalBerat_Click(object sender, EventArgs e)
        {

        }
        private void btnRiwayatSetorSampah_Click(object sender, EventArgs e)
        {
            FormRiwayatSetorSampah frm = new FormRiwayatSetorSampah();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            this.Hide();
        }

        private void btnRiwayatDashboardPetugas_Click(object sender, EventArgs e)
        {
            FormRiwayatSetorSampah frm = new FormRiwayatSetorSampah();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            this.Hide();
        }

        private void btnKelolaJenisSampah_Click(object sender, EventArgs e)
        {
            KelolaJenisSampah form = new KelolaJenisSampah();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();

            this.Hide();
        }

        private void btnSetorSampah_Click(object sender, EventArgs e)
        {
            LayananPenyetoran form = new LayananPenyetoran(Session.IdNasabah);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();

            this.Hide();
        }

        private void btnSetorDashboardPetugas_Click(object sender, EventArgs e)
        {
            LayananPenyetoran form = new LayananPenyetoran(Session.IdNasabah);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();

            this.Hide();
        }

        private void btnKelolaSampahDashboardPetugas_Click(object sender, EventArgs e)
        {
            KelolaJenisSampah form = new KelolaJenisSampah();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();

            this.Hide();
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
