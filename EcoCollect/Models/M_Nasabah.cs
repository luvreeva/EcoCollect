using System;
using System.Collections.Generic;
using EcoCollect.Config;
using Npgsql;

namespace EcoCollect.Models
{
    public class NasabahModel
    {
        public int IdNasabah { get; set; }
        public string NamaLengkap { get; set; }
        public string Username { get; set; }
        public string NoHp { get; set; }

        public int TotalFrekuensi { get; set; }
        public decimal TotalMassa { get; set; }

        public string Initial
        {
            get
            {
                if (string.IsNullOrWhiteSpace(NamaLengkap))
                    return "-";

                string[] nama = NamaLengkap.Trim().Split(' ');

                if (nama.Length >= 2)
                {
                    return nama[0][0].ToString().ToUpper() +
                           nama[1][0].ToString().ToUpper();
                }

                return nama[0][0].ToString().ToUpper();
            }
        }

        public static List<NasabahModel> CariNasabah(string keyword)
        {
            List<NasabahModel> list = new List<NasabahModel>();

            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT id_nasabah, nama_lengkap, username, no_hp
                    FROM nasabah
                    WHERE nama_lengkap ILIKE @keyword
                       OR username ILIKE @keyword
                    ORDER BY nama_lengkap ASC
                    LIMIT 30;
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NasabahModel
                            {
                                IdNasabah = Convert.ToInt32(reader["id_nasabah"]),
                                NamaLengkap = reader["nama_lengkap"].ToString(),
                                Username = reader["username"].ToString(),
                                NoHp = reader["no_hp"] == DBNull.Value ? "" : reader["no_hp"].ToString()
                            });
                        }
                    }
                }
            }

            return list;
        }

        public static NasabahModel GetDetailNasabah(int idNasabah)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT 
                        n.id_nasabah,
                        n.nama_lengkap,
                        n.username,
                        n.no_hp,
                        COUNT(DISTINCT ts.id_setor) AS total_frekuensi,
                        COALESCE(SUM(ds.berat_kg), 0) AS total_massa
                    FROM nasabah n
                    LEFT JOIN transaksi_setor ts 
                        ON ts.id_nasabah = n.id_nasabah
                    LEFT JOIN detail_setor ds 
                        ON ds.id_setor = ts.id_setor
                    WHERE n.id_nasabah = @id_nasabah
                    GROUP BY n.id_nasabah, n.nama_lengkap, n.username, n.no_hp;
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_nasabah", idNasabah);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new NasabahModel
                            {
                                IdNasabah = Convert.ToInt32(reader["id_nasabah"]),
                                NamaLengkap = reader["nama_lengkap"].ToString(),
                                Username = reader["username"].ToString(),
                                NoHp = reader["no_hp"] == DBNull.Value ? "" : reader["no_hp"].ToString(),
                                TotalFrekuensi = Convert.ToInt32(reader["total_frekuensi"]),
                                TotalMassa = Convert.ToDecimal(reader["total_massa"])
                            };
                        }
                    }
                }
            }

            return null;
        }
        public static NasabahModel GetNasabahById(int idNasabah)
        {
            NasabahModel nasabah = null;

            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT id_nasabah, nama_lengkap, username, no_hp
                    FROM nasabah
                    WHERE id_nasabah = @id_nasabah
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_nasabah", idNasabah);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nasabah = new NasabahModel
                            {
                                IdNasabah = Convert.ToInt32(reader["id_nasabah"]),
                                NamaLengkap = reader["nama_lengkap"].ToString(),
                                Username = reader["username"].ToString(),
                                NoHp = reader["no_hp"] == DBNull.Value ? "" : reader["no_hp"].ToString()
                            };
                        }
                    }
                }
            }

            return nasabah;
        }
    }
}