using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace P5Parcial.Models
{
    public class progreso
    {
        public int iduser { get; set; }
        public int progreso_peso { get; set; }
        public double progreso_imc { get; set; }
        public DateTime fecha{ get; set; }
        public DataTable tbl { get; set; }

        //public DataTable consultaProg(int id)
        //{
        //    capaDatos objCapaDatos = new capaDatos();
        //    StringBuilder sqlQuery = new StringBuilder();
        //    SqlCommand comando = new SqlCommand();
        //    DataTable tabla = new DataTable();
        //    try
        //    {
        //        sqlQuery.Append(" Select * from progreso where iduser = @usuario ");
        //        if (id > 0)
        //        {
        //            comando.Parameters.Add("@usuario", SqlDbType.Int).Value = id;
        //            tabla = objCapaDatos.EjecutarConsulta(sqlQuery, comando);
        //        }
        //        return tabla;
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Error en la consulta");
        //    }
        //}
    }
}