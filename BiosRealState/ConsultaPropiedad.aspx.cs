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
    public partial class ConsultaPropiedad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //Llenamos las zonas
                    List<Zona> variasZonas = Fabrica_Logica.getLogica_Zona.Listado_Activos();
                    txtZona.Items.Add("--Todas--");
                    foreach (Zona unaZona in variasZonas)
                    {
                        txtZona.Items.Add(unaZona.Nombre);
                    }
                    Hacer_Busqueda();

                }
                else
                {

                }
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }

        protected void btnMostrarCuadro_Click(object sender, EventArgs e)
        {
            try
            {
                divBusqueda.Visible = !divBusqueda.Visible;
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }

        private void Hacer_Busqueda()
        {
            try
            {
                List<Propiedad> vp = new List<Propiedad>();

                if (txtTipo.SelectedValue == "local")
                {
                    List<Local> variasPropiedad = Fabrica_Logica.getLogica_Local.Listado_Activos();
                    vp = variasPropiedad.Select(s => (Propiedad)s).ToList();
                }
                else if (txtTipo.SelectedValue == "apartamento")
                {
                    List<Apartamento> variasPropiedad = Fabrica_Logica.getLogica_Apartamento.Listado_Activo();
                    vp = variasPropiedad.Select(s => (Propiedad)s).ToList();
                }
                else if (txtTipo.SelectedValue == "casa")
                {
                    List<Casa> variasPropiedad = Fabrica_Logica.getLogica_Casa.Listado_Activos();
                    vp = variasPropiedad.Select(s => (Propiedad)s).ToList();
                }
                else
                {
                    vp = Fabrica_Logica.getLogica_Propiedad.Listado_Activos();
                }
                //Filtro de Tipo
                if (txtAccion.SelectedItem.Text != "--Todas--")
                {
                    List<Propiedad> prototipo = new List<Propiedad>();
                    foreach (Propiedad p in vp)
                    {
                        if (p.Accion.ToString() == txtAccion.SelectedItem.Text)
                        {
                            prototipo.Add(p);
                        }
                    }
                    vp = prototipo;
                }




                decimal numero = 0;
                //Solo numeros
                if (txtPrecio.Text != "")
                {

                    if (!decimal.TryParse(txtPrecio.Text, out numero))
                    {
                        txtPrecio.Text = "0";
                    }
                }

                //Verificamos los tipos de Linq to Object
                if (txtZona.SelectedItem.Text == "--Todas--" && txtPrecio.Text != "")
                {
                    var filtro = from p in vp
                                 where p.Precio == numero
                                 select p;
                    repetidor.DataSource = filtro;
                    repetidor.DataBind();
                }
                else if (txtZona.SelectedItem.Text != "--Todas--" && txtPrecio.Text == "")
                {
                    var filtro = from p in vp
                                 where p.Zona.Nombre == txtZona.SelectedItem.Text
                                 select p;
                    repetidor.DataSource = filtro;
                    repetidor.DataBind();
                }
                else if (txtZona.SelectedItem.Text != "--Todas--" && txtPrecio.Text != "")
                {
                    var filtro = from p in vp
                                 where p.Zona.Nombre == txtZona.SelectedItem.Text
                                 where p.Precio == numero
                                 select p;
                    repetidor.DataSource = filtro;
                    repetidor.DataBind();
                }
                else
                {
                    var filtro = from p in vp
                                 select p;
                    repetidor.DataSource = filtro;
                    repetidor.DataBind();
                }

            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }



        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Hacer_Busqueda();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                txtTipo.SelectedValue = "todo";
                txtAccion.SelectedValue = "todo";
                txtPrecio.Text = "";
                txtZona.Items.Clear();
                List<Zona> variasZonas = Fabrica_Logica.getLogica_Zona.Listado_Activos();
                txtZona.Items.Add("--Todas--");
                foreach (Zona unaZona in variasZonas)
                {
                    txtZona.Items.Add(unaZona.Nombre);
                }
                Hacer_Busqueda();
            }
            catch
            {
                Response.Write("<h5 class='red darken-2 white-text card-panel hoverable'>Error de servidor, su peticion fue rechazada</h5>");
            }
        }
    }
}