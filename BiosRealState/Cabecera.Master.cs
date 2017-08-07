using Entidades.Realidad;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiosRealState
{
    public partial class Cabecera : System.Web.UI.MasterPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Verificamos que el usuario 
            Empleado Logeado = (Empleado)Session["usuario"];
            if(Logeado == null)
            {
                Bienvenida.InnerHtml = "Bienvenido a Bios Real State";
                L_Consulta.Visible = true;
                //Se ocultan los atributos de empleados
                Zonas.Visible = false;
                Casas.Visible = false;
                Apartamentos.Visible = false;
                Locales.Visible = false;
                Empleados.Visible = false;
                CerrarSesion.Visible = false;

                L_Zonas.Visible = false;
                L_Locales.Visible = false;
                L_Empleados.Visible = false;
                L_Casas.Visible = false;
                L_Apartamentos.Visible = false;
                L_CerrarSesion.Visible = false;
                

                //Se define el boton usuario como redireccionador
                Usuario.Text = "Iniciar Sesion";
            }
            //Verifica que el usuario siga existiendo
            else if(Fabrica_Logica.getLogica_Empleado.Iniciar_Sesion(Logeado.Nombre,Logeado.Contrasenia).Count != 0)
            {
                Session["usuario"] = null;
                Response.Redirect("/Errores/Caducado.aspx");
            }
            //Es el empleado
            else
            {
                L_Consulta.Visible = false;
                Bienvenida.InnerHtml = "Bienvenido " + Logeado.Nombre + " a Bios Real State";
                Usuario.Visible = false;
                L_Usuario.Visible = false;

            }
        }

        protected void CerrarSesion_Click(object sender, EventArgs e)
        {
            Session["usuario"] = null;
            Response.Redirect("/");
        }
    }
}