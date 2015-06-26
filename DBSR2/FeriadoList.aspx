<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FeriadoList.aspx.cs" Inherits="DBSR2.FeriadoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Feriados
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:GridView ID="FeriadosGridView" runat="server" AutoGenerateColumns="false" 
        EmptyDataText="No se encontraron registros para los filtros seleccionados." ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="30" CssClass="grillaprincipal">
        <Columns>
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" /> 
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" /> 
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