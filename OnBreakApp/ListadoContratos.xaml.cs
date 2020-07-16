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
using System.Data;
using Negocio;

namespace OnBreakApp
{
    public partial class ListadoContratos : MetroWindow
    {
        /**************
         * CONSTRUCTORES *
         **************/

        /* Recibe un valor entero que asigna visibilidad 
         * al boton seleccionar, 0 = oculto, 1 = visible
         */
        // Actualmente se utiliza desde el menu principal y desde el boton listado de contratos
        public ListadoContratos(int visibility)
        {
            InitializeComponent();
            // Agregar datos al elemento GridClientes
            CompletarTabla(GridContratos, new DbCrud().ObtenerContratos());
            btnSelecionarContrato.Visibility = (Visibility)visibility;
        }

        public ListadoContratos(int visibility, string rut)
        {
            InitializeComponent();
            // Agregar datos al elemento GridClientes
            CompletarTabla(GridContratos, new DbCrud().FiltrarContratos("", rut, ""));

            btnSelecionarContrato.Visibility = (Visibility)visibility;
        }


        /***********
        *  EVENTOS *
        ************/

        // CLICK MENU
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            InitWindow(new Menu());
        }

        // CLICK FILTRAR
        private void Button_Click_Filtrar(object sender, RoutedEventArgs e)
        {
            if (txtContratoFiltrar.Text.Length > 0 || txtRutFiltrar.Text.Length > 0 || txtTipoFiltrar.Text.Length > 0)
            {
                DataTable dataTable = new DbCrud().FiltrarContratos(txtContratoFiltrar.Text, txtRutFiltrar.Text, txtTipoFiltrar.Text);
                CompletarTabla(GridContratos, dataTable);
                CleanFields();
            }
            else
            {
                CompletarTabla(GridContratos, new DbCrud().ObtenerContratos());
            }
        }

        // CLICK SELECCIONAR
        private void Button_Click_Seleccionar(object sender, RoutedEventArgs e)
        {
            if (GridContratos.SelectedItem != null)
            {
                DataRowView data = (DataRowView)GridContratos.SelectedItem;
                string numeroContrato = data.Row[1].ToString();
                Contrato contrato = new DbCrud().BuscarContrato(numeroContrato);
                InitWindow(new AdminContratos(contrato));
            }
            else
            {
                NotifyUser(1);
            }
        }

        // CLICK CARGAR CLIENTE
        private void Button_Click_Cliente(object sender, RoutedEventArgs e)
        {
            InitWindow(new ListadoClientes(1, 1));
        }


        /*****************
         *  UTILITARIOS *
         *****************/

        // Inicializa cualquier ventana de tipo MetroWindow y cierra la ventana anterior
        private void InitWindow(MetroWindow window)
        {
            this.Close();
            window.ShowDialog();
        }

        // Solicita los datos a la DB y los despliega en la tabla correspondiente
        private void CompletarTabla(DataGrid grid, DataTable dataSet)
        {
            grid.ItemsSource = dataSet.DefaultView;
        }

        // Deja los campos de texto vacios de toda la ventana
        private void CleanFields()
        {
            txtContratoFiltrar.Text = "";
            txtRutFiltrar.Text = "";
            txtTipoFiltrar.Text = "";
        }

        // Despliega un mensaje en pantalla notificando al usuario segun el caso respectivo
        private async void NotifyUser(int option)
        {
            switch (option)
            {
                case 1: await this.ShowMessageAsync("Seleccionar", "No haz seleccionado ningun contrato"); break;
            }
        }


    }
}
