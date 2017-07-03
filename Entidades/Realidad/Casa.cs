using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Realidad
{
    /// <summary>
    /// Tipo de propiedad, que tiene jardin y terreno
    /// </summary>
    public class Casa : Propiead
    {
        //atributos
        private bool _Jardin = false;
        private decimal _Tamanio_Terreno = -1;

        //Accesores
        public bool Jardin { get { return _Jardin; } set { _Jardin = value; } }
        public decimal Tamanio_Terreno { get { return _Tamanio_Terreno; } set { _Tamanio_Terreno = value; } }

        /// <summary>
        /// Constructor Basico, Dejara como '<Sin Definir>' las variables
        /// </summary>
        public Casa() : base() { }

        /// <summary>
        /// Constructor Completo 
        /// </summary>
        public Casa(int Padron, string Direccion, Tipo_Accion Accion, int Cantidad_banios, decimal Metros_Cuadrados, decimal Precio, bool Jardin, decimal Tamanio_Terreno, int Cantidad_Habitaciones, Zona Zona)
            : base( Padron, Direccion, Accion, Cantidad_banios, Metros_Cuadrados, Precio, Cantidad_Habitaciones, Zona)
        {
            _Tamanio_Terreno = Tamanio_Terreno;
            _Jardin = Jardin;
        }
    }
}
