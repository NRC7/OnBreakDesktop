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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

namespace OnBreakApp
{

    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();
            txtUser.Text = "admin";
        }

        // Evento Click Boton Ingresar
        private void Button_Click_Ingresar(object sender, RoutedEventArgs e)
        {


            string user = txtUser.Text;
            string pass = pbPassword.Password;


            if (DebugValidation(user))
            {

                InitMenu(true);
            }
            else
            {

                NotifyWarning("Fallo");

                CleanPassBox(pbPassword);
            }
        }

        // Valida las credenciales ingresadas por el usuario y entrega el resultado true o false
        private bool Validation(string user, string pass)
        {

            if (user.Length > 3 && pass.Length > 5)
            {

                if (user.Equals("admin") && pass.Equals("123456"))
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

        // Inicia la ventana Menu y cierra la ventana Login
        private void InitMenu(bool isReady)
        {
            if (isReady)
            {
                Menu menu = new Menu();
                this.Close();
                menu.ShowDialog();
            }
        }

        // Despliega los mensajes de fallo respectivos en pantalla
        private async void NotifyWarning(string title)
        {
            if (title.Length > 0 && title.Equals("Fallo"))
                await this.ShowMessageAsync("Fallo", "El usuario y/o contraseña ingresado(s) es(son) correcto(s)");
        }

        // Limpia el PasswordBox seleccionado
        private void CleanPassBox(PasswordBox box)
        {
            box.Password = "";
        }

        private bool DebugValidation(string user)
        {


            if (user.Equals("admin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
