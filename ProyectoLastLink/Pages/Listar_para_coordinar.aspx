<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Listar_para_coordinar.aspx.cs" EnableEventValidation="false" Inherits="ProyectoLastLink.Pages.Listar_para_coordinar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
       <div class="container box p-12   has-background-light "> 
      <div class="row ">
          <div class="col-md-12">
      <h1 class="title has-text-centered  has-text-success">Listado de Pedidos</h1>
                                                <p class="title has-text-centered  has-text-link">Formato:<asp:LinkButton ID="LinkButton2" runat="server" OnClick="Button6_Click"><img src="../imagenes/migrar_excel.png" height="100" width="80" /></asp:LinkButton>
</p>

     
              <p class="title has-text-centered  has-text-success"></p>
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
                                         <span class="has-text-danger" >Warning</span>
                                     </div>
                                       <br />
                                       
                                       Permite Realizar Busqueda de Pedidos<br /><br />
                                   <asp:Button runat="server" ID="Button3" class="button is-medium" Text="Buscar por Fecha" OnClick="Button3_Click" Visible="True"/>    
                                   <asp:Button runat="server" ID="Button4" class="button is-medium" Text="Buscar por Estado" OnClick="Button4_Click" Visible="True"/>    
  
                             </div>          
                        </article>
                    
                
                   </asp:Panel>
            
    
                 
   </div>
               
  
    <br />
      <div class="row align-content-center">
          <asp:Panel ID="Panel3" runat="server" Visible="false">
                     <div class="row align-content-center">
                            <div class="col-md-6 col-sm-6 col-xs-6   pad-adjust">
                             <div class="field">
                                 <div class="control has-icons-left has-icons-right">
                                      <asp:TextBox runat="server" ID="BuscarNombre" CssClass="input is-success" type="text" placeholder="Buscador"  ForeColor="Red"></asp:TextBox>
                                      <span class="icon is-small is-left"> 
                                        <i class="fas fa-user"></i> 
                                      </span> 
                                 </div>     
                             </div>
                           </div>

                                 <div class="col-md-6 col-sm-6 col-xs-6   pad-adjust">
                                  <div class="field">
                                      <div class="control has-icons-left has-icons-right">
                                          <asp:Button runat="server" ID="Button1" CssClass="button is-warning" OnClick="Button1_Click"  Text="Buscar Por Seller o Codigo Pedido"/>
                                       </div>     
                                  </div>

                                </div>


                     
                        
                              
                          </div>
                   
            </asp:Panel>
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
      <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  BackColor="#D2FFE9" AllowPaging="True" PageSize="30" OnPageIndexChanging="GridView1_PageIndexChanging" BorderStyle="None">
          <AlternatingRowStyle BackColor="WhiteSmoke" />
          <Columns>
                         


              <asp:BoundField DataField="Id_Plat" HeaderText="Plat"  >
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Nombre_Comprador" HeaderText="Nombre">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Telefono" HeaderText="Teléfono">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Correo_electronico" HeaderText="E-mail">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
                         


              <asp:BoundField DataField="Direccion" HeaderText="Dirección">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Comuna" HeaderText="Comuna">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Proveedor" HeaderText="Proveedor">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Fecha_Venta" HeaderText="Fecha" >
                <HeaderStyle BackColor="#99FFCC" />
           </asp:BoundField>
              <asp:BoundField DataField="Estado" HeaderText="Estado">
              <HeaderStyle BackColor="#99FFCC" />
              <ItemStyle Font-Bold="True" ForeColor="#003366" />
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




           <asp:GridView ID="GridView2"  runat="server" Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="GridView2_PageIndexChanging"  BackColor="#D2FFE9" BorderStyle="None" Visible="false">
            <AlternatingRowStyle BackColor="WhiteSmoke" />
            <Columns>
               <asp:BoundField DataField="Id_Plat" HeaderText="Plat"  >
                <HeaderStyle BackColor="#99FFCC" />
                </asp:BoundField>
                <asp:BoundField DataField="Nombre_Comprador" HeaderText="Nombre">
                <HeaderStyle BackColor="#99FFCC" />
                </asp:BoundField>
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono">
                <HeaderStyle BackColor="#99FFCC" />
                </asp:BoundField>
                <asp:BoundField DataField="Correo_electronico" HeaderText="E-mail">
                <HeaderStyle BackColor="#99FFCC" />
                </asp:BoundField>
                <asp:BoundField DataField="Direccion" HeaderText="Dirección">
                <HeaderStyle BackColor="#99FFCC" />
                </asp:BoundField>
                <asp:BoundField DataField="Comuna" HeaderText="Comuna">
                <HeaderStyle BackColor="#99FFCC" />
                </asp:BoundField>
                <asp:BoundField DataField="Proveedor" HeaderText="Proveedor">
                <HeaderStyle BackColor="#99FFCC" />
                </asp:BoundField>
                <asp:BoundField DataField="Fecha_Venta" HeaderText="Fecha" >
                  <HeaderStyle BackColor="#99FFCC" />
                 </asp:BoundField>
                <asp:BoundField DataField="Estado" HeaderText="Estado">
                <HeaderStyle BackColor="#99FFCC" />
                <ItemStyle Font-Bold="True" ForeColor="#003366" />
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

        




    <br /> 
      </div>
            





    
    
       


</div>




 </asp:Content>

