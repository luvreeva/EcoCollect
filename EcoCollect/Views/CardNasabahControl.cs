using System;
using System.Drawing;
using System.Windows.Forms;
using EcoCollect.Models;

namespace EcoCollect.Views
{
    public partial class CardNasabahControl : UserControl
    {
        public NasabahModel Nasabah { get; private set; }

        public event Action<NasabahModel> NasabahClicked;

        public CardNasabahControl()
        {
            InitializeComponent();

            this.Height = 65;
            this.BackColor = Color.FromArgb(225, 242, 245);
            this.Cursor = Cursors.Hand;

            this.Click += Card_Click;
            lblNama.Click += Card_Click;
            lblUser.Click += Card_Click;
            lblPanah.Click += Card_Click;

            lblNama.Cursor = Cursors.Hand;
            lblUser.Cursor = Cursors.Hand;
            lblPanah.Cursor = Cursors.Hand;
        }

        public void SetData(NasabahModel nasabah)
        {
            Nasabah = nasabah;

            lblNama.Text = string.IsNullOrWhiteSpace(nasabah.NamaLengkap)
                ? "-"
                : nasabah.NamaLengkap;

            lblUser.Text = string.IsNullOrWhiteSpace(nasabah.Username)
                ? "@-"
                : "@" + nasabah.Username;

            lblPanah.Text = "›";

            lblPanah.Left = this.Width - 45;
            lblPanah.Top = 8;
            lblPanah.BringToFront();
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (Nasabah == null)
                return;

            NasabahClicked?.Invoke(Nasabah);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (lblPanah != null)
            {
                lblPanah.Left = this.Width - 45;
                lblPanah.Top = 8;
            }
        }
    }
}