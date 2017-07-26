using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Realidad;

namespace Persistencia.Interfaces
{
    public interface IPersistencia_Zona
    {
        int Alta(Zona Objeto);
        int Baja(Zona Objeto);
        int Modificar(Zona Objeto);
        List<Zona> Listado_Activos();
        List<Zona> Listado_Todo();
        int Alta_Servicio(string Letra_Apartamento, string Codigo, string Nombre_Servicio);
        int Baja_Servicio(string Letra_Apartamento, string Codigo, string Nombre_Servicio);
        void Listar_Servicios(ref Zona ZonaSeleccionada);
        Zona Generar(string codigo, string letra_departamento);
    }
}
