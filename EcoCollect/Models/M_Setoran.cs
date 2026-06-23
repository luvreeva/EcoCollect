using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoCollect.Models
{
    public class SetoranModel
    {
        public int IdNasabah { get; set; }
        public int IdPetugas { get; set; }
        public string KodeTransaksi { get; set; }
        public DateTime TanggalSetor { get; set; }

        public List<ItemSetoranModel> DetailSetoran { get; set; } = new List<ItemSetoranModel>();

        public decimal TotalBerat
        {
            get
            {
                return DetailSetoran.Sum(x => x.BeratKg);
            }
        }

        public decimal TotalNominal
        {
            get
            {
                return DetailSetoran.Sum(x => x.Subtotal);
            }
        }
    }
}