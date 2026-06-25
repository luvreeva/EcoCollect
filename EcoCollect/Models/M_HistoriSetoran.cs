using System;
using System.Collections.Generic;
using EcoCollect.Config;
using Npgsql;

namespace EcoCollect.Models
{
    public class HistoriSetoranModel
    {
        public string KodeTransaksi { get; set; }
        public string Kategori { get; set; }
        public decimal BeratKg { get; set; }
        public decimal NilaiRupiah { get; set; }

        public static List<HistoriSetoranModel> GetHistoriSetoran(int idNasabah)
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