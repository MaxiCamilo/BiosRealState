using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades.Realidad;
using Entidades.Interfaces;

namespace BiosRealState.Controles
{
    public partial class Listado_Entidades : System.Web.UI.UserControl
    {
        public delegate void Funcion_Retorno(object sender, Evento_Entidad entidad);

        private dynamic _Items;
        private TableRow _PrimeraFila = new TableRow();
        private bool _Habilitar_Modificar = false;
        private bool _Habilitar_Eliminar = false;
        private bool _Habilitar_Seleccion = false;
        private string _Dialogo_Vacio = "No hay nada que mostrar";

        public event Funcion_Retorno Eliminar;
        public event Funcion_Retorno Modificar;
        public event Funcion_Retorno Seleccionado;

        public dynamic Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
                Asignar_Objetos();
            }
        } 
        public bool Habilitar_Modificar
        {
            get { return _Habilitar_Modificar; }
            set { _Habilitar_Modificar = value; }
        }
        public bool Habilitar_Eliminar
        {
            get { return _Habilitar_Eliminar; }
            set { _Habilitar_Eliminar = value; }
        }

        public bool Habilitar_Seleccion
        {
            get { return _Habilitar_Seleccion; }
            set { _Habilitar_Seleccion = value; }
        }

        public string Dialogo_Vacio
        {
            get { return _Dialogo_Vacio; }
            set { _Dialogo_Vacio = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.InnerHtml = _Dialogo_Vacio;
        }
        public void Asignar_Objetos()
        {
            //Primero agregamos los nombres
            Tabla.Rows.Clear();
            _PrimeraFila.Cells.Clear();
            //Verifica que alla por lo menos algun objeto
            if(_Items.Count == 0)
            {
                lblError.Visible = true;
                Tabla.Visible = false;
                return;
            }
            lblError.Visible = false;
            Tabla.Visible = true;
            if (_Habilitar_Seleccion)
            {
                _PrimeraFila.Cells.Add(new TableCell());
            } 
            //Agregamos los nombres de las propiedades en la primera fila
            foreach (KeyValuePair<string, object> par in _Items[0].Parametros())
            {
                string retorno = par.Key;
                TableCell Celda = new TableCell();
                Celda.Text = retorno;

                _PrimeraFila.Cells.Add(Celda);
            }
            //Decimos que la primera fila es un thead
            _PrimeraFila.TableSection = TableRowSection.TableHeader;
            Tabla.Rows.Add(_PrimeraFila);
            //Ahora asignamos los valores
            int iterador = 0;
            foreach (Entidad seleccionado in _Items)
            {
                //Creamos una fila
                TableRow Fila = new TableRow();
                if (_Habilitar_Seleccion)
                {
                    //Creamos boton de seleccionado
                    Button botonSeleccionar = new Button();
                    botonSeleccionar.Text = "Seleccionar";
                    botonSeleccionar.Attributes.Add("title","Seleccionar");
                    botonSeleccionar.CssClass = "btn waves-effect waves-light light-blue darken-1";
                    botonSeleccionar.ID = "Seleccionar" + iterador;
                    TableCell CeldaSeleccionar = new TableCell();
                    CeldaSeleccionar.Controls.Add(botonSeleccionar);
                    Fila.Cells.Add(CeldaSeleccionar);
                    //Asignamos el evento
                    botonSeleccionar.Click += (object sender, EventArgs e) =>
                    {
                        Seleccionado(sender, new Evento_Entidad() { Envio = seleccionado });
                    };
                }                

                foreach (KeyValuePair<string, object> par in seleccionado.Parametros())
                {
                    //Asignamos los valores
                    TableCell Celda = new TableCell();
                    //Vemos si es un booleano
                    if(par.Value is Boolean)
                    {
                        if ((bool)par.Value)
                        {
                            Celda.Text = "Si";
                        }
                        else
                        {
                            Celda.Text = "No";
                        }
                    }
                    else
                    {
                        Celda.Text = par.Value.ToString();
                    }
                    
                    Fila.Cells.Add(Celda);
                }
                if(_Habilitar_Eliminar || _Habilitar_Seleccion)
                {
                    TableCell CeldaSeleccionar = new TableCell();
                    //Creamos boton eliminar
                    if (_Habilitar_Eliminar)
                    {
                        Button botonSeleccionar = new Button();
                        botonSeleccionar.Text = "Eliminar";
                        botonSeleccionar.ID = "Eliminar" + iterador;
                        botonSeleccionar.CssClass = "btn waves-effect waves-light red darken-1";
                        CeldaSeleccionar.Controls.Add(botonSeleccionar);
                        botonSeleccionar.Click += (object sender, EventArgs e) =>
                        {
                            Eliminar(sender, new Evento_Entidad() { Envio = seleccionado });
                        };
                    }
                    
                    //Creamos boton modificar
                    if (_Habilitar_Modificar)
                    {
                        Button botonSeleccionar = new Button();
                        botonSeleccionar.Text = "Modificar";
                        botonSeleccionar.ID = "Modificar" + iterador;
                        botonSeleccionar.CssClass = "btn waves-effect waves-light light-green darken-1";
                        CeldaSeleccionar.Controls.Add(botonSeleccionar);
                        botonSeleccionar.Click += (object sender, EventArgs e) =>
                        {
                            Modificar(sender, new Evento_Entidad() { Envio = seleccionado });
                        };
                    }
                    Fila.Cells.Add(CeldaSeleccionar);
                    
                }
                iterador++;
                Tabla.Rows.Add(Fila);
            }
        }
        

        


    }
}