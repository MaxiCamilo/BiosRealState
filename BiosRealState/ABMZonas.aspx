<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="ABMZonas.aspx.cs" Inherits="BiosRealState.ABMZonas" %>

<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>
<%@ Register TagPrefix="Maxi" TagName="Tabla" Src="~/Controles/Listado_Entidades.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">
    <!-- <asp:ObjectDataSource runat="server" ID="ZonasActivas" SelectMethod="Listado_Activos" TypeName="Logica.Singleton.Logica_Zona"></asp:ObjectDataSource> !-->
    <section class="container">
        <h3 class="center light-blue-text text-darken-4">Administracion de zonas</h3>
        <p>Una zona es un territorio  donde se encuentran las propiedades, ademas se definen servicios, interesantes (puede que incluso indispensables) para los clientes.</p>
    </section>
    <hr />
    <!-- Alta  !-->

    <ul class="collapsible" data-collapsible="accordion">
        <li>
            <div class="collapsible-header"><i class="material-icons">add_box</i>Agregar Zona</div>
            <div class="collapsible-body container">
                <section class="row">
                    <h5>Paso 1: Identificadores</h5>
                    <p>Para poder identificar una zona en la base de datos, se debe introducir un codigo de 3 letras, y la letra del departamento. El código debe ser unico de el departamento.</p>
                    <div class="input-field col s12 m6">
                        <i class="material-icons prefix">location_city</i>
                        <label for="txtAltaDepartamento" runat="server">Letra Departamento:</label>
                        <asp:TextBox runat="server" ID="txtAltaDepartamento" Text=""></asp:TextBox>
                    </div>
                    <div class="input-field col s12 m6">
                        <i class="material-icons prefix">vpn_key</i>
                        <label for="txtAltaCodigo" runat="server">Código:</label>
                        <asp:TextBox runat="server" ID="txtAltaCodigo" Text=""></asp:TextBox>
                    </div>
                </section>
                <section class="row">
                    <h5>Paso 2: Informacion Basica:</h5>
                    <p>Se necesita saber su nombre y cantidad de habitantes</p>
                    <div class="input-field col s12 m8">
                        <i class="material-icons prefix">room</i>
                        <label for="txtAltaNombre" runat="server">Nombre:</label>
                        <asp:TextBox runat="server" ID="txtAltaNombre" Text=""></asp:TextBox>
                    </div>
                    <div class="input-field col s12 m4">
                        <i class="material-icons prefix">streetview</i>
                        <label for="txtAltaHabitantes" runat="server">Habitantes:</label>
                        <asp:TextBox runat="server" ID="txtAltaHabitantes" Text=""></asp:TextBox>
                    </div>
                </section>

                <section class="container">
                    <div class="row">
                        <p class="col s12 m7">Si desea confirmar los datos, precione el boton: </p>
                        <asp:Button runat="server" ID="btnAltaZona" Text="Agregar Zona" OnClick="btnAltaZona_Click" CssClass="col s12 m5 waves-effect waves-light btn light-green" />
                        
                    </div>
                    <h3 runat="server" id="AltaPositivo" visible="false" class="light-green lighten-2 white-text card-panel hoverable">Se agrego la zona Correctamente</h3>
                </section>
                <section class="row">
                    <Maxi:Resultado runat="server" ID="MxResultadoAlta" Titulo="No se agrego la zona" Dialogo="No se pudo agregar la zona porque hay datos que no son correctos, a continuacion, se le especificara el porque son inlvalidos:" Visible="false"/>
                </section>
            </div>
        </li>
        <!-- Servicios  !-->
        <li>
            <div class="collapsible-header"><i class="material-icons">shopping_cart</i>Gestion de Servicios</div>
            <div class="collapsible-body">
                <p>Puede especificar los servicios de la zona, interesantes para el cliente, como hospitales, almacenes, teatros, etc.</p>
                <section>
                    <p>Primero seleccione una zona:</p>
                    <Maxi:Tabla ID="MxZonas_Servicio" runat="server" Habilitar_Seleccion="true" OnSeleccionado="MxZonas_Servicio_Seleccionado" Dialogo_Vacio="No existen zonas en la base de datos" />
                </section>
                <div runat="server" id="Panel_Servicios" visible="false">
                    <h5 class="center">Detalles de los servicios de la zona seleccionada</h5>
                    <div class="container">
                        <section class="row">
                            <div class="input-field s12 m9">
                                <label for="txtAltaServicio" runat="server">Nombre de Servicio:</label>
                                <asp:TextBox runat="server" ID="txtAltaServicio" Text=""></asp:TextBox>
                            </div>
                            <asp:Button runat="server" ID="btnAltaServicio" Text="Agregar Servicio" OnClick="btnAltaServicio_Click" CssClass="s12 waves-effect m3  waves-light btn light-green accent-2" />
                        </section>
                    </div>
                    <h5 class="center" runat="server" id="errorAltaServicio"></h5>
                    <asp:ListBox runat="server" ID="Listado_Servicios" CssClass="Mostrar MinimoLista"></asp:ListBox>
                    <br />
                    <section>
                        <label for="btnAltaEliminarServicio" runat="server">Eliminar servicio: solo debe seleccionar el nombre para eliminar el servicio:</label>
                        <asp:Button runat="server" ID="btnAltaEliminarServicio" Text="Eliminar Servicio" OnClick="btnAltaEliminarServicio_Click" CssClass="red lighten-1 waves-effect m3  waves-light btn" />
                    </section>

                </div>
            </div>
        </li>

        <!-- Modificar y eliminar !-->
        <li>
            <div class="collapsible-header"><i class="material-icons">edit</i>Listado y alteración de zonas</div>
            <div class="collapsible-body">
                <h3>Listado de las zonas</h3>
                <p>Puede ver todos los detalles de las zonas, ademas podra modificar y eliminar</p>
                <Maxi:Tabla ID="mxTabla_Listado" runat="server" Habilitar_Seleccion="false" Habilitar_Eliminar="true"
                    Habilitar_Modificar="true" Dialogo_Vacio="No existen zonas en la base de datos" OnModificar="mxTabla_Listado_Modificar" OnEliminar="mxTabla_Listado_Eliminar" />

                <section runat="server" id="Panel_Modificar" visible="false" class="container">
                    <h3 class="center">Modificar Zona</h3>

                    <section class="row">
                        <div class="input-field col s12 m6">
                            <i class="material-icons prefix">room</i>
                            <label for="txtModificarNombre" runat="server">Nombre:</label>
                            <asp:TextBox runat="server" ID="txtModificarNombre" Text=""></asp:TextBox>
                        </div>
                        <div class="input-field col s12 m6">
                            <i class="material-icons prefix">streetview</i>
                            <label for="txtModificarHabitantes" runat="server">Habitantes:</label>
                            <asp:TextBox runat="server" ID="txtModificarHabitantes" Text=""></asp:TextBox>
                        </div>
                    </section>
                    <Maxi:Resultado runat="server" ID="MxResultadoModificar" Visible="false" />
                    <section class="container">
                        <div class="row">
                            <p class="col s12 m7">Si desea confirmar los datos, precione el boton: </p>
                            <asp:Button runat="server" ID="btnModificarZona" Text="Modificar Zona" OnClick="btnModificarZona_Click" CssClass="col s12 m5 waves-effect waves-light btn light-green accent-2"/>
                        </div>
                    </section>
                </section>
            </div>
        </li>
    </ul>
</asp:Content>
