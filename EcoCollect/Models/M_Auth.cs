using System;
using EcoCollect.Config;
using Npgsql;

namespace EcoCollect.Models
{
    public class AuthModel
    {
        public static bool RegisterNasabah(string nama, string username, string password, string noHp)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string cekQuery = "SELECT COUNT(1) FROM nasabah WHERE username = @user";

                using (var cmdCek = new NpgsqlCommand(cekQuery, conn))
                {
                    cmdCek.Parameters.AddWithValue("@user", username);

                    int usernameTerpakai = Convert.ToInt32(cmdCek.ExecuteScalar());

                    if (usernameTerpakai > 0)
                        throw new Exception("Username sudah digunakan!");
                }

                string insertQuery = @"
                    INSERT INTO nasabah 
                    (
                        nama_lengkap, 
                        username, 
                        password, 
                        no_hp, 
                        saldo
                    ) 
                    VALUES 
                    (
                        @nama, 
                        @user, 
                        @pass, 
                        @hp, 
                        0
                    )
                ";

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

        public static AuthNasabahModel GetNasabahLogin(string username)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT 
                        id_nasabah, 
                        nama_lengkap, 
                        username, 
                        password
                    FROM nasabah
                    WHERE username = @user
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AuthNasabahModel
                            {
                                IdNasabah = Convert.ToInt32(reader["id_nasabah"]),
                                NamaLengkap = reader["nama_lengkap"].ToString(),
                                Username = reader["username"].ToString(),
                                Password = reader["password"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static AuthPetugasModel GetPetugasLogin(string username, string password)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT 
                        id_petugas, 
                        nama_lengkap,
                        username
                    FROM petugas
                    WHERE username = @user
                    AND password = @pass
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AuthPetugasModel
                            {
                                IdPetugas = Convert.ToInt32(reader["id_petugas"]),
                                NamaLengkap = reader["nama_lengkap"].ToString(),
                                Username = reader["username"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static bool UpdateProfilNasabah(string username, string namaBaru, string noHpBaru)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    UPDATE nasabah 
                    SET 
                        nama_lengkap = @nama, 
                        no_hp = @hp 
                    WHERE username = @user
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", namaBaru);
                    cmd.Parameters.AddWithValue("@hp", noHpBaru);
                    cmd.Parameters.AddWithValue("@user", username);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }

    public class AuthNasabahModel
    {
        public int IdNasabah { get; set; }
        public string NamaLengkap { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthPetugasModel
    {
        public int IdPetugas { get; set; }
        public string NamaLengkap { get; set; }
        public string Username { get; set; }
    }
}