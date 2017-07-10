using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Entidades.Utilidades;

namespace Entidades.Realidad
{
    /// <summary>
    /// Entidad que representan una consulta de un cliente a una propiedad
    /// </summary>
    public class Consulta
    {
        //atributos
        private string _Telefono = "<Sin Definir>";
        private string _Nombre = "<Sin Definir>";
        private DateTime _Fecha = DateTime.Now;
        private string _Hora = DateTime.Now.ToShortTimeString();

        //Atributo Externo
        private Propiead _Propiedad = new Propiead();

        //Accesores
        public string Telefono { get { return _Telefono; } set { _Telefono = value; } }
        public string Nombre { get { return _Nombre; } set { _Nombre = value; } }
        public DateTime Fecha { get { return _Fecha; } set { _Fecha = value; } }
        public string Hora { get { return _Hora; } set { _Hora = value; } }
        public Propiead Propiedad { get { return  _Propiedad;  } set { _Propiedad = value; } }

        /// <summary>
        /// Constructor Basico, Dejara como '<Sin Definir>' las variables
        /// </summary>
        /// 
        public Consulta() { }
        /// <summary>
        /// Constructor Completo 
        /// </summary>
        public Consulta(string Telefono, string Nombre, DateTime Fecha, string Hora, Propiead Propiedad) {
            _Telefono = Telefono;
            _Nombre = Nombre;
            _Fecha = Fecha;
            _Hora = Hora;
            _Propiedad = Propiedad;
        }

        /// <summary>
        /// Se puede crear el objeto a partir de una letura en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        public Consulta(SqlDataReader lector)
        {
            Consulta retorno = Generador_Objeto(lector);

            _Nombre = retorno.Nombre;
            _Telefono = retorno.Telefono;
            _Fecha = retorno.Fecha;
            _Hora = retorno.Hora;
            _Propiedad = retorno.Propiedad;

        }
        /// <summary>
        /// Metodo para crear el objeto a partir de una consulta en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        /// <returns>Retorna el objeto ya generado.</returns>
        public static Consulta Generador_Objeto(SqlDataReader lector)
        {
            Consulta retorno = new Consulta();

            retorno.Nombre = lector["nombre"].ToString();
            retorno.Propiedad = new Propiead() { Padron = int.Parse(lector["padron_propiedad"].ToString()) };
            retorno.Telefono = lector["telefono"].ToString();
            retorno.Hora = lector["hora"].ToString();
            retorno.Fecha = Convert.ToDateTime(lector["fecha"].ToString());

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
