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

        public ActionResult Ingresar(int id)
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
                DataTable temptabla = new DataTable();
                int idu = 0;
                tabla = objMetodos.Consulta(userr.correo);
                if (tabla.Rows.Count >0)
                {
                    idu = Convert.ToInt32(tabla.Rows[0][0]);
                    if (tabla.Rows[0][1].ToString() == userr.correo && tabla.Rows[0][2].ToString() == userr.contrasena)
                    {
                        temptabla = objMetodos.consultaProg(idu);
                        if (temptabla.Rows.Count == 0)
                        {
                            objMetodos.InsertarProgreso(idu, Convert.ToInt32(tabla.Rows[0][4]), Convert.ToInt32(tabla.Rows[0][9]));
                        }
                        return View("Ingresar",idu);
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

        public ActionResult Registrar(usuario ussuario) {
            MetodosUser objMetodos = new MetodosUser();
            DataTable tabla = new DataTable();
            if (ModelState.IsValid)
            {
                tabla = objMetodos.Consulta(ussuario.correo);
                if (tabla.Rows.Count >0)
                {
                    ViewBag.msg = "Esta cuenta de correo ya esta ligada a un usuario";
                    return View();
                }
                else
                {
                    double tmb = 0.0;
                    double imc = 0.0;
                    tmb = objMetodos.TMB(ussuario.sexo, ussuario.altura, ussuario.peso, ussuario.frecuencia, ussuario.edad, ussuario.kxp);
                    imc = objMetodos.IMC(ussuario.peso, ussuario.altura);
                    objMetodos.Insertar(ussuario.correo, ussuario.contrasena, ussuario.nombreusuario, ussuario.peso, ussuario.altura, ussuario.sexo, ussuario.frecuencia, tmb, imc, ussuario.kxp, ussuario.edad);    
                    return RedirectToAction("Index");
                }                 
            }
            else
            {
                ViewBag.msg = "Debe completar todos los campos";
                return View();
            }            
        }

    }
}