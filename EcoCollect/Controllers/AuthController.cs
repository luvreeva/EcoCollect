using System;
using Npgsql;
using EcoCollect.Config;

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
                    {
                        throw new Exception("Username sudah digunakan! Pilihlah username lain.");
                    }
                }

                string insertQuery = @"INSERT INTO nasabah (nama_lengkap, username, password, no_hp, saldo) 
                                      VALUES (@nama, @user, @pass, @hp,0)";

                using (var cmdInsert = new NpgsqlCommand(insertQuery, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@nama", nama);
                    cmdInsert.Parameters.AddWithValue("@user", username);
                    cmdInsert.Parameters.AddWithValue("@pass", password);
                    cmdInsert.Parameters.AddWithValue("@hp", noHp);
              
                    int barisTersimpan = cmdInsert.ExecuteNonQuery();
                    return barisTersimpan > 0;
                }
            }
        }


        public int LoginNasabah(string username, string password)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                // 1. Cek dulu apakah username-nya ada di pgAdmin
                string cekUserQuery = "SELECT password FROM nasabah WHERE username = @user";
                using (var cmd = new NpgsqlCommand(cekUserQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return 0; // KODE 0: Username tidak ditemukan/belum terdaftar
                        }

                        // 2. Jika username ada, ambil password asli dari database dan bandingkan
                        string passwordDiDatabase = reader["password"].ToString();
                        if (passwordDiDatabase == password)
                        {
                            return 1; // KODE 1: Cocok semua (Login Sukses)
                        }
                        else
                        {
                            return -1; // KODE -1: Username ada, tapi password-nya salah
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
                
                string query = "SELECT COUNT(1) FROM petugas WHERE username = @user AND password = @pass";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    int jumlahCocok = Convert.ToInt32(cmd.ExecuteScalar());
                    return jumlahCocok > 0; 
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

         