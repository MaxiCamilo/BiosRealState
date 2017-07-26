using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Realidad;

namespace Persistencia.Interfaces
{
    public interface IPersistencia_Local
    {
        int Alta(Local Objeto);
        int Baja(Local Objeto);
        int Modificar(Local Objeto);
        List<Local> Listado_Activos();
        List<Local> Listado_Todo();
        Local Generar(int Padron);
    }
}
