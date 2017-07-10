using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Entidades.Utilidades;

namespace Entidades.Realidad
{
    /// <summary>
    /// Tipo de propiedad, solo tiene habilitacion
    /// </summary>
    public class Local : Propiead
    {
        //Atributos
        private bool _Habilitacion = false;

        //Accesores
        public bool Habilitacion { get { return _Habilitacion; } set { _Habilitacion = value; } }

        /// <summary>
        /// Constructor Basico, Dejara como '<Sin Definir>' las variables
        /// </summary>
        public Local() : base() { }

        /// <summary>
        /// Constructor Completo 
        /// </summary>
        public Local(int Padron, string Direccion, Tipo_Accion Accion, int Cantidad_banios, decimal Metros_Cuadrados, decimal Precio, bool Habilitacion, int Cantidad_Habitaciones, Zona Zona)
            : base( Padron, Direccion, Accion, Cantidad_banios, Metros_Cuadrados, Precio, Cantidad_Habitaciones, Zona)
        {
            _Habilitacion = Habilitacion;
        }

        /// <summary>
        /// Se puede crear el objeto a partir de una letura en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        public Local (SqlDataReader lector) : base(lector)
        {
            _Habilitacion = lector["habilitacion"].ToString() == "1";

        }
        /// <summary>
        /// Metodo para crear el objeto a partir de una consulta en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        /// <returns>Retorna el objeto ya generado.</returns>
        public static Local Generador_Objeto(SqlDataReader lector)
        {
            Local retorno = new Local() { Habilitacion = lector["habilitacion"].ToString() == "1" };
            Propiead retorno_base = Generador_Objeto_Base(lector);

            retorno.Padron = retorno_base.Padron;
            retorno.Direccion = retorno_base.Direccion;
            retorno.Accion = retorno_base.Accion;
            retorno.Cantidad_Banios = retorno_base.Cantidad_Banios;
            retorno.Cantidad_Habitaciones = retorno_base.Cantidad_Habitaciones;
            retorno.Metros_Cuadrados = retorno_base.Metros_Cuadrados;
            retorno.Precio = retorno_base.Precio;
            retorno.Zona = retorno_base.Zona;
            retorno.Precio = retorno_base.Precio;
            
            return retorno;
        }

        /// <summary>
        /// Imprime en linea todas las propiedades 
        /// Nota! Solo usar en validadores o en debug.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Ver_Propiedades.En_Linea(this);
        }

    }
}
