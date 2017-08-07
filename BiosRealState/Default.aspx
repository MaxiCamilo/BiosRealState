<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BiosRealState.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">
    <h3 id="TituloBienvenida" class="center-align">Bios Real State, la forma más cómoda de consultar propiedades.
    </h3>
    <div class="container">
        <section class="row center">
            <article class="center-align">
                <h5 class="blue-text text-darken-1">¿Que es Bios Real State?</h5>
                <p class="center-align">Es una plataforma online donde podrá buscar y informarse sobre las propiedades disponibles.</p>
                <br />
                <h5 class="blue-text text-darken-2">¿Que tipo de información  hay?</h5>
                <p>
                    Puede ver la zona donde se encuentra la propiedad y que servicios tiene disponible, además de información básica como la cantidad de cuartos, baños, tamaño de la propided,
                precio y que contrató se puede hacer para conseguir la propiedad (si es un alquiler, venta o si es una permuta)
                </p>
                <br />
                <h5 class="blue-text text-darken-3">¿Que tipo de propiedades ofrecen?</h5>
                <p class="center-align">Ofrecemos casas, apartamento y locales comerciales</p>
                <br />
                <h5 class="blue-text text-darken-4">Estoy interesado en una propiedad ¿Que debo hacer para consultar la propiedad?</h5>
                <p>
                    Es muy fácil, solo debe ir al botón “Consultar Propiedad” que se encuentra en la parte de arriba de la página.
                Después tendrá que elegir la propiedad y presionar el botón “Listar Consulta”. Solo tendrá que incluir su teléfono,
                nombre, fecha y hora de la consulta.
                </p>
                <br />
            </article>
        </section>
    </div>
    <style>
        body{
            background: url(/Imagenes/Gato.jpg);
        }
    </style>
</asp:Content>
