using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Entidades.Utilidades;

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

        /// <summary>
        /// Se puede crear el objeto a partir de una letura en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        public Empleado(SqlDataReader lector)
        {
            Empleado retorno = Generador_Objeto(lector);

            _Nombre = retorno.Nombre;

        }
        /// <summary>
        /// Metodo para crear el objeto a partir de una consulta en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        /// <returns>Retorna el objeto ya generado.</returns>
        public static Empleado Generador_Objeto(SqlDataReader lector)
        {
            Empleado retorno = new Empleado();
            retorno.Nombre = lector["nombre"].ToString();

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
