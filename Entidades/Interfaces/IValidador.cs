using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Interfaces
{
    /// <summary>
    /// Interfaz para comunicar los validadores
    /// </summary>
    public interface IValidador
    {
        bool Validar(Object Valor);

    }
    public interface IConversor
    {
        bool Convertir(ref Object Valor);
    }
}
