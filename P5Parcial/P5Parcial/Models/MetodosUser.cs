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

        public DataTable Consulta(string id = "") {
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

        public void Insertar(string correo, string contrasena, string nombreusuario)
        {
            StringBuilder sqlQuery = new StringBuilder();
            SqlCommand comando = new SqlCommand();
            int resultado = 0;
            try
            {
                sqlQuery.Append(" insert into usuario values(@correo, @contrasena, @nombreusuario) ");
                comando.Parameters.Add("@correo", SqlDbType.NVarChar).Value = correo.Trim();
                comando.Parameters.Add("@contrasena", SqlDbType.NVarChar).Value = contrasena.Trim();
                comando.Parameters.Add("@nombreusuario", SqlDbType.NVarChar).Value = nombreusuario.Trim();
                resultado = objCapaDatos.Insertar(sqlQuery, comando);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public double TMB(int sexo, int altura, int peso, int frecuencia, int edad) {
            double tmb = 0;
            int calNec = frecuencia;
            double multip = 0;

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


            if (sexo == 1)   //hombre
            {
                tmb = ((10* peso) +(6.25* altura)-(5* edad) +(5));
                tmb = tmb * multip;
            }
            else if (sexo == 2)  //mujer
            {
                tmb = ((10 * peso) + (6.25 * altura) - (5 * edad) - (161));
                tmb = tmb * multip;
            }
            return tmb;
        }
    }
}