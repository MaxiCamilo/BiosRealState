using Entidades.Realidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Interfaces;
using System.Data.SqlClient;
using Persistencia.Interfaces;
using Entidades.Validadores;
using Persistencia;
using Logica.Interfaces;

namespace Logica.Singleton
{
    public class Logica_Empleado : ILogica_Empleado
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Logica_Empleado> _instancia = new Lazy<Logica_Empleado>(() => new Logica_Empleado());

        // No se permite usar la clase en entornos locales
        private Logica_Empleado() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Logica_Empleado Instancia { get { return _instancia.Value; } }

        #endregion

        #region Implementacion

        public List<string> Alta_empleado(object nombre, object contrasenia, object confirmacion)
        {
            if (contrasenia.ToString() != confirmacion.ToString())
            {
                return new List<string>() { "Las 2 contraseñeras deben coincidir" };
            }

            Empleado unEmpleado = new Empleado();
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {
                    new Controlador_Valores(){
                        Nombre = "Nombre",
                        Valor = nombre.ToString(),
                        Asignar = a => unEmpleado.Nombre = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=5,Solo_Validar_Largo=true},
                            new Hasta(){Limite=30,Solo_Validar_Largo=true},
                            new SinEspacios()
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Contraseña",
                        Valor = contrasenia.ToString(),
                        Asignar = a => unEmpleado.Contrasenia = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=10,Solo_Validar_Largo=true},
                            new Hasta(){Limite=10,Solo_Validar_Largo=true}
                        }
                    },
                }

            };

            List<string> retorno = logica.Iniciar_Comprobacion();
            if (retorno.Count == 0)
            {
                int valor = Fabrica_Persistencia.getPersistencia_Empleado.Alta(unEmpleado);
                if (valor == 0)
                {
                    return retorno;
                }
                else if (valor == -1)
                {
                    retorno.Add("Ya existe un empleado con el mismo nombre");
                }
                else if (valor == -2)
                {
                    retorno.Add("Contraseña invalida, solo puede tener 10 digitos");
                }
                else
                {
                    retorno.Add("No se pudo agregar al empleado");
                }
                return retorno;
            }
            return retorno;
        }

        public List<string> Baja_Empleado(object nombre, object contrasenia, object confirmacion)
        {
            if (contrasenia.ToString() != confirmacion.ToString())
            {
                return new List<string>() { "Las 2 contraseñeras deben coincidir" };
            }
            Empleado unEmpleado = new Empleado();
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {
                    new Controlador_Valores(){
                        Nombre = "Nombre",
                        Valor = nombre.ToString(),
                        Asignar = a => unEmpleado.Nombre = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=5,Solo_Validar_Largo=true},
                            new Hasta(){Limite=30,Solo_Validar_Largo=true},
                            new SinEspacios()
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Contraseña",
                        Valor = contrasenia.ToString(),
                        Asignar = a => unEmpleado.Contrasenia = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=10, Solo_Validar_Largo=true},
                            new Hasta(){Limite=10, Solo_Validar_Largo=true}
                        }
                    },
                }

            };

            List<string> retorno = logica.Iniciar_Comprobacion();
            if (retorno.Count == 0)
            {
                int valor = Fabrica_Persistencia.getPersistencia_Empleado.Baja(unEmpleado);
                if (valor == 0)
                {
                    return retorno;
                }
                else if (valor == -1)
                {
                    retorno.Add("No Existe ese usuario o ya fue eliminado");
                }
                else if (valor == -2)
                {
                    retorno.Add("Contraseña Invalida");
                }
                else
                {
                    retorno.Add("Fallo la eliminacion del empleado");
                }
                return retorno;
            }
            return retorno;
        }
        public List<string> Modificar_empleado(Empleado unEmpleado, object contrasenia_nueva, object confirmacion)
        {
            if (contrasenia_nueva.ToString() != confirmacion.ToString())
            {
                return new List<string>() { "Las 2 contraseñas nuevas deben coincidir" };
            }
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {

                    new Controlador_Valores(){
                        Nombre = "Contraseña nueva",
                        Valor = contrasenia_nueva.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=10,Solo_Validar_Largo=true},
                            new Hasta(){Limite=10,Solo_Validar_Largo=true}
                        }
                    },
                }

            };
            List<string> retorno = logica.Iniciar_Comprobacion();
            if (retorno.Count == 0)
            {
                int valor = Fabrica_Persistencia.getPersistencia_Empleado.Modificar(unEmpleado, contrasenia_nueva.ToString());
                if (valor == 0)
                {
                    return retorno;
                }
                else if (valor == -1)
                {
                    retorno.Add("Contraseña invalida, solo puede tener 10 digitos");
                }
                else if (valor == -2)
                {
                    retorno.Add("No existe el usuario");
                }
                else if (valor == -3)
                {
                    retorno.Add("Contraseña actual invalida");
                }
                else
                {
                    retorno.Add("Fallo la asignacion de nueva contraseña del empleado");
                }
                return retorno;
            }
            return retorno;
            #endregion
        }
        #region Listados
        public List<Empleado> Listado(int padron)
        {
            return Fabrica_Persistencia.getPersistencia_Empleado.Listado(padron);
        }

        public List<Propiedad> Consultas_Propiedades_Modificadas(Empleado Empleado_Seleccionado)
        {
            return Fabrica_Persistencia.getPersistencia_Empleado.Consultas_Propiedades_Modificadas(Empleado_Seleccionado);
        }

        #endregion

        #region Inicio Sesion

        public List<string> Iniciar_Sesion(object nombre, object contrasenia)
        {
            Empleado unEmpleado = new Empleado();
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {
                    new Controlador_Valores(){
                        Nombre = "Nombre",
                        Valor = nombre.ToString(),
                        Asignar = a => unEmpleado.Nombre = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=5,Solo_Validar_Largo=true},
                            new Hasta(){Limite=30,Solo_Validar_Largo=true},
                            new SinEspacios()
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Contraseña",
                        Valor = contrasenia.ToString(),
                        Asignar = a => unEmpleado.Contrasenia = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=10,Solo_Validar_Largo=true},
                            new Hasta(){Limite=10,Solo_Validar_Largo=true}
                        }
                    },
                }

            };

            List<string> retorno = logica.Iniciar_Comprobacion();
            if(retorno.Count == 0)
            {
                int dio = Fabrica_Persistencia.getPersistencia_Empleado.Inicio_Sesion(unEmpleado);
                if(dio == 0)
                {
                    return retorno;
                }
                else if(dio == 1)
                {
                    retorno.Add("No existe un usuario con ese nombre");
                }
                else if (dio == 2)
                {
                    retorno.Add("Contraseña incorrecta");
                }
                else
                {
                    retorno.Add("Fallo el inicio de sesion");
                }
            }
            return retorno;
        }

#endregion
    }
}
