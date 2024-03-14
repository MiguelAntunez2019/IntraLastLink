<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="finsesion.aspx.cs" Inherits="ProyectoLastLink.Pages.finsesion" %>
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
              
                 <h3 class="title has-text-white">Fin de Sesion, Ingrese nuevamente al Sistema</h3>
                 <hr class="login-hr">
              
               </div>

                 <div class="box">
                   <div class="box">
                       <img src="../imagenes/finsesion.png" width="200" height="100" /></div>
                   
                  
                                   
                  
                  
                  
                  <p class="has-text-grey">
                 <asp:Button runat="server" id="ingresar" class="button is-success" text="Ingresar al sistema" OnClick="ingresar_Click"/>
                      &nbsp;·&nbsp;
                                       
                           
                 </p>    
                 </div>
             </div>
           </div>
         </div>
 

     










 </div>

  
</asp:Content>
