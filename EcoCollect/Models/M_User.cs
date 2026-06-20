using System;

namespace EcoCollect.Models
{
    // 1. ABSTRACT CLASS: Kelas induk yang tidak bisa di-instansiasi langsung
    public abstract class M_User
    {
        // 2. ENKAPSULASI: Menyembunyikan data sensitif menggunakan backing field private
        private int _idUser;
        private string _namaLengkap;
        private string _username;
        private string _password;

        // Properti Public untuk mengakses field private (Getter & Setter)
        public int IdUser { get => _idUser; set => _idUser = value; }
        public string NamaLengkap { get => _namaLengkap; set => _namaLengkap = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }

        // METHOD ABSTRACT: Wajib ada di abstract class agar di-override oleh kelas anak
        public abstract string GetRole();
    }
    public class M_Nasabah : M_User
    {
        public string NoHp { get; set; }
        public decimal Saldo { get; set; }
        public DateTime CreatedAt { get; set; }
        public override string GetRole()
        {
            return "Nasabah";
        }
    }

    public class M_Petugas : M_User
    {
        public DateTime CreatedAt { get; set; }
        public override string GetRole()
        {
            return "Petugas / Admin";
        }
    }
}