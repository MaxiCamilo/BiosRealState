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
    public class Persistencia_Propiedad : IPersistencia_Propiedad
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Persistencia_Propiedad> _instancia = new Lazy<Persistencia_Propiedad>(() => new Persistencia_Propiedad());

        // No se permite usar la clase en entornos locales
        private Persistencia_Propiedad() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Persistencia_Propiedad Instancia { get { return _instancia.Value; } }

        #endregion

        #region Implementacion
        public int Alta(Propiead Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "AltaPropiedad", Objeto.Parametros);
        }
        public int Baja(Propiead Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "BajaPropiedad", Objeto.Identificadores);
        }
        public int Modificar(Propiead Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "ModificarPropiedad", Objeto.Parametros);
        }
        public List<Propiead> Listado_Activos()
        {
            Propiead Prototipo = new Propiead();
            ComandoSQL comando = new ComandoSQL("LISTADO_PROPIEDADES_ACTIVAS");
            return comando.Generar_Listado<Propiead>(Prototipo.Generador_Objeto, false);
        }
        public List<Propiead> Listado_Todo()
        {
            Propiead Prototipo = new Propiead();
            ComandoSQL comando = new ComandoSQL("LISTADO_PROPIEDADES");
            return comando.Generar_Listado<Propiead>(Prototipo.Generador_Objeto, false);
        }

        public List<Consulta> Listar_Consultas(Propiead Propiedad_Elegida)
        {
            ComandoSQL comando = new ComandoSQL("LISTADO_CONSULTAS_PROPIEDAD(@padron)");
            comando.AgregarParametro("padron", Propiedad_Elegida.Padron);
            Consulta Prototipo = new Consulta();
            return comando.Generar_Listado<Consulta>(Prototipo.Generador_Objeto);
        }

        #endregion

        #region Generadores
        public Propiead Generar(int Padron)
        {
            ComandoSQL comando = new ComandoSQL("SELECCIONAR_PROPIEDAD(@padron)");
            comando.AgregarParametro("padron", Padron);
            Propiead Protoripo = new Propiead();
            return comando.Ejecutar_Lector<Propiead>(Protoripo.Generador_Objeto);
        }

        public void Detallar_Zona(ref Propiead Propiedad_Elegida)
        {            
            ComandoSQL comando = new ComandoSQL("DETALLAR_ZONA_PADRON(@padron)");
            comando.AgregarParametro("padron", Propiedad_Elegida.Padron);
            Zona Prototipo = new Zona();
            Propiedad_Elegida.Zona = comando.Ejecutar_Lector<Zona>(Prototipo.Generador_Objeto);
            Prototipo = Propiedad_Elegida.Zona;
            Persistencia_Zona.Instancia.Listar_Servicios(ref Prototipo);
            Propiedad_Elegida.Zona = Prototipo;
        }

        public void Detalle_Empleado(ref Propiead Propiedad_Elegida)
        {
            ComandoSQL comando = new ComandoSQL("LISTADO_EMPLEADO_PROPIEDAD(@padron)");
            comando.AgregarParametro("padron", Propiedad_Elegida.Padron);
            Empleado Prototipo = new Empleado();
            Propiedad_Elegida.Empleado = comando.Ejecutar_Lector<Empleado>(Prototipo.Generador_Objeto);
        }

        

        #endregion


    }
}
