using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Interfaces;
using System.Reflection;

namespace Entidades.Validadores
{
    public delegate bool TryParser<T>(string input, out T result);
    /// <summary>
    /// Este validador sirve para verificar y convertir objetos numericos
    /// </summary>
    public class Numeros<Tipo> : IValidador, IConversor
    {

        //Constructor vacio
        public Numeros()
        {
        }
        /// <summary>
        /// Verificador
        /// </summary>
        public bool Validar(Object Valor)
        {
            Object _No_Referenciar = Valor;
            return Convertir(ref _No_Referenciar);
        }

        /// <summary>
        /// Metodo que verifica si es un numero y lo cambia a la clase deseada
        /// </summary>
        public bool Convertir(ref Object Valor)
        {
            //Primero hay que saber si no es nulo
            if (Valor == null)
            {
                return false;
            }
            //Sera del mismo valor?
            if(Valor is Tipo)
            {
                return true;
            }
            //Entonces no es un numero
            else
            {
                //Iniciamos una conversion
                Tipo Retorno = default(Tipo);
                bool Resultado = TryParse(Valor.ToString(), out Retorno);
                Valor = Retorno;
                return Resultado;
            }
        }

        /// <summary>
        /// Funcion que convierte cualquier tipo de objeto a numero mientras tenga el objeto a convertir un .toString() y el tipo de objeto a convertir un .TryParse
        /// Copiado de: https://stackoverflow.com/questions/11481853/how-to-call-tryparse-dynamically
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Original">Objeto a convertir</param>
        /// <param name="Resultado">El resultado final lo devuelve en out</param>
        /// <param name="tryParser">Usarlo si ya le asigno una funcion personalizada</param>
        /// <returns></returns>
        public static bool TryParse<T>
             (string Original, out T Resultado, TryParser<T> tryParser = null)
        {
            if (Original == null)
                throw new ArgumentNullException("No puede ser nulo");
            if (tryParser == null)
            {
                var method = typeof(T).GetMethod
                         ("TryParse", new[] { typeof(string), typeof(T).MakeByRefType() });

                if (method == null)
                    throw new InvalidOperationException("Type does not have a built in try-parser.");

                tryParser = (TryParser<T>)Delegate.CreateDelegate
                    (typeof(TryParser<T>), method);
            }

            return tryParser(Original, out Resultado);
        }

        
    }
    
}
