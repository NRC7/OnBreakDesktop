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

namespace OnBreakApp
{

    public partial class CalcularTotal : MetroWindow
    {

        /*****************
         * CONSTRUCTORES *
         *****************/

        public CalcularTotal(string numeroContrato, string nombreEvento)
        {

            InitializeComponent();
            txtNumeroContrato.Text = numeroContrato;
            txtTipoEvento.Text = nombreEvento;
            TipoEvento evento = new DbCrud().BuscarTipoEvento(nombreEvento);
            txtValorBase.Text = evento.Valor_base.ToString();
            txtPersonal.Text = evento.Personal_base.ToString();
        }


        /************
         *  EVENTOS *
         ************/

        // CLICK MENU
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            InitWindow(new Menu());
        }

        // CLICK GUARDAR
        private void Button_Click_Guardar(object sender, RoutedEventArgs e)
        {
            // ABRIR ULTIMA VENTANA PARA ACTUALIZAR REGISTRO DETALLE_EVENTO
            // CON INFO OPCIONAL PARA EL EVENTO
            NotifyUser(2);

            if (new DbCrud().GuardarValorContrato(int.Parse(txtValorTotal.Text), txtNumeroContrato.Text))
            {
                NotifyUser(2);
            }
            else
            {
                NotifyUser(3);
            }
        }

        // CLICK CALCULAR VALORES CONTRATO
        private void Button_Click_Calcular(object sender, RoutedEventArgs e)
        {
            if (ValidarFormatoNumeroContrato(txtAsistentes.Text) && ValidarFormatoNumeroContrato(txtPersonal.Text))
            {

                float[] valores = new DbCrud().CalcularTotal(txtValorBase.Text, txtAsistentes.Text, txtPersonal.Text).ToArray();

                txtRecargoAsistentes.Text = valores[1].ToString();

                txRecargoPersonal.Text = valores[0].ToString();

                txtValorTotal.Text = valores[2].ToString();
            }
            else
            {
                NotifyUser(1);
            }
        }


        /****************
         *  UTILITARIOS *
         ****************/

        // Despliega un mensaje en pantalla notificando al usuario segun el caso respectivo
        private async void NotifyUser(int option)
        {
            switch (option)
            {
                case 1: await this.ShowMessageAsync("Calcular", "Ingresa los datos solicitados"); break;
                case 2: await this.ShowMessageAsync("Calcular", "Valor guardado con exito"); break;
                case 3: await this.ShowMessageAsync("Calcular", "Intentalo nuevamente"); break;
            }
        }

        private bool ValidarFormatoNumeroContrato(string numero)
        {
            if (numero.Trim().Length > 0 && int.TryParse(numero.ToString(), out int result) && int.Parse(numero) >= 0)
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
    }
}
