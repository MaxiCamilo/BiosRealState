using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Realidad
{
    /// <summary>
    /// Entidad que representan una persona encargada de la gestion de la página
    /// </summary>
    public class Empleado
    {
        //atributos
        string _Nombre = "<Sin Definir>";
        string _Contrasenia = "<Sin Definir>";

        //Accesores
        public string Nombre { get { return _Nombre; } set { _Contrasenia = value; } }
        public string Contrasenia { get { return _Contrasenia; } set { _Contrasenia = value; } }

        /// <summary>
        /// Constructor Basico, Dejara como '<Sin Definir>' las variables
        /// </summary>
        /// 
        public Empleado() { }
        /// <summary>
        /// Constructor Completo 
        /// </summary>
        public Empleado(string Nombre, string Contrasenia) {
            _Nombre = Nombre;
            _Contrasenia = Contrasenia;
        }
    }
}
