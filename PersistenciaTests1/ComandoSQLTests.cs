using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Persistencia.Tests
{
    [TestClass()]
    public class ComandoSQLTests
    {
        [TestMethod()]
        public void AgregarZona()
        {
            ComandoSQL prueba = new ComandoSQL("AltaZona");
            //EXEC AltaZona 'a','A25','La Casona',250000
            //@letra_departamento char,	@codigo varchar(3), @nombre varchar(30), @habitantes
            prueba.AgregarParametro("@letra_departamento", "o");
            prueba.AgregarParametro("@codigo", "uio");
            prueba.AgregarParametro("@nombre", "caca");
            prueba.AgregarParametro("@habitantes", 2000);

            Assert.AreEqual(prueba.Ejecutar_Transaccion(),0);
            //Assert.Fail("prueba",prueba);
        }
        [TestMethod()]
        public void Hacer_Listado()
        {
            
        }
    }
}