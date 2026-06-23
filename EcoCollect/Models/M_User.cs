using System;

namespace EcoCollect.Models
{
    public abstract class M_User
    {
        private int _idUser;
        private string _namaLengkap;
        private string _username;
        private string _password;

       
        public int IdUser { get => _idUser; set => _idUser = value; }
        public string NamaLengkap { get => _namaLengkap; set => _namaLengkap = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
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