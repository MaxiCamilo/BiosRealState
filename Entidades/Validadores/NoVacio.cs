using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Interfaces;

namespace Entidades.Validadores
{
    /// <summary>
    /// Este validador se usa para evitar los objetos nulos o vacios
    /// </summary>
    public class NoVacio : IValidador
    {

        //Constructor vacio
        public NoVacio()
        {
            
        }
        

        //Funcion de validador
        public bool Validar(Object Valor)
        {
            //Primero hay que saber si no es nulo
            if (Valor == null)
            {
                return false;
            }
            //Es un número entero? entonces averigua si es 0
            else if (Valor is int)
            {
                return ((int)Valor != 0);
            }
            //Es un número entero grande? entonces averigua si es 0
            else if (Valor is Int64 || Valor is long)
            {
                return ((Int64)Valor != 0);
            }
            //Es un número decimal? entonces averigua si es 0
            else if (Valor is decimal)
            {
                return ((decimal)Valor != 0);
            }
            //Es un número chico? entonces averigua si es 0
            else if (Valor is short)
            {
                return ((short)Valor != 0);
            }
            //Es un número flotante? entonces averigua si es 0
            else if (Valor is float)
            {
                return ((float)Valor != 0);
            }
            //Es un string o un formato convertible? Entonces averigua que no sea una cadena vacio
            else
            {
                try
                {
                    return (Valor.ToString() != "");
                }
                catch
                {
                    //oh oh! no se puede convertir a string
                    throw new FormatException("Conversión fallida de " + Valor.GetType().ToString() + " a string");
                }
            }
            
        }
    }
}
