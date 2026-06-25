using System.Collections.Generic;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class DashboardPetugasController
    {
        public DashboardSummaryModel GetDashboardSummary(int idPetugas)
        {
            return DashboardSummaryModel.GetDashboardSummary(idPetugas);
        }

        public List<RiwayatDashboardModel> GetRiwayatSetorPetugas(int idPetugas)
        {
            return RiwayatDashboardModel.GetRiwayatSetorPetugas(idPetugas);
        }
    }
}