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

        // CASO DE PRUEBA ID 2: Guardar cliente sin rut en la base de datos
        [TestMethod()]
        public void GuardarClienteTest()
        {

            DbCrud servicio = new DbCrud();
            bool resultado = servicio.GuardarCliente("", "arturo", "vidal",
                "barcelona 123", "987654321", "a.vidal@gmail.com", "EIRL", "Manufactura");
            Assert.IsFalse(resultado);
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

        }

        // CASO PRUEBA ID 6
        [TestMethod()]
        public void ValidarNumeroContratoTest()
        {
            DbCrud servicio = new DbCrud();
            string resultado = servicio.GenerarNumeroContrato();
            Assert.IsNotNull(resultado);
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

        }

    }
}