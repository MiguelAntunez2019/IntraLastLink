<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ListaPedidos.aspx.cs" EnableEventValidation="false" Inherits="ProyectoLastLink.Pages.ListaPedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
       <div class="container box p-12   has-background-light "> 
            <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
        <script type="text/javascript">
            function searchKeyPress(e) {
                e = e || window.event;
                if (e.keyCode == 13) {
                    Swal.fire({
                        title: 'Sitio web no admite el uso de Enter en esta página',
                        icon: 'warning',
                        confirmButtonText: 'Aceptar'
                    });
                    e.preventDefault();
                    return false;
                }
                return true;
            }
        </script>
      <div class="row ">
          <div class="col-md-12">
      <h1 class="title has-text-centered  has-text-success">Listado de Pedidos</h1>
               <p class="title has-text-centered  has-text-success"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="Button6_Click"><img src="../imagenes/migrar_excel.png" height="100" width="80" /></asp:LinkButton>
               </p>
           </div>
      </div>
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
              <div class="row ">
                   <asp:Panel ID="Panel1" runat="server">
                  
                        <article class="message is-success">
                                   <div class="message-body">
                                       <div class="icon-text">
                                       <span class="icon has-text-warning">
                                         <i class="fas fa-exclamation-triangle"></i>
                                       </span>
                                         <span class="has-text-danger" >Permite Realizar Busqueda de Pedidos</span>
                                     </div>
                                       <br />
                                                                             
                                   <asp:Button runat="server" ID="Button3" class="button is-medium" Text="Buscar por Fecha" OnClick="Button3_Click" Visible="True"/>    
                                   <asp:Button runat="server" ID="Button4" class="button is-medium" Text="Buscar por Otros" OnClick="Button4_Click" Visible="True"/>    
  
                             </div>          
                        </article>
                    
                
                   </asp:Panel>
            
    
                 
   </div>
               
        <div class="row align-content-center">
      

    <ContentTemplate>
        <asp:Panel ID="Panel3" runat="server" Visible="false">
            <div class="row align-content-center">
                <div class="col-md-6 col-sm-6 col-xs-6 pad-adjust">
                    <div class="field">
                        <div class="control has-icons-left has-icons-right">
                            <asp:TextBox runat="server" ID="BuscarNombre" CssClass="input is-success" type="text" placeholder="Buscador" ForeColor="Red" onkeydown="return searchKeyPress(event);"></asp:TextBox>
                            <span class="icon is-small is-left">
                                <i class="fas fa-user"></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-6 pad-adjust">
                    <div class="field">
                        <div class="control has-icons-left has-icons-right">
                            <asp:Button runat="server" ID="Button1" CssClass="button is-warning" OnClick="Button1_Click" Text="Buscar Por Seller o Codigo Pedido" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>

           </div>

           

           <div class="row align-content-center">
                        <asp:Panel ID="Panel_Fecha" runat="server" Visible="false">   
    
                             <div class="row align-content-center">

                                  <div class="col-md-3 col-sm-3 col-xs-3   pad-adjust">
                                    <div class="field">
                                        <div class="control has-icons-left has-icons-right">
                                                    <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged">
                                                        <TitleStyle BackColor="#99FFCC" />
                                                    </asp:Calendar>
                                        </div>     
                                    </div>
                                  </div>
                                <div class="col-md-3 col-sm-3 col-xs-3   pad-adjust">
                                 <div class="field">
                                     <div class="control has-icons-left has-icons-right">
                                         <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged">
                                             <TitleStyle BackColor="#99FFCC" />
                                         </asp:Calendar>
                                          <br />
                                     </div>     
                                 </div>
                               <div class="col-md-24 col-sm-24 col-xs-24  pad-adjust">
                                <div class="field">
                                    <div class="control has-icons-left has-icons-right">
                                   <asp:Button runat="server" ID="Button5" CssClass="button is-warning" OnClick="Button5_Click"  Text="Buscar Por Fecha"/>
                                    </div>     
                                </div>
                              </div>
                         </div>
                       </div>
                    </asp:Panel> 
                      <asp:TextBox runat="server" ID="BuscarNombre_calendario" type="text"  Visible="false"></asp:TextBox>
                      <asp:TextBox runat="server" ID="BuscarNombre2_calendario" type="text"  Visible="false"></asp:TextBox>
     
            </div>
                      

     <br />
  <br />           

    <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
      <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="id" AllowPaging="True" PageSize="30"  OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="#D2FFE9" BorderStyle="None"  OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
          <AlternatingRowStyle BackColor="WhiteSmoke" />
          <Columns>
              <asp:BoundField DataField="id" HeaderText="Id">
              <HeaderStyle BackColor="#99FFCC" />
              <ItemStyle Height="10px" Width="80px" />
              </asp:BoundField>
              <asp:BoundField DataField="Origen" HeaderText="Origen">
              <HeaderStyle BackColor="#99FFCC" />
              <ItemStyle Height="50px" Width="100px" />
              </asp:BoundField>
              <asp:BoundField DataField="Id_Plat" HeaderText="Pedido"  >
              <HeaderStyle BackColor="#99FFCC" />
                  <ItemStyle Font-Bold="True" Height="80px" Width="150px" />
              </asp:BoundField>
              <asp:BoundField DataField="Proveedor" HeaderText="Proveedor">
              <HeaderStyle BackColor="#99FFCC" />
              <ItemStyle Height="80px" Width="120px" />
              </asp:BoundField>
              <asp:BoundField DataField="Nombre_Comprador" HeaderText="Nombre">
              <HeaderStyle BackColor="#99FFCC" />
              <ItemStyle Height="100px" Width="150px" />
              </asp:BoundField>
              <asp:BoundField DataField="Direccion" HeaderText="Dirección">
              <HeaderStyle BackColor="#99FFCC" />
              <ItemStyle Height="100px" Width="150px" />
              </asp:BoundField>
              <asp:BoundField DataField="Telefono" HeaderText="Teléfono" Visible="False">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Correo_electronico" HeaderText="E-mail">
              <HeaderStyle BackColor="#99FFCC" />
              <ItemStyle Height="100px" Width="150px" />
              </asp:BoundField>
              <asp:BoundField DataField="Fecha_Venta" HeaderText="Fecha" >
                <HeaderStyle BackColor="#99FFCC" />
              <ItemStyle Height="100px" Width="150px" />
             </asp:BoundField>
              <asp:BoundField DataField="Estado" HeaderText="Estado">
              <HeaderStyle BackColor="#99FFCC" />
              <ItemStyle Font-Bold="True" ForeColor="#003366" Height="80px" Width="120px" />
              </asp:BoundField>
              <asp:BoundField DataField="comentario" HeaderText="Nota">
              <HeaderStyle BackColor="#99FFCC" Font-Bold="True" Height="100px" Width="150px" />
              <ItemStyle Font-Bold="True" ForeColor="Red" Height="100px" Width="150px" />
              </asp:BoundField>
              <asp:BoundField DataField="Regalo" HeaderText="Regalo">
              <HeaderStyle BackColor="#99FFCC" />
              <ItemStyle Height="100px" Width="100px" />
              </asp:BoundField>
             <asp:CommandField ButtonType="Button" ShowSelectButton="True" HeaderText="Acciones" SelectText="Ver +" >
              <ControlStyle  CssClass="button is-link is-light" Font-Size="small"/>
              <HeaderStyle BackColor="#99FFCC" />
              </asp:CommandField>
             <asp:CommandField ButtonType="Button" ShowDeleteButton="True" HeaderText="" DeleteText="Anular">
             <ControlStyle CssClass="button is-danger is-light" Font-Size="small"/>
             <HeaderStyle BackColor="#99FFCC" />
             </asp:CommandField>
          </Columns>
          <FooterStyle BackColor="White" ForeColor="#000066" />
          <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
          <PagerSettings Mode="NextPrevious" />
          <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
          <RowStyle ForeColor="#000066"/>
          <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
          <SortedAscendingCellStyle BackColor="#F1F1F1" />
          <SortedAscendingHeaderStyle BackColor="#007DBB" />
          <SortedDescendingCellStyle BackColor="#CAC9C9" />
          <SortedDescendingHeaderStyle BackColor="#00547E" />
      </asp:GridView>
    <br /> 
         <asp:GridView ID="GridView2"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="id" BackColor="#D2FFE9" BorderStyle="None" Visible="False">
     <AlternatingRowStyle BackColor="WhiteSmoke" />
     <Columns>
         <asp:BoundField DataField="id" HeaderText="Id">
         <HeaderStyle BackColor="#99FFCC" />
         </asp:BoundField>
         <asp:BoundField DataField="Origen" HeaderText="Origen">
         <HeaderStyle BackColor="#99FFCC" />
         </asp:BoundField>
         <asp:BoundField DataField="Id_Plat" HeaderText="Plat"  >
         <HeaderStyle BackColor="#99FFCC" />
         </asp:BoundField>
         <asp:BoundField DataField="Proveedor" HeaderText="Proveedor">
         <HeaderStyle BackColor="#99FFCC" />
         </asp:BoundField>
         <asp:BoundField DataField="Nombre_Comprador" HeaderText="Nombre">
         <HeaderStyle BackColor="#99FFCC" />
         </asp:BoundField>
         <asp:BoundField DataField="Direccion" HeaderText="Dirección">
         <HeaderStyle BackColor="#99FFCC" />
         </asp:BoundField>
         <asp:BoundField DataField="Telefono" HeaderText="Teléfono" Visible="False">
         <HeaderStyle BackColor="#99FFCC" />
         </asp:BoundField>
         <asp:BoundField DataField="Correo_electronico" HeaderText="E-mail">
         <HeaderStyle BackColor="#99FFCC" />
         </asp:BoundField>
         <asp:BoundField DataField="Fecha_Venta" HeaderText="Fecha" >
           <HeaderStyle BackColor="#99FFCC" />
        </asp:BoundField>
         <asp:BoundField DataField="Estado" HeaderText="Estado">
         <HeaderStyle BackColor="#99FFCC" />
         <ItemStyle Font-Bold="True" ForeColor="#003366" />
         </asp:BoundField>
      
         <asp:BoundField DataField="Regalo" HeaderText="Regalo">
         <HeaderStyle BackColor="#99FFCC" />
         </asp:BoundField>
      
     </Columns>
     <FooterStyle BackColor="White" ForeColor="#000066" />
     <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
     <PagerSettings Mode="NextPrevious" />
     <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
     <RowStyle ForeColor="#000066"/>
     <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
     <SortedAscendingCellStyle BackColor="#F1F1F1" />
     <SortedAscendingHeaderStyle BackColor="#007DBB" />
     <SortedDescendingCellStyle BackColor="#CAC9C9" />
     <SortedDescendingHeaderStyle BackColor="#00547E" />
 </asp:GridView>








      </div>
            





    
    
          <asp:LinkButton  ID="LinkButton1" runat="server" CssClass="modalPopup" Style="display:none">LinkButton</asp:LinkButton>
             <div class="container" >
                 <asp:Panel ID="Panel2" class="has-background-light"   runat="server" Height="700px" ScrollBars="Auto" Width="1000px">
                    <div class="row ">
                        
                                       
                        <div class="container box p-12   has-background-light "> 
                       
                            
                              <div class="row ">
                                <div class="col-md-12">
                                 <h4 class="title has-text-centered  has-text-link">Información de la Compra</h4>
                                    <p class="title has-text-centered  has-text-success">&nbsp;</p>
                                 </div>
                                </div>
                        

                            <div class="table-container">
                                              <table class="table">
                                                <thead>
                                                </thead>
                                                <tbody>
                                                <tr>
                                                  <td>Origen</td>
                                                  <td> <asp:Label ID="Origen" runat="server" Text="Label"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                   <td>Vendedor</td>
                                                   <td><asp:Label ID="Proveedor" runat="server" Text="Label"></asp:Label></td>
                                                 </tr>
                                                 <tr>
                                                   <td>ID Plataforma</td>
                                                     <td><asp:Label ID="Id_plat" runat="server" Text="Label"></asp:Label></td>
                                                 </tr>
                                                <tr>
                                                   <td>Nombre Comprador</td>
                                                 <td><asp:Label ID="Nombre_Comprador" runat="server" Text="Label"></asp:Label></td>
                                           
                                                 </tr>
                                                  <tr>
                                                 <td>Correo Electrónico Comprador</td>
                                                 <td><asp:Label ID="Correo_electronico" runat="server" Text="Label"></asp:Label></td>
                                               </tr>
                                                <tr>
                                                   <td>Dirección Comprador</td>
                                                      <td><asp:Label ID="Direccion" runat="server" Text="Label"></asp:Label></td>
                                           
                                                 </tr> 

                                                    <tr>
                                                     <td>Imprimir Etiqueta </td>
                                                      <td>
                                                              <asp:Button runat="server" ID="ImprimirButon" CssClass="button is-primary" OnClick="btnImprimir_Click"  Text="Imprimir"/>
                                        


                                                      </td>
                                           
                                                    </tr> 



                                                </tbody>
                                              </table>
                            </div>
                        


                        
                        
                        </div>









                    
                    
                    
                    
                    </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 pad-adjust">
                             <div class="field">
                               <div class="control">
                            <br/>
                          <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
                      <asp:GridView ID="GridView11"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="id_prod_ped" AllowPaging="True" PageSize="20"  OnPageIndexChanging="GridView11_PageIndexChanging"  OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" BackColor="#D2FFE9"   BorderStyle="None">
                          <AlternatingRowStyle BackColor="WhiteSmoke" />
                          <Columns>
                              <asp:BoundField DataField="id_prod_ped" HeaderText="ID">
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Id_Plat" HeaderText="Id Plat" Visible="False" ReadOnly="True">
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Nombre_Prod_ped" HeaderText="Nombre Producto" ReadOnly="True">
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Cantidad_ped" HeaderText="Cantidad" ReadOnly="True">
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Sku_ped" HeaderText="SKU" >
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>


                              <asp:BoundField DataField="Sku" HeaderText="SKU Bodega">
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>


                              <asp:ImageField DataImageUrlField="url" HeaderText="Imagen" ReadOnly="True">
                                  <HeaderStyle BackColor="#99FFCC" />
                                  <ItemStyle Height="80px" Width="80px" />
                              </asp:ImageField>
                               
                              <asp:CommandField ButtonType="Button" ShowDeleteButton="True" HeaderText="" DeleteText="Eliminar">
                                <ControlStyle CssClass="button is-danger is-light" Font-Size="small"/>
                                <HeaderStyle BackColor="#99FFCC" />
                                </asp:CommandField>
                            

       

                          </Columns>
                          <FooterStyle BackColor="White" ForeColor="#000066" />
                          <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                          <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                          <RowStyle ForeColor="#000066"/>
                          <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                          <SortedAscendingCellStyle BackColor="#F1F1F1" />
                          <SortedAscendingHeaderStyle BackColor="#007DBB" />
                          <SortedDescendingCellStyle BackColor="#CAC9C9" />
                          <SortedDescendingHeaderStyle BackColor="#00547E" />
                      </asp:GridView>
    
                    </div>

                                   <div class="col-md-6 col-sm-6 col-xs-6   pad-adjust">
                                      <div class="field">
                                         <div class="control has-icons-left has-icons-right">
                                             <br />
                                              <br />
                                           <asp:Label runat="server" ID="Label1" Visible="false" Text="Sin Resultados" CssClass="message" />
                     
                                             <asp:Button runat="server" ID="Button2" CssClass="button is-danger" OnClick="Button1_Click"  Text="Cerrar Ventana"/>
                                                
                                                          </div>     
                                                        </div>
                                       f
                                    <div>
                                      </div>
                                    </div>
                                   
                      </div>
                   </div>
   
                           </div>
                        </div>
            </asp:Panel>
          </div>
           <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modal-background" TargetControlID="LinkButton1" PopupControlID="Panel2" CancelControlID="Button2"></ajaxToolkit:ModalPopupExtender>

 
      


</div>




 </asp:Content>

