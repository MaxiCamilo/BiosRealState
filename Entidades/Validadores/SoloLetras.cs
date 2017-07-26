using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Entidades.Validadores
{
    /// <summary>
    /// Este validador verifica que solo alla letras en un string, no admite números, caracteres ni espacios
    /// </summary>
    public class SoloLetras : Validador
    {
        public SoloLetras()
        {
            Mensaje = "Solo puede tener letras";
        }
        //Funcion de validador
        public override bool Validar()
        {
            return Regex.IsMatch(Valor.ToString(), @"^[a-zA-Z]+$");
        }
    }
}

