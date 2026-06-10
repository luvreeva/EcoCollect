using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EcoCollect.Controllers;
namespace EcoCollect.Views
{
    public partial class FormRegristrasiNasabah : Form
    {
        public FormRegristrasiNasabah()
        {
            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormRegristrasiNasabah_Load(object sender, EventArgs e)
        {

        }

        private void btnKeLogin_Click(object sender, EventArgs e)
        {
            FormLoginNasabah loginNasabah = new FormLoginNasabah();
            loginNasabah.Show();
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
