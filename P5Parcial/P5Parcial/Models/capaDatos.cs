using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace P5Parcial.Models
{
    public class capaDatos
    {
        static string cadenaconexion = "Data Source = (local); Initial Catalog=dieta; Integrated Security=True";
        static SqlConnection conexion = new SqlConnection(cadenaconexion);
        public DataTable EjecutarConsulta(StringBuilder query, SqlCommand comando = null)
        {
            DataTable tabla = new DataTable();
            try
            {
                conexion.Open();
                if (comando == null)
                {
                    comando = new SqlCommand();
                }

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();
                SqlDataReader lector = comando.ExecuteReader();
                tabla.Load(lector);
                conexion.Close();

                return tabla;
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw new Exception("Error en Capa de Datos" + ex.Message);
            }
        }

        public int Insertar(StringBuilder query, SqlCommand comando)
        {
            int resultado = 0;
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();
                resultado = comando.ExecuteNonQuery();
                conexion.Close();
                return resultado;
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw new Exception("Error en Capa de Datos" + ex.Message);
            }
        }
    }
}