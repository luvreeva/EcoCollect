using System;
using System.Collections.Generic;
using EcoCollect.Models;

namespace EcoCollect.Controllers
{
    public class SetoranController : ICrud_Controller
    {
        // Menyimpan instansiasi objek model
        private SetoranModel _model = new SetoranModel();

        // Property bawaan interface ICrud_Controller tetap dipertahankan
        public int IdNasabah { get; set; }
        public int IdPetugas { get; set; }
        public int IdKategori { get; set; }
        public decimal BeratKg { get; set; }
        public decimal HargaSaatTransaksi { get; set; }
        public decimal Subtotal { get; set; }

        public bool Tambah() => false;

        // Memanggil fungsi pencarian data dari Model
        public NasabahModel GetNasabahById(int idNasabah)
        {
            return _model.GetNasabahById(idNasabah);
        }

        public List<KategoriSampahModel> GetKategoriSampah()
        {
            return _model.GetKategoriSampah();
        }

        public string GenerateKodeTransaksi()
        {
            return "TX-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

      
        public int SimpanSetoran(SetoranModel setoran)
        {
            if (setoran == null)
                throw new Exception("Data setoran tidak boleh kosong.");

            if (setoran.IdNasabah <= 0)
                throw new Exception("Nasabah belum dipilih.");

            if (setoran.IdPetugas <= 0)
                throw new Exception("Petugas belum terdeteksi. Silakan login ulang.");

            if (setoran.DetailSetoran == null || setoran.DetailSetoran.Count == 0)
                throw new Exception("Detail sampah belum diisi.");

    
            return setoran.ExecuteSimpanSetoran();
        }

        public StrukSetoranModel GetStrukSetoran(int idSetor)
        {
            return _model.GetStrukSetoran(idSetor);
        }
    }
}