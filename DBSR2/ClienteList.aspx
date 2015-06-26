<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClienteList.aspx.cs" Inherits="DBSR2.ClienteList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Clientes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:GridView ID="ClientesGridView" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="30" AllowSorting="true" CssClass="grillaprincipal">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" /> 
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