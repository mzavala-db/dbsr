<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelProyectoForm.aspx.cs" Inherits="DBSR2.PanelProyectoForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoTitulo" runat="server">
    Panel de proyectos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table class="tablaformancha">
        <tr>
            <td width="200px"><strong>Proyecto</strong></td>
            <td>
                <asp:DropDownList ID="ProyectoDropDown" runat="server" DataValueField="IdProyecto" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="true">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:Panel ID="ProyectoPanel" runat="server" Visible="false">
        <table class="tablaformancha">
            <tr>
                <td width="200px"><strong>Project Leader</strong></td>
                <td colspan="3">
                    <asp:Label ID="ProjectLeaderLabel" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="200px"><strong>Cliente</strong></td>
                <td>
                    <asp:Label ID="ClienteLabel" runat="server" />
                </td>
                <td width="200px"><strong>Cuenta de facturación</strong></td>
                <td>
                    <asp:Label ID="CuentaFacturacionLabel" runat="server" />
                </td>
            </tr>
            <tr>
                <td><strong>Modalidad</strong></td>
                <td>
                    <asp:Label ID="ModalidadLabel" runat="server" />
                </td>
                <td><strong>Activo</strong></td>
                <td>
                    <asp:Label ID="ActivoLabel" runat="server" />
                </td>
            </tr>
            <tr>
                <td><strong>Fecha de inicio estimada</strong></td>
                <td>
                    <asp:Label ID="FechaInicioEstimadaLabel" runat="server" />
                </td>
                <td><strong>Fecha de inicio real</strong></td>
                <td>
                    <asp:Label ID="FechaInicioRealLabel" runat="server" />
                </td>
            </tr>
            <tr>
                <td><strong>Fecha de fin estimada</strong></td>
                <td>
                    <asp:Label ID="FechaFinEstimadaLabel" runat="server" />
                </td>
                <td><strong>Fecha de fin real</strong></td>
                <td>
                    <asp:Label ID="FechaFinRealLabel" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="EditarProyectoButton" runat="server" Text="Editar" CssClass="boton" />
        <asp:Button ID="ProximoProyectoButton" runat="server" Text="Próximo" CssClass="boton" />
        <asp:Button ID="VolverButton" runat="server" Text="Volver" CssClass="boton" />
    </asp:Panel>
    <asp:UpdatePanel ID="RecursosUpdatePanel" runat="server" Visible="false">
        <ContentTemplate>
            <h2>Recursos</h2>
            <asp:GridView ID="RecursoGridView" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                AllowPaging="false" CssClass="grillaprincipal" EmptyDataText="No hay recursos asignados para este proyecto.">
                <Columns>
                    <asp:BoundField DataField="FechaDesde" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha desde" />
                    <asp:BoundField DataField="FechaHasta" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha hasta" />
                    <asp:BoundField DataField="Recurso" HeaderText="Recurso" />
                    <asp:BoundField DataField="Horas" HeaderText="Horas reales" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="HorasFacturacion" HeaderText="Horas vendidas" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="FechaProximaLicencia" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Próxima licencia" />
                    <asp:BoundField DataField="MotivoLicencia" HeaderText="Motivo" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Vigente?
                            <asp:DropDownList ID="ProyectoVigenteDropDown" runat="server" OnSelectedIndexChanged="ProyectoVigenteDropDownChanged"
                                AutoPostBack="true">
                                <asp:ListItem Text="" Value="-1" />
                                <asp:ListItem Text="Sí" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("Vigente")) ? "Sí" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="ReporteEstadoUpdatePanel" runat="server" Visible="false">
        <ContentTemplate>
            <h2>Reportes de estado</h2>
            <asp:GridView ID="ReporteEstadoGridView" runat="server" AutoGenerateColumns="false"
                ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="5" CssClass="grillaancha"
                EmptyDataText="No hay reportes de estado para este proyecto.">
                <Columns>
                    <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:TemplateField>
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="EditarReporteEstadoButton" ImageUrl="~/Images/edit.gif" runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdReporteEstado") %>' />
                            <asp:ImageButton ID="EliminarReporteEstadoLink" ImageUrl="~/Images/delete.gif" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IdReporteEstado") %>' OnClientClick="return confirm('Confirme que desea eliminar el reporte de estado.');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
            </asp:GridView>
            <br />
            <asp:Button ID="AgregarReporteEstadoButton" Text="Agregar..." runat="server" CssClass="botonchico" />
            <asp:Panel ID="ReporteEstadoPanel" runat="server" Style="display: none; width: 550px; height: 280px; padding: 5px;" CssClass="popup">
                <table>
                    <tr>
                        <td colspan="2"><strong>Reporte de estado</strong></td>
                    </tr>
                    <tr valign="top">
                        <td width="100px">Descripción</td>
                        <td>
                            <asp:TextBox ID="ReporteEstadoDescripcionTextBox" runat="server" TextMode="MultiLine" Width="450px" Height="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ReporteEstadoDescripcionRequiredFieldValidator" runat="server" ErrorMessage="Ingrese la descripción." ControlToValidate="ReporteEstadoDescripcionTextBox"
                                ValidationGroup="ReporteEstadoValidation">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <p />
                &nbsp;<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ReporteEstadoValidation" />
                <p>
                    <asp:Button ID="GrabarReporteEstadoButton" runat="server" CssClass="botonchico" Text="Grabar" ValidationGroup="ReporteEstadoValidation" />
                    <asp:Button ID="CancelarReporteEstadoButton" runat="server" CausesValidation="false" CssClass="botonchico" Text="Cancelar" />
                </p>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="ReporteEstadoModalPopupExtender" runat="server"
                TargetControlID="AgregarReporteEstadoButton" PopupControlID="ReporteEstadoPanel"
                CancelControlID="CancelarReporteEstadoButton" BackgroundCssClass="fondoaplicacion" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="HitoUpdatePanel" runat="server" Visible="false">
        <ContentTemplate>
            <h2>Hitos</h2>
            <asp:GridView ID="HitoGridView" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                AllowPaging="true" PageSize="5" CssClass="grillaancha" EmptyDataText="No hay hitos para este proyecto.">
                <Columns>
                    <asp:BoundField DataField="FechaAlta" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha alta" />
                    <asp:BoundField DataField="FechaEstimada" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha est" />
                    <asp:BoundField DataField="FechaCumplimiento" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha cum" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:TemplateField>
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="EditarHitoButton" ImageUrl="~/Images/edit.gif" runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdHito") %>' />
                            <asp:ImageButton ID="EliminarHitoLink" ImageUrl="~/Images/delete.gif" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IdHito") %>' OnClientClick="return confirm('Confirme que desea eliminar el hito.');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
            </asp:GridView>
            <br />
            <asp:Button ID="AgregarHitoButton" Text="Agregar..." runat="server" CssClass="botonchico" />
            <asp:Panel ID="HitoPanel" runat="server" Style="display: none; width: 650px; height: 300px; padding: 5px;" CssClass="popup">
                <table>
                    <tr>
                        <td colspan="2"><strong>Hito</strong></td>
                    </tr>
                    <tr>
                        <td width="100px">Fecha estimada</td>
                        <td>
                            <asp:TextBox ID="HitoFechaEstimadaTextBox" runat="server" Width="10em" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="HitoFechaEstimadaRequiredFieldValidator" runat="server"
                                ControlToValidate="HitoFechaEstimadaTextBox" ErrorMessage="Ingrese la fecha estimada"
                                Display="Dynamic" ValidationGroup="HitoValidation">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="HitoFechaEstimadaCompareValidator" runat="server" ControlToValidate="HitoFechaEstimadaTextBox"
                                Operator="DataTypeCheck" Type="Date" ErrorMessage="La fecha estimada ingresada no es correcta"
                                Display="Dynamic" ValidationGroup="HitoValidation">*</asp:CompareValidator>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="HitoFechaEstimadaTextBox" Mask="99/99/9999" MaskType="Date" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">Fecha cumplimiento</td>
                        <td>
                            <asp:TextBox ID="HitoFechaCumplimientoTextBox" runat="server" Width="10em" MaxLength="10"></asp:TextBox>
                            <asp:CompareValidator ID="HitoFechaCumplimientoCompareValidator" runat="server" ControlToValidate="HitoFechaCumplimientoTextBox"
                                Operator="DataTypeCheck" Type="Date" ErrorMessage="La fecha de cumplimiento ingresada no es correcta"
                                Display="Dynamic" ValidationGroup="HitoValidation">*</asp:CompareValidator>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="HitoFechaCumplimientoTextBox" Mask="99/99/9999" MaskType="Date" runat="server"/>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>Descripción</td>
                        <td>
                            <asp:TextBox ID="HitoDescripcionTextBox" runat="server" TextMode="MultiLine" Width="450px" Height="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="HitoDescripcionRequiredFieldValidator" runat="server" ErrorMessage="Ingrese la descripción." ControlToValidate="HitoDescripcionTextBox"
                                ValidationGroup="HitoValidation">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <p>
                    <asp:Button ID="GrabarHitoButton" runat="server" CssClass="botonchico" Text="Grabar" ValidationGroup="HitoValidation" />
                    <asp:Button ID="CancelarHitoButton" runat="server" CausesValidation="false" CssClass="botonchico" Text="Cancelar" />
                </p>
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="HitoValidation" />
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="HitoModalPopupExtender" runat="server" TargetControlID="AgregarHitoButton"
                PopupControlID="HitoPanel" CancelControlID="CancelarHitoButton" BackgroundCssClass="fondoaplicacion" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <p />
</asp:Content>
