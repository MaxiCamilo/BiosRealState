using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    }
}
