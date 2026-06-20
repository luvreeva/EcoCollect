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
    public partial class FormRiwayatKeuangan : Form
    {
        public FormRiwayatKeuangan()
        {
            InitializeComponent();
            this.Load += FormRiwayatKeuangan_Load;
        }
        private void FormRiwayatKeuangan_Load(object sender, EventArgs e)
        {
            LoadRiwayatPenyetoran();

            LoadSaldoAnda();
            LoadHasilPenyetoran();
            LoadTelahDitarik();
        }

        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=vea123;Database=db_ecocollect";

        private void LoadRiwayatPenyetoran()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    string query = @"
                SELECT
                    ts.tanggal AS ""Tanggal"",
                    ts.kode_transaksi AS ""ID Transaksi"",
                    ks.nama_jenis AS ""Jenis Sampah"",
                    ds.berat_kg AS ""Berat (Kg)"",
                    ds.subtotal AS ""Nominal""
                FROM transaksi_setor ts
                JOIN detail_setor ds ON ts.id_setor = ds.id_setor
                JOIN kategori_sampah ks ON ds.id_kategori = ks.id_kategori
                ORDER BY ts.tanggal DESC;
            ";

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvPenyetoranSampah.DataSource = dt;
                }

                FormatDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormatDGV()
        {
            dgvPenyetoranSampah.ReadOnly = true;
            dgvPenyetoranSampah.AllowUserToAddRows = false;
            dgvPenyetoranSampah.AllowUserToDeleteRows = false;
            dgvPenyetoranSampah.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPenyetoranSampah.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (dgvPenyetoranSampah.Columns["Nominal"] != null)
            {
                dgvPenyetoranSampah.Columns["Nominal"].DefaultCellStyle.Format = "C0";
                dgvPenyetoranSampah.Columns["Nominal"].DefaultCellStyle.FormatProvider =
                    new System.Globalization.CultureInfo("id-ID");
            }
        }
        private void LoadSaldoAnda()
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

                lblSaldoAnda.Text = "Rp " + saldo.ToString("N0");
            }
        }
        private void LoadHasilPenyetoran()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string query = "SELECT COALESCE(SUM(total_nilai),0) FROM transaksi_setor";

                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                decimal total = Convert.ToDecimal(result);

                lblHasilPenyetoran.Text = "Rp " + total.ToString("N0");
            }
        }
        private void LoadTelahDitarik()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string query = "SELECT COALESCE(SUM(total_potong),0) FROM penarikan";

                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                decimal total = Convert.ToDecimal(result);

                lblTelahDitarik.Text = "Rp " + total.ToString("N0");
            }
        }

        private void btnPenarikanSaldo_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormRiwayatKeuanganPenarikan riwayatkeuanganpenarikan = new EcoCollect.Views.FormRiwayatKeuanganPenarikan();
            riwayatkeuanganpenarikan.Show();
            this.Hide();
        }

        private void btnTarikSaldo_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormTarikSaldo tariksaldo = new EcoCollect.Views.FormTarikSaldo();
            tariksaldo.Show();
            this.Hide();
        }

        private void btnDashboardNasabah_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormDashboardNasabah dashboard = new EcoCollect.Views.FormDashboardNasabah();
            dashboard.Show();
            this.Close();
        }

        private void btnLogoutNasabah_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormBeranda beranda = new EcoCollect.Views.FormBeranda();
            beranda.Show();
            this.Close();
        }
    }
}
