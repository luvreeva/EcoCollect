using EcoCollect.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EcoCollect.Config;


namespace EcoCollect.Views
{
    public partial class FormLoginNasabah : Form
    {
        public FormLoginNasabah()
        {
            InitializeComponent();
        }

        private void btnKeRegristrasi_Click(object sender, EventArgs e)
        {
            FormRegristrasiNasabah regis = new FormRegristrasiNasabah();
            regis.Show();
            this.Hide();
        }

        private void btnKeBeranda_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormBeranda beranda = new EcoCollect.Views.FormBeranda();
            beranda.Show();
            this.Close();
        }

    private void btnMasuk_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Username dan Password harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
               
                AuthController auth = new AuthController();
                bool cekLogin = auth.LoginNasabah(txtUsername.Text, txtPassword.Text);

               
                if (cekLogin)
                {
                    MessageBox.Show("Login Sukses! Selamat datang Nasabah.", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    FormDashboardNasabah dashboard = new FormDashboardNasabah();
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show("Username atau Password salah!", "Gagal Masuk", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi Error: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
