<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlanificadorVacacionesForm.aspx.cs" Inherits="DBSR2.PlanificadorVacacionesForm" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoTitulo" runat="server">
Planificador de vacaciones
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">

<asp:Calendar ID="VacacionesCalendar" runat="server" Width="1000px" Font-Size="8pt"
    DayHeaderStyle-Height="25px" 
    TitleStyle-Height="35px" TitleStyle-Font-Bold="true"
    NextMonthText="siguiente >>"
    PrevMonthText="<< anterior"
    SelectionMode="None"
    DayStyle-VerticalAlign="Top" DayStyle-HorizontalAlign="Left" DayStyle-BorderWidth="1px" DayStyle-Height="100px"
    OtherMonthDayStyle-BackColor="LightGray"
    EnableViewState="false">
</asp:Calendar>
<br />
<asp:Button ID="VolverButton" runat="server" Text="Volver" CssClass="boton" />

</asp:Content>
