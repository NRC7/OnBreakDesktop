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
using Negocio;

namespace OnBreakApp
{
    public partial class AdminClientes : MetroWindow
    {
        // Arreglos de opciones combobox
        private readonly string[] tipos = { "SPA", "EIRL", "Limitada", "Sociedad Anonima" };
        private readonly string[] actividades = { "Agropecuaria", "Minería", "Manufactura", "Comercio", "Hoteleria", "Alimentos", "Transporte", "Servicios" };
        Cliente cliente = null;
        /*****************
         * CONSTRUCTORES *
         *****************/

        // Se utiliza desde el menu principal
        public AdminClientes()
        {
            InitializeComponent();
            // Completa el combo box con las opciones habilitadas
            PopularTipos(1, cbType);
            PopularTipos(2, cbActividad);
        }

        // Se utiliza desde ventana auxiliar, recibe un cliente seleccionado
        public AdminClientes(Cliente cliente)
        {
            InitializeComponent();
            // Completa todos los campos con la informacion del cliente seleccionado en el listado
            CompletarFormulario(cliente);
            this.cliente = cliente;
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
            string rut = txtRutBuscar.Text;

            if (rut.Length <= 0 || rut.Length > 9)
            {
                NotifyUser(3);
            }
            else
            {
                Cliente cliente = new DbCrud().BuscarCliente(rut);
                if (cliente != null)
                {
                    CompletarFormulario(cliente);
                }
                else
                {
                    NotifyUser(4);
                }
            }
        }

        // CLICK BOTON ELIMINAR
        private void Button_Click_Eliminar(object sender, RoutedEventArgs e)
        {
            //txtRut.Text.Length > 0 && txtRut.Text != null
            if (cliente != null)
            {
                if (new DbCrud().EliminarCliente(cliente))
                {
                    NotifyUser(7);
                }
                else
                {
                    NotifyUser(5);
                }
            }
            else
            {
                if (txtRut.Text.Length > 0 && txtRut.Text != null && new DbCrud().EliminarCliente(txtRut.Text))
                {
                    NotifyUser(7);
                }
                else
                {
                    NotifyUser(5);
                }
            }
            CleanForm();
        }

        // CLICK BOTON LISTADO
        private void Button_Click_Listar(object sender, RoutedEventArgs e)
        {
            CleanForm();
            InitWindow(new ListadoClientes(0, 1));
        }

        // CLICK BOTON ACTUALIZAR
        private void Button_Click_Actualizar(object sender, RoutedEventArgs e)
        {
            if (txtRut.Text.Length > 0
                && cbType.SelectedItem != null
                && cbActividad.SelectedItem != null
                && ValidarFormatoRut(txtRut.Text)
                && ValidarFormatoRut(txtPhoneNumber.Text)
                && new DbCrud().ActualizarCliente(txtRut.Text, txtName.Text,
                txtLastName.Text, txtAddress.Text, txtPhoneNumber.Text,
                txtEmail.Text, cbType.SelectedItem.ToString(), cbActividad.SelectedItem.ToString()))
            {
                NotifyUser(8);
            }
            else
            {
                NotifyUser(6);
            }
            CleanForm();
        }

        // CLICK BOTON GUARDAR CLIENTE
        private void Button_Click_Guardar(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos(txtRut.Text, txtName.Text, txtLastName.Text, txtAddress.Text, txtPhoneNumber.Text, txtEmail.Text)
                && cbType.SelectedItem != null && cbActividad.SelectedItem != null && ValidarFormatoRut(txtRut.Text))
            {
                if (new DbCrud().GuardarCliente(txtRut.Text, txtName.Text, txtLastName.Text, txtAddress.Text, txtPhoneNumber.Text, txtEmail.Text, cbType.SelectedItem.ToString(), cbActividad.SelectedItem.ToString()))
                {

                    NotifyUser(1);

                    CleanForm();
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


        /****************
         *  UTILITARIOS *
         ****************/

        // Valida que la cadena solo tenga caracteres numericos 
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

        // Valida que todos los campos del formulario hayan sido completados al guardar un nuevo cliente
        private bool ValidarCampos(string name, string lastName, string rut, string address, string phoneNumber, string email)
        {
            if (name.Length > 0 && lastName.Length > 0 && rut.Length > 0 && address.Length > 0 &&
                phoneNumber.Length > 0 && email.Length > 0 && email.Contains('@'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Valida que los datos ingresados en el campo sean distintos a los que tiene el cliente ingresado
        private Cliente ValidarCliente(Cliente cliente)
        {
            if (cbType.SelectedItem != null && cbActividad.SelectedItem != null && ValidarFormatoRut(txtPhoneNumber.Text))
            {
                cliente.TipoEmpresa = cbType.SelectedItem.ToString();
            }

            if (cbActividad.SelectedItem != null && cliente.ActividadEmpresa != cbActividad.SelectedItem.ToString())
            {
                cliente.ActividadEmpresa = cbActividad.SelectedItem.ToString();
            }

            if (cliente.Nombre != txtName.Text && txtName.Text.Length > 0)
            {
                cliente.Nombre = txtName.Text;
            }

            if (cliente.Apellido != txtLastName.Text && txtLastName.Text.Length > 0)
            {
                cliente.Apellido = txtLastName.Text;
            }

            if (cliente.Direccion != txtAddress.Text && txtAddress.Text.Length > 0)
            {
                cliente.Direccion = txtAddress.Text;
            }

            if (cliente.Telefono != txtPhoneNumber.Text && txtPhoneNumber.Text.Length > 0)
            {
                cliente.Direccion = txtAddress.Text;
            }

            if (cliente.Email != txtEmail.Text && txtEmail.Text.Length > 0)
            {
                cliente.Email = txtEmail.Text;
            }

            return cliente;
        }

        // Inicializa cualquier ventana de tipo MetroWindow y cierra la ventana anterior
        private void InitWindow(MetroWindow window)
        {
            this.Close();
            window.ShowDialog();
        }

        // Añade las opciones respectivas a cada combobox
        private void PopularTipos(int option, ComboBox box)
        {
            switch (option)
            {
                case 1:
                    for (int i = 0; i < tipos.Length; i++)
                    {
                        box.Items.Add(tipos[i]);
                    }
                    box.SelectedItem = 0; break;
                case 2:
                    for (int i = 0; i < actividades.Length; i++)
                    {
                        box.Items.Add(actividades[i]);
                    }
                    box.SelectedItem = 0; break;
            }
        }

        // Deja los campos de texto de toda la ventana vacios
        private void CleanForm()
        {
            txtName.Text = "";
            txtLastName.Text = "";
            txtRut.Text = "";
            txtAddress.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            txtRutBuscar.Text = "";
        }

        // Despliega un mensaje en pantalla notificando al usuario segun cada caso
        private async void NotifyUser(int option)
        {
            switch (option)
            {
                case 1: await this.ShowMessageAsync("Guardar", "Cliente guardado con exito"); break;
                case 2: await this.ShowMessageAsync("Guardar", "Todos los datos son obligatorios"); break;
                case 3: await this.ShowMessageAsync("Buscar", "El Rut ingresado no es valido"); break;
                case 4: await this.ShowMessageAsync("Buscar", "Rut no encontrado"); break;
                case 5: await this.ShowMessageAsync("Eliminar", "Cliente no puede ser eliminado"); break;
                case 6: await this.ShowMessageAsync("Guardar", "Intentalo nuevamente"); break;
                case 7: await this.ShowMessageAsync("Eliminar", "Cliente eliminado con exito"); break;
                case 8: await this.ShowMessageAsync("Actualizar", "Cliente actualizado con exito"); break;
            }
        }

        // Selecciona opciones por defecto para los combobox
        private void OpcionComboBox(Cliente cliente)
        {
            for (int i = 0; i < tipos.Length; i++)
            {
                if (cliente.TipoEmpresa.Equals(tipos[i]))
                {
                    cbType.SelectedIndex = i;
                }
            }

            for (int i = 0; i < actividades.Length; i++)
            {
                if (cliente.ActividadEmpresa.Equals(actividades[i]))
                {
                    cbActividad.SelectedIndex = i;
                }
            }
        }

        // Completa todos los campos con la informacion del cliente seleccionado
        private void CompletarFormulario(Cliente cliente)
        {
            txtName.Text = cliente.Nombre;
            txtLastName.Text = cliente.Apellido;
            txtRut.Text = cliente.Rut;
            txtAddress.Text = cliente.Direccion;
            txtPhoneNumber.Text = cliente.Telefono;
            txtEmail.Text = cliente.Email;

            PopularTipos(1, cbType);
            PopularTipos(2, cbActividad);

            OpcionComboBox(cliente);
        }
    }
}
