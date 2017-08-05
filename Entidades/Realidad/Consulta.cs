using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Entidades.Utilidades;
using Entidades.Interfaces;

namespace Entidades.Realidad
{
    /// <summary>
    /// Entidad que representan una consulta de un cliente a una propiedad
    /// </summary>
    public class Consulta : Entidad
    {
        //atributos
        private string _Telefono = "<Sin Definir>";
        private string _Nombre = "<Sin Definir>";
        private DateTime _Fecha = DateTime.Now;
        private string _Hora = DateTime.Now.ToShortTimeString();

        //Atributo Externo
        private Propiedad _Propiedad = new Propiedad();

        //Accesores
        public string Telefono { get { return _Telefono; } set { _Telefono = value; } }
        public string Nombre { get { return _Nombre; } set { _Nombre = value; } }
        public DateTime Fecha { get { return _Fecha; } set { _Fecha = value; } }
        public string Hora { get { return _Hora; } set { _Hora = value; } }
        public Propiedad Propiedad { get { return  _Propiedad;  } set { _Propiedad = value; } }

        /// <summary>
        /// Constructor Basico, Dejara como '<Sin Definir>' las variables
        /// </summary>
        /// 
        public Consulta() { }
        /// <summary>
        /// Constructor Completo 
        /// </summary>
        public Consulta(string Telefono, string Nombre, DateTime Fecha, string Hora, Propiedad Propiedad) {
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
            Consulta retorno = (Consulta)Generador_Objeto(lector);

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
        public Consulta Generador_Objeto(SqlDataReader lector)
        {
            Consulta retorno = new Consulta();

            retorno.Nombre = lector["nombre"].ToString();
            retorno.Propiedad = new Propiedad() { Padron = int.Parse(lector["padron_propiedad"].ToString()) };
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

        /// <summary>
        /// Funcion necesaria para poder comunicarce con la base de datos
        /// </summary>
        /// <returns>Retorna los parametros para la comunicacion de la base de datos.</returns>
        public override Dictionary<string, object> Parametros()
        {
            Dictionary<string, object> retorno = new Dictionary<string, object>();

            retorno.Add("telefono", _Telefono);
            retorno.Add("nombre", _Nombre);
            retorno.Add("fecha", _Fecha);
            retorno.Add("hora", _Hora);
            retorno.Add("padron_propiedad", _Propiedad.Padron);
            

            return retorno;

        }

        /// <summary>
        /// Funcion donde solo retorna los items identificadores, util para verificacion
        /// </summary>
        /// <returns>Retorna los objetos primarios</returns>
        public override Dictionary<string, object> Identificadores()
        {
            Dictionary<string, object> retorno = new Dictionary<string, object>();

            retorno.Add("telefono", _Telefono);
            //retorno.Add("padron_propiedad", _Propiedad.Padron);
            retorno.Add("fecha", _Fecha);
            retorno.Add("hora", _Hora);
            return retorno;
        }
    }
}
