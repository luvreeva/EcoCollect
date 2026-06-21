using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoCollect.Helpers
{
    public static class Session
    {
        public static int IdPetugas { get; private set; }
        public static string NamaPetugas { get; private set; }
        public static string Username { get; private set; }

        public static int IdNasabah { get; private set; }
        public static string NamaNasabah { get; private set; }

        // method untuk set data login petugas
        public static void SetPetugas(int id, string nama, string username)
        {
            IdPetugas = id;
            NamaPetugas = nama;
            Username = username;
        }

        // method untuk reset logout petugas
        public static void ClearPetugas()
        {
            IdPetugas = 0;
            NamaPetugas = "";
            Username = "";
        }

        // nasabah
        public static void SetNasabah(int id, string nama, string username)
        {
            IdNasabah = id;
            NamaNasabah = nama;
            Username = username;
        }

        public static void ClearNasabah()
        {
            IdNasabah = 0;
            NamaNasabah = "";
            Username = "";
        }
    }
}