using Entidades.Realidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica.Interfaces
{
    public interface ILogica_Empleado
    {
        List<string> Alta_empleado(object nombre, object contrasenia, object confirmacion);
        List<string> Baja_Empleado(object nombre, object contrasenia, object confirmacion);
        List<string> Modificar_empleado(Empleado unEmpleado, object contrasenia_nueva, object confirmacion);
        List<Empleado> Listado(int padron);
        List<Propiedad> Consultas_Propiedades_Modificadas(Empleado Empleado_Seleccionado);
        List<string> Iniciar_Sesion(object nombre, object contrasenia);

    }
}
