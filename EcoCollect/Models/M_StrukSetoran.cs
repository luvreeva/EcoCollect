using System;
using System.Collections.Generic;

namespace EcoCollect.Models
{
    public class StrukSetoranModel
    {
        public int IdSetor { get; set; }
        public string KodeTransaksi { get; set; }
        public DateTime Tanggal { get; set; }

        public string NamaNasabah { get; set; }
        public string NoHp { get; set; }

        public decimal TotalNilai { get; set; }

        public List<StrukDetailSetoranModel> DetailSetoran { get; set; } = new List<StrukDetailSetoranModel>();
    }

    public class StrukDetailSetoranModel
    {
        public string NamaJenis { get; set; }
        public decimal BeratKg { get; set; }
        public decimal HargaPerKg { get; set; }
        public decimal Subtotal { get; set; }
    }
}