<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="ABMApartamentos.aspx.cs" Inherits="BiosRealState.ABMApartamentos" %>

<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>
<%@ Register TagPrefix="Maxi" TagName="Tabla" Src="~/Controles/Listado_Entidades.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">
    <section>
        <h3 class="center light-blue-text text-darken-1">Administracion de Apartamentos</h3>
        <p class="center">Los apartamentos son un tipo de propiedad que tiene asignado un piso, y se especifica si tiene ascensor</p>
    </section>
    <!-- Alta  !-->
    <ul class="collapsible" data-collapsible="accordion">
        <li>
            <div class="collapsible-header "><i class="material-icons">add_box</i>Agregar un nuevo Apartamento</div>
            <section class="collapsible-body container ">
                <h4>Agregar una nuevo apartamento</h4>
                <section class="row">
                    <h5>Paso 1: Identificadores</h5>
                    <p>Para poder identificar un apartamento en la base de datos, se debe introducir su padron (números) y su zona correspondiente</p>

                    <div class="input-field col s12">
                        <i class="material-icons prefix">local_mall</i>
                        <label for="txtAltaPadron" runat="server">Padron:</label>
                        <asp:TextBox runat="server" ID="txtAltaPadron" Text=""></asp:TextBox>
                    </div>
                    <div class="col s12">
                        <h4>Propiedad donde se va a encontrar:</h4>
                        <Maxi:Tabla ID="MxZonas_Alta" runat="server" Habilitar_Seleccion="true" OnSeleccionado="MxZonas_Alta_Seleccionado" Dialogo_Vacio="No existen zonas en la base de datos" />
                    </div>
                </section>
                <section class="row">
                    <h5>Paso 2: Informacion Basica de una propiedad:</h5>
                    <p>Se necesitan saber algunos datos basicos y legales de la propiedad.</p>
                    <div class="input-field col s12 m12">
                        <i class="material-icons prefix">pin_drop</i>
                        <label for="txtAltaDireccion" runat="server">Direccion:</label>
                        <asp:TextBox runat="server" ID="txtAltaDireccion" Text=""></asp:TextBox>
                    </div>
                    <div class="input-field col s12 m4">
                        <i class="material-icons prefix">opacity</i>
                        <label for="txtAltaCantidad_banio" runat="server">Cantidad de Baños:</label>
                        <asp:TextBox runat="server" ID="txtAltaCantidad_banio" Text=""></asp:TextBox>
                    </div>
                    <div class="input-field col s12 m4">
                        <i class="material-icons prefix">hotel</i>
                        <label for="txtAltacantidad_habitaciones" runat="server">Cantidad Habitaciones:</label>
                        <asp:TextBox runat="server" ID="txtAltacantidad_habitaciones" Text=""></asp:TextBox>
                    </div>
                    <div class="input-field col s12 m4">
                        <i class="material-icons prefix">crop_free</i>
                        <label for="txtAltaMetros_cuadrados" runat="server">Metros Cuadrados:</label>
                        <asp:TextBox runat="server" ID="txtAltaMetros_cuadrados" Text=""></asp:TextBox>
                    </div>
                    <div class="input-field col s12 m6">
                        <i class="material-icons prefix">monetization_on</i>
                        <label for="txtAltaPrecio" runat="server">Precio:</label>
                        <asp:TextBox runat="server" ID="txtAltaPrecio" Text=""></asp:TextBox>
                    </div>
                    <div class="col s12 m6">
                        <label for="txtAltaAccion" runat="server">Accion:</label>
                        <asp:DropDownList ID="txtAltaAccion" runat="server" CssClass="Mostrar">
                            <asp:ListItem Value="alquiler">Alquiler</asp:ListItem>
                            <asp:ListItem Value="venta">Venta</asp:ListItem>
                            <asp:ListItem Value="permuta">Permuta</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </section>

                <section class="row">
                    <h5>Paso 3: Datos adicionales de un apartamento</h5>
                    <div class="col input-field s12 m6">
                        <i class="material-icons prefix">business</i>
                        <label for="txtAltaPiso" runat="server">Piso:</label>
                        <asp:TextBox runat="server" ID="txtAltaPiso" Text=""></asp:TextBox>
                    </div>
                    <div class="col s12 m6">
                        <i class="material-icons prefix">call_merge</i>
                        <label for="txtAltaAscensor" runat="server">Tiene Ascensor?:</label>
                        <asp:CheckBox ID="txtAltaAscensor" runat="server" CssClass="Mostrar" />
                    </div>
                </section>

                <section class="container">
                    <div class="row">
                        <p class="col s12 m7 right-align">Si desea confirmar los datos, precione el boton: </p>
                        <asp:Button runat="server" ID="btnAltaApartamento" Text="Agregar Apartamento" OnClick="btnAltaApartamento_Click" CssClass="col s12 m5 waves-effect waves-light btn light-green" />

                    </div>
                    <h5 runat="server" id="AltaPositivo" visible="false" class="light-green lighten-2 white-text card-panel hoverable">Se agrego el apartamento Correctamente</h5>
                </section>
                <section class="row">
                    <Maxi:Resultado runat="server" ID="MxResultadoAlta" Titulo="No se agrego el apartamento" Dialogo="No se pudo agregar el apartamento porque hay datos que no son correctos, a continuacion, se le especificara el porque son invalidos:" Visible="false" />
                </section>
            </section>
        </li>
        <li>
            <div class="collapsible-header active"><i class="material-icons">edit</i>Listado y alteración de apartamentos</div>
            <!-- Modificar y eliminar !-->
            <div class="collapsible-body active">
                <h3>Listado de los apartamentos</h3>
                <p>Puede ver todos los detalles de los apartamento, ademas podra modificar y eliminar</p>
                <Maxi:Tabla ID="mxTabla_Listado" runat="server" Habilitar_Seleccion="false" Habilitar_Eliminar="true"
                    Habilitar_Modificar="true" Dialogo_Vacio="No existen apartamentos en la base de datos" OnModificar="mxTabla_Listado_Modificar" OnEliminar="mxTabla_Listado_Eliminar" />

                <section runat="server" id="Panel_Modificar" visible="false" class="container">
                    <h4>Modificar Apartamento</h4>
                    <section class="row">
                        <p>Se necesitan saber algunos datos basicos y legales de la propiedad.</p>
                        <div class="input-field col s12">
                            <i class="material-icons prefix">pin_drop</i>
                            <label for="txtModificarDireccion" runat="server">Direccion:</label>
                            <asp:TextBox runat="server" ID="txtModificarDireccion" Text=""></asp:TextBox>
                        </div>

                        <div class="input-field col s12 m4">
                            <i class="material-icons prefix">opacity</i>
                            <label for="txtModificarCantidad_banio" runat="server">Cantidad de Baños:</label>
                            <asp:TextBox runat="server" ID="txtModificarCantidad_banio" Text=""></asp:TextBox>
                        </div>
                        <div class="input-field col s12 m4">
                            <i class="material-icons prefix">hotel</i>
                            <label for="txtModificarcantidad_habitaciones" runat="server">Cantidad Habitaciones:</label>
                            <asp:TextBox runat="server" ID="txtModificarcantidad_habitaciones" Text=""></asp:TextBox>
                        </div>
                        <div class="input-field col s12 m4">
                            <i class="material-icons prefix">crop_free</i>
                            <label for="txtModificarMetros_cuadrados" runat="server">Metros Cuadrados:</label>
                            <asp:TextBox runat="server" ID="txtModificarMetros_cuadrados" Text=""></asp:TextBox>
                        </div>
                        <div class="input-field col s12 m6">
                            <i class="material-icons prefix">monetization_on</i>
                            <label for="txtModificarPrecio" runat="server">Precio:</label>
                            <asp:TextBox runat="server" ID="txtModificarPrecio" Text=""></asp:TextBox>
                        </div>
                        <div class="col s12 m6">
                            <label for="txtModificarAccion" runat="server">Accion:</label>
                            <asp:DropDownList ID="txtModificarAccion" runat="server" CssClass="Mostrar">
                                <asp:ListItem Value="alquiler">Alquiler</asp:ListItem>
                                <asp:ListItem Value="venta">Venta</asp:ListItem>
                                <asp:ListItem Value="permuta">Permuta</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </section>


                    <section class="row">
                        <h5>Datos adicionales de un apartamento</h5>
                        <div class="input-field col s12 m6">
                            <i class="material-icons prefix">business</i>
                            <label for="txtModificarPiso" runat="server">Piso</label>
                            <asp:TextBox runat="server" ID="txtModificarPiso" Text=""></asp:TextBox>
                        </div>
                        <div class="input-field col s12 m6">
                            <i class="material-icons prefix">call_merge</i>
                            <label for="txtModificarAscensor" runat="server">Tiene Ascensor?:</label>
                            <asp:CheckBox ID="txtModificarAscensor" runat="server" CssClass="Mostrar" />
                        </div>
                    </section>
                    <section>
                        <div>
                            <h5>Si desea confirmar los datos, precione el boton: </h5>
                            <asp:Button runat="server" ID="btnModificarApartamento" Text="Modificar Apartamento" OnClick="btnModificarApartamento_Click" CssClass="waves-effect waves-light btn light-green accent-2" />
                        </div>
                    </section>
                    <Maxi:Resultado runat="server" ID="MxResultadoModificar" Visible="false" />
                </section>
            </div>
        </li>
    </ul>

</asp:Content>

