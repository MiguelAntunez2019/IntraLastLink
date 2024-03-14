<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Index.Master" AutoEventWireup="true" CodeBehind="recuperar_contrasena.aspx.cs" Inherits="ProyectoLastLink.Pages.recuperar_contrasena" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
   <div class="hero  is-primary">
    <div class="hero is-primary"></div>

            <div class="hero is-fullheight is-primary">
          <div class="hero-body">
            <div class="container has-text-centered">
              <div class="column is-8 is-offset-2">
             
                <h3 class="title has-text-white">Recuperar Contraseña</h3>
                <hr class="login-hr">
             
              </div>

                <div class="box">
                  <div class="box"><img src="../imagenes/LOGO1.png" /></div>
                  
                 
                   <div class="field">
                     <div class="control">

                                                          
                        <asp:Textbox runat="server" id="email" class="input is-large" type="text" placeholder="ej. pabloperez@lastlink.cl" autofocus=""></asp:Textbox>    
                     </div>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* E-Mail es Obligatorio" ValidationGroup="Registrar" ControlToValidate="email" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <br />
                        
                   </div>
                                
                 
                 
                 
                 <p class="has-text-grey">
                <asp:Button runat="server" id="recuperar" class="button is-success" text="Recuperar Contraseña" OnClick="recuperar_Click" ValidationGroup='Registrar' CausesValidation="true"/>
                     &nbsp;·&nbsp;
                <asp:Button runat="server" id="volver" class="button is-warning" text="Volver al Login" OnClick="volver_Click"/>
                     
                          
                </p>    
                </div>
            </div>
          </div>
        </div>


    










</div>

</asp:Content>
