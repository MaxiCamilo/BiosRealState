using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logica.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica.Singleton.Tests
{
    [TestClass()]
    public class Logica_ConsultaTests
    {
        [TestMethod()]
        public void Alta_ConsultaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Alta_Consulta()
        {
            List<string> prueba=  Logica_Consulta.Instancia.Alta_Consulta("091689612", "Maxi Camilo", "11/09/2017", "23:00", 10133);
            Assert.AreEqual("", prueba[0]);
        }
        [TestMethod()]
        public void Baja_Consulta()
        {
            List<string> prueba = Logica_Consulta.Instancia.Baja_Consulta("091689612", "11/09/2017","23:00");
            Assert.AreEqual("", prueba[0]);
        }
        [TestMethod()]
        public void Listado_Consulta()
        {
            List<Entidades.Realidad.Consulta> prueba = Logica_Consulta.Instancia.Listado(52568);
            Assert.AreEqual(0, prueba.Count);
        }
        [TestMethod()]
        public void Listado_Propiedad()
        {
            List<Entidades.Realidad.Consulta> prueba = Logica_Consulta.Instancia.Consultas_Propiedad(52568);
            Assert.AreEqual(0, prueba.Count);
        }
    }
}