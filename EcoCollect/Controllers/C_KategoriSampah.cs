using System;
using System.Collections.Generic;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class KategoriSampahController
    {
        private KategoriSampahModel _modelPembantu = new KategoriSampahModel();

        public List<KategoriSampahModel> GetAllKategori(string keyword = "")
        {
            return _modelPembantu.GetAllFromDb(keyword);
        }

        public bool TambahKategori(KategoriSampahModel kategori)
        {
            if (kategori == null || string.IsNullOrWhiteSpace(kategori.NamaJenis)) return false;

            return kategori.TambahInDb();
        }

        public bool UpdateKategori(KategoriSampahModel kategori)
        {
            if (kategori == null || kategori.IdKategori <= 0) return false;

            return kategori.UpdateInDb();
        }

        public bool HapusKategori(int idKategori)
        {
            if (idKategori <= 0) return false;

            return _modelPembantu.HapusFromDb(idKategori);
        }
    }
}