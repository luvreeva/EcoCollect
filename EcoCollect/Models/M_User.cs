using System;
using System.Data;
using Npgsql;    

namespace EcoCollect.Models
{
    public abstract class M_User
    {
        private int _idUser;
        private string _namaLengkap;
        private string _username;
        private string _password;

        public int IdUser { get => _idUser; set => _idUser = value; }
        public string NamaLengkap { get => _namaLengkap; set => _namaLengkap = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public abstract string GetRole();
    }

    public class M_Nasabah : M_User
    {
        public string NoHp { get; set; }
        public decimal Saldo { get; set; }
        public DateTime CreatedAt { get; set; }
        public override string GetRole()
        {
            return "Nasabah";
        }
    }

    public class M_Petugas : M_User
    {
        public DateTime CreatedAt { get; set; }
        public override string GetRole()
        {
            return "Petugas / Admin";
        }

        public DataTable GetRiwayatSetoran(int idPetugas, string keyword = "")
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    ts.kode_transaksi AS Kode,
                    n.nama_lengkap AS Nasabah,
                    p.nama_lengkap AS Petugas,
                    ts.tanggal AS Tanggal,
                    ts.total_nilai AS Total
                FROM transaksi_setor ts
                JOIN nasabah n ON ts.id_nasabah = n.id_nasabah
                JOIN petugas p ON ts.id_petugas = p.id_petugas
                WHERE ts.id_petugas = @idPetugas
                AND (
                    ts.kode_transaksi ILIKE @keyword
                    OR n.nama_lengkap ILIKE @keyword
                )
                ORDER BY ts.tanggal DESC";

            using (NpgsqlConnection conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPetugas", idPetugas);
                    cmd.Parameters.AddWithValue("@keyword", string.IsNullOrEmpty(keyword) ? "%%" : "%" + keyword + "%");

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
