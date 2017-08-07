using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Interfaces;

namespace Entidades.Validadores
{
    /// <summary>
    /// Este validador sirve de filtro para aceptar determinados valor
    /// </summary>
    public class Igual : Validador
    {
        /// <summary>
        /// En esta lista se definen los datos que debe tener un dato valido
        /// </summary>
        public List<Object> Aceptados { get; set; } 
        public Igual()
        {
            Mensaje = "El dato no es un valor aceptado";
            Aceptados = new List<Object>();
        }
        //Funcion de validador
        public override bool Validar()
        {
            foreach(Object aceptar in Aceptados)
            {
                //Averigua si tiene el mismo tipo de clase
                if(aceptar.GetType() == Valor.GetType())
                {
                    if(Valor == aceptar)
                    {
                        return true;
                    }
                }
                //sino se usa el ToString()
                else
                {
                    if (Valor.ToString() == aceptar.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
