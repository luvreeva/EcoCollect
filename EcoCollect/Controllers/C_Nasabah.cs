using System;
using System.Collections.Generic;
using Npgsql;
using EcoCollect.Config;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class NasabahController
    {
        public List<NasabahModel> CariNasabah(string keyword)
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
                                NoHp = reader["no_hp"].ToString()
                            });
                        }
                    }
                }
            }

            return list;
        }

        public NasabahModel GetDetailNasabah(int idNasabah)
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
                                NoHp = reader["no_hp"].ToString(),
                                TotalFrekuensi = Convert.ToInt32(reader["total_frekuensi"]),
                                TotalMassa = Convert.ToDecimal(reader["total_massa"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public List<HistoriSetoranModel> GetHistoriSetoran(int idNasabah)
        {
            List<HistoriSetoranModel> list = new List<HistoriSetoranModel>();

            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT 
                        ts.kode_transaksi,
                        ks.nama_jenis AS kategori,
                        ds.berat_kg,
                        ds.subtotal AS nilai_rupiah
                    FROM transaksi_setor ts
                    JOIN detail_setor ds 
                        ON ds.id_setor = ts.id_setor
                    JOIN kategori_sampah ks 
                        ON ks.id_kategori = ds.id_kategori
                    WHERE ts.id_nasabah = @id_nasabah
                    ORDER BY ts.tanggal DESC
                    LIMIT 10;
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_nasabah", idNasabah);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new HistoriSetoranModel
                            {
                                KodeTransaksi = reader["kode_transaksi"].ToString(),
                                Kategori = reader["kategori"].ToString(),
                                BeratKg = Convert.ToDecimal(reader["berat_kg"]),
                                NilaiRupiah = Convert.ToDecimal(reader["nilai_rupiah"])
                            });
                        }
                    }
                }
            }

            return list;
        }
    }
}