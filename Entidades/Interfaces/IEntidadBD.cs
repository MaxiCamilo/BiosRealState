using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades.Interfaces
{
    public abstract class Entidad
    {
        public abstract Dictionary<string, object> Parametros();
        public abstract Dictionary<string, object> Identificadores();
        //public abstract Entidad Generador_Objeto(SqlDataReader lector);
    }
}
