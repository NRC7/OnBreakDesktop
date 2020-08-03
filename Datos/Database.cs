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
            // server=localhost en este pc
            cadena = "";

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
