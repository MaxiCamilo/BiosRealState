using Entidades.Realidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica.Interfaces
{
    public interface ILogica_Zona
    {
        List<string> Alta_Zona(Object nombre, Object codigo, Object departamento, Object habitantes);
        List<string> Baja_Zona(Object departamento, Object codigo);
        List<string> Modificar_Zona(Object nombre, Object codigo, Object departamento, Object habitantes);
        List<Zona> Listado_Activos();
        List<Zona> Listado_Todo();
        void Listar_Servicios(ref Zona ZonaSeleccionada);
        List<string> Agregar_Servicio(Zona Zona_Seleccionada, Object Nombre_Servicio);
        List<string> Eliminar_Servicio(Zona Zona_Seleccionada, Object Nombre_Servicio);
        Zona Generar(string codigo, string letra_departamento);
    }
}
