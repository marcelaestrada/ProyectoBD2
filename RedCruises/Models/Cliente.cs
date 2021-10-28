using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedCruises.Models
{
    public class Cliente
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DPI { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
    }
}