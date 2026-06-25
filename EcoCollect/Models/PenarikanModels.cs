using System;
using Npgsql;

namespace EcoCollect.Models
{
    public class PenarikanModel
    {
        public int IdNasabah { get; set; }
        public string Metode { get; set; }
        public string NomorTujuan { get; set; }
        public decimal Nominal { get; set; }
        public decimal BiayaAdmin { get; set; }
        public decimal TotalPotong { get; set; }

     
        public bool SimpanPenarikanKeDb()
        {
            using (NpgsqlConnection conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                      
                        string insertQuery = @"
                            INSERT INTO penarikan (kode_penarikan, id_nasabah, metode, nomor_tujuan, nominal, biaya_admin, total_potong)
                            VALUES ('WD-' || FLOOR(RANDOM()*89999 + 10000)::text, @id, @metode, @tujuan, @nominal, @biaya, @total);";

                        using (NpgsqlCommand cmdInsert = new NpgsqlCommand(insertQuery, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@id", this.IdNasabah);
                            cmdInsert.Parameters.AddWithValue("@metode", this.Metode);
                            cmdInsert.Parameters.AddWithValue("@tujuan", this.NomorTujuan);
                            cmdInsert.Parameters.AddWithValue("@nominal", this.Nominal);
                            cmdInsert.Parameters.AddWithValue("@biaya", this.BiayaAdmin);
                            cmdInsert.Parameters.AddWithValue("@total", this.TotalPotong);
                            cmdInsert.ExecuteNonQuery();
                        }

                      
                        string updateQuery = "UPDATE nasabah SET saldo = saldo - @total WHERE id_nasabah = @id";
                        using (NpgsqlCommand cmdUp = new NpgsqlCommand(updateQuery, conn))
                        {
                            cmdUp.Parameters.AddWithValue("@total", this.TotalPotong);
                            cmdUp.Parameters.AddWithValue("@id", this.IdNasabah);
                            cmdUp.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

       
        public decimal AmbilSaldoNasabah(string username)
        {
            EcoCollect.Models.NasabahModel model = new EcoCollect.Models.NasabahModel();
            return model.GetSaldoMurni(username); 
        }

        public int AmbilIdNasabah(string username)
        {
            EcoCollect.Models.NasabahModel model = new EcoCollect.Models.NasabahModel();
            return model.GetIdNasabahMurni(username); 
        }
    }
}