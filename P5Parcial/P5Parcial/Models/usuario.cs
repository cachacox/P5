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

        public usuario()
        {
            correo = "";
            contrasena = "";
            nombreusuario = "";
        }
    }
}