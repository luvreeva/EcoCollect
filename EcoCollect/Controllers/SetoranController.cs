using System.Collections.Generic;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class SetoranController : ICrud_Controller
    {
        public bool Tambah()
        {
            return false;
        }

        public NasabahModel GetNasabahById(int idNasabah)
        {
            return NasabahModel.GetNasabahById(idNasabah);
        }

        public List<KategoriSampahModel> GetKategoriSampah()
        {
            return KategoriSampahModel.GetKategoriSampah();
        }

        public string GenerateKodeTransaksi()
        {
            return SetoranModel.GenerateKodeTransaksi();
        }

        public int SimpanSetoran(SetoranModel setoran)
        {
            return SetoranModel.SimpanSetoran(setoran);
        }

        public StrukSetoranModel GetStrukSetoran(int idSetor)
        {
            return StrukSetoranModel.GetStrukSetoran(idSetor);
        }
    }
}