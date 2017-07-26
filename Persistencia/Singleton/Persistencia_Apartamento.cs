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
    public class Persistencia_Apartamento : IPersistencia_Apartamento
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Persistencia_Apartamento> _instancia = new Lazy<Persistencia_Apartamento>(() => new Persistencia_Apartamento());

        // No se permite usar la clase en entornos locales
        private Persistencia_Apartamento() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Persistencia_Apartamento Instancia { get { return _instancia.Value; } }

        #endregion

        #region Implementacion
        public int Alta(Apartamento Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "AltaApartamento", Objeto.Parametros);
        }
        public int Baja(Apartamento Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "BajaApartamento", Objeto.Identificadores);
        }
        public int Modificar(Apartamento Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "ModificarApartamento", Objeto.Parametros);
        }
        public List<Apartamento> Listado_Activos()
        {
            Apartamento Prototipo = new Apartamento();
            ComandoSQL comando = new ComandoSQL("LISTADO_APARTAMENTO_ACTIVAS");
            return comando.Generar_Listado<Apartamento>(Prototipo.Generador_Objeto, false);
        }
        public List<Apartamento> Listado_Todo()
        {
            Apartamento Prototipo = new Apartamento();
            ComandoSQL comando = new ComandoSQL("LISTADO_APARTAMENTO");
            return comando.Generar_Listado<Apartamento>(Prototipo.Generador_Objeto, false);
        }
        #endregion

        #region Generadores
        public Apartamento Generar(int Padron)
        {
            ComandoSQL comando = new ComandoSQL("SELECCIONAR_Apartamento(@padron)");
            comando.AgregarParametro("padron", Padron);
            Apartamento Protoripo = new Apartamento();
            return comando.Ejecutar_Lector<Apartamento>(Protoripo.Generador_Objeto);
        }

        #endregion
    }
}
