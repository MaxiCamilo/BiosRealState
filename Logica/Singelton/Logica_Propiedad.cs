using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Realidad;
using Entidades.Validadores;
using Entidades.Interfaces;
using Persistencia;
using static Entidades.Realidad.Propiead;

namespace Logica.Singelton
{
    /// <summary>
    /// Clase para usar las propiedades de las entidades propiedades
    /// OJO! Algunas funciones no tienen impacto sobre la base de datos ya que necesita ser clasificadas segun su tipo, solo verifica y enlista
    /// </summary>
    public class Logica_Propiedad
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Logica_Propiedad> _instancia = new Lazy<Logica_Propiedad>(() => new Logica_Propiedad());

        // No se permite usar la clase en entornos locales
        private Logica_Propiedad() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Logica_Propiedad Instancia { get { return _instancia.Value; } }

        #endregion
#region Implementacion
        public List<String> Verificar_Propiedad(out Propiead Generado, Object padron, Object direccion, Object precio, Object accion, Object cantidad_banio,
            Object cantidad_habitaciones, Object metros_cuadrados, Object codigo_zona, Object letra_departamento, Object nombre_empleado, bool VerificarSql = false)
        {
            Propiead unaPropiedad = new Propiead();            
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {
                    //Validador de padron
                    new Controlador_Valores(){
                        Nombre="Padron",
                        Valor=padron,
                        Asignar = a => unaPropiedad.Padron = int.Parse(a.ToString()),
                        Validadores =
                        {
                            new NoVacio(),
                            new Numeros<int>()
                        }
                    },

                    //Validador de direccion
                    new Controlador_Valores(){
                        Nombre="Direccion",
                        Valor=direccion,
                        Asignar = a => unaPropiedad.Direccion = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=100}
                        }
                    },

                    //Validador de precio
                    new Controlador_Valores(){
                        Nombre="Precio",
                        Valor=precio,
                        Asignar = a => unaPropiedad.Precio = decimal.Parse(a.ToString()),
                        Validadores =
                        {
                            new NoVacio(),
                            new Numeros<Decimal>(),
                            new Minimo(){Limite=0}
                        }
                    },
                    //Validador de accion
                    new Controlador_Valores(){
                        Nombre="Tipo de transaccion",
                        Valor=accion,
                        Asignar = a => {
                            //Convertir string a Enum
                            Tipo_Accion accion_convertida;
                            Enum.TryParse(a.ToString(), out accion_convertida);
                            unaPropiedad.Accion = accion_convertida;
                        },
                        Validadores =
                        {
                            new NoVacio(),
                            new Igual(){Aceptados={ "alquiler", "venta", "permuta" } }
                        }
                    },
                     //Validador de cantidad de baños
                    new Controlador_Valores(){
                        Nombre="Cantidad de baños",
                        Valor=cantidad_banio,
                        Asignar = a => unaPropiedad.Cantidad_Banios = int.Parse(a.ToString()),                        
                        Validadores =
                        {
                            new NoVacio(),
                            new Numeros<int>(),
                            new Minimo(){Limite=0}
                        }
                    },
                    //Validador de cantidad de habitaciones
                    new Controlador_Valores(){
                        Nombre="Cantidad de habitaciones",
                        Valor=cantidad_habitaciones,
                        Asignar = a => unaPropiedad.Cantidad_Habitaciones = int.Parse(a.ToString()),
                        Validadores =
                        {
                            new NoVacio(),
                            new Numeros<int>(),
                            new Minimo(){Limite=0}
                        }
                    },
                    //Validador de metros cuadrados
                    new Controlador_Valores(){
                        Nombre="Metros Cuadrados",
                        Valor=metros_cuadrados,
                        Asignar = a => unaPropiedad.Metros_Cuadrados= decimal.Parse(a.ToString()),
                        Validadores =
                        {
                            new NoVacio(),
                            new Numeros<int>(),
                            new Minimo(){Limite=0.1M}
                        }
                    },
                    //Validador de codigo de zona
                    new Controlador_Valores(){
                        Nombre = "Codigo",
                        Valor=codigo_zona,
                        Asignar = a => unaPropiedad.Zona.Codigo = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=3, Solo_Validar_Largo=true}
                        }
                    },
                    //Validacion de letra de departamento de la zona
                    new Controlador_Valores(){
                        Nombre = "Letra de Departamento",
                        Valor=letra_departamento,
                        Asignar = a => unaPropiedad.Zona.Letra_Departamento = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new SoloLetras(),
                            new Minimo(){Limite=1, Solo_Validar_Largo=true }
                        }
                    },
                    //Validacion del Nombre del empleado
                    new Controlador_Valores(){
                        Nombre = "Empleado",
                        Valor=nombre_empleado,
                        Asignar = a => unaPropiedad.Empleado.Nombre = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new SoloLetras(),
                            new Hasta(){Limite=30}
                        }
                    },
                },
                Validadores_Persistencia =
                {
                    new Controlador_Valores()
                    {
                        Nombre="Base de datos",
                        Valor="VerificarPropiedad",
                        Validadores =
                        {
                            new ValidadorSQL()
                            {
                                ErrorA = "No puede haber 2 propiedades con los mismos padron",
                                ErrorB = "No existe esa zona",
                                ErrorC = "Empleado Inexistente",
                                Parametros =
                                {
                                    {"padron", padron },
                                    {"letra_departamento", letra_departamento },
                                    {"codigo_zona", codigo_zona },                                    
                                    {"nombre_empleado", nombre_empleado }

                                }
                            }
                        }
                    }
                }
               
            };

            if (!VerificarSql)
            {
                logica.Validadores_Persistencia = new List<Controlador_Valores>();
            }

            List<string> retorno = logica.Iniciar_Comprobacion();
            Generado = unaPropiedad;
            return retorno;
        }

        public List<string> Baja_Propiedad(Object Padron)
        {
            List<string> retorno = new List<string>();
            Propiead unaPropiedad = new Propiead();
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {
                    new Controlador_Valores
                    {
                        Nombre="Padron",
                        Asignar= a => unaPropiedad.Padron = int.Parse(a.ToString()),
                        Valor = Padron,
                        Validadores ={
                            new NoVacio(),
                            new Numeros<int>()
                        }

                    }
                }

            };
            retorno = logica.Iniciar_Comprobacion();
            if (retorno.Count == 0)
            {
                int persistencia = Fabrica_Persistencia.getPersistencia_Propiedad.Baja(unaPropiedad);
                if (persistencia == 0)
                {
                    return retorno;
                }
                else if (persistencia == -1)
                {
                    retorno.Add("No existe esa propiedad o ya fue eliminada");
                }
                else
                {
                    retorno.Add("Fallo la eliminacion de la propiedad");
                }
            }


            return retorno;
        }

        #endregion

        #region Listados

        public List<Propiead> Listado_Activos() { return Fabrica_Persistencia.getPersistencia_Propiedad.Listado_Activos(); }

        public List<Propiead> Listado_Todo() { return Fabrica_Persistencia.getPersistencia_Propiedad.Listado_Todo(); }

        #endregion

        #region

        public Propiead Generar(int Padron) { return Fabrica_Persistencia.getPersistencia_Propiedad.Generar(Padron); }

        void Detallar_Zona(ref Propiead Propiedad_Elegida) { Fabrica_Persistencia.getPersistencia_Propiedad.Detallar_Zona(ref Propiedad_Elegida); }

        public void Detalle_Empleado(ref Propiead Propiedad_Elegida) { Fabrica_Persistencia.getPersistencia_Propiedad.Detalle_Empleado(ref Propiedad_Elegida); }

        public List<Consulta> Listar_Consultas(Propiead Propiedad_Elegida) { return Fabrica_Persistencia.getPersistencia_Propiedad.Listar_Consultas(Propiedad_Elegida); }

        #endregion

    }
}
