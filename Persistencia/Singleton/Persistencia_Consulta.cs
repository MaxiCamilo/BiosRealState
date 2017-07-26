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
    public class Persistencia_Consulta : IPersistencia_Consulta
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Persistencia_Consulta> _instancia = new Lazy<Persistencia_Consulta>(() => new Persistencia_Consulta());

        // No se permite usar la clase en entornos locales
        private Persistencia_Consulta() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Persistencia_Consulta Instancia { get { return _instancia.Value; } }

        #endregion

        #region Implementacion
        public int Alta(Consulta Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "AltaConsulta", Objeto.Parametros);
        }
        public int Baja(Consulta Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "BajaConsulta", Objeto.Identificadores);
        }
        public List<Consulta> Listado(int padron)
        {
            Consulta Prototipo = new Consulta();
            ComandoSQL comando = new ComandoSQL("LISTADO_CONSULTAS_PROPIEDAD(@padron)");
            comando.AgregarParametro("padron", padron);
            return comando.Generar_Listado<Consulta>(Prototipo.Generador_Objeto, false);
        }
        #endregion

        #region Generadores
        public Consulta Generar(DateTime Fecha, string telefono)
        {
            ComandoSQL comando = new ComandoSQL("SELECCIONAR_CONSULTA (@telefono, @fecha)");
            comando.AgregarParametro("telefono", telefono);
            comando.AgregarParametro("fecha", Fecha);
            Consulta Protoripo = new Consulta();
            return comando.Ejecutar_Lector<Consulta>(Protoripo.Generador_Objeto);
        }    
        
        public List<Consulta> Consultas_Propiedad(int Padron)
        {
            Consulta Prototipo = new Consulta();
            ComandoSQL comando = new ComandoSQL("LISTADO_CONSULTAS_PROPIEDAD (@padron)");
            comando.AgregarParametro("padron", Padron);
            return comando.Generar_Listado<Consulta>(Prototipo.Generador_Objeto, false);
        } 
        #endregion


    }
}