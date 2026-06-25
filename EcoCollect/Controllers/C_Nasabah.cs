using System.Collections.Generic;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class NasabahController
    {
        public List<NasabahModel> CariNasabah(string keyword)
        {
            return NasabahModel.CariNasabah(keyword);
        }

        public NasabahModel GetDetailNasabah(int idNasabah)
        {
            return NasabahModel.GetDetailNasabah(idNasabah);
        }

        public List<HistoriSetoranModel> GetHistoriSetoran(int idNasabah)
        {
            return HistoriSetoranModel.GetHistoriSetoran(idNasabah);
        }
    }
}