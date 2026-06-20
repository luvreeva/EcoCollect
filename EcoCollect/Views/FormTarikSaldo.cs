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
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=vea123;Database=db_ecocollect";
        private void btnTarikDana_Click(object sender, EventArgs e)
        {
            try
            {
                string metode = cmbMetodePenarikan.Text;
                string tujuan = tbNomor.Text;

                if (string.IsNullOrWhiteSpace(tbNomor.Text))
                {
                    MessageBox.Show("Nomor tujuan tidak boleh kosong!");
                    return;
                }

                if (metode == "Pilih metode pembayaran")
                {
                    MessageBox.Show("Silakan pilih metode pembayaran!");
                    return;
                }


                if (!decimal.TryParse(tbNominal.Text,
                    NumberStyles.Any,
                    new CultureInfo("id-ID"),
                    out decimal nominal))
                {
                    MessageBox.Show("Nominal harus berupa angka!");
                    return;
                }

                if (nominal < 10000)
                {
                    MessageBox.Show("Minimal penarikan adalah Rp 10.000");
                    return;
                }

                decimal biayaAdmin = metode.ToUpper().Contains("BANK") ? 1000 : 500;
                decimal totalPotong = nominal + biayaAdmin;

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    string cekSaldo = @"
                SELECT 
                COALESCE((SELECT SUM(total_nilai) FROM transaksi_setor),0)
                -
                COALESCE((SELECT SUM(total_potong) FROM penarikan),0)
            ";

                    NpgsqlCommand cmdCek = new NpgsqlCommand(cekSaldo, conn);
                    decimal saldo = Convert.ToDecimal(cmdCek.ExecuteScalar());

                    if (totalPotong > saldo)
                    {
                        MessageBox.Show("Saldo tidak cukup!");
                        return;
                    }

                    string insert = @"
                INSERT INTO penarikan 
                (kode_penarikan, id_nasabah, metode, nomor_tujuan, nominal, biaya_admin, total_potong)
                VALUES 
                ('WD-' || FLOOR(RANDOM()*10000), 1, @metode, @tujuan, @nominal, @biaya, @total);
            ";

                    NpgsqlCommand cmd = new NpgsqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@metode", metode);
                    cmd.Parameters.AddWithValue("@tujuan", tujuan);
                    cmd.Parameters.AddWithValue("@nominal", nominal);
                    cmd.Parameters.AddWithValue("@biaya", biayaAdmin);
                    cmd.Parameters.AddWithValue("@total", totalPotong);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Penarikan berhasil!");

                var dashboard = Application.OpenForms["FormDashboardNasabah"] as FormDashboardNasabah;

                if (dashboard != null)
                {
                    dashboard.RefreshDashboard();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSaldoAnda()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string query = @"
            SELECT 
            COALESCE((SELECT SUM(total_nilai) FROM transaksi_setor),0)
            -
            COALESCE((SELECT SUM(total_potong) FROM penarikan),0)
            AS saldo;
        ";

                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                decimal saldo = Convert.ToDecimal(result);

                lblSaldoAnda.Text = "Rp " + saldo.ToString("N0");
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
    }
}
