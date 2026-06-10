using System;
using Npgsql;
using EcoCollect.Config;

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


        public bool LoginNasabah(string username, string password)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM nasabah WHERE username = @user AND password = @pass";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    int jumlahCocok = Convert.ToInt32(cmd.ExecuteScalar());
                    return jumlahCocok > 0;
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

    }
}

         