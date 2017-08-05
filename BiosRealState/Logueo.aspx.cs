using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades.Realidad;

namespace BiosRealState
{
    public partial class Logueo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            List<string> retorno = Fabrica_Logica.getLogica_Empleado.Iniciar_Sesion(txtNombre.Text, txtContrasenia.Text);
            
            if(retorno.Count == 0)
            {
                Session["usuario"] = new Empleado(txtNombre.Text.ToString(), txtContrasenia.Text.ToString());
                Server.Transfer("/Default.aspx", true);
            }
            else
            {
                MxResultado.Visible = true;
                MxResultado.Resultado = retorno;
            }
        }
    }
}