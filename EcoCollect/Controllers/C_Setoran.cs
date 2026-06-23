using System;
using System.Collections.Generic;
using EcoCollect.Models;
using EcoCollect.Config;
using Npgsql;

namespace EcoCollect.Controllers
{
    public class SetoranController
    {
        public NasabahModel GetNasabahById(int idNasabah)
        {
            NasabahModel nasabah = null;

            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT id_nasabah, nama_lengkap, username, no_hp, saldo
                    FROM nasabah
                    WHERE id_nasabah = @id_nasabah
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_nasabah", idNasabah);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nasabah = new NasabahModel
                            {
                                IdNasabah = Convert.ToInt32(reader["id_nasabah"]),
                                NamaLengkap = reader["nama_lengkap"].ToString(),
                                Username = reader["username"].ToString(),
                                NoHp = reader["no_hp"].ToString()
                            };
                        }
                    }
                }
            }

            return nasabah;
        }

        public List<KategoriSampahModel> GetKategoriSampah()
        {
            List<KategoriSampahModel> daftarKategori = new List<KategoriSampahModel>();

            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT id_kategori, nama_jenis, harga_per_kg
                    FROM kategori_sampah
                    WHERE is_aktif = TRUE
                    ORDER BY nama_jenis ASC
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        daftarKategori.Add(new KategoriSampahModel
                        {
                            IdKategori = Convert.ToInt32(reader["id_kategori"]),
                            NamaJenis = reader["nama_jenis"].ToString(),
                            HargaPerKg = Convert.ToDecimal(reader["harga_per_kg"])
                        });
                    }
                }
            }

            return daftarKategori;
        }

        public string GenerateKodeTransaksi()
        {
            return "TX-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public int SimpanSetoran(SetoranModel setoran)
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
        public StrukSetoranModel GetStrukSetoran(int idSetor)
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
                                NoHp = reader["no_hp"].ToString()
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
}