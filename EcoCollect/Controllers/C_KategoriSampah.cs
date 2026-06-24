using System;
using System.Collections.Generic;
using EcoCollect.Config;
using EcoCollect.Models;
using Npgsql;

namespace EcoCollect.Controllers
{
    public class KategoriSampahController
    {
        public List<KategoriSampahModel> GetAllKategori(string keyword = "")
        {
            List<KategoriSampahModel> daftarKategori = new List<KategoriSampahModel>();

            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT 
                        id_kategori,
                        nama_jenis,
                        harga_per_kg,
                        foto_thumbnail,
                        deskripsi,
                        is_aktif
                    FROM kategori_sampah
                    WHERE is_aktif = TRUE
                    AND (
                        @keyword = ''
                        OR nama_jenis ILIKE @search
                        OR deskripsi ILIKE @search
                    )
                    ORDER BY id_kategori DESC
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", keyword);
                    cmd.Parameters.AddWithValue("@search", "%" + keyword + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            daftarKategori.Add(new KategoriSampahModel
                            {
                                IdKategori = Convert.ToInt32(reader["id_kategori"]),
                                NamaJenis = reader["nama_jenis"].ToString(),
                                HargaPerKg = Convert.ToDecimal(reader["harga_per_kg"]),
                                FotoThumbnail = reader["foto_thumbnail"].ToString(),
                                Deskripsi = reader["deskripsi"].ToString(),
                                IsAktif = Convert.ToBoolean(reader["is_aktif"])
                            });
                        }
                    }
                }
            }

            return daftarKategori;
        }

        public bool TambahKategori(KategoriSampahModel kategori)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    INSERT INTO kategori_sampah
                    (
                        nama_jenis,
                        harga_per_kg,
                        foto_thumbnail,
                        deskripsi,
                        is_aktif
                    )
                    VALUES
                    (
                        @nama_jenis,
                        @harga_per_kg,
                        @foto_thumbnail,
                        @deskripsi,
                        TRUE
                    )
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama_jenis", kategori.NamaJenis);
                    cmd.Parameters.AddWithValue("@harga_per_kg", kategori.HargaPerKg);
                    cmd.Parameters.AddWithValue("@foto_thumbnail", kategori.FotoThumbnail ?? "");
                    cmd.Parameters.AddWithValue("@deskripsi", kategori.Deskripsi ?? "");

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateKategori(KategoriSampahModel kategori)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    UPDATE kategori_sampah
                    SET
                        nama_jenis = @nama_jenis,
                        harga_per_kg = @harga_per_kg,
                        foto_thumbnail = @foto_thumbnail,
                        deskripsi = @deskripsi
                    WHERE id_kategori = @id_kategori
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_kategori", kategori.IdKategori);
                    cmd.Parameters.AddWithValue("@nama_jenis", kategori.NamaJenis);
                    cmd.Parameters.AddWithValue("@harga_per_kg", kategori.HargaPerKg);
                    cmd.Parameters.AddWithValue("@foto_thumbnail", kategori.FotoThumbnail ?? "");
                    cmd.Parameters.AddWithValue("@deskripsi", kategori.Deskripsi ?? "");

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool HapusKategori(int idKategori)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
                    UPDATE kategori_sampah
                    SET is_aktif = FALSE
                    WHERE id_kategori = @id_kategori
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_kategori", idKategori);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}