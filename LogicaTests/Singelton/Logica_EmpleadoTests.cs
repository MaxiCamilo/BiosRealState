using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logica.Singelton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica.Singelton.Tests
{
    [TestClass()]
    public class Logica_EmpleadoTests
    {
        [TestMethod()]
        public void Iniciar_SesionTest()
        {
            Assert.Fail();
        }
        [TestMethod()]
        public void Alta_Empleado()
        {
            List<string> Prueba = Logica_Empleado.Instancia.Alta_empleado("Maxiro", 1234567890, 1234567890);
            Assert.AreEqual(Prueba[0], "");

        }
        [TestMethod()]
        public void Baja_Empleado()
        {
            List<string> Prueba = Logica_Empleado.Instancia.Baja_Empleado("Maxiro", 1234567890, 1234567890);
            Assert.AreEqual(Prueba[0], "");

        }

        [TestMethod()]
        public void Modificar_Empleado()
        {
            List<string> Prueba = Logica_Empleado.Instancia.Modificar_empleado(new Entidades.Realidad.Empleado("Maxiro", "1234567890"), "MaxiroMaxi", "MaxiroMaxi");
            Assert.AreEqual(Prueba[0], "");

        }

        [TestMethod()]
        public void Iniciar_Sesion()
        {
            List<string> Prueba = Logica_Empleado.Instancia.Iniciar_Sesion("Maxiro", "MaxiroMaxi");
            Assert.AreEqual(Prueba[0], "");
        }
        [TestMethod()]
        public void Listado()
        {
            //List<string> Prueba = Logica_Empleado.Instancia.Iniciar_Sesion("Maxiro", "MaxiroMaxi");
            Assert.AreEqual(0, Logica_Empleado.Instancia.Consultas_Propiedades_Modificadas(new Entidades.Realidad.Empleado("Maxi", "1234567890")).Count);
        }
        


    }
}