using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class C_Penarikan : ICrud_Controller
    {
        public bool Tambah()
        {
            return false;
        }

        public bool Tambah(PenarikanModel penarikan)
        {
            return PenarikanModel.TambahPenarikan(penarikan);
        }

        public PenarikanNasabahInfoModel GetDataNasabah(string username)
        {
            return PenarikanModel.GetDataNasabah(username);
        }

        public decimal GetSaldo(string username)
        {
            return PenarikanModel.GetSaldo(username);
        }

        public decimal HitungBiayaAdmin(string metodePembayaran)
        {
            return PenarikanModel.HitungBiayaAdmin(metodePembayaran);
        }
    }
}