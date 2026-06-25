using System;
using EcoCollect.Config;
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

        public static PenarikanNasabahInfoModel GetDataNasabah(string username)
        {
            using (NpgsqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT id_nasabah, COALESCE(saldo, 0) AS saldo
                    FROM nasabah
                    WHERE username = @user
                ";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PenarikanNasabahInfoModel
                            {
                                IdNasabah = Convert.ToInt32(reader["id_nasabah"]),
                                Saldo = Convert.ToDecimal(reader["saldo"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static decimal GetSaldo(string username)
        {
            using (NpgsqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT COALESCE(saldo, 0) FROM nasabah WHERE username = @user";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        public static bool TambahPenarikan(PenarikanModel penarikan)
        {
            using (NpgsqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = @"
                            INSERT INTO penarikan 
                            (
                                kode_penarikan, 
                                id_nasabah, 
                                metode, 
                                nomor_tujuan, 
                                nominal, 
                                biaya_admin, 
                                total_potong
                            )
                            VALUES 
                            (
                                'WD-' || FLOOR(RANDOM()*89999 + 10000)::text, 
                                @id, 
                                @metode, 
                                @tujuan, 
                                @nominal, 
                                @biaya, 
                                @total
                            )
                        ";

                        using (NpgsqlCommand cmdInsert = new NpgsqlCommand(insertQuery, conn, trans))
                        {
                            cmdInsert.Parameters.AddWithValue("@id", penarikan.IdNasabah);
                            cmdInsert.Parameters.AddWithValue("@metode", penarikan.Metode);
                            cmdInsert.Parameters.AddWithValue("@tujuan", penarikan.NomorTujuan);
                            cmdInsert.Parameters.AddWithValue("@nominal", penarikan.Nominal);
                            cmdInsert.Parameters.AddWithValue("@biaya", penarikan.BiayaAdmin);
                            cmdInsert.Parameters.AddWithValue("@total", penarikan.TotalPotong);

                            cmdInsert.ExecuteNonQuery();
                        }

                        string updateQuery = @"
                            UPDATE nasabah 
                            SET saldo = saldo - @total 
                            WHERE id_nasabah = @id
                            AND saldo >= @total
                        ";

                        using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(updateQuery, conn, trans))
                        {
                            cmdUpdate.Parameters.AddWithValue("@total", penarikan.TotalPotong);
                            cmdUpdate.Parameters.AddWithValue("@id", penarikan.IdNasabah);

                            int affected = cmdUpdate.ExecuteNonQuery();

                            if (affected == 0)
                                throw new Exception("Saldo tidak mencukupi atau nasabah tidak ditemukan.");
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

        public static decimal HitungBiayaAdmin(string metodePembayaran)
        {
            return metodePembayaran.ToUpper().Contains("BANK") ? 1000 : 500;
        }
    }

    public class PenarikanNasabahInfoModel
    {
        public int IdNasabah { get; set; }
        public decimal Saldo { get; set; }
    }
}