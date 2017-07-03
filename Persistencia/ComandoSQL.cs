using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Entidades.Excepiones.Persistencia;
using Entidades.Interfaces;

namespace Persistencia
{
    /// <summary>
    /// Clase Base de la persistencia, con ella se puede comunicar a los distintos procedimientos de la base de datos
    /// </summary>
    public class ComandoSQL
    {
        private SqlConnection _Conector = new SqlConnection(Configuracion.Cadena_Conexion);
        private SqlCommand _Comando = new SqlCommand();
        private SqlParameter _Parametro = new SqlParameter("retorno", System.Data.SqlDbType.Int);
        private SqlTransaction _Transaccion;
        private SqlDataReader _Lector = null;
        private string _Accion = "";

        public string Accion { get { return _Accion; } set { _Accion = value; } }

        /// <summary>
        /// Crea el comando
        /// </summary>
        /// <param name="comando">Debe poner el nombre del procedimiento</param>
        public ComandoSQL(string comando)
        {
            _Accion = comando;
            _Conector.ConnectionString = Configuracion.Cadena_Conexion;
            
            _Comando = _Conector.CreateCommand();
            _Comando.Connection = _Conector;





        }
        /// <summary>
        /// Agrega los parametros necesarios para ejecutar el procedimiento
        /// </summary>
        /// <param name="_parametro">Nombre del parametro</param>
        /// <param name="_valor">Valor asignado al mismo</param>
        public void AgregarParametro(string _parametro, Object _valor)
        {
            _Comando.Parameters.AddWithValue(_parametro, _valor);
        }
        /// <summary>
        /// Pone en marcha la ejecucion por medio de una transaccion. SI O SI tiene que retornar 0, sino deshace las modificaciones
        /// </summary>
        /// <returns>Retorna el número devuelto del procedimiento</returns>
        public int Ejecutar_Transaccion()
        {
            try
            {
                _Conector.Open();
                _Comando.CommandType = System.Data.CommandType.StoredProcedure;
                _Transaccion = _Conector.BeginTransaction(_Accion);
                _Comando.Transaction = _Transaccion;
                _Comando.CommandText = _Accion;
            }
            catch (Exception ex)
            {
                //¡O Rayos!
                
                throw new ExErrorConexion(ex.Message, _Conector);
            }
            //Revisa si el parametro de retorno no se alla repetido
            if (_Comando.Parameters.Count != 0 && _Comando.Parameters[0] != _Parametro)
            {
                _Comando.Parameters.Insert(0, _Parametro);
            }
            
            //Asigna al parametro
            _Parametro.Direction = System.Data.ParameterDirection.ReturnValue;
            try
            {
                //Ejecuta
                
                _Comando.ExecuteNonQuery();
                
                //En caso de ser 0, confirma la transaccion, sino la deshace
         
                if ((int)_Parametro.Value == 0)
                {
                    _Transaccion.Commit();
                }
                else
                {
                    _Transaccion.Rollback();
                }
                
            }
            catch(Exception ex)
            {
                //¡O Rayos!
                try
                {
                    _Transaccion.Rollback();
                }
                catch (Exception exx)
                {
                    throw new ExErrorConexion(ex.Message + ". No se pudo deshacer la transaccion: " + exx.Message, _Conector);
                }
                throw new ExErrorConexion(ex.Message,_Conector);
            }
            finally
            {
                _Conector.Close();
            }
            //Retorna el valor del procedimiento
            return (int)_Parametro.Value;
        }

        /// <summary>
        /// Pone en marcha la ejecucion
        /// </summary>
        /// <returns>Retorna el número devuelto del procedimiento</returns>
        public int Ejecutar()
        {
            _Comando.CommandType = System.Data.CommandType.StoredProcedure;
            _Comando.CommandText = _Accion;
            //Revisa si el parametro de retorno no se alla repetido
            if (_Comando.Parameters.Count != 0 && _Comando.Parameters[0] != _Parametro)
            {
                _Comando.Parameters.Insert(0, _Parametro);
            }
            //Revisa si la conexion esta cerrada
            CerrarConexion();
            //Asigna al parametro
            _Parametro.Direction = System.Data.ParameterDirection.ReturnValue;
            try
            {
                //Ejecuta
                _Conector.Open();
                _Comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //¡O Rayos!
                throw new ExErrorConexion(ex.Message, _Conector);
            }
            finally
            {
                CerrarConexion();
            }
            //Retorna el valor del procedimiento
            return (int)_Parametro.Value;
        }

        /// <summary>
        /// Funcion que utilicé para hacer los listado en el proyecto del año pasado (por eso el VIEJO), ya que no me dejaban usar los DataTable (desconecto-fobia)
        /// </summary>
        /// <returns>Obtiene un complicada diccionario con un menjuje de objetos</returns>
        public Dictionary<int, List<Object>> VIEJO_ObtenerRetorno()
        {
            if (_Comando.Parameters.Count != 0 && _Comando.Parameters[0] == _Parametro)
            {
                _Comando.Parameters.RemoveAt(0);
            }
            if (_Conector.State == System.Data.ConnectionState.Open)
            {
                _Conector.Close();
            }
            _Conector.Open();
            SqlDataReader lector = _Comando.ExecuteReader();
            Dictionary<int, List<Object>> Tabla = new Dictionary<int, List<Object>>();
            int Contador = 0;
            while (lector.Read())
            {
                Tabla.Add(Contador, new List<Object>());
                for (int i = 0; i < lector.FieldCount; i++)
                {
                    Tabla[Contador].Add(lector[i]);
                }

                Contador++;
            }
            _Conector.Close();
            return Tabla;
        }
        /// <summary>
        /// Cierra la conexion
        /// </summary>
        public void CerrarConexion()
        {

            if (_Conector.State == System.Data.ConnectionState.Open)
            {
                _Conector.Close();
            }
        }
        /// <summary>
        /// Funcion sensilla (je je je) para generar listado, especificando 
        /// </summary>
        /// <typeparam name="QueGenera">Especifique que clase de objeto quiere almacenar en la lista.</typeparam>
        /// <param name="Procesador">Algo de Js en C#. Funcion encargada en transformar los datos "crudos" de la base de datos, en objetos que puede procesar el sistemas. ES IMPORTANTE, que reciba como parametro un SqlDataReader (recibirá los datos) y retorne el objeto especificado. Puede utilizar funciones anonimal.</param>
        /// <param name="esProcedimiento">Pregunta si el listado lo genera un procedimiento Sql, en caso contario (como por ejemplo una vista o funcion sql) debe poner el nombre, o el nombre y entre paréntesis los parametros en caso de que sea una funcion sql. ¡CUIDADO!, no esta blindado contra inyecciones SQL.</param>
        /// <returns></returns>
        public List<QueGenera> Mostrar_Seleccion <QueGenera>(Func< SqlDataReader, QueGenera> Procesador, bool esProcedimiento = false)
        {
            List<QueGenera> retorno = new List<QueGenera>();
            CerrarConexion();
            //primero se verifica si es procediminiento
            if (esProcedimiento)
            {
                _Comando.CommandType = CommandType.StoredProcedure;
                _Comando.CommandText = _Accion;
                //Revisa si el parametro de retorno no se alla repetido
                if (_Comando.Parameters.Count != 0 && _Comando.Parameters[0] != _Parametro)
                {
                    _Comando.Parameters.Insert(0, _Parametro);
                }
            }
            else
            {
                _Comando.CommandType = CommandType.Text;
                _Comando.CommandText = "select * from " + _Accion;
            }

            try
            {
                _Conector.Open();
                _Lector = _Comando.ExecuteReader();
                if (!_Lector.HasRows)
                {
                    CerrarConexion();
                    return retorno;
                }
                while (_Lector.Read())
                {
                    retorno.Add(Procesador(_Lector));
                }
            }
            catch(Exception ex)
            {
                //¡O Rayos!
                CerrarConexion();
                throw new ExErrorConexion(ex.Message, _Conector);
            }
            CerrarConexion();
            return retorno;


        }


    }
}
