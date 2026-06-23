using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoCollect.Models
{
    public class HistoriSetoranModel
    {
        public string KodeTransaksi { get; set; }
        public string Kategori { get; set; }
        public decimal BeratKg { get; set; }
        public decimal NilaiRupiah { get; set; }
    }
}
