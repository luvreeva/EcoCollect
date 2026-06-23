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
using EcoCollect;

namespace EcoCollect.Views
{
    public partial class FormDashboardNasabah : Form
    {
        // 1. MEMPERBAIKI NAMA DATABASE SINKRON DENGAN PGADMIN
        // Ubah bagian atas ini menjadi deklarasi saja
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=Reeva97;Database=\"ECO-COLLECT\"";
        private string usernameLogin; // Biarkan kosong dulu di sini
        private EcoCollect.Controllers.C_NasabahDashboard nasabahCtrl = new EcoCollect.Controllers.C_NasabahDashboard();
        public FormDashboardNasabah()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("id-ID");
        }

        private void FormDashboardNasabah_Load(object sender, EventArgs e)
        {
            // AMBIL SESSION USERNAME DI SINI (Tepat saat form dibuka)
            usernameLogin = EcoCollect.Models.UserSession.UsernameBaruLogin;

            // Tambahkan debug kecil ini untuk memastikan username tidak kosong saat demo
            if (string.IsNullOrEmpty(usernameLogin))
            {
                MessageBox.Show("Debug: Session Username Kosong! Silakan login ulang melalui Form Login.", "Peringatan");
            }

            LoadNamaNasabah();
            RefreshDashboard();
        }

        private void LoadNamaNasabah()
        {
            try
            {
                using (NpgsqlConnection conn = EcoCollect.Config.DbConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT nama_lengkap FROM nasabah WHERE username = @user";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", usernameLogin);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // Ubah text label ucapan dashboard-mu (sesuaikan dengan nama objek label kamu jika berbeda)
                            // lblSelamatDatang.Text = "Selamat Datang, " + result.ToString() + "!";
                        }
                    }
                }
            }
            catch { }
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
                dgvRiwayatPenyetoranDashboard.Columns["subtotal"].DefaultCellStyle.FormatProvider = new CultureInfo("id-ID");
            }
        }

        private void LoadRingkasanRiwayat()
        {
            try
            {
                // Ambil riwayat penyetoran, batasi hanya 5 baris terakhir untuk dashboard
                dgvRiwayatPenyetoranDashboard.DataSource = nasabahCtrl.GetRiwayatPenyetoran(usernameLogin, 5);
                FormatDGVPenyetoran();
            }
            catch { }
        }

        private void FormatDGVPenarikan()
        {
            if (dgvRiwayatPenarikan.Columns.Contains("Nominal"))
            {
                dgvRiwayatPenarikan.Columns["Nominal"].DefaultCellStyle.Format = "C0";
                dgvRiwayatPenarikan.Columns["Nominal"].DefaultCellStyle.FormatProvider = new CultureInfo("id-ID");
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
                dgvRiwayatPenarikan.DataSource = nasabahCtrl.GetRiwayatPenarikan(usernameLogin);
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
                lblSaldo.Text = "Rp " + nasabahCtrl.GetSaldo(usernameLogin).ToString("N0");
            }
            catch { }
        }
        private void LoadTotalSetor()
        {
            try
            {
                lblTotalSetor.Text = "Rp " + nasabahCtrl.GetTotalSetor(usernameLogin).ToString("N0");
            }
            catch { }
        }

        private void LoadTotalTarik()
        {
            try
            {
                lblTotalTarik.Text = "Rp " + nasabahCtrl.GetTotalTarik(usernameLogin).ToString("N0");
            }
            catch { }
        }

        public void RefreshDashboard()
        {
            LoadRingkasanRiwayat();
            LoadRiwayatPenarikan();
            LoadTotalSetor();
            LoadTotalTarik();
            LoadSaldoRealtime();
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

        private void btnRiwayatKeuangan_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormRiwayatKeuangan riwayatkeuangan = new EcoCollect.Views.FormRiwayatKeuangan();
            riwayatkeuangan.Show();
            this.Hide();
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            try
            {
                EcoCollect.ProfilNasabah formProfil = new EcoCollect.ProfilNasabah();
                formProfil.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka halaman profil: " + ex.Message, "Error Navigasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}