using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Interfaces
{

    ////PRUEBA FALLIDA

    /// <summary>
    /// Clase singleton para implementar 
    /// </summary>
    public class Singleton <Clase>
    {
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        private static readonly Lazy<Clase> _instancia = new Lazy<Clase>(() => default(Clase));

        // Constructor privado para evitar la instanciación directa
        private Singleton()
        {
        }

        // Propiedad para acceder a la instancia
        public static Clase Instancia
        {
            get
            {
                return _instancia.Value;
            }
        }

    }
}
