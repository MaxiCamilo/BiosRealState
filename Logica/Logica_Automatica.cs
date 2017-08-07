using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Entidades.Interfaces;

namespace Logica
{

    /// <summary>
    /// Clase con funciones comunes para realizar tareas habituales con la capa logica
    /// </summary>
    public class Logica_Automatica
    {
        /// <summary>
        /// Se asignan los controladores (con sus respectivos validadores), para verificar que los valores tengan un formato correcto
        /// </summary>
        public List<Controlador_Valores> Validadores_Formato { get; set; } 

        /// <summary>
        /// Cuando se alla verificados que los formatos sean correcto, se hace una comprobacion para que los datos sean
        /// correcto en la base de datos (usar los validadores de la capa persistencia)
        /// </summary>
        public List<Controlador_Valores> Validadores_Persistencia { get; set; }

        

        
        
        public Logica_Automatica() {
            Validadores_Formato = new List<Controlador_Valores>();
            Validadores_Persistencia = new List<Controlador_Valores>();
        }

        /// <summary>
        /// Inicia la comprobacion con los verificadores ya asignados
        /// Es necesario que se alla asignado List<Controlador_Valores> Validadores, Funcion Funcion_Persistencia y Generador Creador.
        /// List<Controlador_Valores>: Incluya los verificadores base con sus valores y verificadores
        /// Funcion_Persistencia: La funcion que en caso de ser todo valido, llama la funcion de persistencia
        /// Creador: Se encarga de crear el objeto de "Entidad", solo lo llama en caso de estar bien los valores.
        /// </summary>
        /// <returns>Retorna los errores generados con sus respectivos mensajes</returns>
        public List<string> Iniciar_Comprobacion()
        {
            List<string> Mensajes = new List<string>();

            foreach(Controlador_Valores val in Validadores_Formato)
            {
                if (!val.Validar())
                {
                    Mensajes.Add(val.Mensaje);
                }
            }

            if(Mensajes.Count == 0)
            {
                foreach (Controlador_Valores val in Validadores_Persistencia)
                {
                    if (!val.Validar())
                    {
                        Mensajes.Add(val.Mensaje);
                    }
                    if(Mensajes.Count != 0)
                    {
                        return Mensajes;
                    }
                }
            }

            return Mensajes;
        }

        

    }
}
