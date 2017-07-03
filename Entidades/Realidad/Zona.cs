using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Realidad
{
    /// <summary>
    /// Entidad que representa un territorio compuesto por propiedades y servicios
    /// </summary>
    public class Zona
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
    }
}
