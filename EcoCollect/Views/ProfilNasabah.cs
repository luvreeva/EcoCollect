using EcoCollect;
using EcoCollect.Config;
using EcoCollect.Controllers;
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

namespace EcoCollect
{
    public partial class ProfilNasabah : Form
    {
        private EcoCollect.Controllers.C_NasabahDashboard nasabahCtrl 
            = new EcoCollect.Controllers.C_NasabahDashboard();

        public ProfilNasabah()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            
        }

        private void ProfilNasabah_Load(object sender, EventArgs e)
        {
            string usernameAktif = EcoCollect.Models.UserSession.UsernameBaruLogin;

            try
            {
                DataTable dt = nasabahCtrl.GetProfilNasabah(usernameAktif);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblUsername.Text = "@" + row["username"].ToString();
                    lblTanggalDaftar.Text = Convert.ToDateTime(row["created_at"]).ToString("dd-MM-yyyy");
                    txtNamaLengkap.Text = row["nama_lengkap"].ToString();
                    txtNoHp.Text = row["no_hp"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat profil: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSimpanPerubahan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNamaLengkap.Text) || string.IsNullOrEmpty(txtNoHp.Text))
            {
                MessageBox.Show("Nama Lengkap dan Nomor HP tidak boleh kosong!", "Peringatan", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string usernameAktif = EcoCollect.Models.UserSession.UsernameBaruLogin;

                bool suksesUpdate = nasabahCtrl.UpdateProfilNasabah(usernameAktif, 
                    txtNamaLengkap.Text, txtNoHp.Text);

                if (suksesUpdate)
                {
                    MessageBox.Show("Profil Anda berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ProfilNasabah_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal memperbarui profil. Data tidak ditemukan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDashboardNasabah_Click(object sender, EventArgs e)
        {
            try
            {
                EcoCollect.Views.FormDashboardNasabah dashboard = new EcoCollect.Views.FormDashboardNasabah();
                dashboard.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal kembali ke Dashboard: " + ex.Message, "Error Navigasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTarikSaldo_Click(object sender, EventArgs e)
        {
            try
            {
                EcoCollect.Views.FormTarikSaldo tarikSaldo = new EcoCollect.Views.FormTarikSaldo();
                tarikSaldo.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka halaman Tarik Saldo: " + ex.Message, "Error Navigasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRiwayatKeuangan_Click(object sender, EventArgs e)
        {
            try
            {
                EcoCollect.Views.FormRiwayatKeuangan riwayat = new EcoCollect.Views.FormRiwayatKeuangan();
                riwayat.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka halaman Riwayat Keuangan: " + ex.Message, "Error Navigasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKeluar_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormLoginNasabah login = new EcoCollect.Views.FormLoginNasabah();
            login.Show();
            this.Close();
        }
    }
}