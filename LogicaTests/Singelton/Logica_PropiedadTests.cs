using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logica.Singelton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica.Singelton.Tests
{
    [TestClass()]
    public class Logica_PropiedadTests
    {
        //[TestMethod()]
        //public void Alta_PropiedadTest()
        //{
        //    List<string> Prueba = Logica_Propiedad.Instancia.Verificar_Propiedad("1585", "Estivao 1558", 12235, "venta", 5, 15, 50, "26a", 'a', "Maxi");
        //    Assert.AreEqual(Prueba[0], "");
        //}
        [TestMethod()]
        public void Alta_Casa()
        {
            List<string> Prueba = Logica_Casa.Instancia.Alta_casa("158555", "Estivao 1558", 12235, "venta", 5, 15, 50, "26a", 'a', "Maxi",true,125);
            Assert.AreEqual(Prueba.Count, 0);
        }

        [TestMethod()]
        public void Baja_Casa()
        {
            List<string> Prueba = Logica_Casa.Instancia.Baja_casa("158555");
            Assert.AreEqual(Prueba.Count, 0);
        }

        [TestMethod()]
        public void Modificar_Casa()
        {
            List<string> Prueba = Logica_Casa.Instancia.Modificar_casa("158555", "Estivao 21", 21, "alquiler", 21, 21, 21, "26a", 'a', "Maxi", false, 21);
            Assert.AreEqual(Prueba.Count, 0);
        }



        [TestMethod()]
        public void Alta_Apartamento()
        {
            List<string> Prueba = Logica_Apartamento.Instancia.Alta_apartamento("158555", "Estivao 1558", 12235, "venta", 5, 15, 50, "26a", 'a', "Maxi", true, 125);
            Assert.AreEqual(Prueba.Count, 0);
        }

        [TestMethod()]
        public void Baja_Apartamento()
        {
            List<string> Prueba = Logica_Apartamento.Instancia.Baja_apartamento("158555");
            Assert.AreEqual(Prueba.Count, 0);
        }

        [TestMethod()]
        public void Modificar_Apartamento()
        {
            List<string> Prueba = Logica_Apartamento.Instancia.Modificar_apartamento("158555", "Estivao 21", 21, "alquiler", 21, 21, 21, "26a", 'a', "Maxi", false, 21);
            Assert.AreEqual(Prueba.Count, 0);
        }

        [TestMethod()]
        public void Alta_Local()
        {
            List<string> Prueba = Logica_Local.Instancia.Alta_local("158555", "Estivao 1558", 12235, "venta", 5, 15, 50, "26a", 'a', "Maxi", true);
            Assert.AreEqual(Prueba.Count, 0);
        }

        [TestMethod()]
        public void Baja_Local()
        {
            List<string> Prueba = Logica_Local.Instancia.Baja_local("158555");
            Assert.AreEqual(Prueba.Count, 0);
        }

        [TestMethod()]
        public void Modificar_Local()
        {
            List<string> Prueba = Logica_Local.Instancia.Modificar_local("158555", "Estivao 201", 21, "alquiler", 21, 21, 21, "26a", 'a', "Maxi", false);
            Assert.AreEqual(Prueba.Count, 0);
        }

    }
}