using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundacion.Ado
{
    public class Conexion
    {
        SqlConnection conexion;
        SqlCommand cmd;
        SqlDataReader dr = null;

        public SqlDataReader Dr { get => dr; set => dr = value; }
        public SqlCommand Cmd { get => cmd; set => cmd = value; }

        public Conexion()
        {
            conexion = new SqlConnection("Server=LAPTOP-9ALNHCMO\\SQLEXPRESS;Database=db_Fundacion_Final;User ID=sa;Password=1234;Trusted_Connection=false;");
            Cmd = new SqlCommand();

        }

        

        public void Leer()
        {
            AbrirConexion();
            Cmd.CommandText = "select p.Id, p.Titulo,p.[Pais-region],p.Contratante, a.Area from Proyectos p join AreasxProyecto ap on p.Id = ap.IdProyecto join Areas a on a.Id = ap.IdArea";
            Dr = Cmd.ExecuteReader();
        }


        public void AbrirConexion()
        {
            conexion.Open();
            Cmd.Connection = conexion;
        }


        public void CerrarConexion()
        {
            conexion.Close();
        }
    }
}
