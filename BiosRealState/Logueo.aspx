<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="Logueo.aspx.cs" Inherits="BiosRealState.Logueo" %>

<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">

    <section>
        <h3 class="center-align">Inicio de sesion</h3>
        <p class="center-align"><strong class="blue-grey-text">Solo para empleados.</strong> Tenga acceso a la plataforma con su nombre y contraseña</p>
    </section>
    <div class="container">
        <section class="row center">
            <div class="row">
                <div class="input-field col s12">
                    <i class="material-icons prefix">person_pin</i>
                    <label for="txtNombre">Usuario:</label>
                    <asp:TextBox runat="server" CssClass="validate" ID="txtNombre" Text=""></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="input-field col s12">
                    <i class="material-icons prefix">vpn_key</i>
                    <label for="txtNombre">Contraseña:</label>
                    <asp:TextBox runat="server" CssClass="validate" ID="txtContrasenia" Text="" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <asp:Button runat="server" Text="Iniciar Sesion" ID="btnIniciarSesion" CssClass="waves-effect waves-light btn light-green accent-2" OnClick="btnIniciarSesion_Click" />
            <br />
        </section>
    </div>



    <Maxi:Resultado runat="server" ID="MxResultado" Titulo="Acceso denegado" Dialogo="Su nombre de usuario o contraseña son incorrectos." Visible="false" />
</asp:Content>
