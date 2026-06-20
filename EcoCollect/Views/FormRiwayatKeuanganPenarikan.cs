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
    public partial class FormRiwayatKeuanganPenarikan : Form
    {
        public FormRiwayatKeuanganPenarikan()
        {
            InitializeComponent();
            this.Load += FormRiwayatKeuanganPenarikan_Load;
        }
        private void FormRiwayatKeuanganPenarikan_Load(object sender, EventArgs e)
        {
            LoadRiwayatPenarikan();

            LoadSaldoAnda();
            LoadHasilPenyetoran();
            LoadTelahDitarik();
        }
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=vea123;Database=db_ecocollect";
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

                    dgvPenarikanSaldo.DataSource = dt;
                }

                FormatPenarikanSaldo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load penarikan: " + ex.Message);
            }
        }
        private void FormatPenarikanSaldo()
        {
            dgvPenarikanSaldo.ReadOnly = true;
            dgvPenarikanSaldo.AllowUserToAddRows = false;
            dgvPenarikanSaldo.AllowUserToDeleteRows = false;
            dgvPenarikanSaldo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPenarikanSaldo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPenarikanSaldo.RowHeadersVisible = false;

            if (dgvPenarikanSaldo.Columns.Contains("Nominal"))
            {
                dgvPenarikanSaldo.Columns["Nominal"].DefaultCellStyle.Format = "C0";
                dgvPenarikanSaldo.Columns["Nominal"].DefaultCellStyle.FormatProvider =
                    new System.Globalization.CultureInfo("id-ID");
            }
        }

        private void btnPenyetoranSampah_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormRiwayatKeuangan riwayatkeuangan = new EcoCollect.Views.FormRiwayatKeuangan();
            riwayatkeuangan.Show();
            this.Hide();
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
        

        private void btnLogout_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormBeranda beranda = new EcoCollect.Views.FormBeranda();
            beranda.Show();
            this.Close();
        }

        private void btnTarikSaldo_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormTarikSaldo tariksaldo = new EcoCollect.Views.FormTarikSaldo();
            tariksaldo.Show();
            this.Hide();
        }

        private void lblDashboardNasabah_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormDashboardNasabah dashboard = new EcoCollect.Views.FormDashboardNasabah();
            dashboard.Show();
            this.Close();
        }
    }
}
