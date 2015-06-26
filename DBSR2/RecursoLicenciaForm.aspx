<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursoLicenciaForm.aspx.cs" Inherits="DBSR2.RecursoLicenciaForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Recurso por proyecto
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table class="tablaform">
        <tr>
            <td width="200px">Recurso</td>
            <td>
                <asp:DropDownList ID="RecursoDropDown" runat="server" DataValueField="IdRecurso" DataTextField="Nombre" AppendDataBoundItems="true">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RecursoRequiredFieldValidator" runat="server" ErrorMessage="Ingrese el recurso." ControlToValidate="RecursoDropDown" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Motivo de licencia</td>
            <td>
                <asp:DropDownList ID="MotivoLicenciaDropDown" runat="server" DataValueField="IdMotivoLicencia" DataTextField="Descripcion" AppendDataBoundItems="true">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="MotivoLicenciaRequiredFieldValidator" runat="server" ErrorMessage="Ingrese el motivo de la licencia." ControlToValidate="MotivoLicenciaDropDown" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Fecha desde</td>
            <td>
                <asp:TextBox id="FechaDesdeTextBox" runat="server" Width="10em" MaxLength="10" />
                <asp:CompareValidator ID="FechaDesdeCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="FechaDesdeTextBox" Type="Date" ErrorMessage="La fecha desde ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
                <asp:RequiredFieldValidator ID="FechaDesdeRequiredFieldValidator" runat="server" ErrorMessage="Ingrese la fecha desde." ControlToValidate="FechaDesdeTextBox" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="FechaDesdeTextBox" Mask="99/99/9999" MaskType="Date" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>Fecha hasta</td>
            <td>
                <asp:TextBox id="FechaHastaTextBox" runat="server" Width="10em" MaxLength="10" />
                <asp:CompareValidator ID="FechaHastaCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="FechaHastaTextBox" Type="Date" ErrorMessage="La fecha hasta ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
                <asp:RequiredFieldValidator ID="FechaHastaRequiredFieldValidator" runat="server" ErrorMessage="Ingrese la fecha hasta." ControlToValidate="FechaHastaTextBox" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="FechaHastaTextBox" Mask="99/99/9999" MaskType="Date" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>Observaciones</td>
            <td>
                <asp:TextBox ID="ObservacionesTextBox" runat="server" TextMode="MultiLine" Width="50em" Height="6em" />
            </td>
        </tr>
    </table>
    <p />
    <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Validation" />
    <asp:Button ID="GrabarButton" runat="server" Text="Grabar" CssClass="boton" ValidationGroup="Validation" />
    <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" CssClass="boton" CausesValidation="false" OnClientClick="return confirm('Confirme que desea eliminar esta licencia.');" />
    <asp:Button ID="CancelarButton" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
</asp:Content>