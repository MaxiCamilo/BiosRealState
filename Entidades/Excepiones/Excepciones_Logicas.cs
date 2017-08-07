using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Excepiones
{
    /// <summary>
    /// Esta excepcion se usa para avisar que una transaccion fue rechazada (se ejecuto un Commit) por devolver un valor distinto a 0
    /// </summary>
    public class ExFalloTransaccion : Exception
    {
        /// <summary>
        /// El número devuelto por el procedimiento
        /// </summary>
        public int Retorno { get; set; }
        /// <summary>
        /// Nombre del procedimiento que falló
        /// </summary>
        public string Nombre_Procedimiento { get; set; }

        /// <summary>
        /// Generar la excepcion de falló de 
        /// </summary>
        public ExFalloTransaccion() { }

        /// <summary>
        /// Generar la excepcion de falló de 
        /// </summary>
        public ExFalloTransaccion(int p_retorno) { Retorno = p_retorno; }

        /// <summary>
        /// Generar la excepcion de falló de 
        /// </summary>
        /// <param name="p_mensaje">¿Como quiere comunicar el error?</param>
        /// <param name="p_retorno">Que retorno la transaccion</param>
        /// <param name="p_nombre">Nombre de la transaccion</param>
        public ExFalloTransaccion(string p_mensaje, int p_retorno, string p_nombre = "") : base(p_mensaje)
        {
            Retorno = p_retorno;
            Nombre_Procedimiento = p_nombre;
        }
    }
}
