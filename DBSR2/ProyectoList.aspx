<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProyectoList.aspx.cs" Inherits="DBSR2.ProyectoList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Proyectos
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Filtros" runat="server">
Project Leader
<br />
<asp:DropDownList ID="ProjectLeaderDropDown" runat="server" OnSelectedIndexChanged="ProjectLeaderDropDownChanged" AutoPostBack="true" AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="IdRecurso" >
    <asp:ListItem text="-- Todos --" Value="-1" />
</asp:DropDownList>
<br />
Cliente
<br />
<asp:DropDownList ID="ClienteDropDown" runat="server" OnSelectedIndexChanged="ClienteDropDownChanged" AutoPostBack="true" AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="IdCliente" >
    <asp:ListItem text="-- Todos --" Value="-1" />
</asp:DropDownList>
<br />
Activo
<br />
<asp:DropDownList id="ActivoDropDown" runat="server" OnSelectedIndexChanged="ActivoDropDownChanged" AutoPostBack="true">
    <asp:ListItem Text="" Value="-1" />
    <asp:ListItem Text="Sí" Value="1" Selected="True" />
    <asp:ListItem Text="No" Value="0" />
</asp:DropDownList>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:GridView ID="ProyectosGridView" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" AllowSorting="true"
    EmptyDataText="No se encontraron registros para los filtros seleccionados." AllowPaging="true" PageSize="30" CssClass="grillaprincipal">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Proyecto" SortExpression="Nombre" />
            <asp:TemplateField HeaderText="Project Leader"  SortExpression="Recurso.Nombre">
                <ItemTemplate>
                    <asp:Literal id="ProjectLeaderLiteral" runat="server" Text='<%# Bind("Recurso.Nombre") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cliente" SortExpression="Cliente.Nombre">
                <ItemTemplate>
                    <asp:Literal id="ClienteLiteral" runat="server" Text='<%# Bind("Cliente.Nombre") %>' />
                </ItemTemplate>
            </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Activo?" SortExpression="Activo">
                <ItemStyle Width="30px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("Activo")) ? "Sí" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemStyle Width="70px" HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:HyperLink Text="Editar" id="EditarLink" ImageUrl="~/Images/edit.gif" runat="server"></asp:HyperLink>
                    <asp:HyperLink Text="Asignar recursos" id="RecursosLink" ImageUrl="~/Images/rel-personas.gif" runat="server"></asp:HyperLink>
                    <asp:HyperLink Text="Panel" id="PanelLink" ImageUrl="~/Images/masinfo.gif" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
    </asp:GridView>
    <p />
    <asp:Button ID="NuevoButton" runat="server" Text="Nuevo" CssClass="boton" />
    <asp:Button ID="VolverButton" runat="server" Text="Volver" CssClass="boton" />
</asp:Content>