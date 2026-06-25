using EcoCollect.Controllers;
using EcoCollect.Helpers;
using EcoCollect.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace EcoCollect.Views
{
    public partial class FormBuatSetoran : Form
    {
        private readonly NasabahController nasabahController = new NasabahController();
        private int idNasabahDipilih = 0;
        private readonly CultureInfo cultureId = new CultureInfo("id-ID");

        public FormBuatSetoran()
        {
            InitializeComponent();
        }

        private void FormBuatSetoran_Load(object sender, EventArgs e)
        {
            pnlFlowNasabah.AutoScroll = true;

            if (pnlFlowNasabah is FlowLayoutPanel flow)
            {
                flow.FlowDirection = FlowDirection.TopDown;
                flow.WrapContents = false;
                flow.Padding = new Padding(0);
            }

            KosongkanProfilNasabah();
            LoadDaftarNasabah();
        }

        private void txtCariNasabah_TextChanged(object sender, EventArgs e)
        {
            LoadDaftarNasabah(txtCariNasabah.Text.Trim());
        }

        private void LoadDaftarNasabah(string keyword = "")
        {
            pnlFlowNasabah.Controls.Clear();

            try
            {
                List<NasabahModel> daftarNasabah = nasabahController.CariNasabah(keyword);

                foreach (NasabahModel nasabah in daftarNasabah)
                {
                    CardNasabahControl card = new CardNasabahControl();

                    card.Width = pnlFlowNasabah.ClientSize.Width - 25;

                    if (card.Width < 300)
                        card.Width = 300;

                    card.Margin = new Padding(5);
                    card.SetData(nasabah);

                    card.NasabahClicked -= CardNasabah_NasabahClicked;
                    card.NasabahClicked += CardNasabah_NasabahClicked;

                    pnlFlowNasabah.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan daftar nasabah: " + ex.Message);
            }
        }

        private void CardNasabah_NasabahClicked(NasabahModel nasabah)
        {
            if (nasabah == null)
                return;

            idNasabahDipilih = nasabah.IdNasabah;

            pnlDetailNasabah.Visible = true;
            pnlDetailNasabah.BringToFront();

            LoadDetailNasabah(idNasabahDipilih);
            LoadHistoriSetoran(idNasabahDipilih);
        }

        private void LoadDetailNasabah(int idNasabah)
        {
            try
            {
                NasabahModel detail = nasabahController.GetDetailNasabah(idNasabah);

                if (detail == null)
                {
                    MessageBox.Show("Data nasabah tidak ditemukan.");
                    KosongkanProfilNasabah();
                    return;
                }

                lblInitial.Text = detail.Initial;
                lblNamaLengkapNasabah.Text = detail.NamaLengkap;
                lblUsernameNasabah.Text = "@" + detail.Username;
                lblNoHp.Text = string.IsNullOrWhiteSpace(detail.NoHp) ? "-" : detail.NoHp;
                lblFrekuensiSetor.Text = detail.TotalFrekuensi.ToString() + " kali";
                lblTotalMassa.Text = detail.TotalMassa.ToString("N2", cultureId) + " KG";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan detail nasabah: " + ex.Message);
            }
        }

        private void LoadHistoriSetoran(int idNasabah)
        {
            try
            {
                List<HistoriSetoranModel> histori = nasabahController.GetHistoriSetoran(idNasabah);

                DataTable dt = new DataTable();
                dt.Columns.Add("ID Transaksi", typeof(string));
                dt.Columns.Add("Kategori", typeof(string));
                dt.Columns.Add("Berat (Kg)", typeof(decimal));
                dt.Columns.Add("Nilai Rupiah", typeof(decimal));

                foreach (HistoriSetoranModel item in histori)
                {
                    dt.Rows.Add(
                        item.KodeTransaksi,
                        item.Kategori,
                        item.BeratKg,
                        item.NilaiRupiah
                    );
                }

                dgvHistoriSetoran.DataSource = null;
                dgvHistoriSetoran.DataSource = dt;

                dgvHistoriSetoran.ReadOnly = true;
                dgvHistoriSetoran.AllowUserToAddRows = false;
                dgvHistoriSetoran.AllowUserToDeleteRows = false;
                dgvHistoriSetoran.RowHeadersVisible = false;
                dgvHistoriSetoran.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvHistoriSetoran.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                if (dgvHistoriSetoran.Columns.Contains("Nilai Rupiah"))
                {
                    dgvHistoriSetoran.Columns["Nilai Rupiah"].DefaultCellStyle.Format = "C0";
                    dgvHistoriSetoran.Columns["Nilai Rupiah"].DefaultCellStyle.FormatProvider = cultureId;
                }

                if (dgvHistoriSetoran.Columns.Contains("Berat (Kg)"))
                {
                    dgvHistoriSetoran.Columns["Berat (Kg)"].DefaultCellStyle.Format = "N2";
                    dgvHistoriSetoran.Columns["Berat (Kg)"].DefaultCellStyle.FormatProvider = cultureId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan histori setoran: " + ex.Message);
            }
        }

        private void KosongkanProfilNasabah()
        {
            idNasabahDipilih = 0;

            pnlDetailNasabah.Visible = false;

            lblInitial.Text = "-";
            lblNamaLengkapNasabah.Text = "-";
            lblUsernameNasabah.Text = "@-";
            lblNoHp.Text = "-";
            lblFrekuensiSetor.Text = "0 kali";
            lblTotalMassa.Text = "0,00 KG";

            dgvHistoriSetoran.DataSource = null;
        }

        private void btnBuatSetoran_Click(object sender, EventArgs e)
        {
            if (idNasabahDipilih == 0)
            {
                MessageBox.Show("Pilih nasabah terlebih dahulu.");
                return;
            }

            FormLayananPenyetoran form = new FormLayananPenyetoran(idNasabahDipilih);

            this.Hide();

            DialogResult result = form.ShowDialog();

            this.Show();

            if (result == DialogResult.OK)
            {
                LoadDetailNasabah(idNasabahDipilih);
                LoadHistoriSetoran(idNasabahDipilih);
                LoadDaftarNasabah(txtCariNasabah.Text.Trim());
            }
        }

        private void btnDashboardPetugas_Click(object sender, EventArgs e)
        {
            FormDashboardPetugas form = new FormDashboardPetugas();
            form.Show();
            this.Close();
        }

        private void btnRiwayatSetorSampah_Click(object sender, EventArgs e)
        {
            FormRiwayatSetorSampah form = new FormRiwayatSetorSampah();
            form.Show();
            this.Close();
        }

        private void btnFormKelolaJenisSampahPetugas_Click(object sender, EventArgs e)
        {
            FormKelolaJenisSampah form = new FormKelolaJenisSampah();
            form.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Yakin ingin logout?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Session.IdPetugas = 0;
                Session.NamaPetugas = "";

                FormLoginPetugas login = new FormLoginPetugas();
                login.Show();

                this.Close();
            }
        }
    }
}