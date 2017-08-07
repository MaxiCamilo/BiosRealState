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
    public partial class HacerConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtFecha.Attributes.Add("type", "date");
                if (!IsPostBack)
                {
                    if (Request.QueryString["padron"] == null || Request.QueryString["padron"] == "")
                    {
                        Principal.InnerHtml = "Error: Padron no valido";
                        return;
                    }
                    else
                    {
                        int prueba = 0;
                        if (!int.TryParse(Request.QueryString["padron"], out prueba))
                        {
                            Principal.InnerHtml = "Error: Padron no valido";
                            return;
                        }
                        Session["Padron"] = prueba;
                        List<Consulta> listado = Fabrica_Logica.getLogica_Consulta.Listado(prueba);
                        Session["Consultas"] = prueba;
                        if (listado.Count == 0)
                        {
                            repetidor.Visible = false;
                        }
                        else
                        {
                            repetidor.Visible = true;
                            repetidor.DataSource = listado;
                            repetidor.DataBind();

                            if (listado.Count >= 2)
                            {
                                LugarConfirmacion.InnerHtml = "<h5 class='red darken-2 white-text card-panel hoverable'>Lamentablemente, esta propiedad paso el cupo de consultas.</h5>";

                            }
                        }


                        //Llenamos de datos
                        Propiedad unaPropiedad = Fabrica_Logica.getLogica_Propiedad.Generar(prueba);

                        Padron.Attributes.Add("value", unaPropiedad.Padron.ToString());
                        Direccion.Attributes.Add("value", unaPropiedad.Direccion.ToString());
                        Accion.Attributes.Add("value", unaPropiedad.Accion.ToString());
                        Banios.Attributes.Add("value", unaPropiedad.Cantidad_Banios.ToString());
                        Habitaciones.Attributes.Add("value", unaPropiedad.Cantidad_Habitaciones.ToString());
                        Metros.Attributes.Add("value", unaPropiedad.Metros_Cuadrados.ToString());
                        Precio.Attributes.Add("value", unaPropiedad.Precio.ToString());

                        //Llenamos zona
                        Zona unaZona = Fabrica_Logica.getLogica_Zona.Generar(unaPropiedad.Zona.Codigo, unaPropiedad.Zona.Letra_Departamento);
                        Fabrica_Logica.getLogica_Zona.Listar_Servicios(ref unaZona);

                        ZonaNombre.Attributes.Add("value", unaZona.Nombre.ToString());
                        ZonaDepartamento.Attributes.Add("value", unaZona.Letra_Departamento.ToString());
                        ZonaId.Attributes.Add("value", unaZona.Codigo.ToString());
                        ZonaHabitantes.Attributes.Add("value", unaZona.Habitantes.ToString());

                        Servicios.DataSource = unaZona.Servicios;
                        Servicios.DataBind();
                    }
                }
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                MxResultado.Visible = false;
                List<string> Retorno = Fabrica_Logica.getLogica_Consulta.Alta_Consulta(txtTelefono.Text, txtNombre.Text, txtFecha.Text, txtHora.Text, Request.QueryString["padron"].ToString());
                if (Retorno.Count == 0)
                {
                    Principal.InnerHtml = "<br /><h1 class='light-green-text text-accent-3'>Consulta creada, lo esperamos luego!</h1><br />";
                }
                else
                {
                    MxResultado.Visible = true;
                    MxResultado.Resultado = Retorno;
                }
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
    }
}