using System;

namespace EcoCollect.Models
{
    public class RiwayatDashboardModel
    {
        public string Kode { get; set; }
        public string Nasabah { get; set; }
        public string Petugas { get; set; }
        public DateTime Tanggal { get; set; }
        public decimal Total { get; set; }
    }
}