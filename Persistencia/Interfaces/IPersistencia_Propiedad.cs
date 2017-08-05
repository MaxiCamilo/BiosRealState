using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Realidad;

namespace Persistencia.Interfaces
{
    public interface IPersistencia_Propiedad
    {
        int Alta(Propiedad Objeto);
        int Baja(Propiedad Objeto);
        int Modificar(Propiedad Objeto);
        List<Propiedad> Listado_Activos();
        List<Propiedad> Listado_Todo();
        Propiedad Generar(int Padron);
        void Detallar_Zona(ref Propiedad Propiedad_Elegida);
        void Detalle_Empleado(ref Propiedad Propiedad_Elegida);
        List<Consulta> Listar_Consultas(Propiedad Propiedad_Elegida);
    }
}
