using Npgsql;
using System;

namespace EcoCollect.Controllers
{
    // 1. INTERFACE IMPLEMENTATION: Kelas ini patuh pada kontrak ICrud_Controller
    public class C_Penarikan : ICrud_Controller
    {
        // Properti data penarikan yang dikirim dari Form View
        public int IdNasabah { get; set; }
        public string Metode { get; set; }
        public string NomorTujuan { get; set; }
        public decimal Nominal { get; set; }
        public decimal BiayaAdmin { get; set; }
        public decimal TotalPotong { get; set; }

        // 2. OVERRIDING: Mengimplementasikan secara nyata method Tambah() dari Interface
        public bool Tambah()
        {
            using (NpgsqlConnection conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // SQL A: Masukkan ke tabel penarikan
                        string insertQuery = @"
                            INSERT INTO penarikan (kode_penarikan, id_nasabah, metode, nomor_tujuan, nominal, biaya_admin, total_potong)
                            VALUES ('WD-' || FLOOR(RANDOM()*89999 + 10000)::text, @id, @metode, @tujuan, @nominal, @biaya, @total);";

                        using (NpgsqlCommand cmdInsert = new NpgsqlCommand(insertQuery, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@id", IdNasabah);
                            cmdInsert.Parameters.AddWithValue("@metode", Metode);
                            cmdInsert.Parameters.AddWithValue("@tujuan", NomorTujuan);
                            cmdInsert.Parameters.AddWithValue("@nominal", Nominal);
                            cmdInsert.Parameters.AddWithValue("@biaya", BiayaAdmin);
                            cmdInsert.Parameters.AddWithValue("@total", TotalPotong);
                            cmdInsert.ExecuteNonQuery();
                        }

                        // SQL B: Potong saldo di tabel nasabah
                        string updateQuery = "UPDATE nasabah SET saldo = saldo - @total WHERE id_nasabah = @id";
                        using (NpgsqlCommand cmdUp = new NpgsqlCommand(updateQuery, conn))
                        {
                            cmdUp.Parameters.AddWithValue("@total", TotalPotong);
                            cmdUp.Parameters.AddWithValue("@id", IdNasabah);
                            cmdUp.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

        // 3. OVERLOADING FUNGSI: Nama fungsi sama, tapi tipe/jumlah parameternya berbeda!
        // Versi 1: Hitung admin normal berdasarkan metode penarikan
        public decimal HitungBiayaAdmin(string metodePembayaran)
        {
            return metodePembayaran.ToUpper().Contains("BANK") ? 1000 : 500;
        }

        // Versi 2: Hitung admin kalau ada parameter kondisi promo (Overloading)
        public decimal HitungBiayaAdmin(string metodePembayaran, bool isPromo)
        {
            if (isPromo) return 0; // Gratis biaya admin jika sedang promo!
            return HitungBiayaAdmin(metodePembayaran);
        }
    }
}