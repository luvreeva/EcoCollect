using Npgsql;
using System;
using System.Data;

namespace EcoCollect.Controllers
{
    public class C_NasabahDashboard
    {
        public decimal GetSaldo(string username)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT COALESCE(saldo, 0) FROM nasabah WHERE username = @user";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        public decimal GetTotalSetor(string username)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT COALESCE(SUM(ts.total_nilai), 0) 
                    FROM transaksi_setor ts
                    JOIN nasabah n ON ts.id_nasabah = n.id_nasabah
                    WHERE n.username = @user";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        public decimal GetTotalTarik(string username)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT COALESCE(SUM(p.total_potong), 0) 
                    FROM penarikan p
                    JOIN nasabah n ON p.id_nasabah = n.id_nasabah
                    WHERE n.username = @user";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }
        public DataTable GetRiwayatPenyetoran(string username, int? limit = null)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT
                        ts.tanggal AS ""Tanggal"",
                        ts.kode_transaksi AS ""ID Transaksi"",
                        ks.nama_jenis AS ""Jenis Sampah"",
                        ds.berat_kg AS ""Berat (Kg)"",
                        ds.subtotal AS ""Nominal""
                    FROM transaksi_setor ts
                    JOIN detail_setor ds ON ts.id_setor = ds.id_setor
                    JOIN kategori_sampah ks ON ds.id_kategori = ks.id_kategori
                    JOIN nasabah n ON ts.id_nasabah = n.id_nasabah
                    WHERE n.username = @user
                    ORDER BY ts.tanggal DESC " + (limit.HasValue ? $"LIMIT {limit.Value}" : "");

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable GetRiwayatPenarikan(string username)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        p.tanggal AS ""Tanggal"",
                        p.kode_penarikan AS ""ID Penarikan"", 
                        p.metode AS ""Metode"",
                        p.nomor_tujuan AS ""Nomor Tujuan"",
                        p.total_potong AS ""Nominal""
                    FROM penarikan p
                    JOIN nasabah n ON p.id_nasabah = n.id_nasabah
                    WHERE n.username = @user
                    ORDER BY p.tanggal DESC;";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable GetProfilNasabah(string username)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT username, nama_lengkap, no_hp, created_at FROM nasabah WHERE username = @user";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public bool UpdateProfilNasabah(string username, string nama, string noHp)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = "UPDATE nasabah SET nama_lengkap = @nama, no_hp = @noHp WHERE username = @user";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@noHp", noHp);
                    cmd.Parameters.AddWithValue("@user", username);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}