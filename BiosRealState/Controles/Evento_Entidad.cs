using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiosRealState.Controles
{
    public class Evento_Entidad : EventArgs
    {
        public Entidad Envio { get; set; }
    }
}