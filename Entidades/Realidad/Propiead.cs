using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Realidad
{
    /// <summary>
    /// Entidad que representan las propiedades a la venta 
    /// </summary>
    public class Propiead
    {
        public enum Tipo_Accion {SinDefinir, alquiler , venta , permuta }

        //atributos
        private int _Padron = -1;
        private string _Direccion = "<Sin Definir>";
        private Tipo_Accion _Accion = Tipo_Accion.SinDefinir;
        private int _Cantidad_Banios = -1;
        private int _Cantidad_Habitaciones = -1;
        private decimal _Metros_Cuadrados = -1;
        private decimal _Precio = -1;

        //Atributos Externos
        private Zona _Zona = new Zona();

        //Accesores
        public int Padron { get { return _Padron; } set { _Padron = value; } }
        public string Direccion { get { return _Direccion; } set { _Direccion = value; } }
        public Tipo_Accion Accion { get { return _Accion; } set { _Accion = value; } }
        public int Cantidad_banios { get { return _Cantidad_Banios; } set { _Cantidad_Banios = value; } }
        public int Cantidad_Habitaciones { get { return _Cantidad_Habitaciones; } set { _Cantidad_Habitaciones = value; } }
        public decimal Metros_Cuadrados { get { return _Metros_Cuadrados; } set { _Metros_Cuadrados = value; } }
        public decimal Precio { get { return _Precio; } set { _Precio = value; } }
        public Zona Zona { get { return _Zona; } set { _Zona = value; } }

        /// <summary>
        /// Constructor Basico, Dejara como '<Sin Definir>' las variables
        /// </summary>
        public Propiead() { }
        /// <summary>
        /// Constructor Completo 
        /// </summary>
        public Propiead(int Padron, string Direccion, Tipo_Accion Accion, int Cantidad_banios, decimal Metros_Cuadrados, decimal Precio, int Cantidad_Habitaciones, Zona Zona) {
            _Padron = Padron;
            _Direccion = Direccion;
            _Accion = Accion;
            _Cantidad_Banios = Cantidad_banios;
            _Cantidad_Habitaciones = Cantidad_Habitaciones;
            _Metros_Cuadrados = Metros_Cuadrados;
            _Precio = Precio;
            _Zona = Zona;
        }
    }
}
