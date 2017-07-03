using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
