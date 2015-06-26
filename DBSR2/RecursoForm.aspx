<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursoForm.aspx.cs" Inherits="DBSR2.RecursoForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Recurso
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table class="tablaform">
        <tr>
            <td width="200px">Nombre</td>
            <td>
                <asp:TextBox id="NombreTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NombreRequiredFieldValidator" runat="server" ErrorMessage="Ingrese el nombre" ControlToValidate="NombreTextBox" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Tipo de recurso</td>
            <td>
                <asp:DropDownList ID="TipoRecursoDropDown" runat="server" DataValueField="IdTipoRecurso" DataTextField="Descripcion" AppendDataBoundItems="true">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="ClienteRequiredFieldValidator" runat="server" ErrorMessage="Ingrese el tipo de recurso" ControlToValidate="TipoRecursoDropDown" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>¿Activo?</td>
            <td>
                <asp:CheckBox ID="ActivoCheckBox" runat="server" />
            </td>
        </tr>
    </table>
    <p />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Validation" />
    <asp:Button ID="GrabarButton" runat="server" Text="Grabar" CssClass="boton" ValidationGroup="Validation" />
    <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" CssClass="boton" CausesValidation="false" OnClientClick="return confirm('Confirme que desea eliminar el recurso.');" />
    <asp:Button ID="CancelarButton" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
</asp:Content>
