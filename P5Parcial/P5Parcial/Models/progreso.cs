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
        capaDatos objCapaDatos = new capaDatos();

        public DataTable consultaProg(int id)
        {
            capaDatos objCapaDatos = new capaDatos();
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

        public void InsertarProgreso(int idusuario, int prog_peso, double prog_imc, DateTime fecha)
        {
            StringBuilder sqlQuery = new StringBuilder();
            SqlCommand comando = new SqlCommand();
            int resultado = 0;
            try
            {
                sqlQuery.Append(" insert into progreso values(@iduser, @progreso_peso, @progreso_imc, @fecha) ");
                comando.Parameters.Add("@iduser", SqlDbType.Int).Value = idusuario;
                comando.Parameters.Add("@progreso_peso", SqlDbType.Int).Value = prog_peso;
                comando.Parameters.Add("@progreso_imc", SqlDbType.Float).Value = prog_imc;
                comando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha;
                resultado = objCapaDatos.Insertar(sqlQuery, comando);

            }
            catch (Exception)
            {
                throw new Exception("Error al insertar progreso");
            }
        }

        public double IMC(int peso, int altura)
        {
            double indice = 0.0;
            double alturadouble = altura;
            alturadouble = alturadouble / 100;

            if (peso > 0 || altura > 0)
            {
                indice = Math.Round(((peso) / (Math.Pow(alturadouble, 2))), 2);
            }
            return indice;
        }
    }
}