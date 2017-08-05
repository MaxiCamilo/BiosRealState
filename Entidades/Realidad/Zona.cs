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
    /// Entidad que representa un territorio compuesto por propiedades y servicios
    /// </summary>
    public class Zona : Entidad
    {
        //Atributos
        private string _Letra_departamento = "<Sin Definir>";
        private string _Codigo = "<Sin Definir>";
        private string _Nombre = "<Sin Definir>";
        private int _Habitantes = -1;
        //Debido a que los servicios, lo unico que contiene es su nombre, no vale la pena hacer otra entidad (clase)
        private List<String> _Servicios = new List<string>();

        //Accesores
        public string Letra_Departamento { get { return _Letra_departamento; } set { _Letra_departamento = value; } }
        public string Codigo { get { return _Codigo; } set { _Codigo = value; } }
        public string Nombre { get { return _Nombre; } set { _Nombre = value; } }
        public int Habitantes { get { return _Habitantes; } set { _Habitantes = value; } }
        public List<String> Servicios { get { return _Servicios; } set { _Servicios = value; } }

        /// <summary>
        /// Constructor Basico, Dejara como '<Sin Definir>' las variables
        /// </summary>
        public Zona() { }
        /// <summary>
        /// Constructor Completo (excepto por los servicios, se agregan aparte)
        /// </summary>
        public Zona(string Letra_Departamento, string Codigo, string Nombre, int Habitantes)
        {
            _Letra_departamento = Letra_Departamento;
            _Codigo = Codigo;
            _Nombre = Nombre;
            _Habitantes = Habitantes;
        }
        /// <summary>
        /// Se puede crear el objeto a partir de una letura en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        public Zona(SqlDataReader lector)
        {
            Zona retorno = (Zona)Generador_Objeto(lector);

            _Letra_departamento = retorno.Letra_Departamento;
            _Codigo = retorno.Codigo;
            _Nombre = retorno.Nombre;
            _Habitantes = retorno.Habitantes;

        }
        /// <summary>
        /// Metodo para crear el objeto a partir de una consulta en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        /// <returns>Retorna el objeto ya generado.</returns>
        public Zona Generador_Objeto(SqlDataReader lector)
        {
            Zona retorno = new Zona();
            retorno.Letra_Departamento = lector["letra_departamento"].ToString();
            retorno.Codigo = lector["codigo"].ToString();
            retorno.Nombre = lector["nombre"].ToString();
            retorno.Habitantes = int.Parse(lector["habitantes"].ToString());

            return retorno;
        }

        /// <summary>
        /// Imprime en linea todas las propiedades 
        /// Nota! Solo usar en validadores o en debug.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "" + Nombre + " (Código: " + Codigo + " y Departamento " + Letra_Departamento + ")";
        }
        
        /// <summary>
        /// Funcion necesaria para poder comunicarce con la base de datos
        /// </summary>
        /// <returns>Retorna los parametros para la comunicacion de la base de datos.</returns>
        public override Dictionary<string, object> Parametros()
        {
            Dictionary<string, object> retorno = new Dictionary<string, object>();
            
            retorno.Add("letra_departamento", _Letra_departamento);
            retorno.Add("codigo", _Codigo);
            retorno.Add("nombre", _Nombre);
            retorno.Add("habitantes", _Habitantes);
            return retorno;
            
        }


        /// <summary>
        /// Funcion donde solo retorna los items identificadores, util para verificacion
        /// </summary>
        /// <returns>Retorna los objetos primarios</returns>
        public override Dictionary<string, object> Identificadores()
        {
            Dictionary<string, object> retorno = new Dictionary<string, object>();

            retorno.Add("letra_departamento", _Letra_departamento);
            retorno.Add("codigo", _Codigo);
            return retorno;
        }
    }
}
