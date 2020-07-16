using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using Datos;
using Microsoft.OData.Edm;

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
            if (reader != null && reader.HasRows)
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
                    Email = reader.GetString(6),
                    ContratoAsociado = reader.GetString(9)
                };
                return cliente;
            }
            else
            {
                return null;
            }
        }
        public Cliente BuscarCliente(int id)
        {
            MySqlDataReader reader = queries.ConsultarCliente(id);
            if (reader != null && reader.HasRows)
            {
                reader.Read();
                Cliente cliente = new Cliente()
                {
                    Id = reader.GetString(0),
                    Rut = reader.GetString(1),
                    Nombre = reader.GetString(2),
                    Apellido = reader.GetString(3),
                    Direccion = reader.GetString(4),
                    Telefono = reader.GetString(5),
                    TipoEmpresa = reader.GetString(7),
                    ActividadEmpresa = reader.GetString(8),
                    Email = reader.GetString(6),
                    ContratoAsociado = reader.GetString(9)
                };
                return cliente;
            }
            else
            {
                return null;
            }
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
            string asociado = cliente.ContratoAsociado;

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
            string telefono, string email, string tipoEmpresa, string actividadEmpresa)
        {
            if (queries.InsertarRegistroCliente(rut, nombre, apellido, direccion, telefono, email, tipoEmpresa, actividadEmpresa) && rut.Length > 0)
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
            string telefono, string email, string tipoEmpresa, string actividadEmpresa)
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

            return cliente;
        }
        // ACTUALIZAR UN CLIENTE
        public bool ActualizarCliente(string rut, string nombre, string apellido, string direccion,
            string telefono, string email, string tipoEmpresa, string actividadEmpresa)
        {
            Cliente cliente = ValidarCliente(rut, nombre, apellido, direccion,
            telefono, email, tipoEmpresa, actividadEmpresa);

            if (queries.ActualizarRegistroCliente(cliente.Rut, cliente.Nombre, cliente.Apellido, cliente.Direccion,
            cliente.Telefono, cliente.Email, cliente.TipoEmpresa, cliente.ActividadEmpresa))
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
            if (reader != null && reader.HasRows)
            {
                reader.Read();
                Contrato contrato = new Contrato();
                try
                {
                    //(reader.GetString(9) != null || !reader.GetString(9).Equals("null"))
                    contrato = new Contrato()
                    {
                        IdContrato = reader.GetString(0),
                        NumeroContrato = reader.GetString(1),
                        FechaCreacion = reader.GetString(2),
                        FechaTermino = reader.GetString(3),
                        HoraCreacion = reader.GetString(4),
                        HoraTermino = reader.GetString(5),
                        Direccion = reader.GetString(6),
                        EstadoContrato = reader.GetString(7),
                        TipoDeEvento = reader.GetString(8),
                        Observaciones = reader.GetString(10),
                        DescripcionEvento = reader.GetString(9),
                        ClienteAsociado = reader.GetString(11),
                        FechaInicio = reader.GetString(12),
                        HoraInicio = reader.GetString(13),
                        Alimentacion = reader.GetString(14),
                        Ambientacion = reader.GetString(15),
                        MusicaAmbiental = reader.GetString(16),
                        LocalEvento = reader.GetString(17),
                        ValorContrato = reader.GetString(18)
                    };
                }
                catch (Exception)
                {

                    MySqlDataReader auxReader = queries.ConsultarContratosSinDetalle(numeroContrato);
                    auxReader.Read();
                    contrato = new Contrato()
                    {
                        IdContrato = auxReader.GetString(0),
                        NumeroContrato = auxReader.GetString(1),
                        FechaCreacion = auxReader.GetString(2),
                        FechaTermino = auxReader.GetString(3),
                        HoraCreacion = auxReader.GetString(4),
                        HoraTermino = auxReader.GetString(5),
                        Direccion = auxReader.GetString(6),
                        EstadoContrato = auxReader.GetString(7),
                        TipoDeEvento = BuscarTipoEvento(int.Parse(auxReader.GetString(8))).Nombre_evento,
                        Observaciones = auxReader.GetString(9),
                        ClienteAsociado = BuscarCliente(int.Parse(auxReader.GetString(10))).Rut,
                        FechaInicio = reader.GetString(11),
                        HoraInicio = reader.GetString(12),
                    };

                }
                return contrato;
            }
            return null;
        }

        // VERIFICA SI EL ESTADO DEL CONTRATO INGRESADO ESTA VIGENTE O NO
        public bool VerificarEstadoContrato(Contrato contrato)
        {
            string asociado = contrato.EstadoContrato;
            if (asociado.Equals("vigente"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // EVALUA LAS FECHAS INGRESADAS Y DEVUELVE EL ESTADO DE TERMINO DE CONTRATO CORRESPONDIENTE
        public string GenerarEstadoFinalContrato(string fechaInicio, string fechaTermino)
        {
            string[] fechaInicioSeparada = fechaInicio.Split('-');
            string[] fechaTerminoSeparada = fechaTermino.Split('-');

            int anioInicio = int.Parse(fechaInicioSeparada[2]);
            int aniotermino = int.Parse(fechaTerminoSeparada[0]);
            int mesInicio = int.Parse(fechaInicioSeparada[1]);
            int mesTermino = int.Parse(fechaTerminoSeparada[1]);
            int diaTermino = int.Parse(fechaTerminoSeparada[2]);
            int diaInicio = int.Parse(fechaInicioSeparada[0]);


            if (aniotermino < anioInicio || mesTermino < mesInicio || diaTermino < diaInicio)
            {
                return "no realizado";
            }
            else if (aniotermino > anioInicio || mesTermino > mesInicio || diaTermino > diaInicio)
            {
                return "realizado";
            }
            else
            {
                return "vigente";
            }
        }
        // CAMBIAR ESTADO DE CONTRATO E INGRESAR FECHA Y HORA DE TERMINO
        public bool TerminarContrato(string numeroContrato)
        {
            Contrato contrato = BuscarContrato(numeroContrato);
            string fechatermino = DateTime.Today.ToString("yyyy-MM-dd");
            string fechaInicio = contrato.FechaInicio.Split(' ')[0];
            string estado = GenerarEstadoFinalContrato(fechaInicio, fechatermino);
            if (numeroContrato.Length == 12 && VerificarEstadoContrato(contrato) && estado != "vigente")
            {
                if (queries.ActualizarRegistroContrato(numeroContrato, estado))
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

        // GENERAR NUMERO DE CONTRATO DEFINITIVO
        public string GenerarNumeroContrato()
        {
            string fechaCreacion = DateTime.Today.ToString("yyyy-MM-dd");
            string auxHoraCreacion = DateTime.Now.ToString("HH:mm");
            string numeroContrato = (fechaCreacion + auxHoraCreacion);

            numeroContrato.Trim();
            string[] charsToRemove = { " ", "-", ":", "/" };
            foreach (var c in charsToRemove)
            {
                numeroContrato = numeroContrato.Replace(c, string.Empty);
            }
            return numeroContrato;
        }
        // GUARDAR CONTRATO
        public bool GuardarContrato(string numeroContrato, string fechaTermino, string horaTermino,
            string direccion, string nombreEvento, string observaciones,
            string rutAsociado, string fechaInicio, string horaInicio)
        {
            string fechaCreacion = DateTime.Today.ToString("yyyy-MM-dd");
            string horaCreacion = DateTime.Now.ToString("HH:mm");
            string estadoContrato = "vigente";

            if (queries.InsertarRegistroContrato(numeroContrato, fechaCreacion, fechaTermino,
                horaCreacion, horaTermino, direccion, estadoContrato, nombreEvento,
                observaciones, rutAsociado, fechaInicio, horaInicio)
                && queries.AsociarContratoACliente(rutAsociado))
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
            queries.ConsultaRegistroTipoEvento().Fill(dataSet);

            List<string> tipos = new List<string>();
            foreach (DataRow row in dataSet.Rows)
            {
                tipos.Add(row["nombre_evento"].ToString());
            }
            return tipos;
        }
        // FILTRAR CONTRATOS
        public DataTable FiltrarContratos(string numeroContrato, string rutCliente, string tipoEvento)
        {
            DataTable dataSet = new DataTable();
            queries.FiltrarRegistroContratos(numeroContrato, rutCliente, tipoEvento).Fill(dataSet);
            if (dataSet != null)
            {
                return dataSet;
            }
            else
            {
                return ObtenerContratos();
            }
        }
        // OBTENER OBJETO TIPO_EVENTO SEGUN ID INGRESADO
        public TipoEvento BuscarTipoEvento(int id)
        {
            MySqlDataReader reader = queries.ConsultarTipoEvento(id);
            if (reader != null && reader.HasRows)
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
        // OBTENER OBJETO TIPO_EVENTO SEGUN NOMBRE
        public TipoEvento BuscarTipoEvento(string nombre)
        {
            MySqlDataReader reader = queries.ConsultarTipoEvento(nombre);
            if (reader != null && reader.HasRows)
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
            if (BuscarTipoEvento(idEvento) != null)
            {
                return BuscarTipoEvento(idEvento).Nombre_evento;
            }
            else
            {
                return "";
            }
        }
        // DEVUELVE EL VALOR_BASE DE UN TIPO_EVENTO SEGUN EL NOMBRE INGRESADO
        public int ObtenerValorBase(string nombreEvento)
        {
            TipoEvento evento = BuscarTipoEvento(nombreEvento);
            if (evento != null)
            {
                return BuscarTipoEvento(nombreEvento).Valor_base;
            }
            else
            {
                return 0;
            }
        }

        // Genera el detalle del evento
        public bool GuardarValorContrato(int valorContrato, string numeroContrato)
        {
            Contrato contrato = BuscarContrato(numeroContrato);
            TipoEvento evento = BuscarTipoEvento(contrato.TipoDeEvento);
            // GUARDAR INFORMACION PARCIAL EN DETALLE_EVENTO
            //public bool GuardarValorContrato(int valorContrato, string numeroContrato, int idTipo, int idContrato)
            if (queries.GuardarValorContrato(valorContrato, numeroContrato, evento.Id, int.Parse(contrato.IdContrato)))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // Entrega los valores para calcular el valor total del contrato
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