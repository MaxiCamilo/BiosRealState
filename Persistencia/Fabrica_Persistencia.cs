using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Interfaces;
using Persistencia.Singleton;

namespace Persistencia
{
    public class Fabrica_Persistencia
    {
        public static IPersistencia_Apartamento getPersistencia_Apartamento
        {
            get { return (Persistencia_Apartamento.Instancia); }
        }
        public static IPersistencia_Casa getPersistencia_Casa
        {
            get { return (Persistencia_Casa.Instancia); }
        }
        public static IPersistencia_Consulta getPersistencia_Consulta
        {
            get { return (Persistencia_Consulta.Instancia); }
        }
        public static IPersistencia_Empleado getPersistencia_Empleado
        {
            get { return (Persistencia_Empleado.Instancia); }
        }
        public static IPersistencia_Local getPersistencia_Local
        {
            get { return (Persistencia_Local.Instancia); }
        }
        public static IPersistencia_Propiedad getPersistencia_Propiedad
        {
            get { return (Persistencia_Propiedad.Instancia); }
        }
        public static IPersistencia_Zona getPersistencia_Zona
        {
            get { return (Persistencia_Zona.Instancia); }
        }
    }
}
