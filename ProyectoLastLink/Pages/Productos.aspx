<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="ProyectoLastLink.Pages.Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
        <script src="https://cdn.jsdelivr.net/npm/bulma-steps@2.2.1/dist/js/bulma-steps.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <br />
    <div clss="contanier box p-12 has-background-Light">
                <div class="steps" id="stepsDemo">
                  <div class="step-item is-active is-success">
                    <div class="step-marker">1</div>
                    <div class="step-details">
                      <p class="step-title  has-text-danger">Productos Nuevos</p>
                    </div>
                  </div>
                  <div class="step-item">
                    <div class="step-marker">2</div>
                    <div class="step-details">
                      <p class="step-title">Ingreso a Bodega Por Unidad</p>
                    </div>
                  </div>
                  <div class="step-item">
                    <div class="step-marker">3</div>
                    <div class="step-details">
                      <p class="step-title">Ingreso a Bodega Por Lotes</p>
                    </div>
                  </div>
                  <div class="step-item">
                    <div class="step-marker">4</div>
                    <div class="step-details">
                      <p class="step-title">Salida de Bodega</p>
                    </div>
                  </div>
  
  
                </div>


    </div>
    
    <br />
<br />
    
    
    <div class="container box p-12   has-background-light" > 
   
         
        <div class="row">
                <div class="col-md-12">
                     <h4 class="title has-text-centered  has-text-success">Productos Nuevos(Maestro)</h4>
                             <p class="title has-text-centered  has-text-link">Formato:<asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><img src="../imagenes/migrar_excel.png" height="100" width="80" /></asp:LinkButton>
         </p>
    

                    <p class="title has-text-centered  has-text-success">&nbsp;</p>
                </div>
          </div>
    <div class="row ">
        <div class="col-md-12">
        
       <article class="message is-link">
          <div class="message-header">
            <p>Carga de Productos Nuevos</p>
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
      
      <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="id_producto" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit"  BackColor="#D2FFE9" BorderStyle="None">
    <AlternatingRowStyle BackColor="WhiteSmoke" />
    <Columns>
        <asp:BoundField DataField="id_producto" HeaderText="Id" ReadOnly="True">
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
        
        <asp:BoundField DataField="Marca" HeaderText="Marca Producto" >

        <HeaderStyle BackColor="#EBEBEB" />

     </asp:BoundField>
       
        <asp:BoundField DataField="Nombre" HeaderText="Nombre Producto" >
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
        
        <asp:BoundField DataField="Stock_inicial" HeaderText="Stock Inicial" >
        <HeaderStyle BackColor="#EBEBEB" />
       </asp:BoundField>
      
        
        <asp:BoundField DataField="Valor_minimo" HeaderText="Valor Minimo" >
      
        
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
        <asp:BoundField DataField="sku" HeaderText="SKU">
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
      
        
        <asp:BoundField DataField="id_seller" ReadOnly="True" HeaderText="Id Seller">
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
        
        <asp:BoundField DataField="Nombre_seller" HeaderText="Nombre Seller" ReadOnly="True">
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
      
        
        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion">
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
      
        
        <asp:CommandField ButtonType="Button" ShowEditButton="True" HeaderText="Modificar" >
        <ControlStyle  CssClass="button is-success is-light" Font-Size="small"/>
        <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" Font-Italic="False" />
        </asp:CommandField>
        
    
    
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







                 <asp:GridView ID="GridView2"  runat="server" Width="100%" AutoGenerateColumns="False" Visible="false">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="Columna" HeaderText="Columna" ReadOnly="True">
        </asp:BoundField>
        <asp:BoundField DataField="Columna0" HeaderText="Columna0" >
     </asp:BoundField>
        <asp:BoundField DataField="Columna1" HeaderText="Columna1" >
        </asp:BoundField>
        <asp:BoundField DataField="Columna2" HeaderText="Columna2" >
       </asp:BoundField>
        <asp:BoundField DataField="Columna3" HeaderText="Columna3" >
        </asp:BoundField>
        <asp:BoundField DataField="Columna4" HeaderText="Columna4">
        </asp:BoundField>
        <asp:BoundField DataField="Columna5" ReadOnly="True" HeaderText="Columna5">
        </asp:BoundField>
        <asp:BoundField DataField="Columna6" HeaderText="Columna6" ReadOnly="True">
        </asp:BoundField>
        <asp:BoundField DataField="Columna7" HeaderText="Columna7">
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
         <asp:Button runat="server" ID="Button2" class="button is-large is-fullwidth" OnClick="Button2_Click" Text="Cargar en Base de datos" Visible="false"/>    </div>
         
        </div> 

  </div> 
  








          






   </asp:Content>

