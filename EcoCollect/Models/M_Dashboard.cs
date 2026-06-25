using System;
using System.Collections.Generic;
using EcoCollect.Config;
using Npgsql;

namespace EcoCollect.Models
{
    public class DashboardSummaryModel
    {
        public int TotalNasabah { get; set; }
        public int TotalJenisSampah { get; set; }
        public int TotalTransaksi { get; set; }
        public decimal TotalSampahKg { get; set; }

      
        public DashboardSummaryModel GetSummaryFromDb(int idPetugas)
        {
            DashboardSummaryModel summary = new DashboardSummaryModel();

            using (NpgsqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string qNasabah = "SELECT COUNT(*) FROM nasabah";
                using (NpgsqlCommand cmd = new NpgsqlCommand(qNasabah, conn))
                {
                    summary.TotalNasabah = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string qJenis = "SELECT COUNT(*) FROM kategori_sampah WHERE is_aktif = TRUE";
                using (NpgsqlCommand cmd = new NpgsqlCommand(qJenis, conn))
                {
                    summary.TotalJenisSampah = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string qTransaksi = @"
                    SELECT COUNT(*) 
                    FROM transaksi_setor
                    WHERE id_petugas = @id_petugas";

                using (NpgsqlCommand cmd = new NpgsqlCommand(qTransaksi, conn))
                {
                    cmd.Parameters.AddWithValue("@id_petugas", idPetugas);
                    summary.TotalTransaksi = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string qSampah = @"
                    SELECT COALESCE(SUM(d.berat_kg), 0)
                    FROM detail_setor d
                    JOIN transaksi_setor ts ON d.id_setor = ts.id_setor
                    WHERE ts.id_petugas = @id_petugas";

                using (NpgsqlCommand cmd = new NpgsqlCommand(qSampah, conn))
                {
                    cmd.Parameters.AddWithValue("@id_petugas", idPetugas);
                    object result = cmd.ExecuteScalar();
                    summary.TotalSampahKg = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                }
            }

            return summary;
        }

        public List<RiwayatDashboardModel> GetRiwayatSetorFromDb(int idPetugas)
        {
            List<RiwayatDashboardModel> daftarRiwayat = new List<RiwayatDashboardModel>();

            using (NpgsqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT 
                        ts.kode_transaksi AS kode,
                        n.nama_lengkap AS nasabah,
                        p.nama_lengkap AS petugas,
                        ts.tanggal AS tanggal,
                        ts.total_nilai AS total
                    FROM transaksi_setor ts
                    JOIN nasabah n ON ts.id_nasabah = n.id_nasabah
                    JOIN petugas p ON ts.id_petugas = p.id_petugas
                    WHERE ts.id_petugas = @id_petugas
                    ORDER BY ts.tanggal DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_petugas", idPetugas);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            daftarRiwayat.Add(new RiwayatDashboardModel
                            {
                                Kode = reader["kode"].ToString(),
                                Nasabah = reader["nasabah"].ToString(),
                                Petugas = reader["petugas"].ToString(),
                                Tanggal = Convert.ToDateTime(reader["tanggal"]),
                                Total = Convert.ToDecimal(reader["total"])
                            });
                        }
                    }
                }
            }

            return daftarRiwayat;
        }
    }
}