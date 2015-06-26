<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuarioForm.aspx.cs" Inherits="DBSR2.UsuarioForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Clientes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table class="tablaform">
        <tr>
            <td width="200px">Nombre</td>
            <td>
                <asp:Literal ID="NombreLiteral" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="200px">Roles</td>
            <td>
                <asp:CheckBoxList ID="RolesList" runat="server" />
            </td>
        </tr>
    </table>
    <p />
    <asp:Button ID="GrabarButton" runat="server" Text="Grabar" CssClass="boton" />
    <asp:Button ID="CancelarButton" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
</asp:Content>