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
        [HttpGet]
        public ActionResult Index()
        {
            return View();            
        }
        [HttpPost]
        public ActionResult Index(usuario userr)
        {
            if (!string.IsNullOrEmpty(userr.correo) && !string.IsNullOrEmpty(userr.contrasena))
            {
                int idu = 0;
                DataTable tabla = new DataTable();
                DataTable temptabla = new DataTable();
                DateTime fecha = DateTime.Now;
                string nombre = userr.correo;
                tabla = userr.Consulta(nombre);
                usuario.tablamain = userr.Consulta(nombre);
                if (tabla.Rows.Count > 0)
                {
                    idu = Convert.ToInt32(tabla.Rows[0][0]);
                    if (usuario.tablamain.Rows[0][1].ToString() == userr.correo && usuario.tablamain.Rows[0][2].ToString() == userr.contrasena)
                    {
                        usuario.usetbl = userr.consultaProg(Convert.ToInt32(usuario.tablamain.Rows[0][0]));
                        if (usuario.usetbl.Rows.Count == 0)
                        {
                            userr.InsertarProgreso(idu, Convert.ToInt32(usuario.tablamain.Rows[0][4]), Convert.ToInt32(usuario.tablamain.Rows[0][9]), fecha);
                        }
                        return RedirectToAction("Ingresar",userr);
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
        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Ingresar(usuario use)
        {
            usuario.usetbl = use.consultaProg(Convert.ToInt32(usuario.tablamain.Rows[0][0]));
            return View(use);
        }
        [HttpPost]
        public ActionResult Registrar(usuario ussuario) {
            DataTable tabla = new DataTable();
            if (ModelState.IsValid)
            {
                tabla = ussuario.Consulta(ussuario.correo);
                if (tabla.Rows.Count >0)
                {
                    ViewBag.msg = "Esta cuenta de correo ya esta ligada a un usuario";
                    return View();
                }
                else
                {
                    double tmb = 0.0;
                    double imc = 0.0;
                    string corporal = "";
                    tmb = ussuario.TMB(ussuario.sexo, ussuario.altura, ussuario.peso, ussuario.frecuencia, ussuario.edad, ussuario.kxp);
                    imc = ussuario.IMC(ussuario.peso, ussuario.altura);
                    corporal = ussuario.composicionCorporal(imc);
                    ussuario.Insertar(ussuario.correo, ussuario.contrasena, ussuario.nombreusuario, ussuario.peso, ussuario.altura, ussuario.sexo, ussuario.frecuencia, tmb, imc, ussuario.kxp, ussuario.edad, corporal);    
                    return RedirectToAction("Index");
                }                 
            }
            else
            {
                ViewBag.msg = "Debe completar todos los campos";
                return View();
            }            
        }
        [HttpPost]
        public ActionResult Ingresar() {          
            return RedirectToAction("Ingresar");
        }

        public ActionResult renovarProg(int nuevopeso) {
            progreso objProg = new progreso();
            DateTime fecha = DateTime.Now;
            double nuevoimc = 0.0;
            nuevoimc = objProg.IMC(nuevopeso, Convert.ToInt32(usuario.tablamain.Rows[0][5]));
            objProg.InsertarProgreso(Convert.ToInt32(usuario.tablamain.Rows[0][0]), nuevopeso,nuevoimc, fecha);  
            return RedirectToAction("Ingresar");
        }

        public ActionResult salir() {
            return RedirectToAction("Index");
        }
    }
}