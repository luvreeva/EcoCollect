using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // ==========================================
        //  TITIPAN QUERY DATABASE NASABAH DASHBOARD
        // ==========================================

        public decimal GetSaldoFromDb(string username)
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

        public decimal GetTotalSetorFromDb(string username)
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

        public decimal GetTotalTarikFromDb(string username)
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

        public DataTable GetRiwayatPenyetoranFromDb(string username, int? limit = null)
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

        public DataTable GetRiwayatPenarikanFromDb(string username)
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

        public DataTable GetProfilNasabahFromDb(string username)
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

        public bool UpdateProfilNasabahInDb(string username, string nama, string noHp)
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

        // ==========================================
        //    TITIPAN QUERY DARI NASABAH CONTROLLER
        // ==========================================

        public List<NasabahModel> CariNasabahFromDb(string keyword)
        {
            List<NasabahModel> list = new List<NasabahModel>();

            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT id_nasabah, nama_lengkap, username, no_hp
                    FROM nasabah
                    WHERE nama_lengkap ILIKE @keyword
                       OR username ILIKE @keyword
                    ORDER BY nama_lengkap ASC
                    LIMIT 30;";

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

        public NasabahModel GetDetailNasabahFromDb(int idNasabah)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
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
                    LEFT JOIN transaksi_setor ts ON ts.id_nasabah = n.id_nasabah
                    LEFT JOIN detail_setor ds ON ds.id_setor = ts.id_setor
                    WHERE n.id_nasabah = @id_nasabah
                    GROUP BY n.id_nasabah, n.nama_lengkap, n.username, n.no_hp;";

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

        public List<HistoriSetoranModel> GetHistoriSetoranFromDb(int idNasabah)
        {
            List<HistoriSetoranModel> list = new List<HistoriSetoranModel>();

            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        ts.kode_transaksi,
                        ks.nama_jenis AS kategori,
                        ds.berat_kg,
                        ds.subtotal AS nilai_rupiah
                    FROM transaksi_setor ts
                    JOIN detail_setor ds ON ds.id_setor = ts.id_setor
                    JOIN kategori_sampah ks ON ks.id_kategori = ds.id_kategori
                    WHERE ts.id_nasabah = @id_nasabah
                    ORDER BY ts.tanggal DESC
                    LIMIT 10;";

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

     

        public bool CekUsernameTerdaftar(string username)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM nasabah WHERE username = @user";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool RegisterInDb(string nama, string username, string password, string noHp)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    INSERT INTO nasabah (nama_lengkap, username, password, no_hp, saldo) 
                    VALUES (@nama, @user, @pass, @hp, 0)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);
                    cmd.Parameters.AddWithValue("@hp", noHp);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable AmbilDataLoginNasabah(string username)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_nasabah, nama_lengkap, username, password FROM nasabah WHERE username = @user";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public string GetNamaLengkapFromDb(string username)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT nama_lengkap FROM nasabah WHERE username = @user";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "";
                }
            }
        }

        public decimal GetSaldoMurni(string username)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT COALESCE(saldo, 0) FROM nasabah WHERE username = @user";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        public int GetIdNasabahMurni(string username)
        {
            using (var conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_nasabah FROM nasabah WHERE username = @user";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }
    }
}