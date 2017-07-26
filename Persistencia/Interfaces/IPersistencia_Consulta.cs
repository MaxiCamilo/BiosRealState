using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Realidad;

namespace Persistencia.Interfaces
{
    public interface IPersistencia_Consulta
    {
        int Alta(Consulta Objeto);
        int Baja(Consulta Objeto);
        List<Consulta> Listado(int padron);
        Consulta Generar(DateTime Fecha, string telefono);
        List<Consulta> Consultas_Propiedad(int Padron);

    }
}
