<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="HacerConsulta.aspx.cs" Inherits="BiosRealState.HacerConsulta" %>
<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">
    <main runat="server" id="Principal">
        <h1>Realizar consulta</h1>
        <p>Perfecto! Ahora solo debe introducir sus datos personales para reservar una consulta, y todo quedara listo.
            ¡Muchas gracias por elegirnos!
        </p>
        <nav>

        <asp:Repeater ID="repetidor" runat="server">
            <HeaderTemplate>
                <h3>Reservas hechas en esta propiedad:</h3>
            </HeaderTemplate>
            <ItemTemplate>
                <ul >
                    <li>Fecha: <%#Eval("Fecha")%></li>
                    <li>Hora: <%#Eval("Hora")%></li>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
    </nav>


        <section>
            <div>
                <label for="txtTelefono" runat="server">Telefono de contacto:</label>
                <asp:TextBox runat="server" ID="txtTelefono" Text=""></asp:TextBox>
            </div>
            <div>
                <label for="txtNombre" runat="server">Nombre Completo:</label>
                <asp:TextBox runat="server" ID="txtNombre" Text=""></asp:TextBox>
            </div>
            <div>
                <label for="txtFecha" runat="server">Fecha deseada:</label>
                <asp:TextBox runat="server" ID="txtFecha"></asp:TextBox>
            </div>
            <div>
                <label for="txtHora" runat="server">Hora deseada:</label>
                <asp:TextBox runat="server" ID="txtHora"></asp:TextBox>
            </div>
        </section>

        <section runat="server" id="LugarConfirmacion">
            <p>Para confirmar, presione el boton:</p>
            <asp:Button runat="server" Text="Confirmar Consulta" id="btnConfirmar" OnClick="btnConfirmar_Click" />
            <Maxi:Resultado runat="server" ID="MxResultado" Visible="false" Titulo="Error en la consulta" Dialogo="Lamentablemente no se agrego la consulta, las causas fueron:" />
        </section>

    </main>
</asp:Content>
