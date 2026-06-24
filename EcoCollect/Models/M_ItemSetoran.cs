using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoCollect.Models
{
    public class ItemSetoranModel
    {
        public int IdKategori { get; set; }
        public string NamaJenis { get; set; }
        public decimal BeratKg { get; set; }
        public decimal HargaPerKg { get; set; }

        public decimal Subtotal
        {
            get
            {
                return BeratKg * HargaPerKg;
            }
        }
    }
}
