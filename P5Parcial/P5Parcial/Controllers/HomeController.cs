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

        public ActionResult Ingresar()
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

                if (tabla.Rows.Count >0)
                {
                    if (tabla.Rows[0][1].ToString() == userr.correo && tabla.Rows[0][2].ToString() == userr.contrasena)
                    {
                        return View("Ingresar");
                    }
                    else
                    {
                        ViewBag.mensaje = "Contraseña o Usuario inválido";
                        return View();
                    }
                }
                else
                {
                    ViewBag.mensaje = "Usuario no existe, regístrese";
                    return View();
                }           
            }
            else
            {
                ViewBag.mensaje = "Debe completar todos los campos";
                return View();
            }
        }
     

    }
}