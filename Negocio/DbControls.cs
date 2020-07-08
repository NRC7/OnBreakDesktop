using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using Datos;

namespace Negocio
{
    public class DbCrud
    {
        private readonly Queries queries = new Queries();

        /*****************
         *    CLIENTE    *
         *****************/

        // FILTRAR CLIENTE
        public DataTable FiltrarClientes(string rutCliente, string tipoEmpresa, string actividadEmpresa)
        {
            DataTable dataSet = new DataTable();
            queries.FiltrarRegistroClientes(rutCliente, tipoEmpresa, actividadEmpresa).Fill(dataSet);
            return dataSet;
        }
        // OBTENER TODOS LOS CLIENTES
        public DataTable ObtenerClientes()
        {
            DataTable dataSet = new DataTable();
            queries.ConsultaRegistroClientes().Fill(dataSet);
            return dataSet;
        }
        // BUSCAR UN CLIENTE
        public Cliente BuscarCliente(string rut)
        {
            MySqlDataReader reader = queries.ConsultarCliente(rut);
            if (reader != null)
            {
                reader.Read();
                Cliente cliente = new Cliente()
                {
                    Id = reader.GetString(0),
                    Rut = rut,
                    Nombre = reader.GetString(2),
                    Apellido = reader.GetString(3),
                    Direccion = reader.GetString(4),
                    Telefono = reader.GetString(5),
                    TipoEmpresa = reader.GetString(7),
                    ActividadEmpresa = reader.GetString(8),
                    Servicio = reader.GetString(9),
                    Email = reader.GetString(6),
                    Contrato = reader.GetString(10)
                };
                return cliente;
            }
            return null;
        }
        // ELIMINAR CLIENTE
        public bool EliminarCliente(Cliente cliente)
        {
            if (cliente != null && !VerificarContratosAsociados(cliente))
            {
                queries.BorrarRegistroCliente(cliente.Rut);
                return true;
            }
            else
            {
                return false;
            }
        }
        // ELIMINAR CLIENTE POR RUT
        public bool EliminarCliente(string rut)
        {
            Cliente cliente = BuscarCliente(rut);
            if (cliente != null && !VerificarContratosAsociados(cliente))
            {
                queries.BorrarRegistroCliente(cliente.Rut);
                return true;
            }
            else
            {
                return false;
            }
        }
        // VERIFICA SI UN CLIENTE TIENE ALGUN CONTRATO ASOCIADO
        public bool VerificarContratosAsociados(Cliente cliente)
        {
            string asociado = cliente.Contrato;

            if (asociado.Equals("si"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        // GUARDAR CLIENTE
        public bool GuardarCliente(string rut, string nombre, string apellido, string direccion,
            string telefono, string email, string tipoEmpresa, string actividadEmpresa, string servicio)
        {
            if (queries.InsertarRegistroCliente(rut, nombre, apellido, direccion, telefono, email, tipoEmpresa, actividadEmpresa, servicio))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // VALIDAR QUE NUEVOS DATOS INGRESADOS SEAN VALIDOS
        private Cliente ValidarCliente(string rut, string nombre, string apellido, string direccion,
            string telefono, string email, string tipoEmpresa, string actividadEmpresa, string servicio)
        {
            Cliente cliente = BuscarCliente(rut);

            if (tipoEmpresa != null && cliente.TipoEmpresa != tipoEmpresa && tipoEmpresa.Length > 0)
            {
                cliente.TipoEmpresa = tipoEmpresa;
            }

            if (actividadEmpresa != null && cliente.ActividadEmpresa != actividadEmpresa && actividadEmpresa.Length > 0)
            {
                cliente.ActividadEmpresa = actividadEmpresa;
            }

            if (cliente.Nombre != nombre && nombre.Length > 0)
            {
                cliente.Nombre = nombre;
            }

            if (cliente.Apellido != apellido && apellido.Length > 0)
            {
                cliente.Apellido = apellido;
            }

            if (cliente.Direccion != direccion && direccion.Length > 0)
            {
                cliente.Direccion = direccion;
            }

            if (cliente.Telefono != telefono && telefono.Length > 0)
            {
                cliente.Direccion = telefono;
            }

            if (cliente.Email != email && email.Length > 0)
            {
                cliente.Email = email;
            }

            if (cliente.Servicio != servicio && servicio.Length > 0)
            {
                cliente.Servicio = servicio;
            }

            return cliente;
        }
        // ACTUALIZAR UN CLIENTE
        public bool ActualizarCliente(string rut, string nombre, string apellido, string direccion,
            string telefono, string email, string tipoEmpresa, string actividadEmpresa, string servicio)
        {
            Cliente cliente = ValidarCliente(rut, nombre, apellido, direccion,
            telefono, email, tipoEmpresa, actividadEmpresa, servicio);

            if (queries.ActualizarRegistroCliente(cliente.Rut, cliente.Nombre, cliente.Apellido, cliente.Direccion,
            cliente.Telefono, cliente.Email, cliente.TipoEmpresa, cliente.ActividadEmpresa, cliente.Servicio))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // OBTENER ID DE UN CLIENTE POR RUT
        public int ObtenerIdCliente(string rut)
        {
            return int.Parse(BuscarCliente(rut).Id);
        }


        /******************
         *    CONTRATO    *
         ******************/

        // OBTENER TODOS LOS CONTRATOS
        public DataTable ObtenerContratos()
        {
            DataTable dataSet = new DataTable();
            queries.ConsultaRegistroContratos().Fill(dataSet);
            return dataSet;
        }
        // BUSCAR UN CONTRATO
        public Contrato BuscarContrato(string numeroContrato)
        {
            MySqlDataReader reader = queries.ConsultarContrato(numeroContrato);
            if (reader != null)
            {
                reader.Read();
                Contrato contrato = new Contrato()
                {
                    FechaCreacion = reader.GetString(2),
                    HoraInicio = reader.GetString(4),
                    NumeroContrato = reader.GetString(1),
                    Direccion = reader.GetString(6),
                    EstadoContrato = reader.GetString(9),
                    Observaciones = reader.GetString(8),
                    TipoEvento = reader.GetString(10),
                    ClienteAsociado = reader.GetString(7),
                    FechaTermino = reader.GetString(3),
                    HoraTermino = reader.GetString(5),
                    ValorContrato = int.Parse(reader.GetString(11))
                };
                return contrato;
            }
            return null;
        }
        // VERIFICA SI EL ESTADO DEL CONTRATO INGRESADO ESTA VIGENTE O NO
        public bool VerificarEstadoContrato(Contrato contrato)
        {
            string asociado = contrato.EstadoContrato;

            if (asociado.Equals("no vigente"))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        // CAMBIA EL ESTADO DEL CONTRATO A NO_VIGENTE
        public bool TerminarContrato(string numeroContrato)
        {
            if (numeroContrato.Length == 12 && VerificarEstadoContrato(BuscarContrato(numeroContrato)))
            {
                string fecha = DateTime.Today.ToString("yyyy-MM-dd");
                string hora = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                if (queries.ActualizarRegistroContrato(fecha, hora, numeroContrato))
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
        // GENERA EL NUMERO DE CONTRATO SEGUN REGLAS DE NEGOCIO
        public string GenerarNumeroContrato(string fechaCreacion, string horaInicio)
        {
            string numeroContrato = (fechaCreacion + horaInicio);
            numeroContrato.Trim();
            string[] charsToRemove = { " ", "-", ":", "/" };
            foreach (var c in charsToRemove)
            {
                numeroContrato = numeroContrato.Replace(c, string.Empty);
            }
            return numeroContrato;
        }
        // GUARDAR CONTRATO
        public bool GuardarContrato(string direccion, string observaciones, string tipoEvento, string rutClienteAsociado)
        {
            string fecha = DateTime.Today.ToString("yyyy-MM-dd");
            string hora = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            string numero = GenerarNumeroContrato(fecha, hora);

            if (queries.InsertarRegistroContrato(numero, fecha, hora, direccion, "vigente", observaciones, tipoEvento,
                ObtenerIdCliente(rutClienteAsociado), "11:11", "1111-11-11"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // OBTENER TODOS LOS TIPOS DE EVENTO
        public List<string> ObtenerTipoEvento()
        {
            DataTable dataSet = new DataTable();
            queries.ConsultaRegistroContratos().Fill(dataSet);

            List<string> tipos = new List<string>();
            foreach (DataRow row in dataSet.Rows)
            {
                tipos.Add(row["nombre_evento"].ToString());
            }
            return tipos;
        }
        // FILTRAR CONTRATOS
        public DataTable FiltrarContratos(string numeroContrato, string rutCliente, string tipoContrato)
        {
            DataTable dataSet = new DataTable();
            queries.FiltrarRegistroContratos(numeroContrato, rutCliente, tipoContrato).Fill(dataSet);
            return dataSet;
        }
        // OBTENER OBJETO TIPO_EVENTO
        public TipoEvento BuscarTipoEvento(int id)
        {
            MySqlDataReader reader = queries.ConsultarTipoEvento(id);
            if (reader != null)
            {
                reader.Read();
                TipoEvento tipoEvento = new TipoEvento()
                {
                    Id = int.Parse(reader.GetString(0)),
                    Nombre_evento = reader.GetString(1),
                    Valor_base = int.Parse(reader.GetString(2)),
                    Personal_base = int.Parse(reader.GetString(3))
                };
                return tipoEvento;
            }
            return null;
        }
        // DEVUELVE EL NOMBRE_EVENTO DE UN TIPO_EVENTO SEGUN EL ID INGRESADO
        public string ObtenerNombreEvento(int idEvento)
        {
            return BuscarTipoEvento(idEvento).Nombre_evento;
        }

        //FIN CLASE

        // 
        public string BuscarNumeroContrato(string rut, string direccion, string tipo)
        {
            string numero = "";

            string consulta = "SELECT con.id, con.numero_contrato, con.fecha_creacion, con.fecha_termino, con.hora_inicio, con.hora_termino, con.direccion, t.nombre_evento, con.observaciones, c.rut, con.valor_contrato FROM Contrato con JOIN Cliente c ON (c.id = con.id_cliente) JOIN Tipo_evento t ON (t.id = con.id_tipo) WHERE c.rut = '"
                + rut + "' AND  con.direccion = '" + direccion + "' AND t.nombre_evento = '" + tipo + "'; ";

            MySqlConnection conexion = db.InitConection();

            MySqlCommand cmd = new MySqlCommand(consulta, conexion);

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    numero = reader.GetString(1); ;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida");
            }
            finally
            {
                conexion.Close();
            }
            return numero;
        }
        //
        public bool GuardarValorContrato(int valorContrato, string numeroContrato)
        {
            if (valorContrato >= 0 && numeroContrato.Length > 0 && BuscarContrato(numeroContrato).ValorContrato <= 1 && BuscarContrato(numeroContrato) != null)
            {
                string consulta = "UPDATE Contrato SET valor_contrato = " + valorContrato + " WHERE numero_contrato = '" + numeroContrato + "';";

                MySqlConnection conexion = db.InitConection();

                try
                {
                    MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    conexion.Close();
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        //
        public int ObtenerValorBase(string nombreEvento)
        {
            int valorBase = 0;

            string consulta = "SELECT * FROM Tipo_evento WHERE nombre_evento ='" + nombreEvento + "'; ";

            MySqlConnection conexion = db.InitConection();

            MySqlCommand cmd = new MySqlCommand(consulta, conexion);

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    string opt = reader.GetString(2);
                    valorBase = int.Parse(reader.GetString(2));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida");
            }
            finally
            {
                conexion.Close();
            }
            return valorBase;
        }
        //
        public List<float> CalcularTotal(string valorBaseTipoEvento, string recargoAsistentes, string recargoPersonal)
        {
            int valorBase = int.Parse(valorBaseTipoEvento);
            int numeroAsistentes = int.Parse(recargoAsistentes);
            int numeroPersonalAdicional = int.Parse(recargoPersonal);

            int valorAsistentes = 0;
            if (numeroAsistentes > 0 && numeroAsistentes <= 20)
            {
                valorAsistentes += 3;
            }
            else if (numeroAsistentes > 20 && numeroAsistentes <= 50)
            {
                valorAsistentes += 5;
            }
            else if (numeroAsistentes > 50 && numeroAsistentes < 70)
            {

                valorAsistentes += 5;
            }
            else if (numeroAsistentes >= 70)
            {
                valorAsistentes += 5 + (2 * (numeroAsistentes - 50) / 20);
            }

            float valorPersonalAdicional = 0;
            if (numeroPersonalAdicional == 2)
            {
                valorPersonalAdicional += 2;
            }
            else if (numeroPersonalAdicional == 3)
            {
                valorPersonalAdicional += 3;
            }
            else if (numeroPersonalAdicional == 4)
            {
                valorPersonalAdicional += 3.5f;
            }
            else if (numeroPersonalAdicional > 4)
            {
                valorPersonalAdicional += 3.5f + (0.5f * numeroPersonalAdicional);
            }

            float valorTotal = valorBase + valorAsistentes + valorPersonalAdicional;

            return new List<float>() { valorPersonalAdicional, valorAsistentes, valorTotal };
        }
    }
}

// ingresar el nombre
// al otro metodo le paso este metodo antes para que busque el nombre y le paso BuscarTipoEvento al nombre encontratdo
// int idEvento = queries.AsignarIdTipo(nombreEvento);


/**********************
 *  CALCULAR VALORES  *
 **********************/

