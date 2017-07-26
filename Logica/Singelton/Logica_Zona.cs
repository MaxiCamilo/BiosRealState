using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Realidad;
using Entidades.Validadores;
using Entidades.Interfaces;
using Persistencia;

namespace Logica.Singelton
{
    public class Logica_Zona
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Logica_Zona> _instancia = new Lazy<Logica_Zona>(() => new Logica_Zona());

        // No se permite usar la clase en entornos locales
        private Logica_Zona() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Logica_Zona Instancia { get { return _instancia.Value; } }

        #endregion

        public List<string> Alta_Zona(Object nombre, Object codigo, Object departamento, Object habitantes)
        {
        #region ABM
            Zona unaZona = new Zona();
            Logica_Automatica logica = new Logica_Automatica() {
                Validadores_Formato =
                {
                    new Controlador_Valores(){
                        Nombre = "Nombre",
                        Valor = nombre.ToString(),
                        Asignar = a => unaZona.Nombre = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Hasta(){Limite=30, Solo_Validar_Largo=true}
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Letra de Departamento",
                        Valor=departamento,
                        Asignar = a => unaZona.Letra_Departamento = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new SoloLetras(),
                            new Hasta(){Limite=1, Solo_Validar_Largo=true }
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Codigo",
                        Valor=codigo,
                        Asignar = a => unaZona.Codigo = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new SinEspacios(),
                            //new Hasta(){Limite=3, Solo_Validar_Largo=true}
                            new Limite(){Maximo=3,Minimo=3}
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Habitantes",
                        Valor=habitantes,
                        Asignar = a => unaZona.Habitantes = int.Parse(habitantes.ToString()),
                        Validadores =
                        {
                            new NoVacio(),
                            new Numeros<int>(),
                            new Minimo(){Limite=1}
                        }
                    },
                },
                Validadores_Persistencia = {
                    new Controlador_Valores(){
                        Nombre="Control de base de datos: ",
                        Valor="VerificarZona",
                        Validadores =
                        {
                            new ValidadorSQL()
                            {
                                Parametros = {
                                    {"letra_departamento",departamento },
                                    {"codigo",codigo },
                                    {"nombre",nombre}
                                },
                                ErrorA = "Existe una zona con la mismo código y letra de departamento",
                                ErrorB = "Ya hay una zona con el mismo nombre en el departamento"                                                                
                            }
                        }
                    }
                }

            };
            List<string> retorno = logica.Iniciar_Comprobacion();
            if(retorno.Count == 0)
            {
                if(Fabrica_Persistencia.getPersistencia_Zona.Alta(unaZona) != 0)
                {
                    retorno.Add("Error al crear el objeto en la base de datos");
                }
            }
            return retorno;
        }
        public List<string> Baja_Zona(Object departamento, Object codigo)
        {
            Zona unaZona = new Zona();
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {                    
                    new Controlador_Valores(){
                        Nombre = "Letra de Departamento",
                        Valor=departamento,
                        Asignar = a => unaZona.Letra_Departamento = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new SoloLetras(),
                            new Hasta(){Limite=1}
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Codigo",
                        Valor=codigo,
                        Asignar = a => unaZona.Codigo = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=3}
                        }
                    }
                }                

            };
            List<string> retorno = logica.Iniciar_Comprobacion();
            if (retorno.Count == 0)
            {
                int valor = Fabrica_Persistencia.getPersistencia_Zona.Baja(unaZona);
                if (valor == 0)
                {
                    return retorno;
                }
                else if(valor == -1)
                {
                    retorno.Add("No existe la zona o ya fue eliminada");
                }
                else
                {
                    retorno.Add("Fallo la eliminacion de la zona");
                }
            }
            return retorno;

        }
        public List<string> Modificar_Zona(Object nombre, Object codigo, Object departamento, Object habitantes)
        {
            Zona unaZona = new Zona();
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {
                    new Controlador_Valores(){
                        Nombre = "Nombre",
                        Valor = nombre.ToString(),
                        Asignar = a => unaZona.Nombre = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=30}
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Letra de Departamento",
                        Valor=departamento,
                        Asignar = a => unaZona.Letra_Departamento = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new SoloLetras(),
                            new Minimo(){Limite=1}
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Codigo",
                        Valor=codigo,
                        Asignar = a => unaZona.Codigo = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=3}
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Habitantes",
                        Valor=habitantes,
                        Asignar = a => unaZona.Habitantes = int.Parse(habitantes.ToString()),
                        Validadores =
                        {
                            new NoVacio(),
                            new Numeros<int>()
                        }
                    },
                }                
            };
            List<string> retorno = logica.Iniciar_Comprobacion();
            if (retorno.Count == 0)
            {
                int valor = Fabrica_Persistencia.getPersistencia_Zona.Modificar(unaZona);
                if (valor == 0)
                {
                    return retorno;
                }
                else if (valor == -1)
                {
                    retorno.Add("No existe esa zona");
                }
                else if (valor == -2)
                {
                    retorno.Add("Ya existe una zona con el mismo nombre");
                }
                else
                {
                    retorno.Add("Fallo la modificacion de la zona");
                }
            }
            return retorno;
        }
        #endregion

        #region Listados
        public List<Zona> Listado_Activos() { return Fabrica_Persistencia.getPersistencia_Zona.Listado_Activos(); }
        public List<Zona> Listado_Todo() { return Fabrica_Persistencia.getPersistencia_Zona.Listado_Todo(); }

        #endregion

        #region Servicios
        public void Listar_Servicios(ref Zona ZonaSeleccionada) { Fabrica_Persistencia.getPersistencia_Zona.Listar_Servicios(ref ZonaSeleccionada); }

        public List<string> Agregar_Servicio(Zona Zona_Seleccionada,Object Nombre_Servicio)
        {
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {
                    new Controlador_Valores()
                    {
                        Nombre="Nombre Servicio",
                        Valor=Nombre_Servicio,
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=30}
                        }
                    }

                }

            };
            List<string> retorno = logica.Iniciar_Comprobacion();
            if (retorno.Count == 0)
            {
                int valor = Fabrica_Persistencia.getPersistencia_Zona.Alta_Servicio(
                    Zona_Seleccionada.Letra_Departamento,Zona_Seleccionada.Codigo,Nombre_Servicio.ToString());
                if (valor == 0)
                {
                    return retorno;
                }
                else if (valor == -1)
                {
                    retorno.Add("No existe la zona");
                }
                else if (valor == -2)
                {
                    retorno.Add("Ya existe un servicio con el mismo nombre");
                }
                else
                {
                    retorno.Add("Fallo la creacion del servicio");
                }
            }
            return retorno;
        }

        public List<string> Eliminar_Servicio(Zona Zona_Seleccionada, Object Nombre_Servicio)
        {
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {
                    new Controlador_Valores()
                    {
                        Nombre="Nombre Servicio",
                        Valor=Nombre_Servicio,
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=30}
                        }
                    }

                }

            };
            List<string> retorno = logica.Iniciar_Comprobacion();
            if (retorno.Count == 0)
            {
                int valor = Fabrica_Persistencia.getPersistencia_Zona.Baja_Servicio(
                    Zona_Seleccionada.Letra_Departamento, Zona_Seleccionada.Codigo, Nombre_Servicio.ToString());
                if (valor == 0)
                {
                    return retorno;
                }
                else if (valor == -1)
                {
                    retorno.Add("No existe ese servicio");
                }
                else
                {
                    retorno.Add("Fallo la creacion del servicio");
                }
            }
            return retorno;
        }
        public Zona Generar(string codigo, string letra_departamento)
        {
            return Fabrica_Persistencia.getPersistencia_Zona.Generar(codigo, letra_departamento);
        }
        #endregion

    }
}
