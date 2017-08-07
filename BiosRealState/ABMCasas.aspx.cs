using BiosRealState.Controles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades.Realidad;
using Logica;

namespace BiosRealState
{
    public partial class ABMCasas : System.Web.UI.Page
    {
        private void Actualziar_Listados()
        {
            List<Zona> Zonas_Activas = Fabrica_Logica.getLogica_Zona.Listado_Activos();
            MxZonas_Alta.Items = Zonas_Activas;
            List<Casa> Casas_Activas = Fabrica_Logica.getLogica_Casa.Listado_Activos();
            mxTabla_Listado.Items = Casas_Activas;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("/Default.aspx");
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

        protected void btnAltaCasa_Click(object sender, EventArgs e)
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
                List<string> retorno = Fabrica_Logica.getLogica_Casa.Alta_casa(txtAltaPadron.Text.ToString(),
                    txtAltaDireccion.Text.ToString(), txtAltaPrecio.Text.ToString(), txtAltaAccion.SelectedValue,
                    txtAltaCantidad_banio.Text.ToString(), txtAltacantidad_habitaciones.Text.ToString(), txtAltaMetros_cuadrados.Text.ToString(),
                    unaZona.Codigo, unaZona.Letra_Departamento, unEmpleado.Nombre, txtAltaJardir.Checked, txtAltaTamanio_terreno.Text.ToString());
                if (retorno.Count != 0)
                {
                    MxResultadoAlta.Resultado = retorno;
                    MxResultadoAlta.Visible = true;
                    AltaPositivo.Visible = false;
                    Actualziar_Listados();
                    return;
                }

                txtAltaCantidad_banio.Text = "";
                txtAltacantidad_habitaciones.Text = "";
                txtAltaDireccion.Text = "";
                txtAltaMetros_cuadrados.Text = "";
                txtAltaPadron.Text = "";
                txtAltaTamanio_terreno.Text = "";
                txtAltaPrecio.Text = "";

                Session["casa"] = null;
                Session["zona"] = null;
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
                Session["casa"] = (Casa)entidad.Envio;
                Casa unaCasa = (Casa)entidad.Envio;
                txtModificarAccion.SelectedValue = unaCasa.Accion.ToString();
                txtModificarCantidad_banio.Text = unaCasa.Cantidad_Banios.ToString();
                txtModificarcantidad_habitaciones.Text = unaCasa.Cantidad_Banios.ToString();
                txtModificarDireccion.Text = unaCasa.Direccion;
                txtModificarJardir.Checked = unaCasa.Jardin;
                txtModificarMetros_cuadrados.Text = unaCasa.Metros_Cuadrados.ToString();
                txtModificarPrecio.Text = unaCasa.Precio.ToString();
                txtModificarTamanio_terreno.Text = unaCasa.Tamanio_Terreno.ToString();
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
                Casa unaCasa = (Casa)entidad.Envio;
                List<string> retorno = Fabrica_Logica.getLogica_Casa.Baja_casa(unaCasa.Padron);
                if (retorno.Count == 0)
                {
                    MxResultadoModificar.Visible = false;
                    Session["casa"] = null;
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

        protected void btnModificarCasa_Click(object sender, EventArgs e)
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
                Casa unaCasa = (Casa)Session["casa"];
                List<string> Retorno = Fabrica_Logica.getLogica_Casa.Modificar_casa(unaCasa.Padron, txtModificarDireccion.Text, txtModificarPrecio.Text, txtModificarAccion.SelectedValue, txtModificarCantidad_banio.Text, txtModificarcantidad_habitaciones.Text, txtModificarMetros_cuadrados.Text, unaCasa.Zona.Codigo, unaCasa.Zona.Letra_Departamento, unEmpleado.Nombre, txtModificarJardir.Checked, txtModificarTamanio_terreno.Text);
                if (Retorno.Count != 0)
                {
                    MxResultadoModificar.Resultado = Retorno;
                    MxResultadoModificar.Visible = true;
                }
                Actualziar_Listados();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
    }
}