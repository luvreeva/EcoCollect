using EcoCollect.Helpers;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class AuthController
    {
        public bool RegisterNasabah(string nama, string username, string password, string noHp)
        {
            return AuthModel.RegisterNasabah(nama, username, password, noHp);
        }

        public int LoginNasabah(string username, string password)
        {
            AuthNasabahModel nasabah = AuthModel.GetNasabahLogin(username);

            if (nasabah == null)
            {
                return 0;
            }

            if (nasabah.Password == password)
            {
                Session.IdNasabah = nasabah.IdNasabah;
                Session.NamaNasabah = nasabah.NamaLengkap;
                Session.Username = nasabah.Username;

                return 1;
            }

            return -1;
        }

        public bool LoginPetugas(string username, string password)
        {
            AuthPetugasModel petugas = AuthModel.GetPetugasLogin(username, password);

            if (petugas == null)
            {
                return false;
            }

            Session.IdPetugas = petugas.IdPetugas;
            Session.NamaPetugas = petugas.NamaLengkap;
            Session.Username = petugas.Username;

            return true;
        }
    }
}