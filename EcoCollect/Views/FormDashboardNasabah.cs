using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoCollect.Views
{
    public partial class FormDashboardNasabah : Form
    {
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=vea123;Database=db_ecocollect";
        public FormDashboardNasabah()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("id-ID");
        }
        private void FormDashboardNasabah_Load(object sender, EventArgs e)
        {
            LoadRingkasanRiwayat();
            LoadRiwayatPenarikan();

            LoadTotalSetor();
            LoadTotalTarik();
            LoadSaldoRealtime();
        }

        private void FormatDGVPenyetoran()
        {
            dgvRiwayatPenyetoranDashboard.ReadOnly = true;
            dgvRiwayatPenyetoranDashboard.AllowUserToAddRows = false;
            dgvRiwayatPenyetoranDashboard.AllowUserToDeleteRows = false;
            dgvRiwayatPenyetoranDashboard.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRiwayatPenyetoranDashboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRiwayatPenyetoranDashboard.RowHeadersVisible = false;

            if (dgvRiwayatPenyetoranDashboard.Columns.Contains("subtotal"))
            {
                dgvRiwayatPenyetoranDashboard.Columns["subtotal"].DefaultCellStyle.Format = "C0";
                dgvRiwayatPenyetoranDashboard.Columns["subtotal"].DefaultCellStyle.FormatProvider =
                    new CultureInfo("id-ID");
            }
        }
        private void LoadRingkasanRiwayat()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string query = @"
            SELECT
                ts.tanggal,
                ts.kode_transaksi,
                ks.nama_jenis,
                ds.berat_kg,
                ds.subtotal
            FROM transaksi_setor ts
            JOIN detail_setor ds ON ts.id_setor = ds.id_setor
            JOIN kategori_sampah ks ON ds.id_kategori = ks.id_kategori
            ORDER BY ts.tanggal DESC
            LIMIT 5;
        ";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvRiwayatPenyetoranDashboard.DataSource = dt;
            }
            FormatDGVPenyetoran();
        }
        private void btnLogoutNasabah_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormBeranda beranda = new EcoCollect.Views.FormBeranda();
            beranda.Show();
            this.Close();
        }

        private void btnFiturTarikSaldo_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormTarikSaldo tariksaldo = new EcoCollect.Views.FormTarikSaldo();
            tariksaldo.Show();
            this.Hide();
        }

        

        private void FormatDGVPenarikan()
        {
            if (dgvRiwayatPenarikan.Columns.Contains("Nominal"))
            {
                dgvRiwayatPenarikan.Columns["Nominal"].DefaultCellStyle.Format = "C0";
                dgvRiwayatPenarikan.Columns["Nominal"].DefaultCellStyle.FormatProvider =
                    new CultureInfo("id-ID");
            }
            dgvRiwayatPenarikan.ReadOnly = true;
            dgvRiwayatPenarikan.AllowUserToAddRows = false;
            dgvRiwayatPenarikan.AllowUserToDeleteRows = false;
            dgvRiwayatPenarikan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRiwayatPenarikan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRiwayatPenarikan.RowHeadersVisible = false;
        }
        private void LoadRiwayatPenarikan()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            tanggal AS ""Tanggal"",
                            kode_penarikan AS ""ID Penarikan"",
                            metode AS ""Metode"",
                            nomor_tujuan AS ""Nomor Tujuan"",
                            total_potong AS ""Nominal""
                        FROM penarikan
                        ORDER BY tanggal DESC;
                    ";

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvRiwayatPenarikan.DataSource = dt;
                }

                FormatDGVPenarikan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load penarikan: " + ex.Message);
            }
        }
        private void LoadSaldoRealtime()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                        COALESCE((SELECT SUM(total_nilai) FROM transaksi_setor),0)
                        -
                        COALESCE((SELECT SUM(total_potong) FROM penarikan),0)
                        AS saldo;
                        ";

                    NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();

                    decimal saldo = Convert.ToDecimal(result);

                    lblSaldo.Text = "Rp " + saldo.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load saldo: " + ex.Message);
            }
        }
        private void LoadTotalSetor()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string query = "SELECT COALESCE(SUM(total_nilai),0) FROM transaksi_setor";

                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                decimal total = Convert.ToDecimal(result);

                lblTotalSetor.Text = "Rp " + total.ToString("N0");
            }
        }
        private void LoadTotalTarik()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string query = "SELECT COALESCE(SUM(total_potong),0) FROM penarikan";

                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                decimal total = Convert.ToDecimal(result);

                lblTotalTarik.Text = "Rp " + total.ToString("N0");
            }
        }

        private void btnRiwayatKeuangan_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormRiwayatKeuangan riwayatkeuangan = new EcoCollect.Views.FormRiwayatKeuangan();
            riwayatkeuangan.Show();
            this.Hide();
        }

        public void RefreshDashboard()
        {
            LoadRingkasanRiwayat();
            LoadRiwayatPenarikan();
            LoadTotalSetor();
            LoadTotalTarik();
            LoadSaldoRealtime();
        }
    }
}
