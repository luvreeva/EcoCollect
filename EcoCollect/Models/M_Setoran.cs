using System;
using System.Collections.Generic;
using System.Linq;
using EcoCollect.Config;
using Npgsql;

namespace EcoCollect.Models
{
    public class SetoranModel
    {
        public int IdNasabah { get; set; }
        public int IdPetugas { get; set; }
        public string KodeTransaksi { get; set; }
        public DateTime TanggalSetor { get; set; }

        public List<ItemSetoranModel> DetailSetoran { get; set; } = new List<ItemSetoranModel>();

        public decimal TotalBerat
        {
            get { return DetailSetoran.Sum(x => x.BeratKg); }
        }

        public decimal TotalNominal
        {
            get { return DetailSetoran.Sum(x => x.Subtotal); }
        }

        public static string GenerateKodeTransaksi()
        {
            return "TX-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static int SimpanSetoran(SetoranModel setoran)
        {
            if (setoran == null)
                throw new Exception("Data setoran tidak boleh kosong.");

            if (setoran.IdNasabah <= 0)
                throw new Exception("Nasabah belum dipilih.");

            if (setoran.IdPetugas <= 0)
                throw new Exception("Petugas belum terdeteksi. Silakan login ulang.");

            if (setoran.DetailSetoran == null || setoran.DetailSetoran.Count == 0)
                throw new Exception("Detail sampah belum diisi.");

            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string querySetor = @"
                            INSERT INTO transaksi_setor
                            (
                                kode_transaksi,
                                id_nasabah,
                                id_petugas,
                                tanggal,
                                total_nilai
                            )
                            VALUES
                            (
                                @kode_transaksi,
                                @id_nasabah,
                                @id_petugas,
                                @tanggal,
                                @total_nilai
                            )
                            RETURNING id_setor
                        ";

                        int idSetor;

                        using (var cmd = new NpgsqlCommand(querySetor, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@kode_transaksi", setoran.KodeTransaksi);
                            cmd.Parameters.AddWithValue("@id_nasabah", setoran.IdNasabah);
                            cmd.Parameters.AddWithValue("@id_petugas", setoran.IdPetugas);
                            cmd.Parameters.AddWithValue("@tanggal", setoran.TanggalSetor);
                            cmd.Parameters.AddWithValue("@total_nilai", setoran.TotalNominal);

                            idSetor = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        string queryDetail = @"
                            INSERT INTO detail_setor
                            (
                                id_setor,
                                id_kategori,
                                berat_kg,
                                harga_saat_transaksi,
                                subtotal
                            )
                            VALUES
                            (
                                @id_setor,
                                @id_kategori,
                                @berat_kg,
                                @harga_saat_transaksi,
                                @subtotal
                            )
                        ";

                        foreach (ItemSetoranModel item in setoran.DetailSetoran)
                        {
                            using (var cmd = new NpgsqlCommand(queryDetail, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@id_setor", idSetor);
                                cmd.Parameters.AddWithValue("@id_kategori", item.IdKategori);
                                cmd.Parameters.AddWithValue("@berat_kg", item.BeratKg);
                                cmd.Parameters.AddWithValue("@harga_saat_transaksi", item.HargaPerKg);
                                cmd.Parameters.AddWithValue("@subtotal", item.Subtotal);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        string queryUpdateSaldo = @"
                            UPDATE nasabah
                            SET saldo = saldo + @total_nilai
                            WHERE id_nasabah = @id_nasabah
                        ";

                        using (var cmd = new NpgsqlCommand(queryUpdateSaldo, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@total_nilai", setoran.TotalNominal);
                            cmd.Parameters.AddWithValue("@id_nasabah", setoran.IdNasabah);

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return idSetor;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}