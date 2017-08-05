using Entidades.Realidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica.Interfaces
{
    public interface ILogica_Apartamento
    {
        List<string> Alta_apartamento(Object padron, Object direccion, Object precio, Object accion, Object cantidad_banio,
            Object cantidad_habitaciones, Object metros_cuadrados, Object codigo_zona, Object letra_departamento, Object nombre_empleado, bool ascensor, Object piso);
        List<string> Baja_apartamento(Object padron);
        List<string> Modificar_apartamento(Object padron, Object direccion, Object precio, Object accion, Object cantidad_banio,
            Object cantidad_habitaciones, Object metros_cuadrados, Object codigo_zona, Object letra_departamento, Object nombre_empleado, bool ascensor, Object piso);
        List<Apartamento> Listado_Activo();
        List<Apartamento> Listado_Todo();
    }
}
