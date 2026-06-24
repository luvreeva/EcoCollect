using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace LAYANANPETUGAS
{
    internal class Koneksi
    {
        private static string connString = "Server=localhost;Port=5432;User Id=postgres;Password=Dharma23;Database=PROJEKAN;";
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connString);
        }
    }
}
