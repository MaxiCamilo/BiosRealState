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
            txtFecha.Attributes.Add("type","date");
            if (!IsPostBack)
            {
               if( Request.QueryString["padron"] == null || Request.QueryString["padron"] == "")
                {
                    Principal.InnerHtml = "Error: Padron no valido";
                    return;
                }
               else {
                    int prueba = 0;
                    if(!int.TryParse(Request.QueryString["padron"], out prueba))
                    {
                        Principal.InnerHtml = "Error: Padron no valido";
                        return;
                    }
                    Session["Padron"] = prueba;
                    List<Consulta> listado = Fabrica_Logica.getLogica_Consulta.Listado(prueba);
                    Session["Consultas"] = prueba;
                    if(listado.Count == 0)
                    {
                        repetidor.Visible = false;
                        return;
                    }
                    repetidor.Visible = true;
                    repetidor.DataSource = listado;
                    repetidor.DataBind();

                    if(listado.Count >= 2)
                    {
                        LugarConfirmacion.InnerHtml = "Lamentablemente, esta propiedad paso el cupo de consultas.";

                    }
                }
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            MxResultado.Visible = false;
            List<string> Retorno = Fabrica_Logica.getLogica_Consulta.Alta_Consulta(txtTelefono.Text, txtNombre.Text, txtFecha.Text,txtHora.Text ,Session["Padron"].ToString());
            if(Retorno.Count == 0)
            {
                Principal.InnerHtml = "Consulta creada, lo esperamos luego!";
            }
            else
            {
                MxResultado.Visible = true;
                MxResultado.Resultado = Retorno;
            }
        }
    }
}