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
    public partial class FormLoginNasabah : Form
    {
        public FormLoginNasabah()
        {
            InitializeComponent();
        }

        private void btnKeRegristrasi_Click(object sender, EventArgs e)
        {
            FormRegristrasiNasabah regis = new FormRegristrasiNasabah();
            regis.Show();
            this.Hide();
        }

        private void btnKeBeranda_Click(object sender, EventArgs e)
        {
            EcoCollect.Views.FormBeranda beranda = new EcoCollect.Views.FormBeranda();
            beranda.Show();
            this.Close();
        }
    }
}
