using System;
using System.Collections.Generic;
using EcoCollect.Config;
using Npgsql;

namespace EcoCollect.Models
{
    public class RiwayatDashboardModel
    {
        public string Kode { get; set; }
        public string Nasabah { get; set; }
        public string Petugas { get; set; }
        public DateTime Tanggal { get; set; }
        public decimal Total { get; set; }

        public static List<RiwayatDashboardModel> GetRiwayatSetorPetugas(int idPetugas)
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
                    ORDER BY ts.tanggal DESC
                ";

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