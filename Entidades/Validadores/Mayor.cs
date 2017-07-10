using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Validadores
{
    /// <summary>
    /// Este validador compara el largo de un texto o si el numero supera un limite
    /// </summary>
    public class Mayor : IValidador
    {
        

        public Decimal Limite { get; set; } = 0;
        public bool Solo_Validar_Largo { get; set; } = false;
        public Mayor()
        {

        }


        //Funcion de validador
        public bool Validar(Object Valor)
        {
            if (Valor == null)
            {
                return false;
            }
            //Verificamos que sea un numero
            decimal esNumero = 0;

            if (!Solo_Validar_Largo && Decimal.TryParse(Valor.ToString(), out esNumero))
            {
                return Limite <= esNumero;
            }
            //Sino es un string, se verifica su largo
            return Valor.ToString().Length >= Limite;
        }
    }
}
