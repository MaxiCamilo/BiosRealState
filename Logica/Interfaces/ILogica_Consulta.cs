using Entidades.Realidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica.Interfaces
{
    public interface ILogica_Consulta
    {
        List<string> Alta_Consulta(object telefono, object nombre, object fecha, object hora, object padron);
        List<string> Baja_Consulta(object telefono, object fecha, object hora);
        List<Consulta> Listado(int padron);
        Consulta Generar(DateTime Fecha, string telefono);

    }
}
