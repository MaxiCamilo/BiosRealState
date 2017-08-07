using Entidades.Interfaces;
using Entidades.Realidad;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiosRealState.Controles;

namespace BiosRealState
{
    public partial class ABMZonas : System.Web.UI.Page
    {
        private void Actualziar_Listados()
        {
            List<Zona> Zonas_Activas = Fabrica_Logica.getLogica_Zona.Listado_Activos();
            MxZonas_Servicio.Items = Zonas_Activas;
            mxTabla_Listado.Items = Zonas_Activas;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("/Default.aspx");
                }

                if (Session["zona"] == null)
                {
                    Panel_Servicios.Visible = false;
                }
                Actualziar_Listados();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }



        }

        protected void btnAltaZona_Click(object sender, EventArgs e)
        {
            try
            {
                AltaPositivo.Visible = false;
                MxResultadoAlta.Visible = false;
                List<string> resultado = Fabrica_Logica.getLogica_Zona.Alta_Zona(txtAltaNombre.Text.ToString(), txtAltaCodigo.Text.ToString(), txtAltaDepartamento.Text.ToString(), txtAltaHabitantes.Text.ToString());
                Session["zona"] = null;
                if (resultado.Count != 0)
                {
                    MxResultadoAlta.Resultado = resultado;
                    MxResultadoAlta.Visible = true;
                    AltaPositivo.Visible = false;
                    Actualziar_Listados();
                    return;

                }
                MxResultadoAlta.Visible = false;
                AltaPositivo.Visible = true;
                Actualziar_Listados();
                txtAltaCodigo.Text = "";
                txtAltaDepartamento.Text = "";
                txtAltaHabitantes.Text = "";
                txtAltaNombre.Text = "";

            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }


        }

        protected void btnAltaServicio_Click(object sender, EventArgs e)
        {
            try
            {
                errorAltaServicio.InnerHtml = "";
                if (txtAltaServicio.Text.ToString() == "")
                {
                    errorAltaServicio.InnerHtml = "Tiene que introducir un nombre.";
                    return;
                }
                foreach (ListItem ser in Listado_Servicios.Items)
                {
                    if (ser.ToString() == txtAltaServicio.Text.ToString())
                    {
                        errorAltaServicio.InnerHtml = "Ya existe un servicio con el mismo nombre.";
                        return;
                    }
                }
                List<string> retorno = Fabrica_Logica.getLogica_Zona.Agregar_Servicio((Zona)Session["zona"], txtAltaServicio.Text.ToString());
                if (retorno.Count == 0)
                {
                    errorAltaServicio.InnerHtml = "El servicio " + txtAltaServicio.Text + " fue ingresado.";
                    Cargar_Lista_Servicio((Zona)Session["zona"]);
                }
                else
                {
                    errorAltaServicio.InnerHtml = retorno[0];
                    Cargar_Lista_Servicio((Zona)Session["zona"]);
                }
                Actualziar_Listados();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }

        }

        protected void MxZonas_Servicio_Seleccionado(object sender, Evento_Entidad entidad)
        {
            try
            {
                Cargar_Lista_Servicio((Zona)entidad.Envio);
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }




        private void Cargar_Lista_Servicio(Zona unaZona)
        {
            try
            {
                Panel_Servicios.Visible = true;
                Session["zona"] = unaZona;

                Fabrica_Logica.getLogica_Zona.Listar_Servicios(ref unaZona);
                Listado_Servicios.Items.Clear();
                foreach (string servicio in unaZona.Servicios)
                {
                    Listado_Servicios.Items.Add(servicio);
                }
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }

        protected void btnAltaEliminarServicio_Click(object sender, EventArgs e)
        {
            try
            {
                errorAltaServicio.InnerHtml = "";
                if (Listado_Servicios.SelectedValue == "")
                {
                    errorAltaServicio.InnerHtml = "Debe seleccionar un servicio.";
                    return;
                }
                List<string> retorno = Fabrica_Logica.getLogica_Zona.Eliminar_Servicio((Zona)Session["zona"], Listado_Servicios.SelectedValue);
                if (retorno.Count == 0)
                {
                    errorAltaServicio.InnerHtml = "Se elimino el servicio correctamente";
                    Cargar_Lista_Servicio((Zona)Session["zona"]);


                }
                else
                {
                    errorAltaServicio.InnerHtml = retorno[0];
                    Cargar_Lista_Servicio((Zona)Session["zona"]);
                }
                Actualziar_Listados();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }

        protected void btnModificarZona_Click(object sender, EventArgs e)
        {
            try
            {
                Zona unaZona = (Zona)Session["zona"];

                MxResultadoModificar.Visible = false;
                List<string> Retorno = Fabrica_Logica.getLogica_Zona.Modificar_Zona(txtModificarNombre.Text.ToString(), unaZona.Codigo, unaZona.Letra_Departamento, txtModificarHabitantes.Text.ToString());
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

        protected void mxTabla_Listado_Modificar(object sender, Evento_Entidad entidad)
        {
            try
            {
                Panel_Modificar.Visible = true;
                Zona unaZona = (Zona)entidad.Envio;
                Session["zona"] = unaZona;
                txtModificarNombre.Text = unaZona.Nombre;
                txtModificarHabitantes.Text = unaZona.Habitantes.ToString();
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
                Zona unaZona = (Zona)entidad.Envio;
                List<string> retorno = Fabrica_Logica.getLogica_Zona.Baja_Zona(unaZona.Letra_Departamento, unaZona.Codigo);
                if (retorno.Count == 0)
                {
                    MxResultadoModificar.Visible = false;
                    Session["zona"] = null;
                    Panel_Servicios.Visible = false;
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
    }
}