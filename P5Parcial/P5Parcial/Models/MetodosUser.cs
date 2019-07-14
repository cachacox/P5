using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace P5Parcial.Models
{
    public class MetodosUser
    {
        capaDatos objCapaDatos = new capaDatos();

        public DataTable Consulta(string id = "")
        {
            StringBuilder sqlQuery = new StringBuilder();
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();
            try
            {
                sqlQuery.Append(" Select * from usuarios ");
                if (id !="")
                {
                    sqlQuery.Append(" where correo = @usuario ");
                    comando.Parameters.Add("@usuario", SqlDbType.NVarChar).Value = id;
                    tabla = objCapaDatos.EjecutarConsulta(sqlQuery,comando);
                }
                else
                {
                    tabla = objCapaDatos.EjecutarConsulta(sqlQuery);
                }
                return tabla;
            }
            catch (Exception)
            {
                throw new Exception("Error en la consulta");
            }
        }

        public DataTable consultaProg(int id)
        {
            StringBuilder sqlQuery = new StringBuilder();
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();
            try
            {
                sqlQuery.Append(" Select * from progreso where iduser = @usuario ");
                if (id > 0)
                {
                    comando.Parameters.Add("@usuario", SqlDbType.Int).Value = id;
                    tabla = objCapaDatos.EjecutarConsulta(sqlQuery, comando);
                }
                return tabla;
            }
            catch (Exception)
            {
                throw new Exception("Error en la consulta");
            }
        }

        public void Insertar(string correo, string contrasena, string nombreusuario, int peso, int altura, int sexo, int frecuencia, double tmb, double imc, int kxp, int edad)
        {
            StringBuilder sqlQuery = new StringBuilder();
            SqlCommand comando = new SqlCommand();
            int resultado = 0;
            try
            {
                sqlQuery.Append(" insert into usuarios values(@correo, @contrasena, @nombreusuario, @peso, @altura, @sexo, @frecuencia, @tmb, @imc, @edad, @kxp) ");
                comando.Parameters.Add("@correo", SqlDbType.NVarChar).Value = correo.Trim();
                comando.Parameters.Add("@contrasena", SqlDbType.NVarChar).Value = contrasena.Trim();
                comando.Parameters.Add("@nombreusuario", SqlDbType.NVarChar).Value = nombreusuario.Trim();
                comando.Parameters.Add("@peso", SqlDbType.Int).Value = peso;
                comando.Parameters.Add("@altura", SqlDbType.Int).Value = altura;
                comando.Parameters.Add("@sexo", SqlDbType.Int).Value = sexo;
                comando.Parameters.Add("@frecuencia", SqlDbType.Int).Value = frecuencia;
                comando.Parameters.Add("@tmb", SqlDbType.Float).Value = tmb;
                comando.Parameters.Add("@imc", SqlDbType.Float).Value = imc;                
                comando.Parameters.Add("@edad", SqlDbType.Int).Value = edad;
                comando.Parameters.Add("@kxp", SqlDbType.Int).Value = kxp;
                resultado = objCapaDatos.Insertar(sqlQuery, comando);

            }
            catch (Exception)
            {
                throw new Exception("Error al insertar");
            }
        }

        public double TMB(int sexo, int altura, int peso, int frecuencia, int edad, int kxp) {
            double tmb = 0;
            int calNec = frecuencia;
            double multip = 0.0;
            int kg = 0;

            switch (calNec)
            {
                case 1:
                    multip = 1.2;
                    break;
                case 2:
                    multip = 1.375;
                    break;
                case 3:
                    multip = 1.55;
                    break;
                case 4:
                    multip = 1.725;
                    break;
                case 5:
                    multip = 1.9;
                    break;
            }

            switch (kxp)
            {
                case 1:
                    kg = 1000;
                    break;
                case 2:
                    kg = 500;
                    break;
            }

            if (sexo == 1)   //hombre
            {
                tmb = ((10* peso) +(6.25* altura)-(5* edad) +(5));
                tmb = (tmb * multip) - kg;
            }
            else if (sexo == 2)  //mujer
            {
                tmb = ((10 * peso) + (6.25 * altura) - (5 * edad) - (161));
                tmb = (tmb * multip) - kg;
            }
            return tmb;
        }

        public double IMC(int peso, int altura) {
            double indice = 0.0;
            double alturadouble = altura;
            alturadouble = alturadouble / 100;

            if (peso>0 || altura>0)
            {
                indice = Math.Round(((peso)/(Math.Pow(alturadouble, 2))),2);
            }
            return indice;
        }

        public void InsertarProgreso(int idusuario, int prog_peso, double prog_imc)
        {
            StringBuilder sqlQuery = new StringBuilder();
            SqlCommand comando = new SqlCommand();
            int resultado = 0;
            try
            {
                sqlQuery.Append(" insert into progreso values(@iduser, @progreso_peso, @progreso_imc) ");
                comando.Parameters.Add("@iduser", SqlDbType.Int).Value = idusuario;
                comando.Parameters.Add("@progreso_peso", SqlDbType.Int).Value = prog_peso;
                comando.Parameters.Add("@progreso_imc", SqlDbType.Float).Value = prog_imc;
                resultado = objCapaDatos.Insertar(sqlQuery, comando);

            }
            catch (Exception)
            {
                throw new Exception("Error al insertar progreso");
            }
        }
    }
}