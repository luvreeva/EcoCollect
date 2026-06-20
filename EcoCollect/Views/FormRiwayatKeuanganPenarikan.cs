using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoCollect.Views
{
    public partial class FormRiwayatKeuanganPenarikan : Form
    {
        private string usernameLogin = EcoCollect.Models.UserSession.UsernameBaruLogin;
        private EcoCollect.Controllers.C_NasabahDashboard nasabahCtrl = new EcoCollect.Controllers.C_NasabahDashboard();

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

        private void LoadRiwayatPenarikan()
        {
            try
            {
                dgvPenarikanSaldo.DataSource = nasabahCtrl.GetRiwayatPenarikan(usernameLogin);
                FormatPenarikanSaldo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load penarikan: " + ex.Message);
            }
        }

        private void LoadSaldoAnda()
        {
            try { lblSaldoAnda.Text = "Rp " + nasabahCtrl.GetSaldo(usernameLogin).ToString("N0"); }
            catch { lblSaldoAnda.Text = "Rp 0"; }
        }

        private void LoadHasilPenyetoran()
        {
            try { lblHasilPenyetoran.Text = "Rp " + nasabahCtrl.GetTotalSetor(usernameLogin).ToString("N0"); }
            catch { lblHasilPenyetoran.Text = "Rp 0"; }
        }

        private void LoadTelahDitarik()
        {
            try { lblTelahDitarik.Text = "Rp " + nasabahCtrl.GetTotalTarik(usernameLogin).ToString("N0"); }
            catch { lblTelahDitarik.Text = "Rp 0"; }
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
                dgvPenarikanSaldo.Columns["Nominal"].DefaultCellStyle.FormatProvider = new CultureInfo("id-ID");
            }
        }

        private void btnPenyetoranSampah_Click(object sender, EventArgs e)
        {
            FormRiwayatKeuangan riwayatkeuangan = new FormRiwayatKeuangan();
            riwayatkeuangan.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            FormBeranda beranda = new FormBeranda();
            beranda.Show();
            this.Close();
        }

        private void btnTarikSaldo_Click(object sender, EventArgs e)
        {
            FormTarikSaldo tariksaldo = new FormTarikSaldo();
            tariksaldo.Show();
            this.Hide();
        }

        private void lblDashboardNasabah_Click(object sender, EventArgs e)
        {
            FormDashboardNasabah dashboard = new FormDashboardNasabah();
            dashboard.Show();
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