<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="Logueo.aspx.cs" Inherits="BiosRealState.Logueo" %>
<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">

    <section>
        <h1 class="Titulo">Inicio de sesion</h1>
        <p><strong>Solo para empleados</strong>, tenga acceso a la plataforma con su nombre y contraseña</p>
    </section>
    <section>
        <asp:TextBox runat="server" ID="txtNombre" Text=""></asp:TextBox>
        <br />
        <asp:TextBox runat="server" ID="txtContrasenia" Text="" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button runat="server" Text="Iniciar Sesion" ID="btnIniciarSesion" OnClick="btnIniciarSesion_Click"/>
        <br />
    </section>



    <Maxi:Resultado runat="server" ID="MxResultado" Titulo="Acceso denegado" Dialogo="Su nombre de usuario o contraseña son incorrectos." Visible="false"/>
</asp:Content>
