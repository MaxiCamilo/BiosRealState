using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Realidad;

namespace Persistencia.Interfaces
{
    public interface IPersistencia_Casa
    {
        int Alta(Casa Objeto);
        int Baja(Casa Objeto);
        int Modificar(Casa Objeto);
        List<Casa> Listado_Activos();
        List<Casa> Listado_Todo();
        Casa Generar(int Padron);
    }
}
