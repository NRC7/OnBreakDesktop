using System;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    class Database
    {
        MySqlConnection conexion = null;
        string cadena = null;

        public MySqlConnection InitConection()
        {
            cadena = "Server=dbonbreak.c3zq6eobr3dm.us-east-1.rds.amazonaws.com;Port=3306;Database=Onbreak;Uid=admin;Pwd=topolino123;;Convert Zero Datetime=true";

            conexion = new MySqlConnection(cadena);

            try
            {
                conexion.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida");
            }
            return conexion;
        }
    }
}
