using System;
using Npgsql;
using EcoCollect.Config;
using EcoCollect.Helpers;

namespace EcoCollect.Controllers
{
    public class AuthController
    {

        public bool RegisterNasabah(string nama, string username, string password, string noHp)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string cekQuery = "SELECT COUNT(1) FROM nasabah WHERE username = @user";
                using (var cmdCek = new NpgsqlCommand(cekQuery, conn))
                {
                    cmdCek.Parameters.AddWithValue("@user", username);
                    int usernameTersedia = Convert.ToInt32(cmdCek.ExecuteScalar());

                    if (usernameTersedia > 0)
                        throw new Exception("Username sudah digunakan!");
                }

                string insertQuery = @"
            INSERT INTO nasabah (nama_lengkap, username, password, no_hp, saldo) 
            VALUES (@nama, @user, @pass, @hp, 0)";

                using (var cmdInsert = new NpgsqlCommand(insertQuery, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@nama", nama);
                    cmdInsert.Parameters.AddWithValue("@user", username);
                    cmdInsert.Parameters.AddWithValue("@pass", password);
                    cmdInsert.Parameters.AddWithValue("@hp", noHp);

                    return cmdInsert.ExecuteNonQuery() > 0;
                }
            }
        }


        public bool LoginNasabah(string username, string password)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
SELECT id_nasabah, nama_lengkap, username
FROM nasabah
WHERE username = @user AND password = @pass";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Session.IdNasabah = Convert.ToInt32(reader["id_nasabah"]);
                            Session.NamaNasabah = reader["nama_lengkap"].ToString();
                            Session.Username = reader["username"].ToString();

                            return true;
                        }

                        return false;
                    }
                }
            }
        }

        public bool LoginPetugas(string username, string password)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
        SELECT id_petugas, nama_lengkap, username
        FROM petugas
        WHERE username = @user
        AND password = @pass";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Session.IdPetugas = Convert.ToInt32(reader["id_petugas"]);
                            Session.NamaPetugas = reader["nama_lengkap"].ToString();
                            Session.Username = reader["username"].ToString();

                            return true;
                        }

                        return false;
                    }
                }
            }
        }

    }
}

         