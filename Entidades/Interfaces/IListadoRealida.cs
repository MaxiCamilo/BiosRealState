using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Entidades.Interfaces
{
    interface IListadoRealida
    {
        void GenerarListado<T>(ref SqlDataReader _Lector,ref List<T> _Listado);

    }
}
