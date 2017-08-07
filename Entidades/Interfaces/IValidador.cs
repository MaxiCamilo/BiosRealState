using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Interfaces
{
    /// <summary>
    /// Interfaz para comunicar los validadores
    /// </summary>
    public abstract class  Validador
    {
        abstract public bool Validar();
        public Object Valor { get; set; }
        public string Mensaje { get; set; }

    }
    public interface IConversor
    {
        bool Convertir(ref Object Valor);
    }
}
