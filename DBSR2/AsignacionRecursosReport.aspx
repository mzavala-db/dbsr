<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignacionRecursosReport.aspx.cs" Inherits="DBSR2.AsignacionRecursosReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Reporte de asignación de recursos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table class="tablaform">
        <tr>
            <td width="200px">Fecha</td>
            <td>
                <asp:TextBox id="FechaTextBox" runat="server" Width="10em" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FechaRequiredFieldValidator" runat="server" ControlToValidate="FechaTextBox" ErrorMessage="Ingrese la fecha" Display="Dynamic" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="FechaCompareValidator" runat="server" ControlToValidate="FechaTextBox" Operator="DataTypeCheck" Type="Date" ErrorMessage="La fecha 'desde' ingresada no es correcta" Display="Dynamic" ValidationGroup="Validation">*</asp:CompareValidator>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="FechaTextBox" Mask="99/99/9999" MaskType="Date" runat="server"/>
            </td>
        </tr>
    </table>
    <p />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Validation" />
    <asp:Button ID="GenerarButton" runat="server" Text="Generar" CssClass="boton" ValidationGroup="Validation" />
    <asp:Button ID="CancelarButton" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
</asp:Content>