using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

//Todas las excepciones de la capa persistencia se encuentran aca
namespace Entidades.Excepiones
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
    

}
