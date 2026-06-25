using System.Data;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class C_NasabahDashboard
    {
        public decimal GetSaldo(string username)
        {
            return NasabahDashboardModel.GetSaldo(username);
        }

        public decimal GetTotalSetor(string username)
        {
            return NasabahDashboardModel.GetTotalSetor(username);
        }

        public decimal GetTotalTarik(string username)
        {
            return NasabahDashboardModel.GetTotalTarik(username);
        }

        public DataTable GetRiwayatPenyetoran(string username, int? limit = null)
        {
            return NasabahDashboardModel.GetRiwayatPenyetoran(username, limit);
        }

        public DataTable GetRiwayatPenarikan(string username)
        {
            return NasabahDashboardModel.GetRiwayatPenarikan(username);
        }

        public DataTable GetProfilNasabah(string username)
        {
            return NasabahDashboardModel.GetProfilNasabah(username);
        }

        public bool UpdateProfilNasabah(string username, string nama, string noHp)
        {
            return NasabahDashboardModel.UpdateProfilNasabah(username, nama, noHp);
        }
    }
}