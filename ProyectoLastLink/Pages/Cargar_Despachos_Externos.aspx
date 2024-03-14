<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Cargar_Despachos_Externos.aspx.cs" Inherits="ProyectoLastLink.Pages.Cargar_Despachos_Externos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
        <script src="https://cdn.jsdelivr.net/npm/bulma-steps@2.2.1/dist/js/bulma-steps.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
        
    <div class="container box p-12   has-background-light" > 
   
         
        <div class="row">
                <div class="col-md-12">
                     <h4 class="title has-text-centered  has-text-success">Productos Nuevos(Maestro)</h4>
                    <p class="title has-text-centered  has-text-success">&nbsp;</p>
                </div>
          </div>
    <div class="row ">
        <div class="col-md-12">
        
       <article class="message is-link">
          <div class="message-header">
            <p>Carga de Despachos Externos</p>
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





      <div class="row ">
       <div class="col-md-12">
      
      <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="id" BackColor="#D2FFE9" BorderStyle="None">
    <AlternatingRowStyle BackColor="WhiteSmoke" />
    <Columns>
        <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True">
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
        
        <asp:BoundField DataField="Id_Plat" HeaderText="Id Plat" >

        <HeaderStyle BackColor="#EBEBEB" />

     </asp:BoundField>
       
        <asp:BoundField DataField="Origen" HeaderText="Origen" >
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
        
        <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" >
        <HeaderStyle BackColor="#EBEBEB" />
       </asp:BoundField>
      
        
        <asp:BoundField DataField="Fecha_Venta" HeaderText="Fecha" >
      
        
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
        <asp:BoundField DataField="Nombre_Comprador" HeaderText="Nombre">
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
      
        
        <asp:BoundField DataField="Direccion" ReadOnly="True" HeaderText="Dirección">
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
        
        <asp:BoundField DataField="Correo_electronico" HeaderText="Correo Electrónico" ReadOnly="True">
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
      
        
    
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



      </div>
     </div>
        <br />
        <div class="row ">
          <div class="col-md-12">
         <asp:Button runat="server" ID="Button2" class="button is-large is-fullwidth" OnClick="Button2_Click" Text="Cargar Pedidos" Visible="false"/>    </div>
         
        </div> 

  </div> 
  

   </asp:Content>
