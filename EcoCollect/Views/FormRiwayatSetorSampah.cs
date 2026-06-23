using EcoCollect.Helpers;
using Npgsql;
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
           // dashboardRef = dashboard;
            //ormDashboardPetugas dashboard
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadRiwayatSetorPetugas(string keyword = "")
        {
            string query = @"
SELECT 
    ts.kode_transaksi AS Kode,
    n.nama_lengkap AS Nasabah,
    p.nama_lengkap AS Petugas,
    ts.tanggal AS Tanggal,
    ts.total_nilai AS Total
FROM transaksi_setor ts
JOIN nasabah n ON ts.id_nasabah = n.id_nasabah
JOIN petugas p ON ts.id_petugas = p.id_petugas
WHERE ts.id_petugas = @idPetugas
AND (
    @keyword = ''
    OR ts.kode_transaksi ILIKE @keyword
    OR n.nama_lengkap ILIKE @keyword
)
ORDER BY ts.tanggal DESC";

            using (NpgsqlConnection conn = Config.DbConnection.GetConnection())
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPetugas", Session.IdPetugas);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvRekapRiwayatSetoran.DataSource = dt;
                    }
                }
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
                Session.IdPetugas = 0;
                Session.NamaPetugas = "";

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
        
    }
}
