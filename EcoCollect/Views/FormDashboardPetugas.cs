using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EcoCollect.Controllers;
using EcoCollect.Helpers;
using EcoCollect.Models;

namespace EcoCollect.Views
{
    public partial class FormDashboardPetugas : Form
    {
        private readonly DashboardPetugasController dashboardController = new DashboardPetugasController();

        public FormDashboardPetugas()
        {
            InitializeComponent();

            btnFormKelolaJenisSampah.BringToFront();
            btnFormKelolaJenisSampah.Click -= btnFormKelolaJenisSampah_Click;
            btnFormKelolaJenisSampah.Click += btnFormKelolaJenisSampah_Click;
        }

        private void FormDashboardPetugas_Load(object sender, EventArgs e)
        {
            LoadDashboardSummary();
            LoadRiwayat();
        }

        private void LoadDashboardSummary()
        {
            try
            {
                DashboardSummaryModel summary = dashboardController.GetDashboardSummary(Session.IdPetugas);

                lbTotalNasabahDashboardPetugas.Text = summary.TotalNasabah.ToString();
                lbTotalJenisSampahDashBoardPetugas.Text = summary.TotalJenisSampah.ToString();
                lbTotalTransaksiDashboardPetugas.Text = summary.TotalTransaksi.ToString();
                lbTotalSampahDashboardPetugas.Text = summary.TotalSampahKg.ToString("0.0") + " kg";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan ringkasan dashboard: " + ex.Message);
            }
        }

        private void LoadRiwayat()
        {
            try
            {
                List<RiwayatDashboardModel> daftarRiwayat =
                    dashboardController.GetRiwayatSetorPetugas(Session.IdPetugas);

                dgvRiwayatDashboardPetugas.DataSource = null;
                dgvRiwayatDashboardPetugas.DataSource = daftarRiwayat;

                dgvRiwayatDashboardPetugas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvRiwayatDashboardPetugas.RowHeadersVisible = false;
                dgvRiwayatDashboardPetugas.AllowUserToAddRows = false;
                dgvRiwayatDashboardPetugas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan riwayat transaksi: " + ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Yakin ingin logout?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Session.IdPetugas = 0;
                Session.NamaPetugas = "";

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

        private void btnRiwayatDashboardPetugas_Click(object sender, EventArgs e)
        {
            FormRiwayatSetorSampah frm = new FormRiwayatSetorSampah();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();

            this.Hide();
        }

        private void btnSetorSampah_Click(object sender, EventArgs e)
        {
            FormBuatSetoran buatsetor = new FormBuatSetoran();
            buatsetor.StartPosition = FormStartPosition.CenterScreen;
            buatsetor.Show();

            this.Hide();
        }

        private void btnFormKelolaJenisSampah_Click(object sender, EventArgs e)
        {
            FormKelolaJenisSampah form = new FormKelolaJenisSampah();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();

            this.Hide();
        }

        private void btnSetorDashboardPetugas_Click(object sender, EventArgs e)
        {
            FormBuatSetoran form = new FormBuatSetoran();
            form.StartPosition =FormStartPosition.CenterScreen;
            form.Show();

            this.Hide();
        }

        private void btnKelolaSampahDashboardPetugas_Click(object sender, EventArgs e)
        {
            FormKelolaJenisSampah form = new FormKelolaJenisSampah();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();

            this.Hide();
        }
    }
}