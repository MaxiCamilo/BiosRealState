using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Realidad;

namespace Persistencia.Interfaces
{
    public interface IPersistencia_Propiedad
    {
        int Alta(Propiead Objeto);
        int Baja(Propiead Objeto);
        int Modificar(Propiead Objeto);
        List<Propiead> Listado_Activos();
        List<Propiead> Listado_Todo();
        Propiead Generar(int Padron);
        void Detallar_Zona(ref Propiead Propiedad_Elegida);
        void Detalle_Empleado(ref Propiead Propiedad_Elegida);
        List<Consulta> Listar_Consultas(Propiead Propiedad_Elegida);
    }
}
