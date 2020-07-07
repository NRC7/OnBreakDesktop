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

        /******************
         *  CRUD CLIENTE  *
         ******************/

        // FILTRAR CLIENTE ACTUALIZADO
        public DataTable FiltrarClientes(string rutCliente, string tipoEmpresa, string actividadEmpresa)
        {
            DataTable dataSet = new DataTable();
            queries.FiltrarRegistroClientes(rutCliente, tipoEmpresa, actividadEmpresa).Fill(dataSet);
            return dataSet;
        }
        // LEER ACTUALIZADO
        public DataTable ObtenerClientes()
        {
            DataTable dataSet = new DataTable();
            queries.ConsultaRegistroClientes().Fill(dataSet);
            return dataSet;
        }
        // BUSCAR ACTUALIZADO
        public Cliente BuscarCliente(string rut)
        {
            MySqlDataReader reader = queries.ConsultarCliente(rut);
            if (reader != null)
            {
                reader.Read();
                Cliente cliente = new Cliente()
                {
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
        // ELIMINAR POR CLIENTE ACTUALIZADO
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
        // ELIMINAR POR RUT ACTUALIZADO
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
        // VERIFICA SI UN CLIENTE TIENE ALGUN CONTRATO ASOCIADO ACTUALIZADO
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
        // GUARDAR CLIENTE ACTUALIZADO
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
        // VALIDAR QUE NUEVOS DATOS SEAN VALIDOS
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
        // ACTUALIZAR CLIENTE ACTUALIZADO
        public bool ActualizarCliente(string rut, string nombre, string apellido, string direccion,
            string telefono, string email, string tipoEmpresa, string actividadEmpresa, string servicio)
        {
            if (queries.ActualizarRegistroCliente(rut, nombre, apellido, direccion,
            telefono, email, tipoEmpresa, actividadEmpresa, servicio))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /****************
        *  UTILITARIOS  *
        *****************/





        public string ObtenerIdCliente(string rut)
        {
            string id = "";
            MySqlConnection conexion = db.InitConection();

            string consulta = "SELECT * FROM Cliente WHERE RUT = " + rut;

            MySqlCommand cmd = new MySqlCommand(consulta, conexion);

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    id = reader.GetString(0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida del id");
            }
            return id;
        }





        /**************
         *  CONTRATO  *
         **************/

        /**********
         *  CRUD  *
         **********/

        // GUARDAR
        public Contrato CrearContrato(string fecha, string hora, string numeroContrato, string direccion, string observaciones, string tipo, string rut)
        {
            Contrato Contrato = new Contrato()
            {
                FechaCreacion = fecha,
                HoraInicio = hora,
                NumeroContrato = numeroContrato,
                Direccion = direccion,
                EstadoContrato = "vigente",
                Observaciones = observaciones,
                TipoEvento = tipo,
                ClienteAsociado = rut,
                HoraTermino = "11:11",
                FechaTermino = "1111-11-11"
            };
            return Contrato;
        }

        public Contrato CrearContrato(string fecha, string hora, string numeroContrato, string direccion, string observaciones, string tipo, string rut, string estado, string fecha_termino
            , string hora_termino, string valor)
        {
            Contrato Contrato = new Contrato()
            {
                FechaCreacion = fecha,
                HoraInicio = hora,
                NumeroContrato = numeroContrato,
                Direccion = direccion,
                EstadoContrato = estado,
                Observaciones = observaciones,
                TipoEvento = tipo,
                ClienteAsociado = rut,
                FechaTermino = fecha_termino,
                HoraTermino = hora_termino,
                ValorContrato = int.Parse(valor)
            };
            return Contrato;
        }

        public bool GuardarContrato(string direccion, string observaciones, string tipo, string rut)
        {
            string fecha = DateTime.Today.ToString("yyyy-MM-dd");
            string hora = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            string numero = GenerarNumeroContrato(fecha, hora);

            if (!ValidarNumeroContrato(numero))
            {
                Contrato contrato = CrearContrato(fecha, hora, numero, direccion, observaciones, tipo, rut);
                if (ToDatabase(contrato))
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

        // LEER
        public DataTable ObtenerContratos()
        {
            DataTable dataSet = null;

            try
            {
                MySqlConnection conexion = db.InitConection();

                string consulta = "SELECT con.id, con.numero_contrato, con.fecha_creacion, con.fecha_termino, con.hora_inicio, con.hora_termino, con.direccion, t.nombre_evento, con.observaciones, c.rut, con.valor_contrato FROM Contrato con JOIN Cliente c ON (c.id = con.id_cliente) JOIN Tipo_evento t ON (t.id = con.id_tipo);";

                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                dataSet = new DataTable();
                adapter.Fill(dataSet);
                conexion.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida obtener contrato");
            }

            return dataSet;
        }

        // ACTUALIZAR & "ELIMINAR"
        public bool TerminarContrato(string txtNumeroContrato)
        {
            if (ValidarNumeroContrato(txtNumeroContrato) && BuscarContrato(txtNumeroContrato).EstadoContrato != "no vigente")
            {

                string fecha = DateTime.Today.ToString("yyyy-MM-dd");
                string hora = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();

                string consulta = "UPDATE Contrato SET fecha_termino = '"
                    + fecha + "', hora_termino = '" + hora
                    + "', estado_contrato = '" + "no vigente" + "' WHERE numero_contrato = '"
                    + txtNumeroContrato + "';";

                MySqlConnection conexion = db.InitConection();

                try
                {
                    MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    conexion.Close();
                }
            }
            else
            {
                return false;
            }
        }


        /****************
        *  UTILITARIOS  *
        *****************/

        public bool ToDatabase(Contrato contrato)
        {
            string consulta = "INSERT INTO Contrato (numero_contrato, fecha_creacion, fecha_termino, hora_inicio, hora_termino, direccion, estado_contrato, id_tipo, observaciones, id_cliente)" +
                                "VALUES ('" + contrato.NumeroContrato + "', '" + contrato.FechaCreacion + "', '" + contrato.FechaTermino + "', '" + contrato.HoraInicio + "', '" + contrato.HoraTermino + "', '"
                               + contrato.Direccion + "', '" + contrato.EstadoContrato + "', '" + AsignarIdTipo(contrato.TipoEvento) + "', '" + contrato.Observaciones + "', '" + ObtenerIdCliente(contrato.ClienteAsociado) + "');";

            MySqlConnection conexion = db.InitConection();

            try
            {

                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

        public bool ValidarNumeroContrato(string numeroContrato)
        {
            MySqlConnection conexion = db.InitConection();

            string consulta = "SELECT * FROM Contrato WHERE numero_contrato = '" + numeroContrato + "';";

            MySqlCommand cmd = new MySqlCommand(consulta, conexion);

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                string comparar = reader.GetString(1);

                if (comparar == numeroContrato)
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

        public List<string> ObtenerTipoEvento()
        {
            DataTable dataSet = new DataTable();
            List<string> tipos = new List<string>();

            MySqlConnection conexion = db.InitConection();

            string consulta = "SELECT nombre_evento FROM Tipo_evento";

            try
            {
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dataSet);

                foreach (DataRow row in dataSet.Rows)
                {
                    tipos.Add(row["nombre_evento"].ToString());
                }
            }
            catch (Exception)
            {

            }

            return tipos;
        }

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

        public int AsignarIdTipo(string tipo)
        {
            MySqlConnection conexion = db.InitConection();

            string consulta = "SELECT id FROM Tipo_evento WHERE nombre_evento = '" + tipo + "';";

            MySqlCommand cmd = new MySqlCommand(consulta, conexion);
            string id = "";

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    id = reader.GetString(0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida del id");
            }
            finally
            {
                conexion.Close();
            }

            return int.Parse(id);
        }

        public Contrato BuscarContrato(string numeroContrato)
        {
            Contrato contrato = null;

            if (ValidarNumeroContrato(numeroContrato))
            {

                string consulta = "SELECT id, numero_contrato, fecha_creacion, fecha_termino, time_format(hora_inicio, '%h:%i'), time_format(hora_termino, '%h:%i'), direccion, estado_contrato, id_tipo, observaciones, id_cliente, valor_contrato from Contrato WHERE numero_contrato = '" + numeroContrato + "'; ";

                MySqlConnection conexion = db.InitConection();

                MySqlCommand cmd = new MySqlCommand(consulta, conexion);

                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        contrato = CrearContrato(reader.GetString(2), reader.GetString(4), reader.GetString(1), reader.GetString(6), reader.GetString(9), reader.GetString(8), reader.GetString(10), reader.GetString(7), reader.GetString(3), reader.GetString(5), reader.GetString(11));
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
                return contrato;
            }
            else
            {
                return contrato;
            }
        }

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

        public DataTable FiltrarContratos(string numeroContrato, string rutCliente, string tipoContrato)
        {
            DataTable dataSet = new DataTable();

            string consulta = "SELECT con.id, con.numero_contrato, con.fecha_creacion, con.fecha_termino, con.hora_inicio, con.hora_termino, con.direccion, t.nombre_evento, con.observaciones, c.rut, con.valor_contrato FROM Contrato con JOIN Cliente c ON (c.id = con.id_cliente) JOIN Tipo_evento t ON (t.id = con.id_tipo) WHERE ";

            if (numeroContrato.Length > 0 && rutCliente.Length > 0 && tipoContrato.Length > 0)
            {
                consulta += " con.numero_contrato = '" + numeroContrato + "' AND c.rut = '" + rutCliente + "' AND t.nombre_evento = '" + tipoContrato + "';";
            }
            else if (numeroContrato.Length > 0 && rutCliente.Length > 0)
            {
                consulta += " con.numero_contrato = '" + numeroContrato + "' AND c.rut = '" + rutCliente + "';";
            }
            else if (numeroContrato.Length > 0 && tipoContrato.Length > 0)
            {
                consulta += " con.numero_contrato = '" + numeroContrato + "' AND t.nombre_evento = '" + tipoContrato + "';";
            }
            else if (rutCliente.Length > 0 && tipoContrato.Length > 0)
            {
                consulta += " c.rut = '" + rutCliente + "' AND t.nombre_evento = '" + tipoContrato + "';";
            }
            else if (numeroContrato.Length == 12)
            {
                consulta += " con.numero_contrato = '" + numeroContrato + "';";
            }
            else if (rutCliente.Length >= 8)
            {
                consulta += " c.rut = '" + rutCliente + "';";
            }
            else if (tipoContrato.Length > 0)
            {
                consulta += " t.nombre_evento = '" + tipoContrato + "';";
            }
            else
            {
                consulta = "SELECT con.id, con.numero_contrato, con.fecha_creacion, con.fecha_termino, con.hora_inicio, con.hora_termino, con.direccion, t.nombre_evento, con.observaciones, c.rut , con.valor_contrato FROM Contrato con JOIN Cliente c ON (c.id = con.id) JOIN Tipo_evento t ON (t.id = con.id);";
            }

            try
            {
                MySqlConnection conexion = db.InitConection();
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataSet);
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida");
            }
            return dataSet;
        }

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

        public string ObtenerNombreEvento(int idEvento)
        {
            string nombreEvento = "";

            string consulta = "SELECT * FROM Tipo_evento WHERE id =" + idEvento + "; ";

            MySqlConnection conexion = db.InitConection();

            MySqlCommand cmd = new MySqlCommand(consulta, conexion);

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    nombreEvento = reader.GetString(1);
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
            return nombreEvento;
        }

        public TipoEvento BuscarTipoEvento(string nombreEvento)
        {
            string consulta = "SELECT * FROM Tipo_evento WHERE nombre_evento = '" + nombreEvento + "';";

            MySqlConnection conexion = db.InitConection();

            MySqlCommand cmd = new MySqlCommand(consulta, conexion);

            TipoEvento tipoEvento = new TipoEvento();
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    tipoEvento.Id = int.Parse(reader.GetString(0));
                    tipoEvento.Nombre_evento = reader.GetString(1);
                    tipoEvento.Valor_base = int.Parse(reader.GetString(2));
                    tipoEvento.Personal_base = int.Parse(reader.GetString(3));
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

            return tipoEvento;
        }


        /**********************
         *  CALCULAR VALORES  *
         **********************/

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
    }
}
