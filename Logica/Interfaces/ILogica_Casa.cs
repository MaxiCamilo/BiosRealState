using Entidades.Realidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica.Interfaces
{
    public interface ILogica_Casa
    {
        List<string> Alta_casa(Object padron, Object direccion, Object precio, Object accion, Object cantidad_banio,
            Object cantidad_habitaciones, Object metros_cuadrados, Object codigo_zona, Object letra_departamento, Object nombre_empleado, bool jardin, Object tamanio_terrento);
        List<string> Baja_casa(Object padron);
        List<string> Modificar_casa(Object padron, Object direccion, Object precio, Object accion, Object cantidad_banio,
            Object cantidad_habitaciones, Object metros_cuadrados, Object codigo_zona, Object letra_departamento, Object nombre_empleado, bool jardin, Object tamanio_terrento);
        List<Casa> Listado_Activos();
        List<Casa> Listado_Todos();
    }
}
