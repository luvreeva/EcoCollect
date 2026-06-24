using EcoCollect.Helpers;
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

namespace EcoCollect.Views
{
    public partial class FormBuatSetoran : Form
    {
        public FormBuatSetoran()
        {
            InitializeComponent();
        }
        public void UpdateStatistikDanHistori(int idNasabah)
        {
            try
            {
                using (NpgsqlConnection conn = DbConnection.GetConnection())
                {
                    conn.Open();
                    string queryStatistik = @"
                        SELECT 
                            COUNT(DISTINCT ts.id_setor) AS total_frekuensi,
                            COALESCE(SUM(ds.berat_kg), 0) AS total_massa
                        FROM transaksi_setor ts
                        LEFT JOIN detail_setor ds ON ts.id_setor = ds.id_setor
                        WHERE ts.id_nasabah = @idNasabah";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryStatistik, conn))
                    {
                        cmd.Parameters.AddWithValue("@idNasabah", idNasabah);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblFrekuensiSetor.Text = reader["total_frekuensi"].ToString() + " kali";

                                double totalMassa = Convert.ToDouble(reader["total_massa"]);
                                lblTotalMassa.Text = totalMassa.ToString("0.##") + " kg";
                            }
                        }
                    }
                    string queryHistori = @"
                        SELECT 
                            ts.kode_transaksi AS ""ID Transaksi"",
                            ks.nama_jenis AS ""Kategori"",
                            ds.berat_kg AS ""Berat (Kg)"",
                            ds.subtotal AS ""Nilai Rupiah""
                        FROM transaksi_setor ts
                        JOIN detail_setor ds ON ts.id_setor = ds.id_setor
                        JOIN kategori_sampah ks ON ds.id_kategori = ks.id_kategori
                        WHERE ts.id_nasabah = @idNasabah
                        ORDER BY ts.tanggal DESC";

                    using (NpgsqlCommand cmdHistori = new NpgsqlCommand(queryHistori, conn))
                    {
                        cmdHistori.Parameters.AddWithValue("@idNasabah", idNasabah);

                        DataTable dt = new DataTable();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmdHistori))
                        {
                            da.Fill(dt);
                        }
                        dgvHistoriSetoran.DataSource = dt;

                        if (dgvHistoriSetoran.Columns.Contains("Nilai Rupiah"))
                        {
                            dgvHistoriSetoran.Columns["Nilai Rupiah"].DefaultCellStyle.Format = "C0";
                            dgvHistoriSetoran.Columns["Nilai Rupiah"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("id-ID");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat statistik dan histori: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormBuatSetoran_Load(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection conn = DbConnection.GetConnection())
                {
                    conn.Open();

                    string queryListTengah = "SELECT id_nasabah, nama_lengkap, no_hp FROM nasabah";

                    using (NpgsqlCommand cmdTengah = new NpgsqlCommand(queryListTengah, conn))
                    {
                        using (NpgsqlDataReader readerTengah = cmdTengah.ExecuteReader())
                        {
                            if (readerTengah.Read())
                            {
                                int idNasabah = Convert.ToInt32(readerTengah["id_nasabah"]);

                                lblNamaNasabah1.Text = readerTengah["nama_lengkap"].ToString();
                                lblDetailNasabah1.Text = readerTengah["no_hp"].ToString();

                                panelNasabah1.Tag = idNasabah;
                                lblNamaNasabah1.Tag = idNasabah;
                                lblDetailNasabah1.Tag = idNasabah;
                            }
                        }
                    }
                }
                dgvHistoriSetoran.DataSource = null;
                lblFrekuensiSetor.Text = "0 kali";
                lblTotalMassa.Text = "0 kg";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat kartu nasabah tengah: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void panelNasabah1_Click(object sender, EventArgs e)
        {
            // Deteksi komponen mana yang diklik (bisa panelnya atau label tulisannya)
            Control komponen = (Control)sender;

            if (komponen.Tag != null)
            {
                int idNasabah = Convert.ToInt32(komponen.Tag);

                // Update data sisi kanan sesuai ID nasabah yang diklik
                UpdateStatistikDanHistori(idNasabah);
            }
        }

        private void btnKelolaJenisSampah_Click(object sender, EventArgs e)
        {
            // Cari Form1 yang asli yang tadi sedang kita sembunyikan
            Form frm1 = Application.OpenForms["Form1"];

            if (frm1 != null)
            {
                frm1.Show(); // Munculkan kembali Form1 yang lama
            }
            else
            {
                // Jaga-jaga kalau misal Form1 belum terbuat (bikin baru)
                KelolaJenisSampah newFrm1 = new KelolaJenisSampah();
                newFrm1.Show();
            }

            // Tutup FormBuatSetoran ini dengan aman
            this.Close();
        }

        private void FormBuatSetoran_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnBuatSetoran_Click(object sender, EventArgs e)
        {
            // CARA KILAT: Langsung intip Tag dari label nama nasabah yang di tengah!
            if (lblNamaNasabah1.Tag != null)
            {
                int idNasabahAktif = Convert.ToInt32(lblNamaNasabah1.Tag);

                // Buka form transaksi baru sebagai dialog box (pop-up)
                LayananPenyetoran formTrans = new LayananPenyetoran(idNasabahAktif);
                formTrans.ShowDialog();

                // Refresh otomatis sisi kanan setelah pop-up ditutup
                UpdateStatistikDanHistori(idNasabahAktif);
            }
            else
            {
                // Jaga-jaga kalau pas aplikasi jalan, data belum ke-load dari database
                MessageBox.Show("Silakan pilih atau pastikan data nasabah sudah muncul!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormBuatSetoran_Load_1(object sender, EventArgs e)
        {

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

        private void btnRiwayatSetorSampah_Click(object sender, EventArgs e)
        {
            FormRiwayatSetorSampah frm = new FormRiwayatSetorSampah();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            this.Hide();
        }

        private void btnSetorDashboardPetugas_Click(object sender, EventArgs e)
        {
            FormRiwayatSetorSampah frm = new FormRiwayatSetorSampah();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            this.Hide();
        }
    }
}
