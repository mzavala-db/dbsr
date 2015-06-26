<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursoProyectoList.aspx.cs" Inherits="DBSR2.RecursoProyectoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Recursos por proyecto
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Filtros" runat="server">
Recurso
<br />
<asp:DropDownList id="RecursoDropDown" runat="server" OnSelectedIndexChanged="RecursoDropDownChanged" AutoPostBack="true" AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="IdRecurso" >
    <asp:ListItem text="-- Todos --" Value="-1" />
</asp:DropDownList>
<br />
Proyecto
<br />
<asp:DropDownList id="ProyectoDropDown" runat="server" OnSelectedIndexChanged="ProyectoDropDownChanged" AutoPostBack="true" AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="IdProyecto" >
    <asp:ListItem text="-- Todos --" Value="-1" />
</asp:DropDownList>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:GridView id="RecursoProyectoGridView" runat="server" AutoGenerateColumns="false" AllowSorting="true"
        EmptyDataText="No se encontraron registros para los filtros seleccionados." ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="30" CssClass="grillaprincipal">
        <Columns>
            <asp:TemplateField HeaderText="Recurso" SortExpression="Recurso.Nombre">
                <ItemTemplate>
                    <%# Eval("Recurso.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Proyecto" SortExpression="Proyecto.Nombre">
                <ItemTemplate>
                    <%# Eval("Proyecto.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FechaDesde" HeaderText="Fecha desde" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="FechaHasta" HeaderText="Fecha hasta" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Horas" HeaderText="Horas reales" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="HorasFacturacion" HeaderText="Horas vendidas" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
            <asp:TemplateField>
                <ItemStyle Width="50px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:HyperLink Text="Editar" id="EditarLink" ImageUrl="~/Images/edit.gif" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
    </asp:GridView>
    <p />
    <asp:Button ID="NuevoButton" runat="server" Text="Nuevo" CssClass="boton" />
    <asp:Button ID="VolverButton" runat="server" Text="Volver" CssClass="boton" />
</asp:Content>