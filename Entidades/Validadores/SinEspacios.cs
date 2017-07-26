

using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Entidades.Validadores
{
    /// <summary>
    /// Este validador verifica que un string no contenga espacios
    /// </summary>
    public class SinEspacios : Validador
    {
        public SinEspacios()
        {
            Mensaje = "No puede tener espacios";
        }
        //Funcion de validador
        public override bool Validar()
        {
            return !(Regex.IsMatch(Valor.ToString(), @"\s"));
        }
    }
}