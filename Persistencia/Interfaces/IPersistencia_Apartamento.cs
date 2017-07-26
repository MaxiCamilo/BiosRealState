using Entidades.Realidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia.Interfaces
{
    public interface IPersistencia_Apartamento
    {
        int Alta(Apartamento Objeto);
        int Baja(Apartamento Objeto);
        int Modificar(Apartamento Objeto);
        List<Apartamento> Listado_Activos();
        List<Apartamento> Listado_Todo();
        Apartamento Generar(int Padron);

    }
}
