using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

//Todas las excepciones de la capa persistencia se encuentran aca
namespace Entidades.Excepiones.Persistencia
{
    /// <summary>
    /// Esta excepcion se usa para avisar que un SqlConnection.open() fallo
    /// </summary>
    public class ExErrorConexion : Exception
    {
        /// <summary>
        /// El SqlConnection que reporto el fallo
        /// </summary>
        public SqlConnection Conexion { get; set; } = null;

        public ExErrorConexion(): base()
        {
             
        }
        /// <summary>
        /// Generar la excepcion de conexion
        /// </summary>
        /// <param name="p_mensaje">¿Como quiere comunicar el error?</param>
        /// <param name="p_conexion">[Opcional] Cual es el conector implicado</param>
        public ExErrorConexion(string p_mensaje, SqlConnection p_conexion = null) : base(p_mensaje)
        {
            Conexion = p_conexion;
        }
    }
    /// <summary>
    /// Esta excepcion se usa para avisar que una transaccion fue rechazada (se ejecuto un Commit) por devolver un valor distinto a 0
    /// </summary>
    public class ExFalloTransaccion : Exception
    {
        /// <summary>
        /// El número devuelto por el procedimiento
        /// </summary>
        public int Retorno { get; set; } = 0;
        /// <summary>
        /// Nombre del procedimiento que falló
        /// </summary>
        public string Nombre_Procedimiento { get; set; } = "<Sin Nombre>";
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
