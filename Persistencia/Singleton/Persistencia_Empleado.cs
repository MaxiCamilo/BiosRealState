using Entidades.Realidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Interfaces;
using System.Data.SqlClient;
using Persistencia.Interfaces;

namespace Persistencia.Singleton
{
    public class Persistencia_Empleado : IPersistencia_Empleado
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Persistencia_Empleado> _instancia = new Lazy<Persistencia_Empleado>(() => new Persistencia_Empleado());

        // No se permite usar la clase en entornos locales
        private Persistencia_Empleado() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Persistencia_Empleado Instancia { get { return _instancia.Value; } }

        #endregion

        #region Implementacion
        public int Alta(Empleado Objeto)
        {
            return Persistencia_Automatica.Ejecucion_Manual("AltaEmpleado", new List<SqlParameter>()
            { new SqlParameter("@nombre",Objeto.Nombre),new SqlParameter("@contrasenia",Objeto.Contrasenia)  },true);
        }
        public int Baja(Empleado Objeto)
        {
            return Persistencia_Automatica.Ejecucion_Manual("BajaEmpleado", new List<SqlParameter>()
            { new SqlParameter("@nombre",Objeto.Nombre),new SqlParameter("@contrasenia",Objeto.Contrasenia)  }, true);
        }
        public int Modificar(Empleado Objeto,string NuevaContrasenia)
        {
            return Persistencia_Automatica.Ejecucion_Manual("ModificarEmpleado", new List<SqlParameter>()
            { new SqlParameter("@nombre",Objeto.Nombre),new SqlParameter("@contraseniaVieja",Objeto.Contrasenia),new SqlParameter("@contraseniaNueva",NuevaContrasenia)  }, true);
        }
        public List<Empleado> Listado(int padron)
        {
            Empleado Prototipo = new Empleado();
            ComandoSQL comando = new ComandoSQL("LISTADO_CONSULTAS_PROPIEDAD(@padron)");
            comando.AgregarParametro("padron", padron);
            return comando.Generar_Listado<Empleado>(Prototipo.Generador_Objeto, false);
        }
        #endregion

        #region Generadores
        public List<Propiedad> Consultas_Propiedades_Modificadas(Empleado Empleado_Seleccionado)
        {
            Propiedad Prototipo = new Propiedad();
            ComandoSQL comando = new ComandoSQL("LISTADO_ACCIONES_EMPLEADO (@nombre)");
            comando.AgregarParametro("nombre", Empleado_Seleccionado.Nombre);
            return comando.Generar_Listado<Propiedad>(Prototipo.Generador_Objeto, false);
        }
        #endregion

        #region Inicio Sesion
        /// <summary>
        /// Funcion que verifica si el usuario es correcto
        /// </summary>
        /// <param name="Empleado_Seleccionado">Empleado que se quiere iniciar sesion.</param>
        /// <returns>Retorna un numero:
        /// 0: Es Valido
        /// 1: Nombre Invalido
        /// 2: Contraseña invalida
        /// </returns>
        public int Inicio_Sesion(Empleado Empleado_Seleccionado)
        {
            ComandoSQL comando = new ComandoSQL("InicioSesion");
            comando.AgregarParametro("nombre", Empleado_Seleccionado.Nombre);
            comando.AgregarParametro("contrasenia", Empleado_Seleccionado.Contrasenia);
            return comando.Ejecutar();
        }

        #endregion

    }
}