<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuarioList.aspx.cs" Inherits="DBSR2.UsuarioList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:GridView ID="UsuariosGridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" SortExpression="UserName" CssClass="grillaprincipal">
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="Nombre" /> 
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
    <asp:Button ID="VolverButton" runat="server" Text="Volver" CssClass="boton" />
</asp:Content>