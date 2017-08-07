using BiosRealState.Controles;
using Entidades.Realidad;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiosRealState
{
    public partial class ABMLocales : System.Web.UI.Page
    {
        private void Actualizar_Listados()
        {
            List<Zona> Zonas_Activas = Fabrica_Logica.getLogica_Zona.Listado_Activos();
            MxZonas_Alta.Items = Zonas_Activas;
            List<Local> Local_Activas = Fabrica_Logica.getLogica_Local.Listado_Activos();
            mxTabla_Listado.Items = Local_Activas;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("/Default.aspx");
                }
                Actualizar_Listados();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
        protected void MxZonas_Alta_Seleccionado(object sender, Evento_Entidad entidad)
        {
            try
            {
                Session["zona"] = entidad.Envio;

            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
        protected void btnAltaLocal_Click(object sender, EventArgs e)
        {
            try
            {
                MxResultadoAlta.Visible = false;
                if (Session["zona"] == null)
                {
                    List<string> dio = new List<string>() { "Debe elegir una zona" };
                    MxResultadoAlta.Resultado = dio;
                    MxResultadoAlta.Visible = true;
                    AltaPositivo.Visible = false;
                    return;
                }
                if (txtAltaAccion.SelectedValue == "")
                {
                    List<string> dio = new List<string>() { "Debe elegir una accion" };
                    MxResultadoAlta.Resultado = dio;
                    MxResultadoAlta.Visible = true;
                    AltaPositivo.Visible = false;
                    return;
                }
                Zona unaZona = (Zona)Session["zona"];
                Empleado unEmpleado = (Empleado)Session["usuario"];
                List<string> retorno = Fabrica_Logica.getLogica_Local.Alta_local(txtAltaPadron.Text.ToString(),
                    txtAltaDireccion.Text.ToString(), txtAltaPrecio.Text.ToString(), txtAltaAccion.SelectedValue,
                    txtAltaCantidad_banio.Text.ToString(), txtAltacantidad_habitaciones.Text.ToString(), txtAltaMetros_cuadrados.Text.ToString(),
                    unaZona.Codigo, unaZona.Letra_Departamento, unEmpleado.Nombre, txtAltaHabilitacion.Checked);
                if (retorno.Count != 0)
                {
                    MxResultadoAlta.Resultado = retorno;
                    MxResultadoAlta.Visible = true;
                    AltaPositivo.Visible = false;
                    Actualizar_Listados();
                    return;
                }
                Session["local"] = null;
                Session["zona"] = null;
                MxResultadoAlta.Visible = false;
                AltaPositivo.Visible = true;
                Panel_Modificar.Visible = false;
                Actualizar_Listados();

                txtAltaCantidad_banio.Text = "";
                txtAltacantidad_habitaciones.Text = "";
                txtAltaDireccion.Text = "";
                txtAltaMetros_cuadrados.Text = "";
                txtAltaPadron.Text = "";
                txtAltaPrecio.Text = "";
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
        protected void mxTabla_Listado_Modificar(object sender, Evento_Entidad entidad)
        {
            try
            {
                Panel_Modificar.Visible = true;
                Session["local"] = (Local)entidad.Envio;
                Local unLocal = (Local)entidad.Envio;
                txtModificarAccion.SelectedValue = unLocal.Accion.ToString();
                txtModificarCantidad_banio.Text = unLocal.Cantidad_Banios.ToString();
                txtModificarcantidad_habitaciones.Text = unLocal.Cantidad_Banios.ToString();
                txtModificarDireccion.Text = unLocal.Direccion;
                txtModificarMetros_cuadrados.Text = unLocal.Metros_Cuadrados.ToString();
                txtModificarPrecio.Text = unLocal.Precio.ToString();
                txtModificarHabilitacion.Checked = unLocal.Habilitacion;
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
        protected void mxTabla_Listado_Eliminar(object sender, Evento_Entidad entidad)
        {
            try
            {
                Panel_Modificar.Visible = false;
                Local unLocal = (Local)entidad.Envio;
                List<string> retorno = Fabrica_Logica.getLogica_Local.Baja_local(unLocal.Padron);
                if (retorno.Count == 0)
                {
                    MxResultadoModificar.Visible = false;
                    Session["local"] = null;
                    Panel_Modificar.Visible = false;
                }
                else
                {
                    MxResultadoModificar.Visible = true;
                    MxResultadoModificar.Resultado = retorno;
                }
                Actualizar_Listados();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
        protected void btnModificarLocal_Click(object sender, EventArgs e)
        {
            try
            {
                MxResultadoModificar.Visible = false;
                Empleado unEmpleado = (Empleado)Session["usuario"];
                if (txtModificarAccion.SelectedValue == "")
                {
                    List<string> dio = new List<string>() { "Debe elegir una accion" };
                    MxResultadoModificar.Resultado = dio;
                    MxResultadoModificar.Visible = true;
                    return;
                }
                Local unLocal = (Local)Session["local"];
                List<string> Retorno = Fabrica_Logica.getLogica_Local.Modificar_local(unLocal.Padron, txtModificarDireccion.Text, txtModificarPrecio.Text, txtModificarAccion.SelectedValue, txtModificarCantidad_banio.Text, txtModificarcantidad_habitaciones.Text, txtModificarMetros_cuadrados.Text, unLocal.Zona.Codigo, unLocal.Zona.Letra_Departamento, unEmpleado.Nombre, txtModificarHabilitacion.Checked);
                if (Retorno.Count != 0)
                {
                    MxResultadoModificar.Resultado = Retorno;
                    MxResultadoModificar.Visible = true;
                }
                Actualizar_Listados();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
    }
}