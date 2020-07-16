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
        // VARIABLES GLOBALES
        Cliente cliente = null;

        /*****************
         * CONSTRUCTORES *
         *****************/

        // Se utiliza desde el menu principal
        public AdminContratos()
        {
            InitializeComponent();
            PopularTipos();
        }

        // Se utiliza desde ventana auxiliar, recibe un contrato seleccionado
        public AdminContratos(Contrato contrato)
        {
            InitializeComponent();
            // Completa el combobox tipo con las opciones disponibles
            PopularTipos();
            if (contrato != null)
            {
                // Completa todos los campos con la informacion del contrato seleccionado del listado
                CompletarFormulario(contrato);
                OpcionComboBox(contrato);
            }
        }

        // Se utiliza desde ventana auxiliar, recibe un cliente seleccionado
        public AdminContratos(Cliente cliente)
        {
            InitializeComponent();
            // Completa el combobox tipo con las opciones disponibles
            PopularTipos();
            // Completa todos los campos con la informacion del contrato seleccionado del listado
            CompletarFormulario(cliente);

            ActivarComponentes();
        }


        /***********
         * EVENTOS *
         ***********/

        // EVENTO QUE CONFIRMA QUE EL RUT INGRESADO ESTA REGISTRADO Y ACTIVA LOS COMPONENTES DEL REGISTRO
        private void Button_Confirmar_Rut(object sender, RoutedEventArgs e)
        {
            string rutIngresado = txtRutContrato.Text;

            if (ValidarFormatoRut(rutIngresado))
            {
                cliente = new DbCrud().BuscarCliente(rutIngresado);
                if (cliente != null)
                {
                    txtNombre.Content = cliente.Nombre + " " + cliente.Apellido;
                    ActivarComponentes();
                }
                else
                {
                    txtNombre.Content = "No Registrado";
                }
            }
            else
            {
                NotifyUser(8);
            }
        }

        // EVENTO QUE ACTIVA GENERAR NUMERO DE CONTRATO
        private void Button_Generar_Numero_Contrato(object sender, RoutedEventArgs e)
        {
            if (txtNumeroContrato.Content.ToString().Equals("Generar N° Contrato")
                && ValidarFormatoRut(txtRutContrato.Text))
            {
                txtNumeroContrato.Content = new DbCrud().GenerarNumeroContrato();
            }
        }

        // EVENTO QUE ABRE VENTANA LISTADO DE CLIENTES
        private void Button_Listado_Clientes(object sender, RoutedEventArgs e)
        {
            InitWindow(new ListadoClientes(1, 0));
        }

        // CLICK BOTON MENU
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            InitWindow(new Menu());
        }

        // CLICK BOTON BUSCAR
        private void Button_Click_Buscar(object sender, RoutedEventArgs e)
        {
            if (ValidarFormatoNumeroContrato(txtRutBuscarContrato.Text))
            {
                Contrato contrato = new DbCrud().BuscarContrato(txtRutBuscarContrato.Text);
                if (contrato != null)
                {
                    CompletarFormulario(contrato);
                    Cliente cliente = new DbCrud().BuscarCliente(contrato.ClienteAsociado);
                    txtNombre.Content = cliente.Nombre + " " + cliente.Apellido;
                }
                else
                {
                    NotifyUser(4);
                    LimpiarCamposInterfaz();
                }
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
                
                if (ValidarCampos(txtdireccionContrato.Text, txtObser.Text, cbTipoContrato.SelectedItem.ToString())
                    && ValidarFormatoRut(txtRutContrato.Text)
                    && ValidarFormatoNumeroContrato(txtNumeroContrato.Content.ToString())
                    && ValidarFormatoFecha(txtFechaTermino.Text) && ValidarFormatoHora(txtHoraTermino.Text)
                    && ValidarFormatoFecha(txtFechaInicio.Text) && ValidarFormatoHora(txtHoraInicio.Text))
                {
                    if (new DbCrud().GuardarContrato(txtNumeroContrato.Content.ToString(), txtFechaTermino.Text, txtHoraTermino.Text,
                        txtdireccionContrato.Text, cbTipoContrato.SelectedItem.ToString(), txtObser.Text, txtRutContrato.Text,
                        txtFechaInicio.Text, txtHoraInicio.Text))
                    {
                        NotifyUser(1);
                        InitWindow(new CalcularTotal(txtNumeroContrato.Content.ToString(), cbTipoContrato.SelectedItem.ToString()));
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

        // CLICK BOTON TERMINAR CONTRATO
        private void Button_Click_Terminar(object sender, RoutedEventArgs e)
        {
            if (new DbCrud().TerminarContrato(txtNumeroContrato.Content.ToString()))
            {
                NotifyUser(5);
                LimpiarCamposInterfaz();
            }
            else
            {
                NotifyUser(6);
            }
        }

        // CLICK BOTON LISTADO DE CONTRATOS
        private void Button_Click_Listar(object sender, RoutedEventArgs e)
        {
            InitWindow(new ListadoContratos(0));
        }

        // CUANDO CAMBIA LA OPCION SELLECIONADA EN COMBO BOX TIPO EVENTO
        private void CbTipoContrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            valor_base.Content = new DbCrud().ObtenerValorBase(cbTipoContrato.SelectedItem.ToString()) + " UF";
        }


        /****************
         *  UTILITARIOS *
         ****************/

        // VALIDA QUE EL RUT INGRESADO CONTENGA SOLO NUMEROS Y CUMPLA CON LA LONGUITUD VALIDA
        private bool ValidarFormatoRut(string rut)
        {
            if (int.TryParse(rut, out int result) && rut.Length >= 8 && rut.Length <= 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // ACTIVA LOS CAMPOS OBLIGATORIOS CUANDO TXTRUTCONTRATO CONTIENE UN RUT VALIDO
        public void ActivarComponentes()
        {
            txtFechaInicio.IsEnabled = true;
            txtHoraInicio.IsEnabled = true;
            txtdireccionContrato.IsEnabled = true;
            txtFechaTermino.IsEnabled = true;
            txtHoraTermino.IsEnabled = true;
            txtObser.IsEnabled = true;
        }

        // AÑADE LAS OPCIONES DE TIPO_EVENTO AL COMBO BOX
        private void PopularTipos()
        {
            string[] tipos = new DbCrud().ObtenerTipoEvento().ToArray();
            for (int i = 0; i < tipos.Length; i++)
            {
                cbTipoContrato.Items.Add(tipos[i]);
            }
        }

        // VALIDA QUE EL RUT INGRESADO CUMPLA CON EL FORMATO DESEADO
        private bool ValidarFormatoNumeroContrato(string numero)
        {
            if (numero.Trim().Length == 12)
            {
                for (int i = 0; i < numero.Length; i++)
                {
                    if (!int.TryParse(numero[i].ToString(), out int result))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        // VALIDA QUE LA FECHA INGRESADA CUMPLA CON EL FORMATO SOLICITADO AAAA-MM-DD
        private bool ValidarFormatoFecha(string fecha)
        {
            if (fecha.Length == 10 && fecha.Contains('-'))
            {
                string[] fechaSeparada = fecha.Split('-');
                if (int.TryParse(fechaSeparada[0], out int result0)
                    && int.TryParse(fechaSeparada[1], out int result1)
                    && int.TryParse(fechaSeparada[2], out int result2)
                    && fechaSeparada[0] != "0000"
                    && fechaSeparada[1] != "00"
                    && fechaSeparada[2] != "00"
                    && int.Parse(fechaSeparada[0]) == DateTime.Today.Year
                    && int.Parse(fechaSeparada[1]) > 0
                    && int.Parse(fechaSeparada[1]) <= 12
                    && int.Parse(fechaSeparada[2]) > 0
                    && int.Parse(fechaSeparada[2]) <= 31)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // VALIDA QUE LA HORA INGRESADA CUMPLA CON EL FORMATO SOLICITADO HH:MM
        private bool ValidarFormatoHora(string hora)
        {
            if (hora.Length == 5 && hora.Contains(':'))
            {
                string[] horaSeparada = hora.Split(':');
                if (int.TryParse(horaSeparada[0], out int result0)
                    && int.TryParse(horaSeparada[1], out int result1)
                    && int.Parse(horaSeparada[0]) >= 0
                    && int.Parse(horaSeparada[0]) <= 24
                    && int.Parse(horaSeparada[0]) >= 0
                    && int.Parse(horaSeparada[0]) <= 60)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Valida que los campos obligatorios sean completados
        private bool ValidarCampos(string direccion, string observaciones, string tipo)
        {
            if (direccion.Length > 0 && observaciones.Length > 0 && tipo.Length > 0)
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
                case 8: await this.ShowMessageAsync("Guardar", "El Rut ingresado no es válido"); break;
            }
        }

        // Deja los campos de texto vacios de toda la ventana
        private void LimpiarCamposInterfaz()
        {
            txtRutBuscarContrato.Text = "";
            txtRutContrato.Text = "";
            txtNombre.Content = "Confirma el Rut ingresado";
            txtNumeroContrato.Content = "Generar N° Contrato";
            txtFechaInicio.Text = "";
            txtHoraInicio.Text = "";
            txtdireccionContrato.Text = "";
            txtFechaTermino.Text = "";
            txtHoraTermino.Text = "";
            txtEstado.Content = "";
            valor_base.Content = "Selecciona el tipo de evento";
            txtObser.Text = "";
        }

        // Completa el formulario a partir de un contrato
        private void CompletarFormulario(Contrato contrato)
        {
            if (contrato != null)
            {
                txtNumeroContrato.Content = contrato.NumeroContrato;
                txtFechaInicio.Text = contrato.FechaInicio;
                txtFechaTermino.Text = contrato.FechaTermino;
                txtHoraInicio.Text = contrato.HoraInicio;
                txtHoraTermino.Text = contrato.HoraTermino;
                txtObser.Text = contrato.Observaciones;
                txtdireccionContrato.Text = contrato.Direccion;
                txtRutContrato.Text = contrato.ClienteAsociado;
                txtEstado.Content = contrato.EstadoContrato;

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
            string nombre = contrato.TipoDeEvento;
            for (int i = 0; i < tipos.Length; i++)
            {
                if (nombre == tipos[i].ToString())
                {
                    cbTipoContrato.SelectedIndex = i;
                }
            }
        }
    }
}

