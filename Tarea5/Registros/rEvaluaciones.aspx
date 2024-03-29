﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="rEvaluaciones.aspx.cs" Inherits="Tarea5.Registros.rEvaluaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="panel-body">
        <div class="form-horizontal col-md-12" role="form">

                <div class="container">
                    <div class="form-row">

                        <%--EvaluacionId--%>
    
                            <asp:Label ID="EvaluacionIdLabel" Text="EvaluacionID" runat="server" />
                            <asp:TextBox ID="EvaluacionIdTextBox" class="form-control input-sm" TextMode="Number" runat="server" placeholder="0"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVId" ValidationGroup="Buscar" ControlToValidate="EvaluacionIdTextBox" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        

                            <%--Buscar Button--%>
                   
                            <div class="input-group-append">
                                <br />
                                <asp:Button Text="Buscar" class="btn btn-primary" runat="server" ID="BuscarButton" OnClick="BuscarButton_Click" />
                            </div>
               


                        <%--Fecha--%>
                      
                            <asp:Label Text="Fecha" runat="server" />
                            <asp:TextBox ID="FechaTextBox" class="form-control input-sm" TextMode="Date" runat="server"></asp:TextBox>
                        

                                    <%--Estudiante--%>
            
                        <label for="Estudiantelabel"  style="font-size: small">Estudiante</label>
                            <asp:DropDownList runat="server" ID="EstudianteDropDownList" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                    </div>
                        <!--Categoria-->
                     
                              <label for="CategoriaLabel"  style="font-size: small">Categoria</label>
                            <asp:DropDownList runat="server" ID="CategoriaDropDownList" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>


                        <%--Valor--%>
                    
                            <asp:Label ID="Valor" runat="server" Text="Valor">Valor</asp:Label><br />
                            <asp:TextBox ID="ValorTextBox"  runat="server" class="form-control input-sm"></asp:TextBox>
                       


                        <%--Logrado--%>
                      
                            <asp:Label ID="Logrado" runat="server" Text="Logrado">Logrado</asp:Label><br />
                            <asp:TextBox ID="LogradoTextBox" runat="server" class="form-control input-sm"></asp:TextBox>
                            <br />
                       

                  

                            <!--Agregar-->
                      
                            <div class="input-group-append">
                                <br />
                                <asp:Button Text="Agregar" class="btn btn-primary" runat="server" ID="AgregarButton" OnClick="AgregarButton_Click1"/>
                            </div>
                            <br />
                              <br />
                 <asp:GridView ID="DetalleGridView" class="col-md-3 col-md-offset-3" runat="server" DataKeyNames="DetalleId" AllowPaging="true" PageSize="10" ShowHeaderWhenEmpty="false" AutoGenerateDeleteButton="true" CellPadding="4" ForeColor="#333333"  GridLines="None" Width="767px" AutoGenerateColumns="true" OnRowDeleting="Grid_RowDeleting">
                  
                    <AlternatingRowStyle BackColor="White" />

                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />


                </asp:GridView>
                                         <div>
                 <label for="TotalTexbox" class="col-md-3 control-label input-sm" style="font-size: small" >Total</label>
                <div class="col-md-2">
                    <asp:TextBox ID="TotalTextBox" runat="server"  ReadOnly="true"  placeholder="0"  class="form-control input-sm" Style="font-size: small"></asp:TextBox>
                </div>
     
                    <br />    
                        <br />        <br />        <br />        <br />    

                <div class="form-group">
                <asp:Button ID="NuevoButton" runat="server" Text="Nuevo" class="btn btn-primary" OnClick="NuevoButton_Click1" />
                <asp:Button ID="GuardarButton" runat="server" Text="Guardar" class="btn btn-success" ValidationGroup="Guardar" OnClick="GuardarButton_Click1" />
                <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" class="btn btn-danger" OnClick="EliminarButton_Click1"  />
            </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
