using System;
using System.Collections.Generic;
using EcoCollect.Config;
using Npgsql;

namespace EcoCollect.Models
{
    public class StrukSetoranModel
    {
        public int IdSetor { get; set; }
        public string KodeTransaksi { get; set; }
        public DateTime Tanggal { get; set; }
        public string NamaNasabah { get; set; }
        public string NoHp { get; set; }
        public decimal TotalNilai { get; set; }

        public List<StrukDetailSetoranModel> DetailSetoran { get; set; } = new List<StrukDetailSetoranModel>();

        public static StrukSetoranModel GetStrukSetoran(int idSetor)
        {
            StrukSetoranModel struk = null;

            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string queryHeader = @"
                    SELECT 
                        ts.id_setor,
                        ts.kode_transaksi,
                        ts.tanggal,
                        ts.total_nilai,
                        n.nama_lengkap,
                        n.no_hp
                    FROM transaksi_setor ts
                    JOIN nasabah n ON ts.id_nasabah = n.id_nasabah
                    WHERE ts.id_setor = @id_setor
                ";

                using (var cmd = new NpgsqlCommand(queryHeader, conn))
                {
                    cmd.Parameters.AddWithValue("@id_setor", idSetor);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            struk = new StrukSetoranModel
                            {
                                IdSetor = Convert.ToInt32(reader["id_setor"]),
                                KodeTransaksi = reader["kode_transaksi"].ToString(),
                                Tanggal = Convert.ToDateTime(reader["tanggal"]),
                                TotalNilai = Convert.ToDecimal(reader["total_nilai"]),
                                NamaNasabah = reader["nama_lengkap"].ToString(),
                                NoHp = reader["no_hp"] == DBNull.Value ? "" : reader["no_hp"].ToString()
                            };
                        }
                    }
                }

                if (struk == null)
                    return null;

                string queryDetail = @"
                    SELECT 
                        ks.nama_jenis,
                        ds.berat_kg,
                        ds.harga_saat_transaksi,
                        ds.subtotal
                    FROM detail_setor ds
                    JOIN kategori_sampah ks ON ds.id_kategori = ks.id_kategori
                    WHERE ds.id_setor = @id_setor
                    ORDER BY ds.id_detail ASC
                ";

                using (var cmd = new NpgsqlCommand(queryDetail, conn))
                {
                    cmd.Parameters.AddWithValue("@id_setor", idSetor);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            struk.DetailSetoran.Add(new StrukDetailSetoranModel
                            {
                                NamaJenis = reader["nama_jenis"].ToString(),
                                BeratKg = Convert.ToDecimal(reader["berat_kg"]),
                                HargaPerKg = Convert.ToDecimal(reader["harga_saat_transaksi"]),
                                Subtotal = Convert.ToDecimal(reader["subtotal"])
                            });
                        }
                    }
                }
            }

            return struk;
        }
    }

    public class StrukDetailSetoranModel
    {
        public string NamaJenis { get; set; }
        public decimal BeratKg { get; set; }
        public decimal HargaPerKg { get; set; }
        public decimal Subtotal { get; set; }
    }
}