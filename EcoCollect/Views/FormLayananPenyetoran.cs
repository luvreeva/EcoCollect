using EcoCollect.Controllers;
using EcoCollect.Helpers;
using EcoCollect.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace EcoCollect.Views
{
    public partial class FormLayananPenyetoran : Form
    {
        public FormLayananPenyetoran(int idNasabah)
        {
            InitializeComponent();
            this.idNasabah = idNasabah;
        }

        private readonly int idNasabah;
        private readonly SetoranController setoranController = new SetoranController();

        private readonly List<ItemSetoranModel> daftarItemSetoran = new List<ItemSetoranModel>();
        private readonly CultureInfo cultureId = new CultureInfo("id-ID");

        private string kodeTransaksi = "";

        private void FormLayananPenyetoran_Load(object sender, EventArgs e)
        {
            kodeTransaksi = setoranController.GenerateKodeTransaksi();


            SetupDataGridView();

            LoadNasabahDipilih();
            LoadJenisSampah();
            RefreshTabelSetoran();
        }

        private void LoadNasabahDipilih()
        {
            try
            {
                NasabahModel nasabah = setoranController.GetNasabahById(idNasabah);

                if (nasabah == null)
                {
                    MessageBox.Show("Data nasabah tidak ditemukan.");
                    Close();
                    return;
                }

                lblNamaNasabah.Text = nasabah.NamaLengkap;
                lblUsernameNasabah.Text = "@" + nasabah.Username;
                lblTeleponNasabah.Text = string.IsNullOrWhiteSpace(nasabah.NoHp) ? "-" : nasabah.NoHp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data nasabah: " + ex.Message);
            }
        }

        private void LoadJenisSampah()
        {
            try
            {
                List<KategoriSampahModel> kategori = setoranController.GetKategoriSampah();

                cmbJenisSampah.DataSource = kategori;
                cmbJenisSampah.DisplayMember = "NamaJenis";
                cmbJenisSampah.ValueMember = "IdKategori";
                cmbJenisSampah.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan jenis sampah: " + ex.Message);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (cmbJenisSampah.SelectedItem == null)
            {
                MessageBox.Show("Pilih jenis sampah terlebih dahulu.");
                return;
            }

            if (!decimal.TryParse(
                txtBeratKg.Text.Trim().Replace(",", "."),
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal beratKg))
            {
                MessageBox.Show("Berat sampah tidak valid.");
                return;
            }

            if (beratKg <= 0)
            {
                MessageBox.Show("Berat sampah harus lebih dari 0.");
                return;
            }

            KategoriSampahModel kategori = cmbJenisSampah.SelectedItem as KategoriSampahModel;

            if (kategori == null)
            {
                MessageBox.Show("Jenis sampah tidak valid.");
                return;
            }

            ItemSetoranModel itemLama = daftarItemSetoran
                .FirstOrDefault(x => x.IdKategori == kategori.IdKategori);

            if (itemLama != null)
            {
                itemLama.BeratKg += beratKg;
            }
            else
            {
                daftarItemSetoran.Add(new ItemSetoranModel
                {
                    IdKategori = kategori.IdKategori,
                    NamaJenis = kategori.NamaJenis,
                    BeratKg = beratKg,
                    HargaPerKg = kategori.HargaPerKg
                });
            }

            txtBeratKg.Clear();
            cmbJenisSampah.SelectedIndex = -1;

            RefreshTabelSetoran();
        }

        private void RefreshTabelSetoran()
        {
            dgvDetailSetoran.Rows.Clear();

            foreach (ItemSetoranModel item in daftarItemSetoran)
            {
                dgvDetailSetoran.Rows.Add(
                    item.NamaJenis,
                    item.BeratKg.ToString("N2", cultureId),
                    item.HargaPerKg.ToString("C0", cultureId),
                    item.Subtotal.ToString("C0", cultureId),
                    "Hapus"
                );
            }

            decimal totalBerat = daftarItemSetoran.Sum(x => x.BeratKg);
            decimal totalNominal = daftarItemSetoran.Sum(x => x.Subtotal);

            lblTotalTimbangan.Text = totalBerat.ToString("N2", cultureId) + " KG";
            lblTotalNominal.Text = totalNominal.ToString("C0", cultureId);
        }
        private void dgvDetailSetoran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvDetailSetoran.Columns[e.ColumnIndex].Name == "colAksi")
            {
                DialogResult result = MessageBox.Show(
                    "Hapus item sampah ini?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    daftarItemSetoran.RemoveAt(e.RowIndex);
                    RefreshTabelSetoran();
                }
            }
        }
        private void SetupDataGridView()
        {
            dgvDetailSetoran.Columns.Clear();

            dgvDetailSetoran.AllowUserToAddRows = false;
            dgvDetailSetoran.AllowUserToDeleteRows = false;
            dgvDetailSetoran.ReadOnly = true;
            dgvDetailSetoran.RowHeadersVisible = false;
            dgvDetailSetoran.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetailSetoran.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDetailSetoran.Columns.Add("colJenisSampah", "Jenis Sampah");
            dgvDetailSetoran.Columns.Add("colBeratKg", "Berat (Kg)");
            dgvDetailSetoran.Columns.Add("colHargaPerKg", "Harga / Kg");
            dgvDetailSetoran.Columns.Add("colSubtotal", "Subtotal");

            DataGridViewButtonColumn colAksi = new DataGridViewButtonColumn();
            colAksi.Name = "colAksi";
            colAksi.HeaderText = "Aksi";
            colAksi.Text = "Hapus";
            colAksi.UseColumnTextForButtonValue = true;

            dgvDetailSetoran.Columns.Add(colAksi);
        }
         
        private void btnSimpanSetoran_Click(object sender, EventArgs e)
        {
            if (daftarItemSetoran.Count == 0)
            {
                MessageBox.Show("Belum ada sampah yang ditambahkan.");
                return;
            }

            try
            {
                SetoranModel setoran = new SetoranModel
                {
                    IdNasabah = idNasabah,

                    IdPetugas = Session.IdPetugas,

                    KodeTransaksi = kodeTransaksi,
                    TanggalSetor = DateTime.Now,
                    DetailSetoran = daftarItemSetoran.ToList()
                };

                int idSetor = setoranController.SimpanSetoran(setoran);

                FormStrukSetoran formStruk = new FormStrukSetoran(idSetor);
                formStruk.ShowDialog();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan setoran: " + ex.Message);
            }
        } 

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}