using System;
using System.Data;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class C_NasabahDashboard
    {
        // Instansiasi model nasabah untuk menjembatani query database
        private NasabahModel _nasabahModel = new NasabahModel();

        public decimal GetSaldo(string username)
        {
            return _nasabahModel.GetSaldoFromDb(username);
        }

        public decimal GetTotalSetor(string username)
        {
            return _nasabahModel.GetTotalSetorFromDb(username);
        }

        public decimal GetTotalTarik(string username)
        {
            return _nasabahModel.GetTotalTarikFromDb(username);
        }

        public DataTable GetRiwayatPenyetoran(string username, int? limit = null)
        {
            return _nasabahModel.GetRiwayatPenyetoranFromDb(username, limit);
        }

        public DataTable GetRiwayatPenarikan(string username)
        {
            return _nasabahModel.GetRiwayatPenarikanFromDb(username);
        }

        public DataTable GetProfilNasabah(string username)
        {
            return _nasabahModel.GetProfilNasabahFromDb(username);
        }

        public bool UpdateProfilNasabah(string username, string nama, string noHp)
        {
            
            if (string.IsNullOrWhiteSpace(nama)) return false;

            return _nasabahModel.UpdateProfilNasabahInDb(username, nama, noHp);
        }

        public string GetNamaLengkap(string username)
        {
           
            EcoCollect.Models.NasabahModel model = new EcoCollect.Models.NasabahModel();
            return model.GetNamaLengkapFromDb(username);
        }
    }
}