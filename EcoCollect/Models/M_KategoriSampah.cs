using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoCollect.Models
{
    public class KategoriSampahModel
    {
        public int IdKategori { get; set; }
        public string NamaJenis { get; set; }
        public decimal HargaPerKg { get; set; }
        public string FotoThumbnail { get; set; }
        public string Deskripsi { get; set; }
        public bool IsAktif { get; set; }

    }
}
