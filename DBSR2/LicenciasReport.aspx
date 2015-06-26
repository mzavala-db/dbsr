<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LicenciasReport.aspx.cs" Inherits="DBSR2.LicenciasReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Reporte de licencias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table class="tablaform">
        <tr>
            <td width="200px">Fecha desde</td>
            <td>
                <asp:TextBox id="FechaDesdeTextBox" runat="server" Width="10em" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FechaDesdeRequiredFieldValidator" runat="server" ControlToValidate="FechaDesdeTextBox" ErrorMessage="Ingrese la fecha" Display="Dynamic" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="FechaDesdeCompareValidator" runat="server" ControlToValidate="FechaDesdeTextBox" Operator="DataTypeCheck" Type="Date" ErrorMessage="La fecha 'desde' ingresada no es correcta" Display="Dynamic" ValidationGroup="Validation">*</asp:CompareValidator>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="FechaDesdeTextBox" Mask="99/99/9999" MaskType="Date" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>Fecha hasta</td>
            <td>
                <asp:TextBox id="FechaHastaTextBox" runat="server" Width="10em" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FechaHastaRequiredFieldValidator" runat="server" ControlToValidate="FechaHastaTextBox" ErrorMessage="Ingrese la fecha" Display="Dynamic" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="FechaHastaCompareValidator" runat="server" ControlToValidate="FechaHastaTextBox" Operator="GreaterThan" ControlToCompare="FechaDesdeTextBox" Type="Date" ErrorMessage="La fecha 'hasta' ingresada no es correcta" Display="Dynamic" ValidationGroup="Validation">*</asp:CompareValidator>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="FechaHastaTextBox" Mask="99/99/9999" MaskType="Date" runat="server"/>
            </td>
        </tr>
    </table>
    <p />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Validation" />
    <asp:Button ID="GenerarButton" runat="server" Text="Generar" CssClass="boton" ValidationGroup="Validation" />
    <asp:Button ID="CancelarButton" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
</asp:Content>