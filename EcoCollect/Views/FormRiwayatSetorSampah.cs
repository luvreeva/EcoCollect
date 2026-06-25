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
    public partial class FormRiwayatSetorSampah : Form
    {
        private FormDashboardPetugas dashboardRef;

        public FormRiwayatSetorSampah()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      
        private void LoadRiwayatSetorPetugas(string keyword = "")
        {
            try
            {
               
                Models.M_Petugas petugasModel = new Models.M_Petugas();

                int idPetugasLogin = Session.IdPetugas;

                DataTable dt = petugasModel.GetRiwayatSetoran(idPetugasLogin, keyword);

                dgvRekapRiwayatSetoran.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat riwayat transaksi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormRiwayatSetorSampah_Load(object sender, EventArgs e)
        {
            LoadRiwayatSetorPetugas("");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Yakin ingin logout?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Session.ClearPetugas();

                FormLoginPetugas login = new FormLoginPetugas();
                login.Show();

                this.Close();
            }
        }

        private void btnDashboardPetugas_Click(object sender, EventArgs e)
        {
            FormDashboardPetugas dashboard = new FormDashboardPetugas();
            dashboard.Show();
            this.Close();
        }

        private void tbCariRiwayatSetoran_TextChanged(object sender, EventArgs e)
        {
            LoadRiwayatSetorPetugas(tbCariRiwayatSetoran.Text);
        }

        private void btnFormLayananPenyetoran_Click(object sender, EventArgs e)
        {
            FormBuatSetoran form = new FormBuatSetoran();
            form.Show();
            this.Close();
        }

        private void btnFormKelolaJenisSampahPetugas_Click(object sender, EventArgs e)
        {
            FormKelolaJenisSampah form = new FormKelolaJenisSampah();
            form.Show();
            this.Close();
        }
    }
}