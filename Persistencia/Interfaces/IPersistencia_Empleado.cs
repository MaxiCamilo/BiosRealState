using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Realidad;

namespace Persistencia.Interfaces
{
    public interface IPersistencia_Empleado
    {
        int Alta(Empleado Objeto);
        int Baja(Empleado Objeto);
        int Modificar(Empleado Objeto, string NuevaContrasenia);
        List<Empleado> Listado(int padron);
        List<Propiedad> Consultas_Propiedades_Modificadas(Empleado Empleado_Seleccionado);
        int Inicio_Sesion(Empleado Empleado_Seleccionado);
    }
}
