using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Validadores
{
    /// <summary>
    /// Este validador compara el largo de un texto o si el numero no supera un limite
    /// </summary>
    public class Hasta : Validador
    {

        public Decimal Limite { get; set; } = 0;
        public bool Solo_Validar_Largo { get; set; } = false;
        public Hasta()
        {
            
        }


        //Funcion de validador
        public override bool Validar()
        {
            Mensaje = "no puede ser mayor de " + Limite;
            if (Valor == null)
            {
                return false;
            }

            if (Solo_Validar_Largo)
            {
                Mensaje = "debe tener hasta " + Limite + " caracteres";
                return Limite >= Valor.ToString().Length ;
            }

            //Es int?
            if (Valor is int)
            {
                return Limite >= (int)Valor;
            }

            //Es int64?
            if (Valor is Int64)
            {
                return Limite >= (Int64)Valor;
            }

            //Es decimal?
            if (Valor is Decimal)
            {
                return Limite >= (Decimal)Valor;
            }

            //Es short?
            if (Valor is short)
            {
                return Limite >= (short)Valor;
            }

            //Verificamos que sea un numero
            decimal esNumero = 0;

            if (!Solo_Validar_Largo && Decimal.TryParse(Valor.ToString(), out esNumero))
            {
                return Limite >= esNumero;
            }
            //Sino es un string, se verifica su largo
            Mensaje = "debe tener como minimo " + Limite + " caracteres";
            return Valor.ToString().Length <= Limite;
        }
    }
}
