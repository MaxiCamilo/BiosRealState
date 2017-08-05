using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logica.Interfaces;
using Logica.Singleton;

namespace Logica
{
    public class Fabrica_Logica
    {
        public static ILogica_Apartamento getLogica_Apartamento
        {
            get { return (Logica_Apartamento.Instancia); }
        }
        public static ILogica_Casa getLogica_Casa
        {
            get { return (Logica_Casa.Instancia); }
        }
        public static ILogica_Consulta getLogica_Consulta
        {
            get { return (Logica_Consulta.Instancia); }
        }
        public static ILogica_Empleado getLogica_Empleado
        {
            get { return (Logica_Empleado.Instancia); }
        }
        public static ILogica_Local getLogica_Local
        {
            get { return (Logica_Local.Instancia); }
        }
        public static ILogica_Propiedad getLogica_Propiedad
        {
            get { return (Logica_Propiedad.Instancia); }
        }
        public static ILogica_Zona getLogica_Zona
        {
            get { return (Logica_Zona.Instancia); }
        }
    }
}
