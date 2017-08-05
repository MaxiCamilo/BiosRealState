<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="ABMZonas.aspx.cs" Inherits="BiosRealState.ABMZonas" %>

<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>
<%@ Register TagPrefix="Maxi" TagName="Tabla" Src="~/Controles/Listado_Entidades.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">
    <!-- <asp:ObjectDataSource runat="server" ID="ZonasActivas" SelectMethod="Listado_Activos" TypeName="Logica.Singleton.Logica_Zona"></asp:ObjectDataSource> !-->
    <section>
        <h1 class="titulo">Administracion de zonas</h1>
        <p>Una zona es un territorio  donde se encuentran las propiedades, ademas se definen servicios, interesantes (puede que incluso indispensables) para los clientes.</p>
    </section>
    <hr />
    <!-- Alta  !-->
    <section>
        <h2>Agregar una nueva Zona</h2>
        <section>
            <h3>Paso 1: Identificadores</h3>
            <p>Para poder identificar una zona en la base de datos, se debe introducir un codigo de 3 letras, y la letra del departamento. El código debe ser unico de el departamento.</p>
            <div>
                <label for="txtAltaDepartamento" runat="server">Letra Departamento:</label>
                <asp:TextBox runat="server" ID="txtAltaDepartamento" Text=""></asp:TextBox>
            </div>
            <div>
                <label for="txtAltaCodigo" runat="server">Código:</label>
                <asp:TextBox runat="server" ID="txtAltaCodigo" Text=""></asp:TextBox>
            </div>
        </section>
        <section>
            <h3>Paso 2: Informacion Basica:</h3>
            <p>Se necesita saber su nombre y cantidad de habitantes</p>
            <div>
                <label for="txtAltaNombre" runat="server">Nombre:</label>
                <asp:TextBox runat="server" ID="txtAltaNombre" Text=""></asp:TextBox>
            </div>
            <div>
                <label for="txtAltaHabitantes" runat="server">Habitantes:</label>
                <asp:TextBox runat="server" ID="txtAltaHabitantes" Text=""></asp:TextBox>
            </div>
        </section>

        <section>
            <div>
                <h3>Si desea confirmar los datos, precione el boton: </h3>
                <asp:Button runat="server" ID="btnAltaZona" Text="Agregar Zona" OnClick="btnAltaZona_Click" />
                <h3 runat="server" id="AltaPositivo" visible="false">Se agrego la zona Correctamente</h3>
            </div>
        </section>
        <section>
            <Maxi:Resultado runat="server" ID="MxResultadoAlta" Titulo="No se agrego la zona" Dialogo="No se pudo agregar la zona porque hay datos que no son correctos, a continuacion, se le especificara el porque son inlvalidos:" Visible="false" />
        </section>
    </section>
    <!-- Servicios  !-->
    <section>
        <h1>Servicios</h1>
        <p>Puede especificar los servicios de la zona, interesantes para el cliente, como hospitales, almacenes, teatros, etc.</p>
        <section>
            <p>Primero seleccione una zona:</p>
            <Maxi:Tabla ID="MxZonas_Servicio" runat="server" Habilitar_Seleccion="true" OnSeleccionado="MxZonas_Servicio_Seleccionado" Dialogo_Vacio="No existen zonas en la base de datos" />
        </section>
        <div runat="server" id="Panel_Servicios" visible="false">
            <p>Detalles de los servicios de la zona seleccionada</p>
            <div>
                <section>
                    <label for="txtAltaServicio" runat="server">Nombre de Servicio:</label>
                    <asp:TextBox runat="server" ID="txtAltaServicio" Text=""></asp:TextBox>
                    <asp:Button runat="server" ID="btnAltaServicio" Text="Agregar Servicio" OnClick="btnAltaServicio_Click" />
                </section>
            </div>
            <p runat="server" id="errorAltaServicio"></p>
            <asp:ListBox runat="server" ID="Listado_Servicios"></asp:ListBox>
            <br />
            <section>
                <label for="btnAltaEliminarServicio" runat="server">Eliminar servicio: solo debe seleccionar el nombre para eliminar el servicio:</label>
                <asp:Button runat="server" ID="btnAltaEliminarServicio" Text="Eliminar Servicio" OnClick="btnAltaEliminarServicio_Click" />
            </section>

        </div>
    </section>

    <!-- Modificar y eliminar !-->
    <section>
        <h1>Listado de las zonas</h1>
        <p>Puede ver todos los detalles de las zonas, ademas podra modificar y eliminar</p>
        <Maxi:Tabla ID="mxTabla_Listado" runat="server" Habilitar_Seleccion="false" Habilitar_Eliminar="true"
            Habilitar_Modificar="true"  Dialogo_Vacio="No existen zonas en la base de datos" OnModificar="mxTabla_Listado_Modificar" OnEliminar="mxTabla_Listado_Eliminar" />
       
        <section runat="server" id="Panel_Modificar" visible="false">
            <h2>Modificar Zona</h2>

            <section>
                <h3>Informacion Basica:</h3>
                <p>Se necesita saber su nombre y cantidad de habitantes</p>
                <div>
                    <label for="txtModificarNombre" runat="server">Nombre:</label>
                    <asp:TextBox runat="server" ID="txtModificarNombre" Text=""></asp:TextBox>
                </div>
                <div>
                    <label for="txtModificarHabitantes" runat="server">Habitantes:</label>
                    <asp:TextBox runat="server" ID="txtModificarHabitantes" Text=""></asp:TextBox>
                </div>
            </section>
                 <Maxi:Resultado runat="server" ID="MxResultadoModificar" Visible="false" />
            <section>
                <div>
                    <h3>Si desea confirmar los datos, precione el boton: </h3>
                    <asp:Button runat="server" ID="btnModificarZona" Text="Modificar Zona" OnClick="btnModificarZona_Click" />
                </div>
            </section>
        </section>
    </section>
</asp:Content>
