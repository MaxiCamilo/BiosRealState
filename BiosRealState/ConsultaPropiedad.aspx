<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="ConsultaPropiedad.aspx.cs" Inherits="BiosRealState.ConsultaPropiedad" %>

<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>
<%@ Register TagPrefix="Maxi" TagName="Tabla" Src="~/Controles/Listado_Entidades.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">
    <div class="container">
        <section class="row">
            <h3 class="titulo">Consultar Propiedades</h3>
            <p>
                Desea ver la propiedades disponibles? Puede ver cada propiedad en el siguiente listado, recuerde que puede realizar una busqueda de una propiedad, con los datos especificos que contenga.
            Si le interesa una propiedad, presione el boton que se encuentra a la derecha del texto descriptivo.
            </p>

            <section class="center">
                <div>
                    <asp:Button runat="server" ID="btnMostrarCuadro" Text="Busqueda Avanzada" OnClick="btnMostrarCuadro_Click" class="waves-effect waves-light btn pulse" />
                </div>

                <div runat="server" id="divBusqueda" visible="false" class="container">
                    <h4>Busqueda Avanzada</h4>
                    <div class="row">
                        <div class="col s12 m6">
                            <label runat="server" for="txtAccion">Tipo de venta:</label>
                            <asp:DropDownList ID="txtAccion" runat="server" CssClass="Mostrar">
                                <asp:ListItem Value="todo">--Todas--</asp:ListItem>
                                <asp:ListItem Value="alquiler">alquiler</asp:ListItem>
                                <asp:ListItem Value="venta">venta</asp:ListItem>
                                <asp:ListItem Value="permuta">permuta</asp:ListItem>
                            </asp:DropDownList>

                        </div>




                        <div class="col s12 m6">
                            <label for="txtTipo" runat="server">Tipo de propiedad:</label>
                            <asp:DropDownList ID="txtTipo" runat="server" CssClass="Mostrar">
                                <asp:ListItem Value="todo">--Todas--</asp:ListItem>
                                <asp:ListItem Value="casa">Casa</asp:ListItem>
                                <asp:ListItem Value="apartamento">Apartamento</asp:ListItem>
                                <asp:ListItem Value="local">Local</asp:ListItem>
                            </asp:DropDownList>
                        </div>


                        <div class="col s12">
                            <label for="txtZona" runat="server" >Zona donde se encuentra:</label>
                            <asp:DropDownList ID="txtZona" runat="server" CssClass="Mostrar"></asp:DropDownList>
                        </div>

                        <div class="input-field col s12">
                            <i class="material-icons prefix">attach_money</i>
                            <label for="txtPrecio" runat="server">Precio:</label>
                            <asp:TextBox runat="server" ID="txtPrecio"></asp:TextBox>
                        </div>

                        <div>
                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" CssClass="waves-effect waves-light btn light-green darken-1" />
                            <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar" OnClick="btnLimpiar_Click" CssClass="waves-effect waves-light btn orange darken-1"/>
                        </div>

                    </div>
                </div>



            </section>
        </section>
    </div>

    <ul class="collection">
        <asp:Repeater ID="repetidor" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>

                <li class="collection-item avatar">
                    <img src="Imagenes/sale-tag-icon.png" alt="" class="circle">
                    <span class="title">Direccion: <%#Eval("Direccion")%></span>
                    <p>Padron: <%#Eval("Padron")%></p>
                    <p>Zona: <%#Eval("Zona")%></p>
                    <p>Tipo de contrato: <%#Eval("Accion")%></p>
                    <p>$ <%#Eval("Precio")%></p>
                    <a href='/HacerConsulta.aspx?padron=<%#Eval("Padron")%>' class="secondary-content" title="Elegir Propiedad"><i class="material-icons medium ">message</i></a>
                </li>

            </ItemTemplate>
        </asp:Repeater>
    </ul>


</asp:Content>
