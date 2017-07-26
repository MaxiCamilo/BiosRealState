using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Persistencia
{
    public class Persistencia_Automatica
    {
        public delegate Dictionary<string, object> Parametros();
        
        /*
        public static List<Entidad> Iniciar_Listado(Entidad Prototipo, string Nombre_Listador, bool esProcedimiento = false)
        {

            ComandoSQL comando = new ComandoSQL(Nombre_Listador);
            return comando.Mostrar_Seleccion(Prototipo.Generador_Objeto, esProcedimiento);            
        }
        */
        public static int Iniciar_Transaccion(Entidad Objeto, string Nombre_Trasaccion, Parametros Parametro_Funcion, Dictionary<string, object> Parametros_Extras = null)
        {
            Dictionary<string, object> Agregar_Parametros = Parametro_Funcion();
            
            ComandoSQL comando = new ComandoSQL(Nombre_Trasaccion);
            
            if(Agregar_Parametros.Count != 0)
            {
                foreach (KeyValuePair<string, object> par in Agregar_Parametros)
                {
                    comando.AgregarParametro(par.Key, par.Value);
                }
            }

            if (Parametros_Extras != null)
            {
                if (Parametros_Extras.Count != 0)
                {
                    foreach (KeyValuePair<string, object> par in Parametros_Extras)
                    {
                        comando.AgregarParametro(par.Key, par.Value);
                    }
                }                
            }

            return comando.Ejecutar_Transaccion();
        }
        public static int Iniciar_Funcion(Entidad Objeto, Parametros Parametro_Funcion, string Nombre_Trasaccion)
        {
            ComandoSQL comando = new ComandoSQL(Nombre_Trasaccion);
            Dictionary<string, object> Agregar_Parametros = Parametro_Funcion();
            if (Agregar_Parametros.Count != 0)
            {
                foreach (KeyValuePair<string, object> par in Agregar_Parametros)
                {
                    comando.AgregarParametro(par.Key, par.Value);
                }
            }
            return comando.Ejecutar();
        }
        public static int Ejecucion_Manual(string Nombre, List<SqlParameter> Parametros,bool esTransaccion = false)
        {
            ComandoSQL comando = new ComandoSQL(Nombre);
            if (Parametros.Count != 0)
            {
                foreach (SqlParameter par in Parametros)
                {
                    comando.Comando.Parameters.Add(par);
                }
            }
            if (esTransaccion)
            {
                return comando.Ejecutar_Transaccion();
            }
            else
            {
                return comando.Ejecutar();
            }
            
        }
        /*
        public static Dictionary<string, object> Agregar_Mas_Parametros(Dictionary<string, object> Parametros_Extras, Parametros Parametro_Funcion)
        {
            Dictionary<string, object> retorno = Parametro_Funcion();
            retorno.Concat(Parametros_Extras);
            return retorno;
        }
        */
    }
}
