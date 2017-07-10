using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Entidades.Utilidades;

namespace Entidades.Realidad
{
    /// <summary>
    /// Entidad que representan las propiedades a la venta 
    /// </summary>
    public class Propiead
    {
        public enum Tipo_Accion {SinDefinir, alquiler , venta , permuta }

        //atributos
        protected int _Padron = -1;
        protected string _Direccion = "<Sin Definir>";
        protected Tipo_Accion _Accion = Tipo_Accion.SinDefinir;
        protected int _Cantidad_Banios = -1;
        protected int _Cantidad_Habitaciones = -1;
        protected decimal _Metros_Cuadrados = -1;
        protected decimal _Precio = -1;

        //Atributos Externos
        private Zona _Zona = new Zona();

        //Accesores
        public int Padron { get { return _Padron; } set { _Padron = value; } }
        public string Direccion { get { return _Direccion; } set { _Direccion = value; } }
        public Tipo_Accion Accion { get { return _Accion; } set { _Accion = value; } }
        public int Cantidad_Banios { get { return _Cantidad_Banios; } set { _Cantidad_Banios = value; } }
        public int Cantidad_Habitaciones { get { return _Cantidad_Habitaciones; } set { _Cantidad_Habitaciones = value; } }
        public decimal Metros_Cuadrados { get { return _Metros_Cuadrados; } set { _Metros_Cuadrados = value; } }
        public decimal Precio { get { return _Precio; } set { _Precio = value; } }
        public Zona Zona { get { return _Zona; } set { _Zona = value; } }

        /// <summary>
        /// Constructor Basico, Dejara como '<Sin Definir>' las variables
        /// </summary>
        public Propiead() { }
        /// <summary>
        /// Constructor Completo 
        /// </summary>
        public Propiead(int Padron, string Direccion, Tipo_Accion Accion, int Cantidad_banios, decimal Metros_Cuadrados, decimal Precio, int Cantidad_Habitaciones, Zona Zona) {
            _Padron = Padron;
            _Direccion = Direccion;
            _Accion = Accion;
            _Cantidad_Banios = Cantidad_banios;
            _Cantidad_Habitaciones = Cantidad_Habitaciones;
            _Metros_Cuadrados = Metros_Cuadrados;
            _Precio = Precio;
            _Zona = Zona;
        }

        /// <summary>
        /// Se puede crear el objeto a partir de una letura en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        public Propiead (SqlDataReader lector)
        {
            Propiead retorno = Generador_Objeto_Base(lector);

            _Padron = retorno.Padron;
            _Direccion = retorno.Direccion;
            _Accion = retorno.Accion;
            _Cantidad_Banios = retorno.Cantidad_Banios;
            _Cantidad_Habitaciones = retorno.Cantidad_Habitaciones;
            _Metros_Cuadrados = retorno.Metros_Cuadrados;
            _Precio = retorno.Precio;
            _Zona = retorno.Zona;
            _Precio = retorno.Precio;

        }
        /// <summary>
        /// Metodo para crear el objeto a partir de una consulta en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        /// <returns>Retorna el objeto ya generado.</returns>
        public static Propiead Generador_Objeto_Base(SqlDataReader lector)
        {
            Propiead retorno = new Propiead();

            retorno.Padron = int.Parse(lector["padron"].ToString());
            retorno.Zona = new Zona() { Codigo = lector["codigo_zona"].ToString(), Letra_Departamento = lector["letra_departamento"].ToString() };

            Tipo_Accion accion_convertida;
            Enum.TryParse(lector["accion"].ToString(), out accion_convertida);
            retorno.Accion = accion_convertida;

            retorno.Cantidad_Banios = int.Parse(lector["cantidad_banio"].ToString());
            retorno.Cantidad_Habitaciones = int.Parse(lector["cantidad_habitaciones"].ToString());
            retorno.Direccion = lector["direccion"].ToString();
            retorno.Metros_Cuadrados = int.Parse(lector["metros_cuadrados"].ToString());
            retorno.Precio = decimal.Parse(lector["precio"].ToString());

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
