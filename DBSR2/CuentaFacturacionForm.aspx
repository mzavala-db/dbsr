<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CuentaFacturacionForm.aspx.cs" Inherits="DBSR2.CuentaFacturacionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Cuentas de facturación
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table>
        <tr>
            <td width="200px">Nombre</td>
            <td>
                <asp:TextBox id="NombreTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NombreRequiredFieldValidator" runat="server" ErrorMessage="Ingrese el nombre." ControlToValidate="NombreTextBox" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <p />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Validation" />
    <asp:Button ID="GrabarButton" runat="server" Text="Grabar" CssClass="boton" ValidationGroup="Validation" />
    <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" CssClass="boton" CausesValidation="false" OnClientClick="return confirm('Confirme que desea eliminar la cuenta de facturación.');" />
    <asp:Button ID="CancelarButton" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />    
</asp:Content>