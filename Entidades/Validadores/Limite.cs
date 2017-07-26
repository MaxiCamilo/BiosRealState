using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Validadores
{
    /// <summary>
    /// Este validador compara el tamaño de un string, o la cantidad de un número, cumpliendo un maximo y minimo.
    /// </summary>
    public class Limite : Validador
    {
        public Decimal Maximo { get; set; } = 0;
        public Decimal Minimo { get; set; } = 0;
        public bool Solo_Validar_Largo { get; set; } = false;
        public Limite()
        {
            Mensaje = "no se encuentra entre los valores permitidos (debe ser de " + Minimo + " a " + Maximo + ")";
        }


        //Funcion de validador
        public override bool Validar()
        {
            if (Valor == null)
            {
                return false;
            }
            if (Solo_Validar_Largo)
            {
                Mensaje = "debe tener entre " + Minimo + " a " + Maximo + " caracteres";
                return Minimo <= Valor.ToString().Length && Maximo >= Valor.ToString().Length;
            }
            //Verificamos que sea un numero
            

            //Es int?
            if(Valor is int)
            {
                return Minimo <= (int)Valor && Maximo >= (int)Valor;
            }

            //Es int64?
            if (Valor is Int64)
            {
                return Minimo <= (Int64)Valor && Maximo >= (Int64)Valor;
            }

            //Es decimal?
            if (Valor is Decimal)
            {
                return Minimo <= (Decimal)Valor && Maximo >= (Decimal)Valor;
            }

            //Es short?
            if (Valor is short)
            {
                return Minimo <= (short)Valor && Maximo >= (short)Valor;
            }

            decimal esNumero = 0;
            if (Decimal.TryParse(Valor.ToString(), out esNumero))
            {
                return Minimo <= esNumero && Maximo >= esNumero;
            }
            else
            {
                Mensaje = "debe tener entre " + Minimo + " a " + Maximo + " caracteres";
                //Sino es un string, se verifica su largo
                return Minimo <= Valor.ToString().Length && Maximo >= Valor.ToString().Length;
            }
            
        }
    }
}

