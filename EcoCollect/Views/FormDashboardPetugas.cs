using EcoCollect.Config;
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
    public partial class FormDashboardPetugas : Form
    {
        public FormDashboardPetugas()
        {
            InitializeComponent();
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

        private void FormDashboardPetugas_Load(object sender, EventArgs e)
        {
            LoadRiwayat();
            LoadDashboardSummary();
        }

        private void LoadDashboardSummary()
        {
            using (NpgsqlConnection conn = Config.DbConnection.GetConnection())
            {
                conn.Open();
                string qNasabah = "SELECT COUNT(*) FROM nasabah";
                NpgsqlCommand cmdNasabah = new NpgsqlCommand(qNasabah, conn);
                lbTotalNasabahDashboardPetugas.Text = cmdNasabah.ExecuteScalar().ToString();

                string qJenis = "SELECT COUNT(*) FROM kategori_sampah";
                NpgsqlCommand cmdJenis = new NpgsqlCommand(qJenis, conn);
                lbTotalJenisSampahDashBoardPetugas.Text = cmdJenis.ExecuteScalar().ToString();

                string qTransaksi = "SELECT COUNT(*) FROM transaksi_setor";
                NpgsqlCommand cmdTransaksi = new NpgsqlCommand(qTransaksi, conn);
                lbTotalTransaksiDashboardPetugas.Text = cmdTransaksi.ExecuteScalar().ToString();

                string qSampah = @"
SELECT COALESCE(SUM(d.berat_kg),0)
FROM detail_setor d
JOIN transaksi_setor ts ON d.id_setor = ts.id_setor
WHERE ts.id_petugas = @idPetugas";

                NpgsqlCommand cmdSampah = new NpgsqlCommand(qSampah, conn);
                cmdSampah.Parameters.AddWithValue("@idPetugas", Session.IdPetugas);

                object result = cmdSampah.ExecuteScalar();
                decimal totalKg = result == DBNull.Value ? 0 : Convert.ToDecimal(result);

                lbTotalSampahDashboardPetugas.Text =
                totalKg.ToString("0.0") + " kg";
            }
        }



        private void LoadRiwayat()
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
ORDER BY ts.tanggal DESC";

            using (NpgsqlConnection conn = Config.DbConnection.GetConnection())
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPetugas", Session.IdPetugas);

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvRiwayatDashboardPetugas.DataSource = dt;
                }
            }
        }

        private void btnRiwayatSetorSampah_Click(object sender, EventArgs e)
        {
            FormRiwayatSetorSampah frm = new FormRiwayatSetorSampah(this);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            this.Hide();
        }

        private void btnRiwayatDashboardPetugas_Click(object sender, EventArgs e)
        {
            FormRiwayatSetorSampah frm = new FormRiwayatSetorSampah(this);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            this.Hide();
        }
    }
}
