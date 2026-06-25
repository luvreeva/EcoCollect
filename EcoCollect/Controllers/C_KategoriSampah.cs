using System.Collections.Generic;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class KategoriSampahController
    {
        public List<KategoriSampahModel> GetAllKategori(string keyword = "")
        {
            return KategoriSampahModel.GetAllKategori(keyword);
        }

        public bool TambahKategori(KategoriSampahModel kategori)
        {
            return KategoriSampahModel.TambahKategori(kategori);
        }

        public bool UpdateKategori(KategoriSampahModel kategori)
        {
            return KategoriSampahModel.UpdateKategori(kategori);
        }

        public bool HapusKategori(int idKategori)
        {
            return KategoriSampahModel.HapusKategori(idKategori);
        }
    }
}