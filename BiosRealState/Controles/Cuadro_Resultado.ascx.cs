using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiosRealState.Controles
{
    public partial class Cuadro_Resultado : System.Web.UI.UserControl
    {
        /// <summary>
        /// Aqui se almacena los resultado
        /// </summary>
        private List<string> _Resultado = new List<string>(); 
        private string _Dialogo = "No se pudo realizar su peticion, las causas fueron:";
        private string _Clase_Texto = "";
        private string _Titulo = "";
        private string _MensajeHTML = "";

        /// <summary>
        /// Lista de los resultados que se desea mostrar, convierte los datos en un listado (<li>)
        /// </summary>
        public List<string> Resultado
        {
            get { return _Resultado; }
            set { _Resultado = value;
                if (Resultado.Count != 0)
                {
                    string Mensajes = "<nav><ul>";

                    foreach (string dato in Resultado)
                    {
                        Mensajes += "<li>" + dato + "</li>";
                    }
                    Mensajes += "</ul></nav>";
                    _MensajeHTML = Mensajes;
                }
                else
                {
                    _MensajeHTML = "";
                }
                htmlMensaje.InnerHtml = _MensajeHTML;

            }
        }

        /// <summary>
        /// Texto descriptivo que se quiere comunicar al cliente
        /// </summary>
        public string Dialogo
        {
            get { return _Dialogo; }
            set { _Dialogo = value; }
        }
        /// <summary>
        /// Que clase desea asignar a los textos
        /// </summary>
        public string Clase_Texto
        {
            get { return _Clase_Texto; }
            set { _Clase_Texto = value; }
        }
        /// <summary>
        /// Texto de gran tamaño y corto que muestra el resultado
        /// </summary>
        public string Titulo
        {
            get { return _Titulo; }
            set { _Titulo = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            htmlTitulo.InnerHtml = Titulo;
            htmlDialogo.InnerHtml = Dialogo;

            
            
        }
    }
}