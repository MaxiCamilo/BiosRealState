using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Interfaces;
using Entidades.Realidad;
using System.Data.SqlClient;
using Persistencia.Interfaces;

namespace Persistencia.Singleton
{
    public class Persistencia_Zona : IPersistencia_Zona
    {
        

        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Persistencia_Zona> _instancia = new Lazy<Persistencia_Zona>(() => new Persistencia_Zona());

        // No se permite usar la clase en entornos locales
        private Persistencia_Zona(){}

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Persistencia_Zona Instancia{get{return _instancia.Value;}}

        #endregion

        #region Implementacion
        public int Alta(Zona Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "AltaZona", Objeto.Parametros);
        }
        public int Baja(Zona Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "BajaZona", Objeto.Identificadores);
        }
        public int Modificar(Zona Objeto)
        {
            return Persistencia_Automatica.Iniciar_Transaccion(Objeto, "ModificarZona", Objeto.Parametros);
            
        }
        public List<Zona> Listado_Activos()
        {
            Zona Prototipo = new Zona();
            //return Persistencia_Automatica.Iniciar_Listado(Prototipo, "LISTADO_ZONAS_ACTIVAS");
            ComandoSQL comando = new ComandoSQL("LISTADO_ZONAS_ACTIVAS");
            return comando.Generar_Listado<Zona>(Prototipo.Generador_Objeto,false);
        }
        public List<Zona> Listado_Todo()
        {
            Zona Prototipo = new Zona();
            //return Persistencia_Automatica.Iniciar_Listado(Prototipo, "LISTADO_ZONAS_ACTIVAS");
            ComandoSQL comando = new ComandoSQL("LISTADO_ZONAS");
            return comando.Generar_Listado<Zona>(Prototipo.Generador_Objeto, false);
        }

        #endregion

        #region Servicios
        public int Alta_Servicio(string Letra_Apartamento, string Codigo, string Nombre_Servicio)
        {
            ComandoSQL comando = new ComandoSQL("AltaServicio");
            comando.AgregarParametro("codigo_zona", Codigo);
            comando.AgregarParametro("letra_departamento", Letra_Apartamento);
            comando.AgregarParametro("nombre", Nombre_Servicio);
            return comando.Ejecutar_Transaccion();
        }

        public int Baja_Servicio(string Letra_Apartamento, string Codigo, string Nombre_Servicio)
        {
            ComandoSQL comando = new ComandoSQL("BajaServicio");
            comando.AgregarParametro("codigo_zona", Codigo);
            comando.AgregarParametro("letra_departamento", Letra_Apartamento);
            comando.AgregarParametro("nombre", Nombre_Servicio);
            return comando.Ejecutar_Transaccion();
        }

        public void Listar_Servicios(ref Zona ZonaSeleccionada)
        {
            Func<SqlDataReader, string> generador = (SqlDataReader par) =>
            {
                return par["nombre"].ToString();
            };

            //ComandoSQL comando = new ComandoSQL("LISTADO_SERVICIOS_ZONA('" + ZonaSeleccionada.Codigo + "', '" + ZonaSeleccionada.Letra_Departamento + "')");
            ComandoSQL comando = new ComandoSQL("LISTADO_SERVICIOS_ZONA(@codigo,@letra)");
            comando.AgregarParametro("codigo", ZonaSeleccionada.Codigo);
           comando.AgregarParametro("letra", ZonaSeleccionada.Letra_Departamento); 
           ZonaSeleccionada.Servicios = comando.Generar_Listado<string>(generador);
        }

        #endregion

        #region Generadores

        public Zona Generar(string codigo, string letra_departamento)
        {
            ComandoSQL comando = new ComandoSQL("SELECCIONAR_PROPIEDAD(@codigo, @letra_departamento)");
            comando.AgregarParametro("codigo", codigo);
            comando.AgregarParametro("letra_departamento", letra_departamento);
            Zona Protoripo = new Zona();
            return comando.Ejecutar_Lector<Zona>(Protoripo.Generador_Objeto);
        }

        #endregion




    }
}
