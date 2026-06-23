using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EcoCollect.Controllers;
using EcoCollect.Models;

namespace EcoCollect.Views
{
    public partial class FormBuatSetoran : Form
    {
        private int idNasabahDipilih = 0;
        private readonly NasabahController nasabahController = new NasabahController();

        public FormBuatSetoran()
        {
            InitializeComponent();
        }

        private void FormBuatSetoran_Load(object sender, EventArgs e)
        {
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
                    Panel card = BuatCardNasabah(nasabah);
                    pnlFlowNasabah.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan daftar nasabah: " + ex.Message);
            }
        }

        private Panel BuatCardNasabah(NasabahModel nasabah)
        {
            Panel card = new Panel();
            card.Width = pnlFlowNasabah.Width - 25;
            card.Height = 65;
            card.BackColor = Color.FromArgb(225, 242, 245);
            card.Margin = new Padding(5);
            card.Cursor = Cursors.Hand;
            card.Tag = nasabah;

            Label lblNama = new Label();
            lblNama.Text = nasabah.NamaLengkap;
            lblNama.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblNama.ForeColor = Color.FromArgb(0, 84, 96);
            lblNama.Location = new Point(15, 10);
            lblNama.AutoSize = true;
            lblNama.Cursor = Cursors.Hand;

            Label lblUser = new Label();
            lblUser.Text = "@" + nasabah.Username;
            lblUser.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            lblUser.ForeColor = Color.Gray;
            lblUser.Location = new Point(15, 33);
            lblUser.AutoSize = true;
            lblUser.Cursor = Cursors.Hand;

            Label lblPanah = new Label();
            lblPanah.Text = "›";
            lblPanah.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            lblPanah.ForeColor = Color.FromArgb(0, 120, 140);
            lblPanah.Location = new Point(card.Width - 45, 10);
            lblPanah.AutoSize = true;
            lblPanah.Cursor = Cursors.Hand;

            card.Controls.Add(lblNama);
            card.Controls.Add(lblUser);
            card.Controls.Add(lblPanah);

            card.Click += CardNasabah_Click;
            lblNama.Click += CardNasabah_Click;
            lblUser.Click += CardNasabah_Click;
            lblPanah.Click += CardNasabah_Click;

            return card;
        }

        private void CardNasabah_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;

            while (control != null && !(control is Panel && control.Tag is NasabahModel))
            {
                control = control.Parent;
            }

            if (control is Panel card && card.Tag is NasabahModel nasabah)
            {
                idNasabahDipilih = nasabah.IdNasabah;

                pnlDetailNasabah.Visible = true;
                pnlDetailNasabah.BringToFront();

                lblInitial.Text = nasabah.Initial;
                lblNamaLengkapNasabah.Text = nasabah.NamaLengkap;
                lblUsernameNasabah.Text = "@" + nasabah.Username;
                lblNoHp.Text = string.IsNullOrWhiteSpace(nasabah.NoHp) ? "-" : nasabah.NoHp;

                LoadDetailNasabah(idNasabahDipilih);
                LoadHistoriSetoran(idNasabahDipilih);
            }
        }

        private void LoadDetailNasabah(int idNasabah)
        {
            try
            {
                NasabahModel nasabah = nasabahController.GetDetailNasabah(idNasabah);

                if (nasabah == null)
                {
                    MessageBox.Show("Data nasabah tidak ditemukan.");
                    return;
                }

                lblInitial.Text = nasabah.Initial;
                lblNamaLengkapNasabah.Text = nasabah.NamaLengkap;
                lblUsernameNasabah.Text = "@" + nasabah.Username;
                lblNoHp.Text = string.IsNullOrWhiteSpace(nasabah.NoHp) ? "-" : nasabah.NoHp;
                lblFrekuensiSetor.Text = nasabah.TotalFrekuensi + " kali";
                lblTotalMassa.Text = nasabah.TotalMassa.ToString("N2") + " kg";
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
                dt.Columns.Add("ID Transaksi");
                dt.Columns.Add("Kategori");
                dt.Columns.Add("Berat (Kg)");
                dt.Columns.Add("Nilai Rupiah");

                foreach (HistoriSetoranModel item in histori)
                {
                    dt.Rows.Add(
                        item.KodeTransaksi,
                        item.Kategori,
                        item.BeratKg.ToString("N2"),
                        "Rp" + item.NilaiRupiah.ToString("N0")
                    );
                }

                dgvHistoriSetoran.DataSource = dt;
                dgvHistoriSetoran.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvHistoriSetoran.RowHeadersVisible = false;
                dgvHistoriSetoran.AllowUserToAddRows = false;
                dgvHistoriSetoran.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            lblUsernameNasabah.Text = "-";
            lblNoHp.Text = "-";
            lblFrekuensiSetor.Text = "0 kali";
            lblTotalMassa.Text = "0 kg";

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

        private void btnSetorDashboardPetugas_Click(object sender, EventArgs e)
        {
            FormDashboardPetugas home = new FormDashboardPetugas();
            home.Show();
            this.Hide();
        }

        private void btnFormKelolaJenisSampah_Click(object sender, EventArgs e)
        {
            FormKelolaJenisSampah form = new FormKelolaJenisSampah();
            form.Show();
            this.Hide();
        }
    }
}