using System;
using System.Collections.Generic;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class DashboardPetugasController
    {
        private DashboardSummaryModel _summaryModel = new DashboardSummaryModel();

        public DashboardSummaryModel GetDashboardSummary(int idPetugas)
        {
            if (idPetugas <= 0) return new DashboardSummaryModel();

            return _summaryModel.GetSummaryFromDb(idPetugas);
        }

        public List<RiwayatDashboardModel> GetRiwayatSetorPetugas(int idPetugas)
        {
            if (idPetugas <= 0) return new List<RiwayatDashboardModel>();

            return _summaryModel.GetRiwayatSetorFromDb(idPetugas);
        }
    }
}