using System;
using Npgsql;
using EcoCollect.Config;
using EcoCollect.Helpers;

namespace EcoCollect.Controllers
{
    public class AuthController
    {
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=Reeva97;Database=\"ECO-COLLECT1\"";

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

        public int LoginNasabah(string username, string password)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                // Mengambil id, nama, dan password untuk validasi sekaligus set Session
                string cekUserQuery = "SELECT id_nasabah, nama_lengkap, username, password FROM nasabah WHERE username = @user";
                using (var cmd = new NpgsqlCommand(cekUserQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return 0; // Username tidak ditemukan
                        }

                        string passwordDiDatabase = reader["password"].ToString();
                        if (passwordDiDatabase == password)
                        {
                            // Menyimpan data ke Session agar bisa digunakan di form lain
                            Session.IdNasabah = Convert.ToInt32(reader["id_nasabah"]);
                            Session.NamaNasabah = reader["nama_lengkap"].ToString();
                            Session.Username = reader["username"].ToString();

                            return 1; // Login sukses
                        }
                        else
                        {
                            return -1; // Password salah
                        }
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
        SELECT id_petugas, username
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
                            
                            Session.Username = reader["username"].ToString();

                            return true;
                        }

                        return false;
                    }
                }
            }
        }

        public bool UpdateProfilNasabah(string username, string namaBaru, string noHpBaru)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE nasabah 
                         SET nama_lengkap = @nama, no_hp = @hp 
                         WHERE username = @user";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", namaBaru);
                    cmd.Parameters.AddWithValue("@hp", noHpBaru);
                    cmd.Parameters.AddWithValue("@user", username);

                    int barisTerupdate = cmd.ExecuteNonQuery();
                    return barisTerupdate > 0;
                }
            }
        }
    }
}