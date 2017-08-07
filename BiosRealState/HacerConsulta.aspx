<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="HacerConsulta.aspx.cs" Inherits="BiosRealState.HacerConsulta" %>

<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">
    <main runat="server" id="Principal">
        <h3 class=" light-blue-text text-darken-1 center">Realizar consulta</h3>
        <p class="center">
            Perfecto! Ahora solo debe introducir sus datos personales para reservar una consulta, y todo quedara listo.
            ¡Muchas gracias por elegirnos!
        </p>
        <div class="container">
            <asp:Repeater ID="repetidor" runat="server">
                <HeaderTemplate>
                    <h3 class="deep-purple-text text-lighten-2">Reservas hechas en esta propiedad:</h3>
                </HeaderTemplate>
                <ItemTemplate>
                    <ul class="collection with-header">
                        <li class="collection-header">Fecha: <%#Eval("Fecha")%></li>
                        <li class="collection-item">Hora: <%#Eval("Hora")%></li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
            <hr />
        </div>
        <div class="container">
            <section class="row">
                <h5 class="blue-text text-darken-2">Datos de la propiedad</h5>
                <div class="input-field col s12 m6">
                    <label for="Padron">Padron </label>
                    <input runat="server" type="text" id="Padron" readonly />
                </div>

                <div class="input-field col s12 m6">
                    <label for="Direccion">Direccion </label>
                    <input runat="server" type="text" id="Direccion" readonly />
                </div>

                <div class="input-field col s12 m6">
                    <label for="Accion">Tipo de contrato </label>
                    <input runat="server" type="text" id="Accion" readonly />
                </div>
                <div class="input-field col s12 m6">
                    <label for="Precio">Precio </label>
                    <input runat="server" type="text" id="Precio" readonly />
                </div>
                <div class="input-field col s12 m4">
                    <label for="Banios">Cantidad de Baños </label>
                    <input runat="server" type="text" id="Banios" readonly />
                </div>

                <div class="input-field col s12 m4">
                    <label for="Habitaciones">Cantidad de Habitaciones </label>
                    <input runat="server" type="text" id="Habitaciones" readonly />
                </div>

                <div class="input-field col s12 m4">
                    <label for="Metros">Metros Cuadrados </label>
                    <input runat="server" type="text" id="Metros" readonly />
                </div>




            </section>
        </div>

        <hr />

        <div class="container">
            <section class="row">

                <h5 class="light-green-text text-darken-2">Datos de la zona</h5>

                <div class="input-field col s12 m12">
                    <label for="ZonaNombre">Nombre </label>
                    <input runat="server" type="text" id="ZonaNombre" readonly />
                </div>

                <div class="input-field col s12 m3">
                    <label for="ZonaDepartamento">Letra Departamento</label>
                    <input runat="server" type="text" id="ZonaDepartamento" readonly />
                </div>

                <div class="input-field col s12 m3">
                    <label for="ZonaId">Código Identificador </label>
                    <input runat="server" type="text" id="ZonaId" readonly />
                </div>

                <div class="input-field col s12 m6">
                    <label for="ZonaHabitantes">Cantidad de habitantes </label>
                    <input runat="server" type="text" id="ZonaHabitantes" readonly />
                </div>


                <hr />

                <asp:Repeater ID="Servicios" runat="server">
                    <HeaderTemplate>
                        <h5 class="lime-text text-lighten-1">Servicios en la zona</h5>
                        <p>Puede interesarle los servicios que estaran cerca de su propiedad.</p>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <ul class="collection">
                            <li class="collection-item"><%#Container.DataItem.ToString()%></li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>

            </section>
        </div>

        <hr />

        '
        <div class="container">
            <section class="row">
                <h2 class="orange-text text-lighten-2 center">Reservar una consulta</h2>
                <p>Para hacer una reserva, solo necesitamos sus datos, llenelos en el siguiente formulario:</p>
                <div class="input-field col s12 m12">
                    <i class="material-icons prefix">phone</i>
                    <label for="txtTelefono" runat="server">Telefono de contacto:</label>
                    <asp:TextBox runat="server" ID="txtTelefono" Text="" CssClass="validate"></asp:TextBox>
                </div>
                <div class="input-field col s12 m12">
                    <i class="material-icons prefix">person_pin</i>
                    <label for="txtNombre" runat="server">Nombre Completo:</label>
                    <asp:TextBox runat="server" ID="txtNombre" Text="" CssClass="validate"></asp:TextBox>
                </div>
                <div class="col s12 m6">
                    <i class="material-icons prefix">today</i>
                    <label for="txtFecha" runat="server">Fecha deseada:</label>
                    <asp:TextBox runat="server" ID="txtFecha" CssClass="validate"></asp:TextBox>
                </div>
                <div class="input-field col s12 m6">
                    <i class="material-icons prefix">timer</i>
                    <label for="txtHora" runat="server">Hora deseada:</label>
                    <asp:TextBox runat="server" ID="txtHora" CssClass="validate"></asp:TextBox>
                </div>
            </section>
        </div>

        <section runat="server" id="LugarConfirmacion" class="container row">
            <p class="col s12 m6 right-align">Para confirmar, presione el boton:</p>
            <asp:Button runat="server" Text="Confirmar Consulta" ID="btnConfirmar" OnClick="btnConfirmar_Click" CssClass="col s12 m3 waves-effect waves-light btn" />
            <Maxi:Resultado runat="server" ID="MxResultado" Visible="false" Titulo="Error en la consulta" Dialogo="Lamentablemente no se agrego la consulta, las causas fueron:" />
        </section>
    </main>
</asp:Content>
