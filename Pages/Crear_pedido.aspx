<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Crear_pedido.aspx.cs" Inherits="ProyectoLastLink.Pages.Crear_pedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="container box p-12   has-background-light"> 
 
      <div class="row ">
          <div class="col-md-12">
      <h1 class="title has-text-centered  has-text-success">Crear Pedidos</h1>
              <p class="title has-text-centered  has-text-success"></p>
           </div>
      </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <br />
      
       
          <div class="notification is-primary">
            <strong>Sección de Información De la Venta Origen</strong>
          </div>
              
        

       <div class="row ">
         <div class="col-md-4 col-sm-4 col-xs-4">
         
               <div class="select"> 
                  
                          <asp:DropDownList ID="Origen" runat="server" CssClass="input is-success" AutoPostBack="true">

                       </asp:DropDownList>
                  
              </div>
            
          </div>



            <div class="col-md-4 col-sm-4 col-xs-4">
            <div class="field">
                 <div class="select"> 
                     <asp:DropDownList ID="Seller" runat="server" CssClass="input is-success" AutoPostBack="true">

                                </asp:DropDownList>
                     
                  
              </div>
             </div>
            </div>
       
    <div class="col-md-4 col-sm-4 col-xs-4">
        <div class="field">
            <div class="control has-icons-left has-icons-right">
                <asp:TextBox runat="server" ID="Id_Plataforma" CssClass="input is-success" type="text" placeholder="Ingrese Id Plataforma">
                 </asp:TextBox>
                <span class="icon is-small is-left"> 
                     <i class="fas fa-user""></i> 
                </span> 
            </div>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Id Plataforma es Obligatorio" ValidationGroup="Registrar" ControlToValidate="Id_Plataforma" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
             </div>
        
        </div>
    </div>
          
          <div class="notification is-primary">
          <strong>Sección de Información De la Venta Datos de Comprador</strong>
           </div>
      <div class="row">
          <div class="col-md-4 col-sm-4 col-xs-4">
        
              <div class="field">
           
                    <div class="control has-icons-left has-icons-right">
                        <asp:TextBox runat="server" ID="Nombre_comprador" CssClass="input is-success" type="text" placeholder="Ingrese Nombre Comprador">
                    
                        </asp:TextBox>
                        <span class="icon is-small is-left"> 
                             <i class="fas fa-user"></i> 
                        </span> 
                    </div>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Nombre es Obligatorio" ValidationGroup="Registrar" ControlToValidate="Nombre_comprador" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
   
           </div>
        </div>
            <div class="col-md-4 col-sm-4 col-xs-4">

                  <div class="field">
   
                        <div class="control has-icons-left has-icons-right">
                            <asp:TextBox runat="server" ID="Apellido_comprador" CssClass="input is-success" type="text" placeholder="Ingrese Apellido Comprador">
            
                            </asp:TextBox>
                            <span class="icon is-small is-left"> 
                                 <i class="fas fa-user"></i> 
                            </span> 
                        </div>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Apellido Comprador es Obligatorio" ValidationGroup="Registrar" ControlToValidate="Apellido_comprador" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
   
               </div>
            </div>

              <div class="col-md-4 col-sm-4 col-xs-4">
              <div class="field"> 
                 
                 <div class="select"> 
    
                      <asp:DropDownList ID="comuna_seller" runat="server" CssClass="input is-success" AutoPostBack="true">

                      </asp:DropDownList>
                  </div> 
             </div>
             </div>

          <br />

           <br />
               <div class="col-md-4 col-sm-4 col-xs-4">
                <div class="field">
           
                    <div class="control has-icons-left has-icons-right">
                        <asp:TextBox runat="server" ID="Direccion_comprador" CssClass="input is-success" type="text" placeholder="Ingrese Dirección Comprador"></asp:TextBox>
                    <span class="icon is-small is-left"> 
                      <i class="fas fa-user"></i> 
                    </span> 
                    </div>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Dirección comprador es Obligatorio" ValidationGroup="Registrar" ControlToValidate="Direccion_comprador" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
   
                 </div>
               </div>
                   <div class="col-md-4 col-sm-4 col-xs-4">
                     <div class="field">
           
                         <div class="control has-icons-left has-icons-right">
                             <asp:TextBox runat="server" ID="Telefono_comprador" CssClass="input is-success" type="text" placeholder="Ingrese telefono Comprador"></asp:TextBox>
                         <span class="icon is-small is-left"> 
                           <i class="fas fa-user"></i> 
                         </span> 
                         </div>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Telefono Comprador es Obligatorio" ValidationGroup="Registrar" ControlToValidate="Telefono_comprador" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
   
                      </div>
                    </div>
              <div class="col-md-4 col-sm-4 col-xs-4">
                <div class="field">
          
                  <div class="control has-icons-left has-icons-right">
                      <asp:TextBox runat="server" ID="Correo_electronico_comp" CssClass="input is-success" type="text" placeholder="Ingrese Correo Electronico Comprador" ForeColor="Red"></asp:TextBox>
                  <span class="icon is-small is-left"> 
                    <i class="fas fa-envelope"></i> 
                 </span> 
                 </div>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="* Correo Electrónico obligatorio" ValidationGroup="Registrar" ControlToValidate="Correo_electronico_comp" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
   
                   </div>
            </div>
        
              
              
             

          <div class="row">
                  
                          <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="field">
          
                      <div class="control has-icons-left has-icons-right">
                          <asp:TextBox runat="server" ID="Nota_Cliente" TextMode="MultiLine" Rows="10" CssClass="input is-success" type="text" placeholder="Ingrese Nota " ForeColor="Red"></asp:TextBox>
                      <span class="icon is-small is-left"> 
                        <i class="fas fa-info-circle"></i> 
                     </span> 
                     </div>

                       </div>
                </div>
      </div>



      
                    <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 pad-adjust">
                             <div class="field">
                            <div class="control">
                                     <br />
                                <asp:Button runat="server" ID="Registrar" CssClass="button is-link" OnClick="Registrar_Click" Text="Registrar Compra" ValidationGroup='Registrar' CausesValidation="true" />
                                <br />
           
                            </div>
                       </div>
       
                  </div>
                </div>
 </div>

</div>

<div class="container align-content-center" > 

 

 
<div class="row align-content-center">
    <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
        <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  OnSelectedIndexChanged="GridView1_SelectedIndexChanged"  DataKeyNames="id"   BackColor="#D2FFE9" BorderStyle="None">
      <AlternatingRowStyle BackColor="WhiteSmoke" />
      <Columns>
          <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True" >
          <FooterStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
          <HeaderStyle BackColor="#99FFCC" />
          </asp:BoundField>
          <asp:BoundField DataField="Id_Plat" HeaderText="Pedido">
          <FooterStyle BackColor="#99FFCC" />
          <HeaderStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
          </asp:BoundField>
          <asp:BoundField DataField="Origen" HeaderText="Origen">
          <FooterStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
          <HeaderStyle BackColor="#99FFCC" />
          </asp:BoundField>
         
          <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" >
          <FooterStyle BackColor="#99FFCC" />
          <HeaderStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
          </asp:BoundField>
          
          
          <asp:BoundField DataField="Fecha_Venta" HeaderText="Fecha">
          <FooterStyle BackColor="#99FFCC" />
          <HeaderStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
          </asp:BoundField>
          <asp:BoundField DataField="Nombre_Comprador" HeaderText="Nombre">
          <FooterStyle BackColor="#99FFCC" />
          <HeaderStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
          </asp:BoundField>
          
          
          <asp:BoundField DataField="Direccion" HeaderText="Direccion">
          <HeaderStyle BackColor="#99FFCC" />
          </asp:BoundField>
          <asp:BoundField DataField="Correo_electronico" HeaderText="Correo Electronico">
          <HeaderStyle BackColor="#99FFCC" />
          </asp:BoundField>
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" HeaderText="Productos" SelectText="Agregar +" >
              <ControlStyle  CssClass="button is-danger is-light" Font-Size="small"/>
              <HeaderStyle BackColor="#99FFCC" />
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




    </div>
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
                                                                   <td>ID Plataforma</td>
                                                                    <td>
                                                                      <asp:TextBox ID="Id_plat2"  runat="server" CssClass="input is-success" type="text" ForeColor="Red" ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                  </tr>
                                                                 <tr>
                                                                    <td>Nombre Producto</td>
                                                                    <td> 
                                                                            <div class="select"> 
                                                                                 <asp:DropDownList ID="Nombre_Producto" runat="server" CssClass="input is-success">

                                                                                    </asp:DropDownList>
                                                                            </div>
                                                                        
                                                                                                                                        
                                                                 
                                                                  </tr>
                                                                  <tr>
                                                                     <td>Cantidad</td>
                                                                     <td>
                                                                    
                                                                         
                                                                        <div class="select"> 
                                                                             <asp:DropDownList ID="cantidad" runat="server" CssClass="input is-success">

                                                                                </asp:DropDownList>
                                                                         </div>  
                                                                     </td>
                                                                   </tr>
                                                                  <tr>
                                                                    <td>

                                                                              <div class="field">
                                                                                 <div class="control">
                                                                                          <br />
                                                                                     <asp:Button runat="server" ID="ButtonArticulo" CssClass="button is-link" OnClick="ButtonArticulo_Click" Text="Registrar Articulo"/>
                                                                                     <br />
           
                                                                                 </div>
                                                                            </div>



                                                                    </td>
                                                                  </tr>
                                                                      
                                                                  
                                                                  
                                                                  </tbody>
                                                           
                                                                    
                                                                
                                                                
                                                                
                                                                </table>
                                              </div>
                                          </div>
                                      </div>

                                       <div class="row align-content-center">
                                            <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
                                                <asp:GridView ID="GridView2"  runat="server" Width="100%" AutoGenerateColumns="False"   DataKeyNames="id_prod_ped"   BackColor="#D2FFE9" BorderStyle="None">
                                              <AlternatingRowStyle BackColor="WhiteSmoke" />
                                              <Columns>
                                                  <asp:BoundField DataField="id_prod_ped" HeaderText="ID" ReadOnly="True" >
                                                  <FooterStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
                                                  <HeaderStyle BackColor="#99FFCC" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="Id_Plat" HeaderText="Id Plat">
                                                  <FooterStyle BackColor="#99FFCC" />
                                                  <HeaderStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="Nombre_Prod_ped" HeaderText="Nombre Producto">
                                                  <FooterStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
                                                  <HeaderStyle BackColor="#99FFCC" />
                                                  </asp:BoundField>
         
                                                  <asp:BoundField DataField="Cantidad_ped" HeaderText="Cantidad" >
                                                  <FooterStyle BackColor="#99FFCC" />
                                                  <HeaderStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
                                                  </asp:BoundField>
          
          
                                                  <asp:BoundField DataField="Sku_ped" HeaderText="SKU">
                                                  <FooterStyle BackColor="#99FFCC" />
                                                  <HeaderStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="ultima_Fecha" HeaderText="Fecha">
                                                  <FooterStyle BackColor="#99FFCC" />
                                                  <HeaderStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
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
                                          <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-6   pad-adjust">
                                               <div class="field">
                                                  <div class="control has-icons-left has-icons-right">
                                                      <br />
                                                       <br />
                                    
                                                      <asp:Button runat="server" ID="Button2" CssClass="button is-danger" OnClick="Button1_Click"  Text="Cerrar Ventana"/>
             
                                                                   </div>     
                                                   </div>

                                             <div>
                                               </div>
                                             </div>
                                            </div>
                              </asp:Panel>
                            </div>
                 <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modal-background" TargetControlID="LinkButton1" PopupControlID="Panel2" CancelControlID="Button2"></ajaxToolkit:ModalPopupExtender>

</div>
</asp:Content>

