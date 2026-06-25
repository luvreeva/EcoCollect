using EcoCollect.Controllers;
using EcoCollect.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoCollect.Views
{
    public partial class FormLoginPetugas : Form
    {
        public FormLoginPetugas()
        {
            InitializeComponent();
        }

        private void FormLoginPetugas_Load(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnKeBeranda_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormBeranda beranda = new EcoCollect.Views.FormBeranda();
            beranda.Show();
            this.Close();
        }

        private void btnMasuk_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan password wajib diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AuthController auth = new AuthController();
            bool berhasil = auth.LoginPetugas(username, password);

            if (berhasil)
            {
                // --- KUNCI PERBAIKAN: Isi Session secara manual berdasarkan username jika Controller-mu belum mengisinya ---
                // Kita panggil class Session agar terisi dengan data yang pas dari text input
                Session.Username = username;

                // Catatan: Idealnya AuthController.LoginPetugas yang bertugas mengisi Session.IdPetugas dan Session.NamaPetugas dari database. 
                // Jika temanmu lupa mengisinya di dalam AuthController, pantesan datanya kosong.

                MessageBox.Show(
                    "Login sebagai " + (string.IsNullOrEmpty(Session.NamaPetugas) ? username : Session.NamaPetugas),
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                FormDashboardPetugas dashboard = new FormDashboardPetugas();
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username atau password salah!", "Gagal",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void btnPetugasKeBeranda_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormBeranda beranda = new EcoCollect.Views.FormBeranda();
            beranda.Show();
            this.Close();
        }
    }
    
}
