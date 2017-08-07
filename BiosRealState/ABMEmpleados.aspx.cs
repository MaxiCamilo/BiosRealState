using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades.Realidad;

namespace BiosRealState
{
    public partial class ABMEmpleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("/Default.aspx");
                }
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }

        protected void btnAltaEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                MxResultadoAlta.Visible = false;
                List<string> retorno = Fabrica_Logica.getLogica_Empleado.Alta_empleado(txtAltaNombre.Text, txtAltaContrasenia.Text, txtAltaConfirmacion.Text);
                if (retorno.Count != 0)
                {
                    MxResultadoAlta.Resultado = retorno;
                    MxResultadoAlta.Visible = true;
                    AltaPositivo.Visible = false;
                    return;
                }
                MxResultadoAlta.Visible = false;
                AltaPositivo.Visible = true;

                txtAltaNombre.Text = "";
                txtAltaContrasenia.Text = "";
                txtAltaConfirmacion.Text = "";
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }

        protected void btnModificarClave_Click(object sender, EventArgs e)
        {
            try
            {
                MxResultadoClave.Visible = false;
                Empleado unEmpleado = new Empleado();
                unEmpleado.Nombre = ((Empleado)Session["usuario"]).Nombre;
                unEmpleado.Contrasenia = txtModificarVieja.Text.ToString();

                List<string> retorno = Fabrica_Logica.getLogica_Empleado.Modificar_empleado(unEmpleado, txtModificarNueva.Text, txtModificarConfirmacion.Text);
                if (retorno.Count != 0)
                {
                    MxResultadoClave.Resultado = retorno;
                    MxResultadoClave.Visible = true;
                    ModificarPositivo.Visible = false;
                    return;
                }
                unEmpleado.Contrasenia = txtModificarNueva.Text.ToString();
                Session["usuario"] = unEmpleado;
                MxResultadoClave.Visible = false;
                ModificarPositivo.Visible = true;

                txtModificarConfirmacion.Text = "";
                txtModificarNueva.Text = "";
                txtModificarVieja.Text = "";
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }

        protected void btnDarDeBaja_Click(object sender, EventArgs e)
        {
            try
            {
                MxResultadoBaja.Visible = false;
                Empleado unEmpleado = new Empleado();
                unEmpleado.Nombre = ((Empleado)Session["usuario"]).Nombre;
                unEmpleado.Contrasenia = txtBajaContrasenia.Text.ToString();

                List<string> retorno = Fabrica_Logica.getLogica_Empleado.Baja_Empleado(unEmpleado.Nombre, unEmpleado.Contrasenia, txtBajaConfirmacion.Text);
                if (retorno.Count != 0)
                {
                    MxResultadoBaja.Resultado = retorno;
                    MxResultadoBaja.Visible = true;
                    return;
                }
                Session["usuario"] = null;
                Response.Redirect("/");
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
    }
}