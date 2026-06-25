using System;
using System.Data;
using EcoCollect.Helpers;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class AuthController
    {
        private NasabahModel _nasabahModel = new NasabahModel();
        private PetugasModel _petugasModel = new PetugasModel();

        public bool RegisterNasabah(string nama, string username, string password, string noHp)
        {
            if (_nasabahModel.CekUsernameTerdaftar(username))
            {
                throw new Exception("Username sudah digunakan!");
            }

            return _nasabahModel.RegisterInDb(nama, username, password, noHp);
        }

        public int LoginNasabah(string username, string password)
        {
            DataTable dt = _nasabahModel.AmbilDataLoginNasabah(username);

            if (dt.Rows.Count == 0)
            {
                return 0; 
            }

            string passwordDiDatabase = dt.Rows[0]["password"].ToString();
            if (passwordDiDatabase == password)
            {
               
                Session.IdNasabah = Convert.ToInt32(dt.Rows[0]["id_nasabah"]);
                Session.NamaNasabah = dt.Rows[0]["nama_lengkap"].ToString();
                Session.Username = dt.Rows[0]["username"].ToString();

                return 1; 
            }

            return -1; 
        }

        public bool LoginPetugas(string username, string password)
        {
            DataTable dt = _petugasModel.AmbilDataLoginPetugas(username, password);

            if (dt.Rows.Count > 0)
            {
                Session.IdPetugas = Convert.ToInt32(dt.Rows[0]["id_petugas"]);
                Session.Username = dt.Rows[0]["username"].ToString();
                return true;
            }

            return false;
        }

        public bool UpdateProfilNasabah(string username, string namaBaru, string noHpBaru)
        {
            return _nasabahModel.UpdateProfilNasabahInDb(username, namaBaru, noHpBaru);
        }
    }
}