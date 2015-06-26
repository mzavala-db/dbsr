<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="FeriadoForm.aspx.cs" Inherits="DBSR2.FeriadoForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Feriado
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table class="tablaform">
        <tr>
            <td width="200px">Fecha</td>
            <td>
                <asp:TextBox id="FechaTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FechaRequiredFieldValidator" runat="server" ErrorMessage="Ingrese la fecha." ControlToValidate="FechaTextBox" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="FechaCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="FechaTextBox" Type="Date" ErrorMessage="La fecha ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="FechaTextBox" Mask="99/99/9999" MaskType="Date" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox id="NombreTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <p />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Validation" />
    <asp:Button ID="GrabarButton" runat="server" Text="Grabar" CssClass="boton" ValidationGroup="Validation" />
    <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" CssClass="boton" CausesValidation="false" OnClientClick="return confirm('Confirme que desea eliminar el feriado.');" />
    <asp:Button ID="CancelarButton" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
</asp:Content>