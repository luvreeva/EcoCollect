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
    public partial class FormTarikSaldo : Form
    {
        private string usernameLogin = EcoCollect.Models.UserSession.UsernameBaruLogin;
        private EcoCollect.Controllers.C_Penarikan penarikanCtrl = new EcoCollect.Controllers.C_Penarikan();

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

                if (string.IsNullOrWhiteSpace(tujuan))
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

                decimal biayaAdmin = penarikanCtrl.HitungBiayaAdmin(metode);
                decimal totalPotong = nominal + biayaAdmin;

                decimal saldoSistem = penarikanCtrl.AmbilSaldoNasabah(usernameLogin);
                int idNasabah = penarikanCtrl.AmbilIdNasabah(usernameLogin);

                if (totalPotong > saldoSistem)
                {
                    MessageBox.Show($"Saldo tidak cukup!\nSaldo Anda: Rp {saldoSistem:N0}\nTotal Potong + Admin: Rp {totalPotong:N0}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                penarikanCtrl.IdNasabah = idNasabah;
                penarikanCtrl.Metode = metode;
                penarikanCtrl.NomorTujuan = tujuan;
                penarikanCtrl.Nominal = nominal;
                penarikanCtrl.BiayaAdmin = biayaAdmin;
                penarikanCtrl.TotalPotong = totalPotong;

                if (penarikanCtrl.Tambah())
                {
                    MessageBox.Show("Penarikan berhasil diproses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                else
                {
                    MessageBox.Show("Gagal memproses transaksi penarikan melalui Controller.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSaldoAnda()
        {
            try
            {
                decimal saldo = penarikanCtrl.AmbilSaldoNasabah(usernameLogin);
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