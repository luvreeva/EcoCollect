using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EcoCollect.Views
{
    public partial class FormDashboardNasabah : Form
    {
        public FormDashboardNasabah()
        {
            InitializeComponent();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin keluar dari aplikasi?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                FormBeranda beranda = new FormBeranda();
                beranda.Show();
            }
        }
    }
}
