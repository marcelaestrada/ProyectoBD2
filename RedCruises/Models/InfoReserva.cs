using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedCruises.Models
{
    public class InfoReserva
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DPI { get; set; }
        public int Camarote { get; set; }
        public string Origen { get; set; }
        public DateTime Salida { get; set; }
        public double Confirmacion { get; set; }
        public string Canal { get; set; }
    }
}