using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades.Validadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Validadores.Tests
{
    [TestClass()]
    public class NumerosTests
    {
        [TestMethod()]
        public void ProbarNumero()
        {
            Object Prueba = "5555";
            Numeros<int> num = new Numeros<int>();
            num.Validar();
            Assert.AreEqual(Prueba, 5555);
            //Assert.Fail();
        }
        [TestMethod()]
        public void ProbarLimite()
        {
            Decimal Prueba = 10;

            Limite validador = new Limite() { Maximo = 10, Minimo = 1, Valor = Prueba };
            

            Assert.AreEqual(validador.Validar(),true);
            //Assert.Fail();
        }
    }
}