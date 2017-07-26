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

namespace Logica.Singleton
{
    public class Logica_Consulta
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Logica_Consulta> _instancia = new Lazy<Logica_Consulta>(() => new Logica_Consulta());

        // No se permite usar la clase en entornos locales
        private Logica_Consulta() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Logica_Consulta Instancia { get { return _instancia.Value; } }

        #endregion

        #region Implementacion

        public List<string> Alta_Consulta(object telefono, object nombre, object fecha, object hora, object padron)
        {
            Consulta unaConsulta = new Consulta();
            string OtroError = "";
            Logica_Automatica logica = new Logica_Automatica()
            {                
                Validadores_Formato =
                {
                    new Controlador_Valores(){
                        Nombre = "Nombre",
                        Valor = nombre.ToString(),
                        Asignar = a => unaConsulta.Nombre = a.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=5,Solo_Validar_Largo=true},
                            new Hasta(){Limite=50,Solo_Validar_Largo=true}
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Telefono",
                        Valor = telefono,
                        Asignar = a => unaConsulta.Telefono = telefono.ToString(),
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=8,Solo_Validar_Largo=true},
                            new Hasta(){Limite=9,Solo_Validar_Largo=true},
                            new Numeros<Int64>(),
                            
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "Fecha",
                        Valor = fecha,
                        Asignar = a => {
                            try
                            {
                                unaConsulta.Fecha = Convert.ToDateTime(a.ToString());
                                if(unaConsulta.Fecha < DateTime.Now)
                                {
                                    OtroError = "Las consultas se asignan en fechas futuras";
                                }
                            }
                            catch
                            {
                                OtroError = "Fecha Invalida";
                            }
                        },
                        Validadores =
                        {
                            new NoVacio()
                        }
                    },
                    new Controlador_Valores(){
                        Nombre = "hora",
                        Valor = hora.ToString(),
                        Asignar = a => {
                            try
                            {
                                unaConsulta.Hora = Convert.ToDateTime(hora.ToString()).ToString();
                            }
                            catch
                            {
                                OtroError = "Hora Invalida";
                            }
                            

                        },
                        Validadores =
                        {
                            new NoVacio(),
                            new Minimo(){Limite=3,Solo_Validar_Largo=true},
                            new Hasta(){Limite=5,Solo_Validar_Largo=true}
                        }
                    },
                    new Controlador_Valores(){
                        Nombre="Padron",
                        Valor=padron,
                        Asignar = a => unaConsulta.Propiedad.Padron = int.Parse(a.ToString()),
                        Validadores =
                        {
                            new NoVacio(),
                            new Numeros<int>()
                        }
                    },
                }
            };
            List<string> retorno = logica.Iniciar_Comprobacion();
            if(OtroError != "")
            {
                retorno.Add(OtroError);
            }
            if (retorno.Count == 0)
            {
                int valor = Fabrica_Persistencia.getPersistencia_Consulta.Alta(unaConsulta);
                if (valor == 0)
                {
                    return retorno;
                }
                else if (valor == -1)
                {
                    retorno.Add("No existe el padron");
                }
                else if (valor == -2)
                {
                    retorno.Add("Fecha invalida");
                }
                else if(valor == -3)
                {
                    retorno.Add("Ya tiene una consulta perdiente");
                }
                else if (valor == -4)
                {
                    retorno.Add("Supero Limite de consulta esta propiedad");
                }
                else if (valor == -5)
                {
                    retorno.Add("Ya Tienes una consulta en esta propiedad");
                }
                else if (valor == -6)
                {
                    retorno.Add("Ya Existe una consulta en esa fecha");
                }
                else if (valor == -7)
                {
                    retorno.Add("Fallo el alta en consulas");
                    
                }
                else
                {
                    retorno.Add("Fallo la agregacion de la consulta");
                }
                return retorno;
            }
            return retorno;

        }

#endregion
    }
}
