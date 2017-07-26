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
            List<string> prueba=  Logica_Consulta.Instancia.Alta_Consulta("0916587891", "Maxi Camilo", "11/09/2017", "23:00", 10133);
            Assert.AreEqual("", prueba[0]);
        }
    }
}