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
    public partial class FormTarikSaldo : Form
    {
        // Mengunci session username nasabah yang aktif login
        private string usernameLogin = EcoCollect.Models.UserSession.UsernameBaruLogin;

        public FormTarikSaldo()
        {
            InitializeComponent();
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

                decimal biayaAdmin = metode.ToUpper().Contains("BANK") ? 1000 : 500;
                decimal totalPotong = nominal + biayaAdmin;

                // Menggunakan koneksi pusat agar menembak database ECO-COLLECT1 yang valid
                using (NpgsqlConnection conn = EcoCollect.Config.DbConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Ambil id_nasabah dan saldo milik nasabah yang sedang login
                    int idNasabah = 0;
                    decimal saldoSistem = 0;

                    string queryUser = "SELECT id_nasabah, COALESCE(saldo, 0) AS saldo FROM nasabah WHERE username = @user";
                    using (NpgsqlCommand cmdUser = new NpgsqlCommand(queryUser, conn))
                    {
                        cmdUser.Parameters.AddWithValue("@user", usernameLogin);
                        using (var reader = cmdUser.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idNasabah = Convert.ToInt32(reader["id_nasabah"]);
                                saldoSistem = Convert.ToDecimal(reader["saldo"]);
                            }
                        }
                    }

                    // Cek kecukupan saldo
                    if (totalPotong > saldoSistem)
                    {
                        MessageBox.Show($"Saldo tidak cukup!\nSaldo Anda: Rp {saldoSistem:N0}\nTotal Potong + Admin: Rp {totalPotong:N0}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 2. Mulai proses pemotongan saldo dan insert riwayat penarikan
                    // --- EKSEKUSI LEWAT CONTROLLER (Penerapan Murni MVC & OOP) ---
                    EcoCollect.Controllers.C_Penarikan penarikanController = new EcoCollect.Controllers.C_Penarikan();
                    penarikanController.IdNasabah = idNasabah;
                    penarikanController.Metode = metode;
                    penarikanController.NomorTujuan = tujuan;
                    penarikanController.Nominal = nominal;
                    penarikanController.BiayaAdmin = biayaAdmin;
                    penarikanController.TotalPotong = totalPotong;

                    // Memanggil fungsi Tambah() dari Interface ICrud_Controller via Controller
                    if (penarikanController.Tambah())
                    {
                        MessageBox.Show("Penarikan berhasil diproses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Gagal memproses transaksi penarikan melalui Controller.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // --- PERUBAHAN DI SINI ---
                // 1. Panggil ulang fungsi load agar label saldo langsung ter-update berkurang di layar
                LoadSaldoAnda();

                // 2. Kosongkan form input agar siap dipakai kembali
                tbNomor.Clear();
                tbNominal.Clear();
                cmbMetodePenarikan.SelectedIndex = 0;

                // 3. Tetap jalankan refresh dashboard di background
                var dashboard = Application.OpenForms["FormDashboardNasabah"] as FormDashboardNasabah;
                if (dashboard != null)
                {
                    dashboard.RefreshDashboard();
                }

                // 4. Perintah 'this.Close();' sudah dihapus dari sini agar view tidak menutup sendiri!
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
                using (NpgsqlConnection conn = EcoCollect.Config.DbConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COALESCE(saldo, 0) FROM nasabah WHERE username = @user";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", usernameLogin);
                        decimal saldo = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblSaldoAnda.Text = "Rp " + saldo.ToString("N0");
                    }
                }
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