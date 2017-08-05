<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="ABMLocales.aspx.cs" Inherits="BiosRealState.ABMLocales" %>
<%@ Register TagPrefix="Maxi" TagName="Resultado" Src="~/Controles/Cuadro_Resultado.ascx" %>
<%@ Register TagPrefix="Maxi" TagName="Tabla" Src="~/Controles/Listado_Entidades.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EspacioContenido" runat="server">
    <section>
        <h1 class="titulo">Administracion de Locales</h1>
        <p>Los locales son un tipo de propiedad comercial, donde se especifican si cuentan con una habilitacion</p>
    </section>
    <!-- Alta  !-->
    <section>
        <h2>Agregar una nuevo local</h2>
        <section>
            <h3>Paso 1: Identificadores</h3>
            <p>Para poder identificar un apartamento en la base de datos, se debe introducir su padron (números) y su zona correspondiente</p>

            <div>
                <label for="txtAltaPadron" runat="server">Padron:</label>
                <asp:TextBox runat="server" ID="txtAltaPadron" Text=""></asp:TextBox>
            </div>
            <div>
                <h4>Propiedad donde se va a encontrar:</h4>
                <Maxi:Tabla ID="MxZonas_Alta" runat="server" Habilitar_Seleccion="true" OnSeleccionado="MxZonas_Alta_Seleccionado" Dialogo_Vacio="No existen zonas en la base de datos" />
            </div>
        </section>
        <section>
            <h3>Paso 2: Informacion Basica de una propiedad:</h3>
            <p>Se necesitan saber algunos datos basicos y legales de la propiedad.</p>
            <div>
                <label for="txtAltaDireccion" runat="server">Direccion:</label>
                <asp:TextBox runat="server" ID="txtAltaDireccion" Text=""></asp:TextBox>
            </div>
            <div>
                <label for="txtAltaPrecio" runat="server">Precio:</label>
                <asp:TextBox runat="server" ID="txtAltaPrecio" Text=""></asp:TextBox>
            </div>
            <div>
                <label for="txtAltaAccion" runat="server">Accion:</label>
                <asp:ListBox ID="txtAltaAccion" runat="server">
                    <asp:ListItem Value="alquiler">Alquiler</asp:ListItem>
                    <asp:ListItem Value="venta">Venta</asp:ListItem>
                    <asp:ListItem Value="permuta">Permuta</asp:ListItem>
                </asp:ListBox>
            </div>
            <div>
                <label for="txtAltaCantidad_banio" runat="server">Cantidad de Baños:</label>
                <asp:TextBox runat="server" ID="txtAltaCantidad_banio" Text=""></asp:TextBox>
            </div>
            <div>
                <label for="txtAltacantidad_habitaciones" runat="server">Cantidad Habitaciones:</label>
                <asp:TextBox runat="server" ID="txtAltacantidad_habitaciones" Text=""></asp:TextBox>
            </div>
            <div>
                <label for="txtAltaMetros_cuadrados" runat="server">Metros Cuadrados:</label>
                <asp:TextBox runat="server" ID="txtAltaMetros_cuadrados" Text=""></asp:TextBox>
            </div>
        </section>

        <section>
            <h3>Paso 3: Datos adicionales de un local</h3>
            <div>
                <label for="txtAltaHabilitacion" runat="server">Cuenta con habilitacion?:</label>
                <asp:CheckBox ID="txtAltaHabilitacion" runat="server" />
            </div>
        </section>

        <section>
            <div>
                <h3>Si desea confirmar los datos, precione el boton: </h3>
                <asp:Button runat="server" ID="btnAltaLocal" Text="Agregar Local" OnClick="btnAltaLocal_Click" />
                <h3 runat="server" id="AltaPositivo" visible="false">Se agrego el local Correctamente</h3>
            </div>
        </section>
        <section>
            <Maxi:Resultado runat="server" ID="MxResultadoAlta" Titulo="No se agrego el local" Dialogo="No se pudo agregar el local porque hay datos que no son correctos, a continuacion, se le especificara el porque son invalidos:" Visible="false" />
        </section>
    </section>
    <!-- Modificar y eliminar !-->
    <section>
        <h1>Listado de los locales</h1>
        <p>Puede ver todos los detalles de los locales, ademas podra modificar y eliminar</p>
        <Maxi:Tabla ID="mxTabla_Listado" runat="server" Habilitar_Seleccion="false" Habilitar_Eliminar="true"
            Habilitar_Modificar="true" Dialogo_Vacio="No existen locales en la base de datos" OnModificar="mxTabla_Listado_Modificar" OnEliminar="mxTabla_Listado_Eliminar" />

        <section runat="server" id="Panel_Modificar" visible="false">
            <h2>Modificar Local seleccionado</h2>

            <section>
                <section>
                    <h3>Informacion Basica de una propiedad:</h3>
                    <p>Se necesitan saber algunos datos basicos y legales de la propiedad.</p>
                    <div>
                        <label for="txtModificarDireccion" runat="server">Direccion:</label>
                        <asp:TextBox runat="server" ID="txtModificarDireccion" Text=""></asp:TextBox>
                    </div>
                    <div>
                        <label for="txtModificarPrecio" runat="server">Precio:</label>
                        <asp:TextBox runat="server" ID="txtModificarPrecio" Text=""></asp:TextBox>
                    </div>
                    <div>
                        <label for="txtModificarAccion" runat="server">Accion:</label>
                        <asp:ListBox ID="txtModificarAccion" runat="server">
                            <asp:ListItem Value="alquiler">Alquiler</asp:ListItem>
                            <asp:ListItem Value="venta">Venta</asp:ListItem>
                            <asp:ListItem Value="permuta">Permuta</asp:ListItem>
                        </asp:ListBox>
                    </div>
                    <div>
                        <label for="txtModificarCantidad_banio" runat="server">Cantidad de Baños:</label>
                        <asp:TextBox runat="server" ID="txtModificarCantidad_banio" Text=""></asp:TextBox>
                    </div>
                    <div>
                        <label for="txtModificarcantidad_habitaciones" runat="server">Cantidad Habitaciones:</label>
                        <asp:TextBox runat="server" ID="txtModificarcantidad_habitaciones" Text=""></asp:TextBox>
                    </div>
                    <div>
                        <label for="txtModificarMetros_cuadrados" runat="server">Metros Cuadrados:</label>
                        <asp:TextBox runat="server" ID="txtModificarMetros_cuadrados" Text=""></asp:TextBox>
                    </div>
                </section>

                <section>
                    <h3>Datos adicionales de un local</h3>                    
                    <div>
                        <label for="txtModificarHabilitacion" runat="server">Cuenta con habilitacion?:</label>
                        <asp:CheckBox ID="txtModificarHabilitacion" runat="server" />
                    </div>
                </section>
                <section>
                    <div>
                        <h3>Si desea confirmar los datos, precione el boton: </h3>
                        <asp:Button runat="server" ID="btnModificarLocal" Text="Modificar Apartamento" OnClick="btnModificarLocal_Click" />
                    </div>
                </section>
            </section>
            


        </section>
        <Maxi:Resultado runat="server" ID="MxResultadoModificar" Visible="false" />
    </section>
</asp:Content>