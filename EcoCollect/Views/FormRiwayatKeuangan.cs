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
        private string usernameLogin = EcoCollect.Models.UserSession.UsernameBaruLogin;
        private EcoCollect.Controllers.C_NasabahDashboard nasabahCtrl = new EcoCollect.Controllers.C_NasabahDashboard();

        public FormRiwayatKeuangan()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            
            this.Load += FormRiwayatKeuangan_Load;
        }

        private void FormRiwayatKeuangan_Load(object sender, EventArgs e)
        {
            LoadRiwayatPenyetoran();
            LoadSaldoAnda();
            LoadHasilPenyetoran();
            LoadTelahDitarik();
        }

        private void LoadRiwayatPenyetoran()
        {
            try
            {
                DataTable dt = nasabahCtrl.GetRiwayatPenyetoran(usernameLogin);
                dgvPenyetoranSampah.DataSource = dt;
                FormatDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load riwayat: " + ex.Message);
            }
        }

        private void LoadSaldoAnda()
        {
            try
            {
                lblSaldoAnda.Text = "Rp " + nasabahCtrl.GetSaldo(usernameLogin).ToString("N0");
            }
            catch { lblSaldoAnda.Text = "Rp 0"; }
        }

        private void LoadHasilPenyetoran()
        {
            try
            {
                lblHasilPenyetoran.Text = "Rp " + nasabahCtrl.GetTotalSetor(usernameLogin).ToString("N0");
            }
            catch { lblHasilPenyetoran.Text = "Rp 0"; }
        }

        private void LoadTelahDitarik()
        {
            try
            {
                // Ubah GetTotalTelahDitarik menjadi GetTotalTarik
                lblTelahDitarik.Text = "Rp " + nasabahCtrl.GetTotalTarik(usernameLogin).ToString("N0");
            }
            catch { lblTelahDitarik.Text = "Rp 0"; }
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
                dgvPenyetoranSampah.Columns["Nominal"].DefaultCellStyle.FormatProvider = new CultureInfo("id-ID");
            }
        }

        private void btnPenarikanSaldo_Click(object sender, EventArgs e)
        {
            FormRiwayatKeuanganPenarikan riwayatkeuanganpenarikan = new FormRiwayatKeuanganPenarikan();
            riwayatkeuanganpenarikan.Show();
            this.Hide();
        }

        private void btnTarikSaldo_Click(object sender, EventArgs e)
        {
            FormTarikSaldo tariksaldo = new FormTarikSaldo();
            tariksaldo.Show();
            this.Hide();
        }

        private void btnDashboardNasabah_Click(object sender, EventArgs e)
        {
            FormDashboardNasabah dashboard = new FormDashboardNasabah();
            dashboard.Show();
            this.Close();
        }

        private void btnLogoutNasabah_Click(object sender, EventArgs e)
        {
            FormBeranda beranda = new FormBeranda();
            beranda.Show();
            this.Close();
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