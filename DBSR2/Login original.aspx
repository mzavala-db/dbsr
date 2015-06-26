<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login original.aspx.cs" Inherits="DBSR2.Login" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoTitulo" runat="server">
DBSR2
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">
    <table class="tablaform">
        <tr>
            <td width="200px">Usuario</td>
            <td>
                <asp:TextBox id="UsuarioTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UsuarioRequiredFieldValidator" runat="server" ErrorMessage="Ingrese el usuario." ControlToValidate="UsuarioTextBox" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Contraseña</td>
            <td>
                <asp:TextBox id="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" ErrorMessage="Ingrese la contraseña." ControlToValidate="PasswordTextBox" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <p />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Validation" />
    <asp:Button ID="IngresarButton" runat="server" Text="Ingresar" CssClass="boton" ValidationGroup="Validation" />
</asp:Content>
