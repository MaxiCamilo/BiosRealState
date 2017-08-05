<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Caducado.aspx.cs" Inherits="BiosRealState.Errores.Caducado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="Formulario" runat="server">
        <div>
            <strong>Usuario anulado</strong>
            <p><em>Su usuario caduco para esta sesion.</em> puede ser debido a que
                se cambio la contraseña o se haya eliminado el usuario.
            </p>
            <p>Si quiere tener acceso a esta plataforma como empleado, debera ingresar nuevamente.</p>
            <asp:Button ID="Volver" runat="server" Text="Volver al inicio" OnClick="Volver_Click" />
        </div>
    </form>
</body>
</html>
