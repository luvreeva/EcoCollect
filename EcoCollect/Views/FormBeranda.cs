using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EcoCollect.Helpers;

namespace EcoCollect.Views
{
    public partial class FormBeranda : Form
    {
        public FormBeranda()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormBeranda_Load(object sender, EventArgs e)
        {
           
        }

        private void btnAksesPetugas_Click(object sender, EventArgs e)
        {


        }

        private void btnAkses_Click(object sender, EventArgs e)
        {
            FormLoginPetugas loginPetugas = new FormLoginPetugas();
            loginPetugas.Show();
            this.Hide();
        }

        private void btnAksesN_Click(object sender, EventArgs e)
        {
            FormLoginNasabah loginNasabah = new FormLoginNasabah();
            loginNasabah.Show();
            this.Hide();
        }
    }
}
