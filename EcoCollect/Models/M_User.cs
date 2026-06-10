using System;

namespace EcoCollect.Models
{
    public abstract class M_User
    {
        public int IdUser { get; set; }
        public string NamaLengkap { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class M_Nasabah : M_User
    {
        public string NoHp { get; set; }
        public decimal Saldo { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class M_Petugas : M_User
    {
        public string Jabatan { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}