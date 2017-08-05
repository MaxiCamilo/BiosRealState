using Entidades.Realidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica.Interfaces
{
    public interface ILogica_Propiedad
    {
        List<String> Verificar_Propiedad(out Propiedad Generado, Object padron, Object direccion, Object precio, Object accion, Object cantidad_banio,
            Object cantidad_habitaciones, Object metros_cuadrados, Object codigo_zona, Object letra_departamento, Object nombre_empleado, bool VerificarSql = false);
        List<string> Baja_Propiedad(Object Padron);
        List<Propiedad> Listado_Activos();
        List<Propiedad> Listado_Todo();
        Propiedad Generar(int Padron);
        void Detallar_Zona(ref Propiedad Propiedad_Elegida);
        void Detalle_Empleado(ref Propiedad Propiedad_Elegida);
        List<Consulta> Listar_Consultas(Propiedad Propiedad_Elegida);
    }
}
