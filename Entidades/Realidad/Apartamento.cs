﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Entidades.Utilidades;
using Entidades.Interfaces;
using System.Reflection;

namespace Entidades.Realidad
{
    /// <summary>
    /// Tipo de propiedad, que tiene ascensor y piso
    /// </summary>
    public class Apartamento : Propiedad
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
        public Apartamento(int Padron, string Direccion, Tipo_Accion Accion, int Cantidad_banios, decimal Metros_Cuadrados, decimal Precio, bool Ascensor, int Piso, int Cantidad_Habitaciones, Zona Zona, Empleado Empleado = null)
            : base( Padron, Direccion, Accion, Cantidad_banios, Metros_Cuadrados, Precio, Cantidad_Habitaciones, Zona, Empleado)
        {
            Piso = _Piso;
            Ascensor = _Ascensor;
        }

        /// <summary>
        /// Constructor de tipo casteo, por medio de reflexion, pasa todos los valores de un objeto del tipo "padre"
        /// Fuente: https://stackoverflow.com/questions/9885644/cast-the-parent-object-to-child-object-in-c-sharp
        /// </summary>
        /// <param name="padre">Objeto por el cual se quiere castear y pasar sus valores</param>
        public Apartamento(Propiedad padre)
        {
            foreach (FieldInfo prop in padre.GetType().GetFields())
                GetType().GetField(prop.Name).SetValue(this, prop.GetValue(padre));

            foreach (PropertyInfo prop in padre.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(padre, null), null);
        }

        /// <summary>
        /// Se puede crear el objeto a partir de una letura en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        public Apartamento(SqlDataReader lector) : base(lector)
        {
            _Ascensor = lector["ascensor"].ToString() == "1";
            _Piso = int.Parse(lector["piso"].ToString());

        }
        /// <summary>
        /// Metodo para crear el objeto a partir de una consulta en la base de datos.
        /// </summary>
        /// <param name="lector">Donde se encuentran los objetos, recorar de usar el read() antes.</param>
        /// <returns>Retorna el objeto ya generado.</returns>
        public new Apartamento Generador_Objeto(SqlDataReader lector)
        {
            Apartamento retorno = new Apartamento()
            {
                Ascensor = lector["ascensor"].ToString() == "1",
                Piso = int.Parse(lector["piso"].ToString())
            };
            Propiedad retorno_base = base.Generador_Objeto(lector);

            retorno.Padron = retorno_base.Padron;
            retorno.Direccion = retorno_base.Direccion;
            retorno.Accion = retorno_base.Accion;
            retorno.Cantidad_Banios = retorno_base.Cantidad_Banios;
            retorno.Cantidad_Habitaciones = retorno_base.Cantidad_Habitaciones;
            retorno.Metros_Cuadrados = retorno_base.Metros_Cuadrados;
            retorno.Precio = retorno_base.Precio;
            retorno.Zona = retorno_base.Zona;
            retorno.Precio = retorno_base.Precio;
            retorno.Empleado = retorno_base.Empleado;
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
            Dictionary<string, object> retorno = base.Parametros();

            retorno.Add("piso", _Piso);
            retorno.Add("ascensor", _Ascensor);
            
            return retorno;
        }
    }
}
