using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoCollect.Controllers
{
    // PILAR OOP: INTERFACE
    // Ini adalah kontrak standar. Semua controller yang mengimplementasikan 
    // interface ini wajib memiliki method Tambah().
    public interface ICrud_Controller
    {
        bool Tambah();
    }
}
