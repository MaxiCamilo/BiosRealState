<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="ConsultaPropiedad.aspx.cs" Inherits="BiosRealState.ConsultaPropiedad" %>

<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>
<%@ Register TagPrefix="Maxi" TagName="Tabla" Src="~/Controles/Listado_Entidades.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">
    <section>
        <h1 class="titulo">Consultar Propiedades</h1>
        <p>Desea ver la propiedades disponibles? Puede ver cada propiedad en el siguiente listado, recuerde que puede realizar una busqueda de una propiedad, con los datos especificos que contenga.</p>

        <section>
            <div>
                <asp:Button runat="server" ID="btnMostrarCuadro" Text="Busqueda Avanzada" OnClick="btnMostrarCuadro_Click" />
            </div>

            <div runat="server" id="divBusqueda" visible="false">
                <h3>Busqueda Avanzada</h3>
                <div>
                    <label for="txtAccion" runat="server">Tipo de venta:</label>
                    <asp:DropDownList ID="txtAccion" runat="server">
                        <asp:ListItem Value="todo">--Todas--</asp:ListItem>
                        <asp:ListItem Value="alquiler">alquiler</asp:ListItem>
                        <asp:ListItem Value="venta">venta</asp:ListItem>
                        <asp:ListItem Value="permuta">permuta</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div>
                    <label for="txtTipo" runat="server">Tipo de propiedad:</label>
                    <asp:DropDownList ID="txtTipo" runat="server">
                        <asp:ListItem Value="todo">--Todas--</asp:ListItem>
                        <asp:ListItem Value="casa">Casa</asp:ListItem>
                        <asp:ListItem Value="apartamento">Apartamento</asp:ListItem>
                        <asp:ListItem Value="local">Local</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div>
                    <label for="txtZona" runat="server">Zona donde se encuentra:</label>
                    <asp:DropDownList ID="txtZona" runat="server"></asp:DropDownList>
                </div>

                <div>
                    <label for="txtPrecio" runat="server">Precio:</label>
                    <asp:TextBox runat="server" ID="txtPrecio"></asp:TextBox>
                </div>

                <div>
                    <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click"/>
                    <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar" OnClick="btnLimpiar_Click"/>
                </div>

            </div>




        </section>
    </section>


    <nav>
        <asp:Repeater ID="repetidor" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <ul >
                    <li>Padron: <%#Eval("Padron")%></li>
                    <li>Direccion: <%#Eval("Direccion")%></li>
                    <li>Zona: <%#Eval("Zona")%></li>
                    <li>Tipo de contrato: <%#Eval("Accion")%></li>
                    <li>$ <%#Eval("Precio")%></li>
                    <input type="button" onclick="location.href = '/HacerConsulta.aspx?padron=<%#Eval("Padron")%>';" value="Hacer Consulta"/>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
    </nav>


</asp:Content>
