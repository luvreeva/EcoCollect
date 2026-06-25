using EcoCollect.Controllers;
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
    public partial class FormRegristrasiNasabah : Form
    {
        public FormRegristrasiNasabah()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

         
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormRegristrasiNasabah_Load(object sender, EventArgs e)
        {

        }

        private void btnKeLogin_Click(object sender, EventArgs e)
        {
            FormLoginNasabah loginNasabah = new FormLoginNasabah();
            loginNasabah.Show();
            this.Hide();
        }

        private void btnKeBeranda_Click(object sender, EventArgs e)
        {

            EcoCollect.Views.FormBeranda beranda = new EcoCollect.Views.FormBeranda();
            beranda.Show();
            this.Close();
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNamaLengkap.Text) || string.IsNullOrEmpty(txtUsername.Text) ||
                string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtNoHP.Text))
            {
                MessageBox.Show("Semua kolom registrasi wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                AuthController auth = new AuthController();
                bool cekDaftar = auth.RegisterNasabah(
                    txtNamaLengkap.Text,
                    txtUsername.Text,
                    txtPassword.Text,
                    txtNoHP.Text
                );

                if (cekDaftar)
                {
                    MessageBox.Show("Akun berhasil terdaftar di database! Silakan balik ke halaman login.", "Registrasi Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FormLoginNasabah login = new FormLoginNasabah();
                    login.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan ke database! Periksa apakah koneksi pgAdmin terputus atau nama tabel salah.", "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Daftar (Sistem Error): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
