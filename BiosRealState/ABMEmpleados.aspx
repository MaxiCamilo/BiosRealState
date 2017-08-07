<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="ABMEmpleados.aspx.cs" Inherits="BiosRealState.ABMEmpleados" %>

<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>
<%@ Register TagPrefix="Maxi" TagName="Tabla" Src="~/Controles/Listado_Entidades.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">
    <section>
        <h3 class="center indigo-text text-darken-4">Mi perfil de usuario</h3>
        <p class="center">En esta seccion podras agregar nuevos empleados, modificar tu contraseña o darte de baja.</p>
    </section>
    <!-- Alta  !-->
    <ul class="collapsible" data-collapsible="accordion">
        <li>
            <div class="collapsible-header"><i class="material-icons">assignment_ind</i>Agregar un Nuevo Usuario</div>
            <div class="collapsible-body container">
                <h4>Nuevo Empleado</h4>
                <p>Para crear un usuario, se debe especificar su nombre de usuario (unico) y contraseña de solo 10 digitos</p>
                <section>
                    <div class="input-field">
                        <label for="txtAltaNombre" runat="server">Nombre el usuario:</label>
                        <asp:TextBox runat="server" ID="txtAltaNombre" Text=""></asp:TextBox>
                    </div>
                    <div class="input-field">
                        <label for="txtAltaContrasenia" runat="server">Contraseña:</label>
                        <asp:TextBox runat="server" ID="txtAltaContrasenia" Text=""></asp:TextBox>
                    </div>
                    <div class="input-field">
                        <label for="txtAltaConfirmacion" runat="server">Vuelva a escribir la Contraseña:</label>
                        <asp:TextBox runat="server" ID="txtAltaConfirmacion" Text=""></asp:TextBox>
                    </div>

                </section>


                <section>
                    <div>
                        <h5>Si desea confirmar los datos, precione el boton: </h5>
                        <asp:Button runat="server" ID="btnAltaEmpleado" Text="Agregar Usuario" OnClick="btnAltaEmpleado_Click" CssClass="s12 waves-effect m3  waves-light btn light-green accent-2" />
                        <h5 runat="server" id="AltaPositivo" visible="false" class="light-green lighten-2 white-text card-panel hoverable">Se agrego el usuario Correctamente, para poder ingresar al nuevo usuario, solo tiene que iniciar sesion</h5>
                    </div>
                </section>
                <section>
                    <Maxi:Resultado runat="server" ID="MxResultadoAlta" Titulo="No se agrego el usuario" Dialogo="No se pudo agregar el usuario porque hay datos que no son correctos, a continuacion, se le especificara el porque son invalidos:" Visible="false" />
                </section>
            </div>
        </li>
        <li>
            <div class="collapsible-header"><i class="material-icons">lock</i>Modificar Contraseña</div>
            <!-- Modificar Contraseña !-->
            <div class="collapsible-body container">
                <h4>Modificar Contraseña</h4>
                <p>Para modificar su clave de acceso, solo tiene que escibir su contraseña actual, luego su nueva contraseña, 2 veces</p>
                <div class="input-field">
                    <label for="txtModificarVieja" runat="server">Contraseña Actual:</label>
                    <asp:TextBox runat="server" ID="txtModificarVieja" Text=""></asp:TextBox>
                </div>
                <div class="input-field">
                    <label for="txtModificarNueva" runat="server">Contraseña Nueva:</label>
                    <asp:TextBox runat="server" ID="txtModificarNueva" Text=""></asp:TextBox>
                </div>
                <div class="input-field">
                    <label for="txtModificarConfirmacion" runat="server">Vuelva a escribir su Contraseña nueva:</label>
                    <asp:TextBox runat="server" ID="txtModificarConfirmacion" Text=""></asp:TextBox>
                </div>
                <div>
                    <h5>Si desea confirmar los datos, precione el boton: </h5>
                    <asp:Button runat="server" ID="btnModificarClave" Text="Modificar Clave" OnClick="btnModificarClave_Click" CssClass="s12 waves-effect m3  waves-light btn light-green accent-2" />
                    <h5 runat="server" id="ModificarPositivo" visible="false" class="light-green lighten-2 white-text card-panel hoverable">Operacion de cambio de contraseña hecha</h5>
                </div>
                <div>
                    <Maxi:Resultado runat="server" ID="MxResultadoClave" Titulo="No se modifico la clave" Dialogo="Hay datos que no son correctos:" Visible="false" />
                </div>

            </div>
        </li>
        <li>
            <!-- Dar de baja !-->
            <div class="collapsible-header"><i class="material-icons">delete_forever</i>Eliminar mi cuenta</div>
            <div class="collapsible-body container">
                <h4>Eliminar Cuenta</h4>
                <p>Si desea darse de baja, solo debe poner su contraseña actual</p>
                <p>CUIDADO! Esta accion no puede ser desecha luego de la confirmacion.</p>
                <div class="input-field">
                    <label for="txtBajaContrasenia" runat="server">Contraseña Actual</label>
                    <asp:TextBox runat="server" ID="txtBajaContrasenia" Text=""></asp:TextBox>
                </div>
                <div class="input-field">
                    <label for="txtBajaConfirmacion" runat="server">Vuelva a escribir su Contraseña:</label>
                    <asp:TextBox runat="server" ID="txtBajaConfirmacion" Text=""></asp:TextBox>
                </div>
                <div>
                    <h5>Si desea confirmar los datos, precione el boton: </h5>
                    <asp:Button runat="server" ID="btnDarDeBaja" Text="Dar de baja" OnClick="btnDarDeBaja_Click" CssClass="s12 waves-effect m3  waves-light btn orange darken-2" />
                </div>
                <div>
                    <Maxi:Resultado runat="server" ID="MxResultadoBaja" Titulo="No se pudo eliminar" Dialogo="Por algun motivo, no se pudo dar de baja:" Visible="false" />
                </div>
            </div>
        </li>
    </ul>



</asp:Content>
