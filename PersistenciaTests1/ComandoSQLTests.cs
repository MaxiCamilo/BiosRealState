using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistencia.Singleton;
using Entidades.Realidad;
using System.Collections.Generic;
using Entidades.Interfaces;

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
            //List<Entidad> lista = Persistencia_Zona.Instancia.Listado_Activos();
            //Assert.AreEqual(((Zona)lista[0]).Nombre, "");

            Zona Prueba = new Zona() { Nombre = "Sabrina Landia", Habitantes = 1, Codigo = "143", Letra_Departamento = "K" };
            //Persistencia_Zona.Instancia.Alta(Prueba);
            Fabrica_Persistencia.getPersistencia_Zona.Alta(Prueba);

            Fabrica_Persistencia.getPersistencia_Zona.Alta_Servicio(Prueba.Letra_Departamento, Prueba.Codigo, "Orinero");
            Fabrica_Persistencia.getPersistencia_Zona.Alta_Servicio(Prueba.Letra_Departamento, Prueba.Codigo, "Cacatero");
            Fabrica_Persistencia.getPersistencia_Zona.Alta_Servicio(Prueba.Letra_Departamento, Prueba.Codigo, "Pirulin");

            Assert.AreEqual(Fabrica_Persistencia.getPersistencia_Zona.Baja_Servicio(Prueba.Letra_Departamento, Prueba.Codigo, "Orinero"), 0);
            
        }
        [TestMethod()]
        public void Averiguar_Listado()
        {
            Zona Prueba = new Zona() { Nombre = "Sabrina Landia", Habitantes = 1, Codigo = "143", Letra_Departamento = "K" };
            Persistencia_Zona.Instancia.Listar_Servicios(ref Prueba);
        }
        [TestMethod()]
        public void Modificar()
        {
            Zona Prueba = new Zona() { Nombre = "Sabrina Pueblo", Habitantes = 12, Codigo = "143", Letra_Departamento = "K" };
            Persistencia_Zona.Instancia.Modificar(Prueba);
        }
        [TestMethod()]
        public void AltaPropiedad()
        {
            Propiead Prueba = new Propiead() { Accion = Propiead.Tipo_Accion.venta, Cantidad_Banios = 2, Cantidad_Habitaciones = 5, Direccion = "Estivao 1558 bis", Metros_Cuadrados = 80, Padron = 122365, Precio = 454.545M, Zona = new Zona("k","143", "Nada", -1),Empleado = new Empleado("Maxi","") };
            Assert.AreEqual(Persistencia_Propiedad.Instancia.Alta(Prueba),0);
        }
        [TestMethod()]
        public void BuscarZona()
        {
            Propiead Prueba = new Propiead() { Accion = Propiead.Tipo_Accion.venta, Cantidad_Banios = 2, Cantidad_Habitaciones = 5, Direccion = "Estivao 1558 bis", Metros_Cuadrados = 80, Padron = 122365, Precio = 454.545M, Zona = new Zona("k", "143", "Nada", -1), Empleado = new Empleado("Maxi", "") };
            Persistencia_Propiedad.Instancia.Detallar_Zona(ref Prueba);
            Assert.AreNotEqual(Prueba.Zona.Servicios[0], "");
        }
        [TestMethod()]
        public void BuscarEmpleado()
        {
            Propiead Prueba = new Propiead() { Accion = Propiead.Tipo_Accion.venta, Cantidad_Banios = 2, Cantidad_Habitaciones = 5, Direccion = "Estivao 1558 bis", Metros_Cuadrados = 80, Padron = 122365, Precio = 454.545M, Zona = new Zona("k", "143", "Nada", -1) };
            Persistencia_Propiedad.Instancia.Detalle_Empleado(ref Prueba);
            Assert.AreNotEqual(Prueba.Empleado.Nombre, "");
        }
        [TestMethod()]
        public void ConsultaPropiedad()
        {
            Propiead Prueba = Persistencia_Propiedad.Instancia.Generar(52568);
            List<Consulta> retorno = Persistencia_Propiedad.Instancia.Listar_Consultas(Prueba);
            Assert.AreEqual(retorno[0].Nombre, "Maxi Camilo");
        }
        [TestMethod()]
        public void AltaEmpleado()
        {
            Empleado Prueba = new Empleado() { Nombre = "Sabrina", Contrasenia = "1234567890" };
            Assert.AreEqual(Persistencia_Empleado.Instancia.Alta(Prueba), 0);
        }

        [TestMethod()]
        public void BajaEmpleado()
        {
            Empleado Prueba = new Empleado() { Nombre = "Sabrina", Contrasenia = "1234567890" };
            Assert.AreEqual(Persistencia_Empleado.Instancia.Baja(Prueba), 0);
        }
        [TestMethod()]
        public void ModificarEmpleado()
        {
            Empleado Prueba = new Empleado() { Nombre = "Sabrina", Contrasenia = "1234567890" };
            Assert.AreEqual(Persistencia_Empleado.Instancia.Modificar(Prueba, "1234567899"), 0);
        }

        [TestMethod()]
        public void IniciarSesion()
        {
            Empleado Prueba = new Empleado() { Nombre = "Sabrina", Contrasenia = "1234567899" };
            Assert.AreEqual(Persistencia_Empleado.Instancia.Inicio_Sesion(Prueba), 0);
        }



    }
}