<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursoProyectoForm.aspx.cs" Inherits="DBSR2.RecursoProyectoForm" %>

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
            <td>Proyecto</td>
            <td>
                <asp:DropDownList ID="ProyectoDropDown" runat="server" DataValueField="IdProyecto" DataTextField="Nombre" AppendDataBoundItems="true">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="ProyectoRequiredFieldValidator" runat="server" ErrorMessage="Ingrese el proyecto." ControlToValidate="ProyectoDropDown" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Fecha desde</td>
            <td>
                <asp:TextBox id="FechaDesdeTextBox" runat="server" Width="10em" MaxLength="10" />
                <asp:CompareValidator ID="FechaDesdeCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="FechaDesdeTextBox" Type="Date" ErrorMessage="La fecha desde ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>Fecha hasta</td>
            <td>
                <asp:TextBox id="FechaHastaTextBox" runat="server" Width="10em" MaxLength="10" />
                <asp:CompareValidator ID="FechaHastaCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="FechaHastaTextBox" Type="Date" ErrorMessage="La fecha hasta ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>Horas reales</td>
            <td>
                <asp:TextBox id="HorasTextBox" runat="server" Width="4em" MaxLength="2" />
                <asp:RequiredFieldValidator ID="HorasRequiredFieldValidator" runat="server" ErrorMessage="Ingrese  la cantidad de horas reales" ControlToValidate="HorasTextBox" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="HorasCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="HorasTextBox" Type="Integer" ErrorMessage="La cantidad de horas reales ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>Horas vendidas</td>
            <td>
                <asp:TextBox id="HorasFacturacionTextBox" runat="server" Width="4em" MaxLength="2" />
                <asp:RequiredFieldValidator ID="HorasFacturacionRequiredFieldValidator" runat="server" ErrorMessage="Ingrese  la cantidad de horas vendidas" ControlToValidate="HorasFacturacionTextBox" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="HorasFacturacionCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="HorasFacturacionTextBox" Type="Integer" ErrorMessage="La cantidad de horas vendidas ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
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
    <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" CssClass="boton" CausesValidation="false" OnClientClick="return confirm('Confirme que desea eliminar esta asignación.');" />
    <asp:Button ID="CancelarButton" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
</asp:Content>