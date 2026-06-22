using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoCollect
{
    public partial class NasabahItemControl : UserControl
    {
        public event EventHandler ItemClicked;
        public NasabahItemControl()
        {
            InitializeComponent();

            lblPanah.Text = ">";

            this.Click += NasabahItemControl_Click;
            lblNama.Click += NasabahItemControl_Click;
            lblUsername.Click += NasabahItemControl_Click;
            lblPanah.Click += NasabahItemControl_Click;
        }
        public void SetData(string nama, string username)
        {
            lblNama.Text = nama;
            lblUsername.Text = username;
        }
        private void NasabahItemControl_Click(object sender, EventArgs e)
        {
            ItemClicked?.Invoke(this, e);
        }

        private void NasabahItemControl_Load(object sender, EventArgs e)
        {
            ItemClicked?.Invoke(this, e);
        }
        public string NamaNasabah => lblNama.Text;
        public string UsernameNasabah => lblUsername.Text;
    }
}
