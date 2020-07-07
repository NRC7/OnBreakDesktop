using Microsoft.VisualStudio.TestTools.UnitTesting;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Tests
{
    [TestClass()]
    public class DbCrudTests
    {
        // CASO DE PRUEBA ID 1
        [TestMethod()]
        public void CrearClienteTest()
        {
            DbCrud servicio = new DbCrud();
            Cliente resultado = servicio.CrearCliente("", "arturo", "vidal", "barcelona 123", "987654321", "a.vidal@gmail.com", "EIRL", "Manufactura", "evento");
            Assert.IsNull(resultado);
        }

        // CASO DE PRUEBA ID 2
        [TestMethod()]
        public void GuardarClienteTest()
        {
            Cliente cliente = new Cliente()
            {
                Rut = "333333333",
                Nombre = "arturo",
                Apellido = "vidal",
                Direccion = "barcelona 123",
                Telefono = "987654321",
                Email = "a.vidal@gmail.com",
                TipoEmpresa = "EIRL",
                ActividadEmpresa = "Manufactura",
                Servicio = "evento",
                Contrato = "no",
            };

            DbCrud servicio = new DbCrud();
            bool resultado = servicio.GuardarCliente(cliente);
            Assert.IsFalse(resultado);
        }

        // CASO DE PRUEBA ID 3
        [TestMethod()]
        public void ContratoAsociadoTest()
        {
            string rutTest = "222222222";
            DbCrud servicio = new DbCrud();
            bool resultado = servicio.ContratoAsociado(rutTest);
            Assert.IsTrue(resultado);
        }

        // CASO PRUEBA ID 4
        [TestMethod()]
        public void EliminarClienteTest()
        {
            string rutTest = "222222222";
            DbCrud servicio = new DbCrud();
            bool resultado = servicio.EliminarCliente(rutTest);
            Assert.IsFalse(resultado);
        }

        // CASO PRUEBA ID 5
        [TestMethod()]
        public void GuardarContratoTest()
        {
            string rutTest = "";
            DbCrud servicio = new DbCrud();
            bool resultado = servicio.GuardarContrato("evento 123", "festival del sushi", "barra libre", rutTest);
            Assert.IsFalse(resultado);
        }

        // CASO PRUEBA ID 6
        [TestMethod()]
        public void ValidarNumeroContratoTest()
        {
            string numeroContratoTest = "334455667788";
            DbCrud servicio = new DbCrud();
            bool resultado = servicio.ValidarNumeroContrato(numeroContratoTest);
            Assert.IsFalse(resultado);
        }

        // CASO PRUEBA ID 7
        [TestMethod()]
        public void CalcularTotalTest()
        {
            string valorBaseTipoEventoTest = "35";
            string recargoAsistentesTest = "21";
            string recargoPersonalTest = "4";

            DbCrud servicio = new DbCrud();
            Assert.IsNotNull(servicio.CalcularTotal(valorBaseTipoEventoTest, recargoAsistentesTest, recargoPersonalTest));
        }

        // CASO PRUEBA ID 8
        [TestMethod()]
        public void GuardarValorContratoTest()
        {
            int valorTotalTest = 40;
            string numeroContratoTest = "";

            DbCrud servicio = new DbCrud();
            Assert.IsFalse(servicio.GuardarValorContrato(valorTotalTest, numeroContratoTest));
        }

    }
}