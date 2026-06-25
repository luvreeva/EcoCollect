using System;
using System.Collections.Generic;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class NasabahController
    {
        
        private NasabahModel _nasabahModel = new NasabahModel();

        public List<NasabahModel> CariNasabah(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<NasabahModel>();

            return _nasabahModel.CariNasabahFromDb(keyword);
        }

        public NasabahModel GetDetailNasabah(int idNasabah)
        {
            if (idNasabah <= 0) return null;

            return _nasabahModel.GetDetailNasabahFromDb(idNasabah);
        }

        public List<HistoriSetoranModel> GetHistoriSetoran(int idNasabah)
        {
            if (idNasabah <= 0) return new List<HistoriSetoranModel>();

            return _nasabahModel.GetHistoriSetoranFromDb(idNasabah);
        }
    }
}