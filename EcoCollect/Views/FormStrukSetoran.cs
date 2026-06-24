using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using EcoCollect.Controllers;
using EcoCollect.Models;

namespace EcoCollect.Views
{
    public partial class FormStrukSetoran : Form
    {
        private readonly int idSetor;
        private readonly SetoranController setoranController = new SetoranController();
        private readonly CultureInfo cultureId = new CultureInfo("id-ID");

        public FormStrukSetoran(int idSetor)
        {
            InitializeComponent();
            this.idSetor = idSetor;
        }

        private void FormStrukSetoran_Load(object sender, EventArgs e)
        {
            LoadStruk();
        }

        private void LoadStruk()
        {
            try
            {
                StrukSetoranModel struk = setoranController.GetStrukSetoran(idSetor);

                if (struk == null)
                {
                    MessageBox.Show("Data struk tidak ditemukan.");
                    Close();
                    return;
                }

                lblTanggalTransaksi.Text = struk.Tanggal.ToString("yyyy-MM-dd HH:mm");
                lblNamaNasabah.Text = struk.NamaNasabah;
                lblNoTelepon.Text = string.IsNullOrWhiteSpace(struk.NoHp) ? "-" : struk.NoHp;
                lblTotalNominal.Text = struk.TotalNilai.ToString("C0", cultureId);

                flpRincianSetoran.Controls.Clear();

                foreach (StrukDetailSetoranModel item in struk.DetailSetoran)
                {
                    Panel panelItem = BuatItemRincian(item);
                    flpRincianSetoran.Controls.Add(panelItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan struk: " + ex.Message);
            }
        }

        private Panel BuatItemRincian(StrukDetailSetoranModel item)
        {
            Panel panel = new Panel();
            panel.Width = flpRincianSetoran.Width - 25;
            panel.Height = 70;
            panel.Margin = new Padding(0, 0, 0, 10);

            Label lblJenis = new Label();
            lblJenis.Text = item.NamaJenis;
            lblJenis.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblJenis.ForeColor = Color.FromArgb(65, 70, 55);
            lblJenis.Location = new Point(0, 0);
            lblJenis.AutoSize = true;

            Label lblHitung = new Label();
            lblHitung.Text = item.BeratKg.ToString("N2", cultureId) +
                             " kg x " +
                             item.HargaPerKg.ToString("C0", cultureId) +
                             "/kg";
            lblHitung.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblHitung.ForeColor = Color.FromArgb(65, 70, 55);
            lblHitung.Location = new Point(0, 30);
            lblHitung.AutoSize = true;

            Label lblSubtotal = new Label();
            lblSubtotal.Text = item.Subtotal.ToString("C0", cultureId);
            lblSubtotal.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSubtotal.ForeColor = Color.FromArgb(65, 70, 55);
            lblSubtotal.AutoSize = true;
            lblSubtotal.Location = new Point(panel.Width - 160, 30);

            panel.Controls.Add(lblJenis);
            panel.Controls.Add(lblHitung);
            panel.Controls.Add(lblSubtotal);

            return panel;
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}