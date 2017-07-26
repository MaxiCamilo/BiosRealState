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

namespace Logica.Singelton
{
    public class Logica_Apartamento
    {
        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
        // Fuente: https://es.wikipedia.org/wiki/Singleton
        private static readonly Lazy<Logica_Apartamento> _instancia = new Lazy<Logica_Apartamento>(() => new Logica_Apartamento());

        // No se permite usar la clase en entornos locales
        private Logica_Apartamento() { }

        // Utilice la instancia para usar el objeto, es seguro para entornos multi hilos
        public static Logica_Apartamento Instancia { get { return _instancia.Value; } }

        #endregion

        #region Implementacion

        public List<string> Alta_apartamento(Object padron, Object direccion, Object precio, Object accion, Object cantidad_banio,
            Object cantidad_habitaciones, Object metros_cuadrados, Object codigo_zona, Object letra_departamento, Object nombre_empleado, bool ascensor, Object piso)
        {
            Propiead laBase = new Propiead();

            List<string> retorno = Logica_Propiedad.Instancia.Verificar_Propiedad(out laBase, padron, direccion, precio, accion, cantidad_banio, cantidad_banio,
                metros_cuadrados, codigo_zona, letra_departamento, nombre_empleado, true);
            if (retorno.Count != 0)
            {
                return retorno;
            }
            Apartamento unApartamento = new Apartamento(laBase);
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {
                    new Controlador_Valores
                    {
                        Nombre="Piso",
                        Valor=piso,
                        Asignar= a => unApartamento.Piso = int.Parse(a.ToString()),
                        Validadores =
                        {
                            new Numeros<int>(),
                            new Minimo(){ Limite=0}
                        }
                    }
                }
            };
            retorno = logica.Iniciar_Comprobacion();
            if (retorno.Count == 0)
            {
                int dio = Fabrica_Persistencia.getPersistencia_Apartamento.Alta(unApartamento);
                if (dio == 0)
                {
                    return retorno;
                }
                else if (dio == -1)
                {
                    retorno.Add("Ya existe una propiedad con el mismo padron, o la misma se encuetra anulada");
                }
                else if (dio == -2)
                {
                    retorno.Add("No existe la zona asignada");
                }
                else if (dio == -3)
                {
                    retorno.Add("Uno o Varias variables tiene un valor numerico negativo");
                }
                else if (dio == -4)
                {
                    retorno.Add("No existe el empleado");
                }
                else if (dio == -5)
                {
                    retorno.Add("Fallo el alta de Propiedades");
                }
                else
                {
                    retorno.Add("No se pudo crear el apartamento en la base de datos");
                }
            }

            return retorno;
        }

        public List<string> Baja_apartamento(Object padron)
        {
            return Logica_Propiedad.Instancia.Baja_Propiedad(padron);
        }

        public List<string> Modificar_apartamento(Object padron, Object direccion, Object precio, Object accion, Object cantidad_banio,
            Object cantidad_habitaciones, Object metros_cuadrados, Object codigo_zona, Object letra_departamento, Object nombre_empleado, bool ascensor, Object piso)
        {
            Propiead laBase = new Propiead();
            List<string> retorno = Logica_Propiedad.Instancia.Verificar_Propiedad(out laBase, padron, direccion, precio, accion, cantidad_banio, cantidad_banio,
                metros_cuadrados, codigo_zona, letra_departamento, nombre_empleado, false);
            if (retorno.Count != 0)
            {
                return retorno;
            }
            Apartamento unApartamento = new Apartamento(laBase);
            Logica_Automatica logica = new Logica_Automatica()
            {
                Validadores_Formato =
                {
                    new Controlador_Valores
                    {
                        Nombre="Piso",
                        Valor=piso,
                        Asignar= a => unApartamento.Piso = int.Parse(a.ToString()),
                        Validadores =
                        {
                            new Numeros<int>(),
                            new Minimo(){ Limite=0}
                        }
                    }
                }
            };
            retorno = logica.Iniciar_Comprobacion();
            if (retorno.Count == 0)
            {
                int dio = Fabrica_Persistencia.getPersistencia_Apartamento.Modificar(unApartamento);
                if (dio == 0)
                {
                    return retorno;
                }
                else if (dio == -1)
                {
                    retorno.Add("No existe la propiedad seleccionada.");
                }
                else if (dio == -2)
                {
                    retorno.Add("Uno o Varias variables tiene un valor numerico negativo");
                }
                else if (dio == -3)
                {
                    retorno.Add("No existe el empleado");
                }
                else if (dio == -4 || dio == -5)
                {
                    retorno.Add("Fallo la modificacion de Propiedades");
                }
                else if (dio == -6)
                {
                    retorno.Add("Esa propiedad no es un apartamento");
                }
                else
                {
                    retorno.Add("No se pudo modificar el local en la base de datos");
                }
            }

            return retorno;
        }




#endregion
    }
}
