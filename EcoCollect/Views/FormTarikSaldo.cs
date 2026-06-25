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
using EcoCollect.Controllers;
using EcoCollect.Models;

namespace EcoCollect.Views
{
    public partial class FormTarikSaldo : Form
    {
        private string usernameLogin = EcoCollect.Models.UserSession.UsernameBaruLogin;

        private C_Penarikan penarikanController = new C_Penarikan();

        public FormTarikSaldo()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.Load += FormTarikSaldo_Load;
        }

        private void FormTarikSaldo_Load(object sender, EventArgs e)
        {
            cmbMetodePenarikan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodePenarikan.Items.Clear();

            cmbMetodePenarikan.Items.Add("Pilih metode pembayaran");
            cmbMetodePenarikan.Items.Add("DANA");
            cmbMetodePenarikan.Items.Add("OVO");
            cmbMetodePenarikan.Items.Add("Bank BRI");
            cmbMetodePenarikan.Items.Add("Bank BCA");

            cmbMetodePenarikan.SelectedIndex = 0;

            LoadSaldoAnda();
        }

        private void btnTarikDana_Click(object sender, EventArgs e)
        {
            try
            {
                string metode = cmbMetodePenarikan.Text;
                string tujuan = tbNomor.Text;

                if (string.IsNullOrWhiteSpace(tbNomor.Text))
                {
                    MessageBox.Show("Nomor tujuan tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (metode == "Pilih metode pembayaran")
                {
                    MessageBox.Show("Silakan pilih metode pembayaran!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(tbNominal.Text, NumberStyles.Any, new CultureInfo("id-ID"), out decimal nominal))
                {
                    MessageBox.Show("Nominal harus berupa angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nominal < 10000)
                {
                    MessageBox.Show("Minimal penarikan adalah Rp 10.000", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PenarikanNasabahInfoModel dataNasabah = penarikanController.GetDataNasabah(usernameLogin);

                if (dataNasabah == null)
                {
                    MessageBox.Show("Data nasabah tidak ditemukan. Silakan login ulang.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal biayaAdmin = penarikanController.HitungBiayaAdmin(metode);
                decimal totalPotong = nominal + biayaAdmin;

                if (totalPotong > dataNasabah.Saldo)
                {
                    MessageBox.Show(
                        $"Saldo tidak cukup!\nSaldo Anda: Rp {dataNasabah.Saldo:N0}\nTotal Potong + Admin: Rp {totalPotong:N0}",
                        "Transaksi Gagal",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                PenarikanModel dataPenarikan = new PenarikanModel
                {
                    IdNasabah = dataNasabah.IdNasabah,
                    Metode = metode,
                    NomorTujuan = tujuan,
                    Nominal = nominal,
                    BiayaAdmin = biayaAdmin,
                    TotalPotong = totalPotong
                };

                bool berhasil = penarikanController.Tambah(dataPenarikan);

                if (berhasil)
                {
                    MessageBox.Show("Penarikan berhasil diproses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Gagal memproses transaksi penarikan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LoadSaldoAnda();

                tbNomor.Clear();
                tbNominal.Clear();
                cmbMetodePenarikan.SelectedIndex = 0;

                var dashboard = Application.OpenForms["FormDashboardNasabah"] as FormDashboardNasabah;
                if (dashboard != null)
                {
                    dashboard.RefreshDashboard();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSaldoAnda()
        {
            try
            {
                decimal saldo = penarikanController.GetSaldo(usernameLogin);
                lblSaldoAnda.Text = "Rp " + saldo.ToString("N0");
            }
            catch
            {
                lblSaldoAnda.Text = "Rp 0";
            }
        }

        private void btnRiwayatKeuangan_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormRiwayatKeuangan riwayatkeuangan = new EcoCollect.Views.FormRiwayatKeuangan();
            riwayatkeuangan.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormBeranda beranda = new EcoCollect.Views.FormBeranda();
            beranda.Show();
            this.Close();
        }

        private void btnDashboardNasabah_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormDashboardNasabah dashboard = new EcoCollect.Views.FormDashboardNasabah();
            dashboard.Show();
            this.Close();
        }

        private void btnPengaturanProfil_Click(object sender, EventArgs e)
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