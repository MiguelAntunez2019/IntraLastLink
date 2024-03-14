<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="CierrePedidos.aspx.cs" Inherits="ProyectoLastLink.Pages.CierrePedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <br />
    
    
   
   
    <div class="container box p-12   has-background-light" > 
   
         
        <div class="row">
                <div class="col-md-12">
                     <h4 class="title has-text-centered  has-text-success">Cierre de Pedidos Entregados</h4>
                                           <p class="title has-text-centered  has-text-link">Formato:<asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><img src="../imagenes/migrar_excel.png" height="100" width="80" /></asp:LinkButton>
   </p>
    
                    <p class="title has-text-centered  has-text-success">&nbsp;</p>
                </div>
          </div>
    <div class="row ">
        <div class="col-md-12">
        
       <article class="message is-link">
          <div class="message-header">
            <p>Cierre de Pedidos</p>
          </div>
          <div class="message-body">
              <div class="icon-text">
              <span class="icon has-text-warning">
                <i class="fas fa-exclamation-triangle"></i>
              </span>
                <span class="has-text-danger" >Warning</span>
            </div>
              <br />
              <span class="icon">
                <i class="fas fa-arrow-right">
                </i>
             </span>
              Para Cargar Archivo Excel debe Presionar Boton Elegir Archivo 
                <div class="file is-warning is-boxed">
                  <label class="file-label">
                    <input class="file-input" type="file" name="resume">
                    <span class="file-cta">
                      <span class="file-icon">
                        <i class="fas fa-cloud-upload-alt"></i>
                      </span>
                      <span class="file-label">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                      </span>
                    </span>
                  </label>
                </div>
              <br />
              <br />
               <span class="icon">
                   <i class="fas fa-arrow-right">
                   </i>
                </span>
              Para Visualizar la Informacción encontrada en archivo excel de presionar el siguiente link:
              
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click1" CssClass="has-text-success">Subir Data a Cargar</asp:LinkButton>
               <br />
          </div>

        </article>
        </div>
 </div>
    <br />


                         <asp:GridView ID="GridView2"  runat="server" Width="100%" AutoGenerateColumns="False" Visible="False">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="Columna" HeaderText="Columna" ReadOnly="True">
        </asp:BoundField>
        <asp:BoundField DataField="Columna0" HeaderText="Columna0" >
     </asp:BoundField>
      
        
    
        <asp:BoundField DataField="Columna1" HeaderText="Columna1" />
      
        
    
    </Columns>
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
    <RowStyle ForeColor="#000066" />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F1F1F1" />
    <SortedAscendingHeaderStyle BackColor="#007DBB" />
    <SortedDescendingCellStyle BackColor="#CAC9C9" />
    <SortedDescendingHeaderStyle BackColor="#00547E" />
</asp:GridView>




        <div class="row ">
          <div class="col-md-12">
         <asp:Button runat="server" ID="Button2" class="button is-info" OnClick="Button2_Click" Text="Cargar en Base de datos" Visible="false" Width="100%"/>    </div>
         
        </div> 

  </div> 
  

   </asp:Content>

