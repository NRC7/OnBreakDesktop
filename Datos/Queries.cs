using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Datos
{
    public class Queries
    {
        private readonly Database db = new Datos.Database();

        /*************
         *  CLIENTE  *
         *************/

        // FILTRAR REGISTRO DE CLIENTE POR RUT O TIPO DE EMPRESA O ACTIVIDAD DE EMPRESA
        public MySqlDataAdapter FiltrarRegistroClientes(string rutCliente, string tipoEmpresa, string actividadEmpresa)
        {
            string consulta = "SELECT * FROM Cliente WHERE ";

            if (rutCliente.Length > 0 && tipoEmpresa.Length > 0 && actividadEmpresa.Length > 0)
            {
                consulta = "SELECT * FROM Cliente WHERE rut = '" + rutCliente + "' AND tipo = '" + tipoEmpresa + "' AND actividad = '" + actividadEmpresa + "';";
            }
            else if (rutCliente.Length > 9 && tipoEmpresa.Length > 0)
            {
                consulta += "RUT = " + rutCliente + " AND TIPO = '" + tipoEmpresa + "'";
            }
            else if (tipoEmpresa.Length > 0 && actividadEmpresa.Length > 0)
            {
                consulta += "TIPO = '" + tipoEmpresa + "' AND ACTIVIDAD = '" + actividadEmpresa + "'";
            }
            else if (rutCliente.Length > 9 && actividadEmpresa.Length > 0)
            {
                consulta += "RUT = " + rutCliente + " AND ACTIVIDAD = '" + actividadEmpresa + "'";
            }
            else if (rutCliente.Length == 9)
            {
                consulta += "RUT = " + rutCliente;
            }
            else if (tipoEmpresa.Length > 0)
            {
                consulta += "TIPO = '" + tipoEmpresa + "'";
            }
            else if (actividadEmpresa.Length > 0)
            {
                consulta += "ACTIVIDAD = '" + actividadEmpresa + "'";
            }
            else
            {
                consulta = "SELECT * FROM Cliente";
            }

            MySqlDataAdapter adapter = null;
            MySqlConnection conexion = db.InitConection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                adapter = new MySqlDataAdapter(cmd);
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida");
            }
            finally
            {
                conexion.Close();
            }
            return adapter;
        }
        // OBTENER TODOS LOS REGISTROS DE LA TABLA CLIENTES
        public MySqlDataAdapter ConsultaRegistroClientes()
        {
            MySqlDataAdapter adapter = null;
            MySqlConnection conexion = db.InitConection();
            try
            {
                string consulta = "SELECT * FROM Cliente";

                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                adapter = new MySqlDataAdapter(cmd);
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida ConsultaRegistroClientes");
            }
            finally
            {
                conexion.Close();
            }
            return adapter;
        }
        // OBTENER 1 REGISTRO DE LA TABLA CLIENTES SEGUN RUT
        public MySqlDataReader ConsultarCliente(string rut)
        {
            MySqlDataReader reader = null;
            MySqlConnection conexion = db.InitConection();

            try
            {
                string consulta = "SELECT * FROM Cliente WHERE RUT = '" + rut + "';";

                MySqlCommand cmd = new MySqlCommand(consulta, conexion);


                reader = cmd.ExecuteReader();

            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida ConsultarCliente");
            }

            return reader;
        }
        // OBTENER 1 REGISTRO DE LA TABLA CLIENTES SEGUN RUT
        public MySqlDataReader ConsultarCliente(int id)
        {
            MySqlDataReader reader = null;
            MySqlConnection conexion = db.InitConection();

            try
            {
                string consulta = "SELECT * FROM Cliente WHERE id = '" + id + "';";

                MySqlCommand cmd = new MySqlCommand(consulta, conexion);


                reader = cmd.ExecuteReader();

            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida ConsultarCliente");
            }

            return reader;
        }
        // BORRA 1 REGISTRO DE LA TABLA CLIENTE SEGUN EL RUT 
        public bool BorrarRegistroCliente(string rut)
        {
            MySqlConnection conexion = db.InitConection();
            string consulta = "DELETE FROM Cliente WHERE rut = " + rut;

            try
            {
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                cmd.BeginExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida BorrarRegistroCliente");
                return false;
            }
        }
        // VERIFICAR SI RUT EXISTE EN REGISTRO DE TABLA CLIENTE
        public bool VerificarRutExistente(string rut)
        {
            MySqlDataReader reader = ConsultarCliente(rut);
            if (reader != null && reader.Read() && reader.HasRows)
            {
                if (reader.GetString(1) == rut)
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
        // INSERTAR REGISTRO EN TABLA CLIENTE
        public bool InsertarRegistroCliente(string rut, string nombre, string apellido, string direccion,
            string telefono, string email, string tipoEmpresa, string actividadEmpresa)
        {
            if (!VerificarRutExistente(rut) && rut.Length > 0)
            {
                MySqlConnection conexion = db.InitConection();
                try
                {
                    string consulta = "INSERT INTO Cliente (rut, nombres, apellidos, direccion, telefono, email, tipo, actividad, contrato_asociado) " +
                                "VALUES ('" + rut + "', '" + nombre + "', '" + apellido + "', '" + direccion + "', '" + telefono + "', '"
                               + email + "', '" + tipoEmpresa + "', '" + actividadEmpresa + "', 'no');";


                    MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Conexion interrumpida InsertarRegistroCliente");
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
        // ACTUALIZAR
        public bool ActualizarRegistroCliente(string rut, string nombre,
            string apellido, string direccion, string telefono,
            string email, string tipoEmpresa, string actividadEmpresa)
        {
            MySqlConnection conexion = db.InitConection();

            try
            {
                string consulta = "UPDATE Cliente SET nombres = '"
                   + nombre + "', apellidos = '" + apellido + "', direccion = '" + direccion + "', telefono = '" + telefono
                   + "', email = '" + email + "', tipo = '" + tipoEmpresa + "', actividad = '" + actividadEmpresa
                   + "' WHERE rut = '" + rut + "';";

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


        /**************
         *  CONTRATO  *
         **************/

        // OBTENER TODOS LOS REGISTROS DE LA TABLA CONTRATO
        public MySqlDataAdapter ConsultaRegistroContratos()
        {
            MySqlDataAdapter adapter = null;
            MySqlConnection conexion = db.InitConection();
            try
            {
                string consulta = "SELECT con.id, con.numero_contrato, con.fecha_creacion, con.fecha_termino, con.hora_creacion, con.hora_termino, con.direccion, con.estado_contrato, t.nombre_evento, t.descripcion, con.observaciones, c.rut, con.fecha_inicio, con.hora_inicio, d.alimentacion, d.ambientacion, d.musica_ambiental, d.local_evento, d.valor_contrato FROM Contrato con LEFT JOIN Cliente c ON (con.id = c.id) LEFT JOIN Detalle_evento d ON (d.id = con.id) LEFT JOIN Tipo_evento t ON (t.id = d.id_tipo);";
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                adapter = new MySqlDataAdapter(cmd);
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida ConsultaRegistroContratos");
            }
            finally
            {
                conexion.Close();
            }
            return adapter;
        }
        // OBTENER 1 REGISTRO DE LA TABLA CONTRATO SEGUN NUMERO DE CONTRATO
        public MySqlDataReader ConsultarContrato(string numeroContrato)
        {
            MySqlDataReader reader = null;
            MySqlConnection conexion = db.InitConection();
            try
            {
                string consulta = "SELECT con.id, con.numero_contrato, con.fecha_creacion, con.fecha_termino, con.hora_creacion, con.hora_termino, con.direccion, con.estado_contrato, t.nombre_evento, t.descripcion, con.observaciones, c.rut, con.fecha_inicio, con.hora_inicio, d.alimentacion, d.ambientacion, d.musica_ambiental, d.local_evento, d.valor_contrato FROM Contrato con LEFT JOIN Cliente c ON (con.id = c.id) LEFT JOIN Detalle_evento d ON (d.id = con.id) LEFT JOIN Tipo_evento t ON (t.id = d.id_tipo) WHERE numero_contrato = " + numeroContrato;
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                reader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida ConsultarContrato");
            }
            return reader;
        }

        /* OBTENER 1 REGISTRO DE LA TABLA CONTRATO SEGUN NUMERO DE CONTRATO
           CUANDO ESTE NO TIENE UN DETALLE DE CLIENTE O VALOR ASOCIADO */
        public MySqlDataReader ConsultarContratosSinDetalle(string numeroContrato)
        {
            MySqlDataReader reader = null;
            MySqlConnection conexion = db.InitConection();
            try
            {
                string consulta = "SELECT * FROM Contrato WHERE numero_contrato = '" + numeroContrato + "'";
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                reader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida ConsultarContrato");
            }
            return reader;
        }
        // PONE TERMINO AL CONTRATO CAMBIANDO EL ESTADO DE CONTRATO A UN REGISTRO DE LA TABLA CONTRATO
        public bool ActualizarRegistroContrato(string numeroContrato, string estado)
        {
            MySqlConnection conexion = db.InitConection();
            try
            {
                string consulta = "UPDATE Contrato SET estado_contrato = '" + estado + "' WHERE numero_contrato = '"
                    + numeroContrato + "';";

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
        // VERIFICAR SI RUT EXISTE EN REGISTRO DE TABLA CLIENTE
        public bool VerificarNumeroContratoExistente(string numeroContrato)
        {
            MySqlDataReader reader = ConsultarContrato(numeroContrato);
            if (reader != null && reader.Read())
            {
                if (reader.GetString(1) == numeroContrato)
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

        // OBTIENE EL ID DE TIPO_EVENTO CORRESPONDIENTE AL NOMBRE DE EVENTO INGRESADO
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

        // OBTIENE EL ID DE CLIENTE CORRESPONDIENTE AL RUT INGRESADO
        public int AsignarIdCliente(string rut)
        {
            MySqlConnection conexion = db.InitConection();

            string consulta = "SELECT id FROM Cliente WHERE rut = '" + rut + "';";

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
                MessageBox.Show("Conexion interrumpida AsignarIdCliente");
            }
            finally
            {
                conexion.Close();
            }

            return int.Parse(id);
        }

        // INSERTA UN NUEVO REGISTRO EN LA TABLA CONTRATO
        // cambiarle el estado al cliente
        public bool InsertarRegistroContrato(string numeroContrato, string fechaCreacion, string fechaTermino,
            string horaCreacion, string horaTermino, string direccion, string estadoContrato, string nombreEvento,
            string observaciones, string rutAsociado, string fechaInicio, string horaInicio)
        {
            if (!VerificarNumeroContratoExistente(numeroContrato))
            {
                MySqlConnection conexion = db.InitConection();
                try
                {
                    string consulta = "INSERT INTO Contrato (numero_contrato, fecha_creacion, fecha_termino, hora_creacion, hora_termino, direccion, estado_contrato, id_tipo, observaciones, id_cliente, fecha_inicio, hora_inicio)" +
                                "VALUES ('" + numeroContrato + "', '" + fechaCreacion + "', '" + fechaTermino + "', '" + horaCreacion + "', '" + horaTermino + "', '"
                               + direccion + "', '" + estadoContrato + "', " + AsignarIdTipo(nombreEvento) + ", '" + observaciones + "', " + AsignarIdCliente(rutAsociado) + ", '"
                               + fechaInicio + "', '" + horaInicio + "');";

                    MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Conexion interrumpida InsertarRegistroContrato");
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

        // CAMBIO DE CONTRATO_ASOCIADO A SI AL INSERTAR UN CLIENTE CORRECTAMENTE
        public bool AsociarContratoACliente(string rut)
        {
            MySqlConnection conexion = db.InitConection();
            try
            {
                string consulta = "UPDATE Cliente SET contrato_asociado = 'si' WHERE id = " + AsignarIdCliente(rut) + ";";

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

        // OBTIENE TODOS LOS REGISTROS DE LA TABLA TIPO EVENTO
        public MySqlDataAdapter ConsultaRegistroTipoEvento()
        {
            MySqlDataAdapter adapter = null;
            MySqlConnection conexion = db.InitConection();
            try
            {
                string consulta = "SELECT nombre_evento FROM Tipo_evento";
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                adapter = new MySqlDataAdapter(cmd);
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida ConsultaRegistroTipoEvento");
            }
            finally
            {
                conexion.Close();
            }
            return adapter;
        }
        // FILTRAR REGISTRO DE CONTRATOS POR NUMERO_CONTRATO O RUT_CLIENTE O TIPO_CONTRATO
        public MySqlDataAdapter FiltrarRegistroContratos(string numeroContrato, string rutCliente, string tipoContrato)
        {
            string consulta = "SELECT con.id, con.numero_contrato, con.fecha_creacion, con.fecha_termino, con.hora_creacion, con.hora_termino, con.direccion, con.estado_contrato, t.nombre_evento, t.descripcion, con.observaciones, c.rut, con.fecha_inicio, con.hora_inicio, d.alimentacion, d.ambientacion, d.musica_ambiental, d.local_evento, d.valor_contrato FROM Contrato con JOIN Cliente c ON(con.id = c.id) JOIN Detalle_evento d ON(d.id = con.id) JOIN Tipo_evento t ON(t.id = d.id_tipo) WHERE ";

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
                consulta = "SELECT con.id, con.numero_contrato, con.fecha_creacion, con.fecha_termino, con.hora_creacion, con.hora_termino, con.direccion, t.nombre_evento, con.observaciones, c.rut, con.valor_contrato, con.modalidad_servicio, con.fecha_inicio, con.hora_inicio FROM Contrato con JOIN Cliente c ON (c.id = con.id) JOIN Tipo_evento t ON (t.id = con.id);";
            }

            MySqlDataAdapter adapter = null;
            MySqlConnection conexion = db.InitConection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                adapter = new MySqlDataAdapter(cmd);
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida FiltrarRegistroContratos");
            }
            finally
            {
                conexion.Close();
            }
            return adapter;
        }
        // OBTENER TODOS LOS REGISTROS DE LA TABLA CLIENTES SEGUN ID INGRESADO
        public MySqlDataReader ConsultarTipoEvento(int idEvento)
        {
            MySqlDataReader reader = null;
            MySqlConnection conexion = db.InitConection();

            try
            {
                string consulta = "SELECT * FROM Tipo_evento WHERE id = " + idEvento + ";";
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                reader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida ConsultarTipoEvento");
            }

            return reader;
        }
        // OBTENER TODOS LOS REGISTROS DE LA TABLA CLIENTES SEGUN NOMBRE_EVENTO INGRESADO
        public MySqlDataReader ConsultarTipoEvento(string nombre)
        {
            MySqlDataReader reader = null;
            MySqlConnection conexion = db.InitConection();

            try
            {
                string consulta = "SELECT * FROM Tipo_evento WHERE nombre_evento = '" + nombre + "';";
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                reader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                MessageBox.Show("Conexion interrumpida ConsultarTipoEvento");
            }

            return reader;
        }

        public bool GuardarValorContrato(int valorContrato, string numeroContrato, int idTipo, int idContrato)
        {
            if (valorContrato >= 0 && numeroContrato.Length > 0)
            {
                string consulta = "INSERT INTO Detalle_evento(alimentacion, ambientacion, musica_ambiental, local_evento, id_tipo, id_contrato, valor_contrato) + " +
                    "VALUES('Mixta', 'no', 'no', 'no', " + idTipo + "," + idContrato + "," + valorContrato + ");";

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

