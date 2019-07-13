using P5Parcial.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P5Parcial.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
            
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

         [HttpPost]

        public ActionResult Index(usuario userr)
        {
            if (!string.IsNullOrEmpty(userr.correo) && !string.IsNullOrEmpty(userr.contrasena))
            {
                MetodosUser objMetodos = new MetodosUser();
                DataTable tabla = new DataTable();
                tabla = objMetodos.Consulta(userr.correo);
                if (tabla.Rows[0][1].ToString() ==  "cacha@null.com")
                {
                    return View("Ingresar");
                }
                else
                {
                    return View();
                }            
            }
            else
            {
                return View();
            }
        }
     

    }
}