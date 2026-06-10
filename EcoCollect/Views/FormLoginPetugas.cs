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
                MessageBox.Show("Login petugas berhasil!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                FormBeranda beranda = new FormBeranda();
                beranda.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username atau password salah!", "Gagal",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
