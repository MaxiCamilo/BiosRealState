using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Realidad
{
    /// <summary>
    /// Tipo de propiedad, que tiene ascensor y piso
    /// </summary>
    public class Apartamento : Propiead
    {
        //atributos
        private bool _Ascensor = false;
        private int _Piso = -1;

        //Accesores
        public bool Ascensor { get { return _Ascensor; } set { _Ascensor = value; } }
        public int Piso { get { return _Piso; } set { _Piso = value; } }

        /// <summary>
        /// Constructor Basico, Dejara como '<Sin Definir>' las variables
        /// </summary>
        public Apartamento() : base() { }

        /// <summary>
        /// Constructor Completo 
        /// </summary>
        public Apartamento(int Padron, string Direccion, Tipo_Accion Accion, int Cantidad_banios, decimal Metros_Cuadrados, decimal Precio, bool Ascensor, int Piso, int Cantidad_Habitaciones, Zona Zona)
            : base( Padron, Direccion, Accion, Cantidad_banios, Metros_Cuadrados, Precio, Cantidad_Habitaciones, Zona)
        {
            Piso = _Piso;
            Ascensor = _Ascensor;
        }
    }
}
