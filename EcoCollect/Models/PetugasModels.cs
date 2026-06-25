using System;
using System.Data;
using EcoCollect.Config;
using Npgsql;

namespace EcoCollect.Models
{
    public class PetugasModel
    {
        public int IdPetugas { get; set; }
        public string NamaLengkap { get; set; }
        public string Username { get; set; }


        public DataTable AmbilDataLoginPetugas(string username, string password)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT id_petugas, username
                    FROM petugas
                    WHERE username = @user AND password = @pass";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}