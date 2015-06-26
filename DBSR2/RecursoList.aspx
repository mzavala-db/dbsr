<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursoList.aspx.cs" Inherits="DBSR2.RecursoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Recursos
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Filtros" runat="server">
Activo
<br />
<asp:DropDownList id="ActivoDropDown" runat="server" OnSelectedIndexChanged="ActivoDropDownChanged" AutoPostBack="true">
    <asp:ListItem Text="" Value="-1" />
    <asp:ListItem Text="Sí" Value="1" Selected="True" />
    <asp:ListItem Text="No" Value="0" />
</asp:DropDownList>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:GridView ID="RecursosGridView" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="30" CssClass="grillaprincipal">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Recurso" />
            <asp:TemplateField HeaderText="Tipo" >
                <ItemTemplate>
                    <asp:Literal id="TipoRecursoLiteral" runat="server" Text='<%# Bind("TipoRecurso.Descripcion") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Activo?" SortExpression="Activo">
                <ItemStyle Width="30px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("Activo")) ? "Sí" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="50px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:HyperLink Text="Editar" id="EditarLink" ImageUrl="~/Images/edit.gif" runat="server"></asp:HyperLink>
                    <asp:HyperLink Text="Licencias" id="LicenciasLink" ImageUrl="~/Images/calendar.gif" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
    </asp:GridView>
    <p />
    <asp:Button ID="NuevoButton" runat="server" Text="Nuevo" CssClass="boton" />
    <asp:Button ID="VolverButton" runat="server" Text="Volver" CssClass="boton" />
</asp:Content>
