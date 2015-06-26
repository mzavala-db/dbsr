<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DBSR2.Login" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoTitulo" runat="server">
DBSR2
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">
    <asp:Login id="Login1" runat="server">
        <LayoutTemplate>
            <table class="tablaform">
                <tr>
                    <td width="200px">Usuario</td>
                    <td>
                        <asp:TextBox id="UserName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ErrorMessage="Ingrese el usuario." ControlToValidate="UserName" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Contraseña</td>
                    <td>
                        <asp:TextBox id="Password" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ErrorMessage="Ingrese la contraseña." ControlToValidate="Password" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Recordar inicio de sesión</td>
                    <td>
                        <asp:CheckBox ID="RememberMe" runat="server" />
                    </td>
                </tr>
            </table>
            <p />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Validation" />
            <asp:Button ID="Login" CommandName="Login" runat="server" Text="Ingresar" CssClass="boton" ValidationGroup="Validation" />
        </LayoutTemplate>
    </asp:Login>
</asp:Content>
