using System;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class C_Penarikan : ICrud_Controller
    {
        // Tetap pertahankan properti agar Form/View tidak error saat memanggil data
        public int IdNasabah { get; set; }
        public string Metode { get; set; }
        public string NomorTujuan { get; set; }
        public decimal Nominal { get; set; }
        public decimal BiayaAdmin { get; set; }
        public decimal TotalPotong { get; set; }

        public bool Tambah()
        {
           
            PenarikanModel penarikan = new PenarikanModel
            {
                IdNasabah = this.IdNasabah,
                Metode = this.Metode,
                NomorTujuan = this.NomorTujuan,
                Nominal = this.Nominal,
                BiayaAdmin = this.BiayaAdmin,
                TotalPotong = this.TotalPotong
            };

           
            return penarikan.SimpanPenarikanKeDb();
        }

        public decimal HitungBiayaAdmin(string metodePembayaran)
        {
            if (string.IsNullOrEmpty(metodePembayaran)) return 0;

            return metodePembayaran.ToUpper().Contains("BANK") ? 1000 : 500;
        }
    }
}