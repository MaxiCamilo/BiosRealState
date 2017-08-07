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
    public partial class ABMApartamentos : System.Web.UI.Page
    {
        private void Actualziar_Listados()
        {
            List<Zona> Zonas_Activas = Fabrica_Logica.getLogica_Zona.Listado_Activos();
            MxZonas_Alta.Items = Zonas_Activas;
            List<Apartamento> Apartamento_Activas = Fabrica_Logica.getLogica_Apartamento.Listado_Activo();
            mxTabla_Listado.Items = Apartamento_Activas;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuario"] == null)
                {
                    Server.Transfer("/Default.aspx", true);
                }
                Actualziar_Listados();
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

        protected void btnAltaApartamento_Click(object sender, EventArgs e)
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
                List<string> retorno = Fabrica_Logica.getLogica_Apartamento.Alta_apartamento(txtAltaPadron.Text.ToString(),
                    txtAltaDireccion.Text.ToString(), txtAltaPrecio.Text.ToString(), txtAltaAccion.SelectedValue,
                    txtAltaCantidad_banio.Text.ToString(), txtAltacantidad_habitaciones.Text.ToString(), txtAltaMetros_cuadrados.Text.ToString(),
                    unaZona.Codigo, unaZona.Letra_Departamento, unEmpleado.Nombre, txtAltaAscensor.Checked, txtAltaPiso.Text.ToString());
                if (retorno.Count != 0)
                {
                    MxResultadoAlta.Resultado = retorno;
                    MxResultadoAlta.Visible = true;
                    AltaPositivo.Visible = false;
                    Actualziar_Listados();
                    return;
                }
                Session["apartamento"] = null;
                Session["zona"] = null;
                txtAltaCantidad_banio.Text = "";
                txtAltacantidad_habitaciones.Text = "";
                txtAltaDireccion.Text = "";
                txtAltaMetros_cuadrados.Text = "";
                txtAltaPadron.Text = "";
                txtAltaPiso.Text = "";
                txtAltaPrecio.Text = "";

                MxResultadoAlta.Visible = false;
                AltaPositivo.Visible = true;
                Panel_Modificar.Visible = false;
                Actualziar_Listados();
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
                Session["apartamento"] = (Apartamento)entidad.Envio;
                Apartamento unApartamento = (Apartamento)entidad.Envio;
                txtModificarAccion.SelectedValue = unApartamento.Accion.ToString();
                txtModificarCantidad_banio.Text = unApartamento.Cantidad_Banios.ToString();
                txtModificarcantidad_habitaciones.Text = unApartamento.Cantidad_Banios.ToString();
                txtModificarDireccion.Text = unApartamento.Direccion;
                txtModificarAscensor.Checked = unApartamento.Ascensor;
                txtModificarMetros_cuadrados.Text = unApartamento.Metros_Cuadrados.ToString();
                txtModificarPrecio.Text = unApartamento.Precio.ToString();
                txtModificarPiso.Text = unApartamento.Piso.ToString();
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
                Apartamento unApartamento = (Apartamento)entidad.Envio;
                List<string> retorno = Fabrica_Logica.getLogica_Apartamento.Baja_apartamento(unApartamento.Padron);
                if (retorno.Count == 0)
                {
                    MxResultadoModificar.Visible = false;
                    Session["apartamento"] = null;
                    Panel_Modificar.Visible = false;
                }
                else
                {
                    MxResultadoModificar.Visible = true;
                    MxResultadoModificar.Resultado = retorno;
                }
                Actualziar_Listados();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }

        protected void btnModificarApartamento_Click(object sender, EventArgs e)
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
                Apartamento unApartamento = (Apartamento)Session["apartamento"];
                List<string> Retorno = Fabrica_Logica.getLogica_Apartamento.Modificar_apartamento(unApartamento.Padron, txtModificarDireccion.Text, txtModificarPrecio.Text, txtModificarAccion.SelectedValue, txtModificarCantidad_banio.Text, txtModificarcantidad_habitaciones.Text, txtModificarMetros_cuadrados.Text, unApartamento.Zona.Codigo, unApartamento.Zona.Letra_Departamento, unEmpleado.Nombre, txtModificarAscensor.Checked, txtModificarPiso.Text);
                if (Retorno.Count != 0)
                {
                    MxResultadoModificar.Resultado = Retorno;
                    MxResultadoModificar.Visible = true;
                }
                Panel_Modificar.Visible = false;
                Actualziar_Listados();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
    }
}