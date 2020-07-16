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
using System.Data;
using Negocio;

namespace OnBreakApp
{
    public partial class ListadoClientes : MetroWindow
    {
        /**************
         * CONSTRUCTORES *
         **************/

        /* Recibe un valor entero que asigna visibilidad 
         * al boton seleccionar, 1 = oculto, 0 = visible
         */
        public ListadoClientes(int visibility, int visibilityDesdeContrato)
        {
            InitializeComponent();
            // Agregar datos al elemento GridClientes
            CompletarTabla(GridClientes, new DbCrud().ObtenerClientes());
            btnSeleccionar.Visibility = (Visibility)visibility;
            btnSeleccionarContrato.Visibility = (Visibility)visibilityDesdeContrato;
        }


        /**************
         * EVENTOS *
         **************/

        // CLICK MENU
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            InitWindow(new Menu());
        }

        // CLICK FILTRAR
        private void Button_Click_Filtrar(object sender, RoutedEventArgs e)
        {
            if (ValidarFormatoRut(txtRutFiltrar.Text) && txtRutFiltrar.Text.Length > 0 || txtTipoEmpresaFiltrar.Text.Length > 0 || txtActividadEmpresaFiltrar.Text.Length > 0)
            {
                CompletarTabla(GridClientes, new DbCrud().FiltrarClientes(txtRutFiltrar.Text, txtTipoEmpresaFiltrar.Text, txtActividadEmpresaFiltrar.Text));
                CleanFields();
            }
            else
            {
                CompletarTabla(GridClientes, new DbCrud().ObtenerClientes());
            }
        }

        private bool ValidarFormatoRut(string rut)
        {
            if (int.TryParse(rut, out int result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // CLICK SELECCIONAR
        private void Button_Click_Seleccionar(object sender, RoutedEventArgs e)
        {
            if (GridClientes.SelectedItem != null)
            {
                DataRowView data = (DataRowView)GridClientes.SelectedItem;
                string rut = data.Row[1].ToString();
                Cliente cliente = new DbCrud().BuscarCliente(rut);
                InitWindow(new AdminClientes(cliente));
            }
            else
            {
                NotifyUser(1);
            }
        }

        // CLICK CARGAR CONTRATOS
        private void Button_Click_Cargar(object sender, RoutedEventArgs e)
        {

            if (GridClientes.SelectedItem != null)
            {
                DataRowView data = (DataRowView)GridClientes.SelectedItem;
                string rut = data.Row[1].ToString();
                Cliente cliente = new DbCrud().BuscarCliente(rut);
                if (new DbCrud().VerificarContratosAsociados(cliente))
                {
                    InitWindow(new ListadoContratos(0, rut));
                }
                else
                {
                    NotifyUser(2);
                }
            }
            else
            {
                NotifyUser(1);
            }
        }

        // CLICK SELECCIONAR DESDE ADMIN CONTRATO
        private void Button_Click_SeleccionarDesdeContrato(object sender, RoutedEventArgs e)
        {
            if (GridClientes.SelectedItem != null)
            {
                DataRowView data = (DataRowView)GridClientes.SelectedItem;
                string rut = data.Row[1].ToString();
                Cliente cliente = new DbCrud().BuscarCliente(rut);
                InitWindow(new AdminContratos(cliente));
            }
            else
            {
                NotifyUser(1);
            }
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
            txtRutFiltrar.Text = "";
            txtTipoEmpresaFiltrar.Text = "";
            txtActividadEmpresaFiltrar.Text = "";
        }

        // Despliega un mensaje en pantalla notificando al usuario segun el caso respectivo
        private async void NotifyUser(int option)
        {
            switch (option)
            {
                case 1: await this.ShowMessageAsync("Seleccionar", "No haz seleccionado ningun cliente"); break;
                case 2: await this.ShowMessageAsync("Seleccionar", "Cliente seleccionado no tiene contratos asociados"); break;
            }
        }

    }
}
