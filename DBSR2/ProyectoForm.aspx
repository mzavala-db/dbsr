<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProyectoForm.aspx.cs" Inherits="DBSR2.ProyectoForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
    Proyectos
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
            <td>Cliente</td>
            <td>
                <asp:DropDownList ID="ClienteDropDown" runat="server" DataValueField="IdCliente" DataTextField="Nombre" AppendDataBoundItems="true">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="ClienteRequiredFieldValidator" runat="server" ErrorMessage="Ingrese el cliente" ControlToValidate="ClienteDropDown" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Cuenta de facturación</td>
            <td>
                <asp:DropDownList ID="CuentaFacturacionDropDown" runat="server" DataValueField="IdCuentaFacturacion" DataTextField="Nombre" AppendDataBoundItems="true">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="CuentaFacturacionRequiredFieldValidator" runat="server" ErrorMessage="Ingrese la cuenta de facturación" ControlToValidate="CuentaFacturacionDropDown" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Modalidad</td>
            <td>
                <asp:DropDownList ID="ModalidadDropDown" runat="server" DataValueField="IdModalidadProyecto" DataTextField="Nombre" AppendDataBoundItems="true">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="ModalidadRequiredFieldValidator" runat="server" ErrorMessage="Ingrese la modalidad" ControlToValidate="ModalidadDropDown" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Project Leader</td>
            <td>
                <asp:DropDownList ID="ProjectLeaderDropDown" runat="server" DataValueField="IdRecurso" DataTextField="Nombre" AppendDataBoundItems="true"> 
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="ProjectLeaderRequiredFieldValidator" runat="server" ErrorMessage="Ingrese el Project Leader" ControlToValidate="ProjectLeaderDropDown" ValidationGroup="Validation">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Fecha de inicio estimada</td>
            <td>
                <asp:TextBox ID="FechaInicioEstimadaTextBox" runat="server" Width="10em" MaxLength="10" />
                <asp:CompareValidator ID="FechaInicioEstimadaCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="FechaInicioEstimadaTextBox" Type="Date" ErrorMessage="La fecha de inicio estimada ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>Fecha de inicio real</td>
            <td>
                <asp:TextBox ID="FechaInicioRealTextBox" runat="server" Width="10em" MaxLength="10" />
                <asp:CompareValidator ID="FechaInicioRealCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="FechaInicioRealTextBox" Type="Date" ErrorMessage="La fecha de inicio real ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>Fecha de fin estimada</td>
            <td>
                <asp:TextBox ID="FechaFinEstimadaTextBox" runat="server" Width="10em" MaxLength="10" />
                <asp:CompareValidator ID="FechaFinEstimadaCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="FechaFinEstimadaTextBox" Type="Date" ErrorMessage="La fecha de fin estimada ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>Fecha de fin real</td>
            <td>
                <asp:TextBox ID="FechaFinRealTextBox" runat="server" Width="10em" MaxLength="10" />
                <asp:CompareValidator ID="FechaFinRealCompareValidator" runat="server" Operator="DataTypeCheck" ControlToValidate="FechaFinRealTextBox" Type="Date" ErrorMessage="La fecha de fin real ingresada no es correcta." ValidationGroup="Validation">*</asp:CompareValidator>
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
    <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" CssClass="boton" CausesValidation="false" OnClientClick="return confirm('Confirme que desea eliminar el proyecto.');" />
    <asp:Button ID="CancelarButton" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
</asp:Content>