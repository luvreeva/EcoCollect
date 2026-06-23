using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoCollect.Models
{
    public class NasabahModel
    {
        public int IdNasabah { get; set; }
        public string NamaLengkap { get; set; }
        public string Username { get; set; }
        public string NoHp { get; set; }

        public int TotalFrekuensi { get; set; }
        public decimal TotalMassa { get; set; }

        public string Initial
        {
            get
            {
                if (string.IsNullOrWhiteSpace(NamaLengkap))
                    return "-";

                string[] nama = NamaLengkap.Trim().Split(' ');

                if (nama.Length >= 2)
                {
                    return nama[0][0].ToString().ToUpper() +
                           nama[1][0].ToString().ToUpper();
                }

                return nama[0][0].ToString().ToUpper();
            }
        }
    }
}
