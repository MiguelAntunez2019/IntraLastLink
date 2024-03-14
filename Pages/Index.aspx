<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Index.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ProyectoLastLink.Pages.Index" %>
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
              
                 <h3 class="title has-text-white">Login</h3>
                 <hr class="login-hr">
              
               </div>

                 <div class="box">
                   <div class="box"><img src="../imagenes/LOGO1.png" /></div>
                   
                  
                    <div class="field">
                      <div class="control">

                                                           
                         <asp:Textbox runat="server" id="usuario" class="input is-large" type="text" placeholder="ej. Joe_Doe12" autofocus=""></asp:Textbox>    
                      </div>
                    </div>
                    <div class="field">
                      <div class="control">

                         
                     <asp:Textbox runat="server" id="clave" class="input is-large" type="password" placeholder="Clave"></asp:Textbox>
                       
                         
                      </div>

                   
                    </div>
                 
                  
                  
                  
                  <p class="has-text-grey">
                 <asp:Button runat="server" id="ingresar" class="button is-success" text="Ingresar al sistema" OnClick="ingresar_Click"/>
                      &nbsp;·&nbsp;
                 <asp:Button runat="server" id="recuperar" class="button is-warning" text="Recuperar Clave" OnClick="recuperar_Click"/>
                      
                           
                 </p>    
                 </div>
             </div>
           </div>
         </div>
 

     










 </div>

  
</asp:Content>
