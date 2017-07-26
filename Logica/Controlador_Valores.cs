using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class Controlador_Valores
    {
        public delegate void Funcion_Asignada(Object _Valor);

        public string Nombre = " < Sin Nombre >";
        public string Mensaje { get; set; } = "";
        public Object Valor { get; set; }
        public Funcion_Asignada Asignar { get; set; }
        public List<Validador> Validadores { get; set; } = new List<Validador>();

        public Controlador_Valores() {
        }

       

        public bool Validar()
        {
            foreach (Validador val in Validadores)
            {
                val.Valor = Valor;
                if (!val.Validar())
                {
                    Mensaje = Nombre + ": " + val.Mensaje;
                    return false;
                }
            }
            //Propiedad_Destino = (Object)Valor;
            Asignar?.Invoke(Valor);
            return true;
        }
    }
}
