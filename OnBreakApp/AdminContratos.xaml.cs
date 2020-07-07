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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using Negocio;
using System.Globalization;
using System.Timers;
using System.Windows.Threading;
using System.Threading;

namespace OnBreakApp
{
    public partial class AdminContratos : MetroWindow
    {
        /*****************
         * CONSTRUCTORES *
         *****************/

        // Se utiliza desde el menu principal
        public AdminContratos()
        {
            InitializeComponent();
            // Completa el combobox tipo con las opciones disponibles
            PopularTipos();
        }

        // Se utiliza desde ventana auxiliar, recibe un contrato seleccionado
        public AdminContratos(Contrato contrato)
        {
            InitializeComponent();
            // Completa el combobox tipo con las opciones disponibles
            PopularTipos();
            // Completa todos los campos con la informacion del contrato seleccionado del listado
            CompletarFormulario(contrato);

            OpcionComboBox(contrato);
        }

        // Se utiliza desde ventana auxiliar, recibe un cliente seleccionado
        public AdminContratos(Cliente cliente)
        {
            InitializeComponent();
            // Completa el combobox tipo con las opciones disponibles
            PopularTipos();
            // Completa todos los campos con la informacion del contrato seleccionado del listado
            CompletarFormulario(cliente);
        }


        /***********
         * EVENTOS *
         ***********/

        // CLICK BOTON MENU
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            InitWindow(new Menu());
        }

        // CLICK BOTON BUSCAR
        private void Button_Click_Buscar(object sender, RoutedEventArgs e)
        {

            if (txtRutBuscarContrato.Text.Length == 12)
            {
                Contrato contrato = new DbCrud().BuscarContrato(txtRutBuscarContrato.Text);

                CompletarFormulario(contrato);

            }
            else
            {
                NotifyUser(4);
            }
        }

        // CLICK BOTON GUARDAR CONTRATO
        private void Button_Click_Guardar(object sender, RoutedEventArgs e)
        {
            if (cbTipoContrato.SelectedItem != null)
            {
                if (ValidarCampos(txtdireccionContrato.Text, txtObser.Text, cbTipoContrato.SelectedItem.ToString(), txtRutContrato.Text))
                {
                    if (new DbCrud().GuardarContrato(txtdireccionContrato.Text, txtObser.Text, cbTipoContrato.SelectedItem.ToString(), txtRutContrato.Text))
                    {
                        NotifyUser(1);

                    }
                    else
                    {
                        NotifyUser(6);
                    }
                }
                else
                {
                    NotifyUser(2);
                }
            }
            else
            {
                NotifyUser(2);
            }
        }

        // CLICK BOTON CALCULAR TOTAL
        private void Button_Click_CalcularTotal(object sender, RoutedEventArgs e)
        {
            if (txtNumeroContrato.Text.Length == 12 && cbTipoContrato.SelectedItem != null)
            {
                InitWindow(new CalcularTotal(txtNumeroContrato.Text, cbTipoContrato.SelectedItem.ToString()));
            }
            else if (txtRutContrato.Text.Length > 0 && txtRutContrato.Text.Length <= 9 && txtdireccionContrato.Text.Length > 0 && cbTipoContrato.SelectedItem != null)
            {
                string numeroContrato = new DbCrud().BuscarNumeroContrato(txtRutContrato.Text, txtdireccionContrato.Text, cbTipoContrato.SelectedItem.ToString());
                InitWindow(new CalcularTotal(numeroContrato, cbTipoContrato.SelectedItem.ToString()));
            }
            else
            {
                NotifyUser(7);
            }
        }

        // CLICK BOTON LISTADO CLIENTES
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitWindow(new ListadoClientes(0, 0));
        }

        // CLICK BOTON IMPRIMIR
        private void Button_Click_Terminar(object sender, RoutedEventArgs e)
        {
            if (new DbCrud().TerminarContrato(txtNumeroContrato.Text))
            {
                NotifyUser(5);
                CleanForm();
            }
            else
            {
                NotifyUser(6);
            }
        }

        // CLICK BOTON LISTADO
        private void Button_Click_Listar(object sender, RoutedEventArgs e)
        {
            InitWindow(new ListadoContratos(0));
        }

        // CUANDO TEXTO CAMBIA EN TEXTBOX RUT ASOCIADO
        private async void TxtRutContrato_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtRutContrato.Text.Length >= 8)
            {

                Task oTast = new Task(CambioTexto);
                oTast.Start();
                await oTast;

                Cliente cliente = new DbCrud().BuscarCliente(txtRutContrato.Text);
                if (cliente == null)
                {
                    txtApellido.Content = "Encontrato";
                    txtNombre.Content = "No";
                }
                else
                {
                    txtApellido.Content = cliente.Nombre;
                    txtNombre.Content = cliente.Apellido;
                }
            }
        }

        // CUANDO CAMBIA LA OPCION SELLECIONADA EN COMBO BOX TIPO EVENTO
        private void CbTipoContrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            valor_base.Content = new DbCrud().ObtenerValorBase(cbTipoContrato.SelectedItem.ToString());
        }


        /****************
         *  UTILITARIOS *
         ****************/

        // Valida que los campos obligatorios sean completados
        private bool ValidarCampos(string direccion, string observaciones, string tipo, string rut)
        {
            if (direccion.Length > 0 && observaciones.Length > 0 && tipo.Length > 0 && rut.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Inicializa cualquier ventana de tipo MetroWindow y cierra la ventana anterior
        private void InitWindow(MetroWindow window)
        {
            this.Close();
            window.ShowDialog();
        }

        // Despliega un mensaje en pantalla notificando al usuario segun el caso respectivo
        private async void NotifyUser(int option)
        {
            switch (option)
            {
                case 1: await this.ShowMessageAsync("Guardar", "Contrato guardado con exito"); break;
                case 2: await this.ShowMessageAsync("Guardar", "Todos los campos son obligatorios"); break;
                case 3: await this.ShowMessageAsync("Buscar", "El Numero de contrato ingresado no es valido"); break;
                case 4: await this.ShowMessageAsync("Buscar", "Numero de contrato no encontrado"); break;
                case 5: await this.ShowMessageAsync("Terminar", "Contrato terminado con exito"); break;
                case 6: await this.ShowMessageAsync("Guardar", "Intenta nuevamente"); break;
                case 7: await this.ShowMessageAsync("Guardar", "No haz ingreado los datos correctamente, busca un contrato o ingresa rut"); break;
            }
        }

        // Deja los campos de texto vacios de toda la ventana
        private void CleanForm()
        {
            txtRutBuscarContrato.Text = "";
            txtNumeroContrato.Text = "";
            txtFechaCreacion.Text = "";
            txtFechaTermino.Text = "";
            txtHoraInicio.Text = "";
            txtHoraTermino.Text = "";
            txtObser.Text = "";
            txtdireccionContrato.Text = "";
            txtRutContrato.Text = "";
        }

        // Añade las opciones de tipo de empresa al combo box
        private void PopularTipos()
        {
            string[] tipos = new DbCrud().ObtenerTipoEvento().ToArray();
            for (int i = 0; i < tipos.Length; i++)
            {
                cbTipoContrato.Items.Add(tipos[i]);
            }
        }

        // Completa el formulario a partir de un contrato
        private void CompletarFormulario(Contrato contrato)
        {
            if (contrato != null)
            {
                txtNumeroContrato.Text = contrato.NumeroContrato;
                txtFechaCreacion.Text = contrato.FechaCreacion;
                txtFechaTermino.Text = contrato.FechaTermino;
                txtHoraInicio.Text = contrato.HoraInicio;
                txtHoraTermino.Text = contrato.HoraTermino;
                txtObser.Text = contrato.Observaciones;
                txtdireccionContrato.Text = contrato.Direccion;
                txtRutContrato.Text = contrato.ClienteAsociado;

                if (contrato.TipoEvento.Equals("vigente"))
                {
                    rbVigente.IsChecked = true;
                }
                else
                {
                    rbNoVigente.IsChecked = true;
                }

                OpcionComboBox(contrato);
            }
            else
            {
                NotifyUser(4);
            }

        }

        // Completa el formulario a partir de un cliente
        private void CompletarFormulario(Cliente cliente)
        {
            if (cliente != null)
            {
                txtRutContrato.Text = cliente.Rut;
            }
            else
            {
                NotifyUser(4);
            }

        }

        // Selecciona opciones por defecto para los combobox
        private void OpcionComboBox(Contrato contrato)
        {
            string[] tipos = new DbCrud().ObtenerTipoEvento().ToArray();
            string nombre = new DbCrud().ObtenerNombreEvento(int.Parse(contrato.TipoEvento));
            for (int i = 0; i < tipos.Length; i++)
            {
                if (nombre == tipos[i].ToString())
                {
                    cbTipoContrato.SelectedIndex = i;
                }
            }
        }

        private void CambioTexto()
        {
            Thread.Sleep(2000);
        }
    }
}

