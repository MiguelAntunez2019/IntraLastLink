<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Seller.aspx.cs" Inherits="ProyectoLastLink.Pages.Seller" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="container box p-12   has-background-light"> 
 
      <div class="row ">
          <div class="col-md-12">
      <h1 class="title has-text-centered  has-text-success">Administrar Sellers</h1>
              <p class="title has-text-centered  has-text-success">&nbsp;</p>
           </div>
      </div>

      <div class="row ">
        <div class="col-md-12">
       </div>
     </div>


       <div class="row ">
         <div class="col-md-4 col-sm-4 col-xs-4">
          <div class="field">
              
              <div class="control has-icons-left has-icons-right">
                 
                  <asp:TextBox runat="server" ID="rut_seller" CssClass="input is-success"  type="text" placeholder="Ingrese Rut Seller">
              
                  </asp:TextBox>
                  <span class="icon is-small is-left"> 
                       <i class="fas fa-user"></i> 
                  </span> 
              </div>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Rut es Obligatorio" ValidationGroup="Registrar" ControlToValidate="rut_seller" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
           </div>
          </div>



            <div class="col-md-4 col-sm-4 col-xs-4">
            <div class="field">
                <div class="control has-icons-left has-icons-right">
                  <asp:TextBox runat="server" ID="nombre_seller" CssClass="input is-success" type="text" placeholder="Ingrese Nombre Seller">
      
                  </asp:TextBox>
                  <span class="icon is-small is-left"> 
                       <i class="fas fa-user"></i> 
                  </span> 
              </div>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Nombre es Obligatorio" ValidationGroup="Registrar" ControlToValidate="nombre_seller" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
   
               </div>
            </div>
       
    <div class="col-md-4 col-sm-4 col-xs-4">
        <div class="field">
            <div class="control has-icons-left has-icons-right">
                <asp:TextBox runat="server" ID="direccion_seller" CssClass="input is-success" type="text" placeholder="Ingrese Dirección Seller">
                 </asp:TextBox>
                <span class="icon is-small is-left"> 
                     <i class="fas fa-envelope"></i> 
                </span> 
            </div>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Dirección es Obligatorio" ValidationGroup="Registrar" ControlToValidate="direccion_seller" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
             </div>
        
        </div>
        
    </div>
      <div class="row ">
        <div class="col-md-6 col-sm-6 col-xs-6">
        <div class="field">
           
            <div class="control has-icons-left has-icons-right">
                <asp:TextBox runat="server" ID="fono_seller" CssClass="input is-success" type="text" placeholder="Ingrese Fono">
                    
                </asp:TextBox>
                <span class="icon is-small is-left"> 
                     <i class="fas fa-user"></i> 
                </span> 
            </div>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Fono Seller es Obligatorio" ValidationGroup="Registrar" ControlToValidate="fono_seller" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
   
           </div>
        </div>
          <div class="col-md-6 col-sm-6 col-xs-6">
          <div class="field"> 
                 
             <div class="select"> 
    
                  <asp:DropDownList ID="comuna_seller" runat="server" CssClass="input is-success" AutoPostBack="true">

                  </asp:DropDownList>
     
              </div> 
         </div>
         </div>

          <br />

          <br />

          <br />
       <div class="col-md-6 col-sm-6 col-xs-6">
        <div class="field">
           
            <div class="control has-icons-left has-icons-right">
                <asp:TextBox runat="server" ID="key_usermame_seller" CssClass="input is-success" type="text" placeholder="Ingrese Key Username"></asp:TextBox>
            <span class="icon is-small is-left"> 
              <i class="fas fa-key"></i> 
            </span> 
            </div>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Key Username es Obligatorio" ValidationGroup="Registrar" ControlToValidate="key_usermame_seller" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
   
         </div>
       </div>
      <div class="col-md-6 col-sm-6 col-xs-6">
        <div class="field">
          
          <div class="control has-icons-left has-icons-right">
              <asp:TextBox runat="server" ID="key_password_seller" CssClass="input is-success" type="text" placeholder="Ingrese Key Password" ForeColor="Red"></asp:TextBox>
          <span class="icon is-small is-left"> 
            <i class="fas fa-key"></i> 
         </span> 
         </div>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="* Key Password obligatorio" ValidationGroup="Registrar" ControlToValidate="key_password_seller" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
   
           </div>
    </div>
        
      
                    <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 pad-adjust">
                             <div class="field">
                            <div class="control">
                                     <br />
                                <asp:Button runat="server" ID="Registrar" CssClass="button is-link" OnClick="Registrar_Click" Text="Enviar" ValidationGroup='Registrar' CausesValidation="true" />
                                <br />
           
                            </div>
                       </div>
       
                  </div>
                </div>
 </div>

</div>

<div class="container align-content-center" > 

 <div class="row align-content-center">
    <div class="col-md-6 col-sm-6 col-xs-6   pad-adjust">
     <div class="field">
         <div class="control has-icons-left has-icons-right">
              <asp:TextBox runat="server" ID="BuscarNombre" CssClass="input is-success" type="text" placeholder="Buscar Por Nombre"  ForeColor="Red"></asp:TextBox>
              <span class="icon is-small is-left"> 
                <i class="fas fa-user"></i> 
              </span> 
         </div>     
     </div>
   </div>

     <div class="col-md-6 col-sm-6 col-xs-6   pad-adjust">
      <div class="field">
          <div class="control has-icons-left has-icons-right">
              <asp:Button runat="server" ID="Button1" CssClass="button is-warning" OnClick="Buscar_Click" Text="Buscar Por Nombre"/>
               
              <br />
              <br />
          </div>     
      </div>
    </div>
</div>


 
<div class="row align-content-center">
    <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
        <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="id_seller" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" BackColor="#D2FFE9" BorderStyle="None">
      <AlternatingRowStyle BackColor="WhiteSmoke" />
      <Columns>
          <asp:BoundField DataField="id_seller" HeaderText="Id Seller" ReadOnly="True" >
          <FooterStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
          <HeaderStyle BackColor="#EBEBEB" />
          </asp:BoundField>
          <asp:BoundField DataField="rut_seller" HeaderText="Rut">
          <FooterStyle BackColor="#99FFCC" />
          <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" Font-Italic="True" />
          </asp:BoundField>
          <asp:BoundField DataField="nombre_seller" HeaderText="Nombre">
          <FooterStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
          <HeaderStyle BackColor="#EBEBEB" />
          </asp:BoundField>
         
          <asp:BoundField DataField="fono_seller" HeaderText="Telefono" >
          <FooterStyle BackColor="#99FFCC" />
          <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" Font-Italic="True" />
          </asp:BoundField>
          
          
          <asp:BoundField DataField="key_usermame_seller" HeaderText="Key Username">
          <FooterStyle BackColor="#99FFCC" />
          <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" Font-Italic="True" />
          </asp:BoundField>
          <asp:BoundField DataField="key_password_seller" HeaderText="Key Password">
          <FooterStyle BackColor="#99FFCC" />
          <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" Font-Italic="True" />
          </asp:BoundField>
          
          
          <asp:CommandField ButtonType="Button" ShowEditButton="True" HeaderText="Modificar" >
          <ControlStyle  CssClass="button is-success is-light" Font-Size="small"/>
          <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" Font-Italic="False" />
          </asp:CommandField>
          <asp:CommandField ButtonType="Button" ShowDeleteButton="True" HeaderText="Eliminar">
          <ControlStyle CssClass="button is-danger is-light" Font-Size="small"/>
          <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" />
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
</div>
</asp:Content>

