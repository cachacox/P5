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
        usuario usb = new usuario();
        progreso objprogreso = new progreso();      
        int identificador = 0;
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
                DataTable tabla = new DataTable();
                DataTable temptabla = new DataTable();
                int idu = 0;
                DateTime fecha = DateTime.Now;
                string nombre = userr.correo;
                tabla = userr.Consulta(nombre);
                if (tabla.Rows.Count > 0)
                {
                    idu = Convert.ToInt32(tabla.Rows[0][0]);
                    //ViewBag.identificador = tabla.Rows[0][0];
                    //identificador = Convert.ToInt32(tabla.Rows[0][0]);

                    if (tabla.Rows[0][1].ToString() == userr.correo && tabla.Rows[0][2].ToString() == userr.contrasena)
                    {
                        userr.iduser = idu;
                        userr.nombreusuario = tabla.Rows[0][3].ToString();
                        userr.peso = Convert.ToInt32(tabla.Rows[0][4]);
                        userr.altura = Convert.ToInt32(tabla.Rows[0][5].ToString());
                        userr.sexo = Convert.ToInt32(tabla.Rows[0][6].ToString());
                        userr.frecuencia = Convert.ToInt32(tabla.Rows[0][7].ToString());
                        userr.tmb = Convert.ToDouble(tabla.Rows[0][8].ToString());
                        userr.imc = Convert.ToDouble(tabla.Rows[0][9].ToString());
                        userr.edad = Convert.ToInt32(tabla.Rows[0][10].ToString());
                        userr.kxp = Convert.ToInt32(tabla.Rows[0][11].ToString());
                        userr.corporal = tabla.Rows[0][12].ToString();

                        temptabla = userr.consultaProg(idu);
                        if (temptabla.Rows.Count == 0)
                        {
                            userr.InsertarProgreso(idu, Convert.ToInt32(tabla.Rows[0][4]), Convert.ToInt32(tabla.Rows[0][9]), fecha);
                        }
                        ViewBag.tablita = tabla;
                        return RedirectToAction("Ingresar",userr);
                        //return View("Ingresar", userr);
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
            return View(use);
        }
        [HttpGet]
        public ActionResult Progreso()
        {
            //DataTable tabela = new DataTable();
            //tabela = usb.Consulta(usb.correo);
            //identificador = Convert.ToInt32(tabela.Rows[0][0]);
            return View();
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
        public ActionResult Ingresar(progreso prog) {
            return View("Progreso");
        }
    }
}