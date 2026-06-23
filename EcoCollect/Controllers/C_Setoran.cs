using Npgsql;
using System;

namespace EcoCollect.Controllers
{
    public class C_Setoran : ICrud_Controller
    {
        public int IdNasabah { get; set; }
        public int IdPetugas { get; set; }
        public int IdKategori { get; set; }
        public decimal BeratKg { get; set; }
        public decimal HargaSaatTransaksi { get; set; }
        public decimal Subtotal { get; set; }

        public bool Tambah()
        {
            using (NpgsqlConnection conn = EcoCollect.Config.DbConnection.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        string insertSetoran = @"
                            INSERT INTO setoran (id_nasabah, id_petugas, tanggal) 
                            VALUES (@idNasabah, @idPetugas, CURRENT_TIMESTAMP) RETURNING id_setor;";

                        int idSetor = 0;
                        using (NpgsqlCommand cmdSetor = new NpgsqlCommand(insertSetoran, conn))
                        {
                            cmdSetor.Parameters.AddWithValue("@idNasabah", IdNasabah);
                            cmdSetor.Parameters.AddWithValue("@idPetugas", IdPetugas);
                            idSetor = Convert.ToInt32(cmdSetor.ExecuteScalar());
                        }

                        // 2. Insert ke tabel detail_setor
                        string insertDetail = @"
                            INSERT INTO detail_setor (id_setor, id_kategori, berat_kg, harga_saat_transaksi, subtotal)
                            VALUES (@idSetor, @idKategori, @berat, @harga, @subtotal);";

                        using (NpgsqlCommand cmdDetail = new NpgsqlCommand(insertDetail, conn))
                        {
                            cmdDetail.Parameters.AddWithValue("@idSetor", idSetor);
                            cmdDetail.Parameters.AddWithValue("@idKategori", IdKategori);
                            cmdDetail.Parameters.AddWithValue("@berat", BeratKg);
                            cmdDetail.Parameters.AddWithValue("@harga", HargaSaatTransaksi);
                            cmdDetail.Parameters.AddWithValue("@subtotal", Subtotal);
                            cmdDetail.ExecuteNonQuery();
                        }

                        
                        string updateSaldo = "UPDATE nasabah SET saldo = saldo + @subtotal WHERE id_nasabah = @idNasabah";
                        using (NpgsqlCommand cmdSaldo = new NpgsqlCommand(updateSaldo, conn))
                        {
                            cmdSaldo.Parameters.AddWithValue("@subtotal", Subtotal);
                            cmdSaldo.Parameters.AddWithValue("@idNasabah", IdNasabah);
                            cmdSaldo.ExecuteNonQuery();
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
    }
}