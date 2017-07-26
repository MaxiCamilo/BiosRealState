using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logica.Singelton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica.Singelton.Tests
{
    [TestClass()]
    public class Logica_ZonaTests
    {
        [TestMethod()]
        public void Agregar_ZonaTest()
        {
            
            List<string> Prueba = Logica_Zona.Instancia.Alta_Zona("La Takara","26a","a", 12000);
            Assert.AreEqual(Prueba.Count, 0);

        }
        [TestMethod()]
        public void Eliminar_ZonaTest()
        {
            List<string> Prueba = Logica_Zona.Instancia.Baja_Zona('a', "26a");
            Assert.AreEqual(Prueba.Count, 0);

        }
        [TestMethod()]
        public void Modificar_ZonaTest()
        {
            List<string> Prueba = Logica_Zona.Instancia.Modificar_Zona("La Takara Enojona", "26a", 'a', 21000);
            Assert.AreEqual(Prueba.Count, 0);

        }
        [TestMethod()]
        public void Agregar_Servicio()
        {
            List<string> Prueba = Logica_Zona.Instancia.Agregar_Servicio(new Entidades.Realidad.Zona() { Codigo = "26a", Letra_Departamento = "a" }, "Las Mañanitas");
            Assert.AreEqual(Prueba.Count, 0);

        }
        [TestMethod()]
        public void Eliminar_Servicio()
        {
            List<string> Prueba = Logica_Zona.Instancia.Eliminar_Servicio(new Entidades.Realidad.Zona() { Codigo = "26a", Letra_Departamento = "a" }, "Las Mañanitas");
            Assert.AreEqual(Prueba.Count, 0);

        }
    }
}