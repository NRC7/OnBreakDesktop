using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
// Referencias especificas
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

namespace OnBreakApp
{

    public partial class Menu : MetroWindow
    {

        /*****************
         * CONSTRUCTORES *
         *****************/
        public Menu()
        {
            InitializeComponent();
        }

        /***********
         * EVENTOS *
         ***********/

        // CLICK ADMIN CLIENTES
        private void Button_Click_AdminClientes(object sender, RoutedEventArgs e)
        {
            InitWindow(new AdminClientes());
        }

        // CLICK LISTADO CLIENTES
        private void Button_Click_ListadoClientes(object sender, RoutedEventArgs e)
        {
            InitWindow(new ListadoClientes(1, 1));
        }

        // CLICK ADMIN CONTRATOS
        private void Button_Click_AdminContratos(object sender, RoutedEventArgs e)
        {
            InitWindow(new AdminContratos());
        }

        // CLICK LISTADO CONTRATOS
        private void Button_Click_ListadoContratos(object sender, RoutedEventArgs e)
        {
            InitWindow(new ListadoContratos(1));
        }

        // CLICK CONFIGURACION
        private void Button_Click_Configuracion(object sender, RoutedEventArgs e)
        {
            // ConfigScreen config = new ConfigScreen(); 
            // InitWindow(config);
        }

        // CLICK AYUDA
        private void Button_Click_Ayuda(object sender, RoutedEventArgs e)
        {
            // HelpScreen help = new HelpScreen(); 
            // InitWindow(help);
        }


        /****************
        *  UTILITARIOS *
        ****************/

        // Inicializa cualquier ventana de tipo MetroWindow y cierra la ventana anterior
        private void InitWindow(MetroWindow window)
        {
            this.Close();
            window.ShowDialog();
        }
    }
}
