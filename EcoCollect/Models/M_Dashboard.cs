using System;
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

        public static DashboardSummaryModel GetDashboardSummary(int idPetugas)
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
                    WHERE id_petugas = @id_petugas
                ";

                using (NpgsqlCommand cmd = new NpgsqlCommand(qTransaksi, conn))
                {
                    cmd.Parameters.AddWithValue("@id_petugas", idPetugas);
                    summary.TotalTransaksi = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string qSampah = @"
                    SELECT COALESCE(SUM(d.berat_kg), 0)
                    FROM detail_setor d
                    JOIN transaksi_setor ts ON d.id_setor = ts.id_setor
                    WHERE ts.id_petugas = @id_petugas
                ";

                using (NpgsqlCommand cmd = new NpgsqlCommand(qSampah, conn))
                {
                    cmd.Parameters.AddWithValue("@id_petugas", idPetugas);

                    object result = cmd.ExecuteScalar();
                    summary.TotalSampahKg = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                }
            }

            return summary;
        }
    }
}