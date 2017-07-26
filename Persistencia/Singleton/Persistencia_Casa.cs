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
    public class Persistencia_Casa : IPersistencia_Casa
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Persistencia_Casa> _instancia = new Lazy<Persistencia_Casa>(() => new Persistencia_Casa());

        // No se permite usar la clase en entornos locales
        private Persistencia_Casa() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Persistencia_Casa Instancia { get { return _instancia.Value; } }

        #endregion

        #region Implementacion
        public int Alta(Casa Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "AltaCasa", Objeto.Parametros);
        }
        public int Baja(Casa Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "BajaCasa", Objeto.Identificadores);
        }
        public int Modificar(Casa Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "ModificarCasa", Objeto.Parametros);
        }
        public List<Casa> Listado_Activos()
        {
            Casa Prototipo = new Casa();
            ComandoSQL comando = new ComandoSQL("LISTADO_CASAS_ACTIVAS");
            return comando.Generar_Listado<Casa>(Prototipo.Generador_Objeto, false);
        }
        public List<Casa> Listado_Todo()
        {
            Casa Prototipo = new Casa();
            ComandoSQL comando = new ComandoSQL("LISTADO_CASAS");
            return comando.Generar_Listado<Casa>(Prototipo.Generador_Objeto, false);
        }
        #endregion

        #region Generadores
        public Casa Generar(int Padron)
        {
            ComandoSQL comando = new ComandoSQL("SELECCIONAR_CASA(@padron)");
            comando.AgregarParametro("padron", Padron);
            Casa Protoripo = new Casa();
            return comando.Ejecutar_Lector<Casa>(Protoripo.Generador_Objeto);
        }        

        #endregion
    }
}
