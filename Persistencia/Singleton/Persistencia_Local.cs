using Entidades.Realidad;
using System;
using System.Collections.Generic;
using Persistencia.Interfaces;
using System.Linq;
using System.Text;
using Entidades.Interfaces;
using System.Data.SqlClient;

namespace Persistencia.Singleton
{
    public class Persistencia_Local : IPersistencia_Local
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Persistencia_Local> _instancia = new Lazy<Persistencia_Local>(() => new Persistencia_Local());

        // No se permite usar la clase en entornos locales
        private Persistencia_Local() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Persistencia_Local Instancia { get { return _instancia.Value; } }

        #endregion

        #region Implementacion
        public int Alta(Local Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "AltaLocal", Objeto.Parametros);
        }
        public int Baja(Local Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "BajaLocal", Objeto.Identificadores);
        }
        public int Modificar(Local Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "ModificarLocal", Objeto.Parametros);
        }
        public List<Local> Listado_Activos()
        {
            Local Prototipo = new Local();
            ComandoSQL comando = new ComandoSQL("LISTADO_LOCALES_ACTIVAS");
            return comando.Generar_Listado<Local>(Prototipo.Generador_Objeto, false);
        }
        public List<Local> Listado_Todo()
        {
            Local Prototipo = new Local();
            ComandoSQL comando = new ComandoSQL("LISTADO_Locales");
            return comando.Generar_Listado<Local>(Prototipo.Generador_Objeto, false);
        }
        #endregion

        #region Generadores
        public Local Generar(int Padron)
        {
            ComandoSQL comando = new ComandoSQL("SELECCIONAR_Local(@padron)");
            comando.AgregarParametro("padron", Padron);
            Local Protoripo = new Local();
            return comando.Ejecutar_Lector<Local>(Protoripo.Generador_Objeto);
        }

        #endregion
    }
}
