<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Seguimiento.aspx.cs" Inherits="ProyectoLastLink.Pages.Seguimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
       <div class="container box p-12   has-background-light "> 
              <div class="row ">
                  <div class="col-md-12">
                    <h1 class="title has-text-centered  has-text-success">Seguimientos Pedidos </h1>
                 </div>
              </div>
           <br />        
           
           <div class="row ">
          <asp:Panel ID="Panel1" runat="server">
  
                                          <article class="message is-success">
                                               <div>
                                           <iframe
                                             name='beetrack-widget'
                                             id='beetrack-widget'
                                             frameBorder='0'
                                             width='90%'
                                             height='300px'
                                             src='https://lastlink.dispatchtrack.com/widget/8HQyuTmYvmPz9iaAG4tifA'
                                           />
                                                </div>          
                                          </article>
             </asp:Panel>
           <div class="col-md-12">
     


       </div>
    </div>

       </div>
 </asp:Content>
