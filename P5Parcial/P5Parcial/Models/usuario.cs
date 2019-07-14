using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P5Parcial.Models
{
    public class usuario
    {
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string nombreusuario { get; set; }
        public int peso { get; set; }
        public int altura { get; set; }
        public int sexo { get; set; }
        public int frecuencia { get; set; }
        public double tmb { get; set; }
        public double imc { get; set; }
        public int edad { get; set; }
        public int kxp { get; set; }

        public usuario()
        {
            correo = "";
            contrasena = "";
            nombreusuario = "";
            peso = 0;
            altura = 0;
            sexo = 0;
            frecuencia = 0;
            tmb = 0.0;
            imc = 0.0;
            edad = 0;
            kxp = 0;
        }
    }
}