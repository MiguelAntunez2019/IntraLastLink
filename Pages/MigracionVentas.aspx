<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="MigracionVentas.aspx.cs" Inherits="ProyectoLastLink.Pages.MigracionVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
   <script>
       function mostrarCargando() {
           
           $('body').addClass('loading');

           $("#divCargando").show();
           $("#divProcesoTerminado").hide();
           $("#divMigrar").hide();
           $("#resultado").hide();
           $.ajax({
               type: "POST",
               url: "../Pages/MigracionVentas.aspx/EjecutarPrograma",
               data: JSON.stringify({ name: 'Mark' }),
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (resultado) {
                  
                   $('body').removeClass('loading');

                   $("#divCargando").hide();
                   $("#resultado").show();
                   $("#divProcesoTerminado").hide();
                   $("#divMigrar").show();

                   if (resultado.d) {
                       var resultObject = resultado.d.Result;
                       if (resultObject) {
                           console.log("Resultado:", resultObject.Contador);

                           $("#resultado").html("Proceso Terminado : Se han procesado: " + resultObject.Contador + " Ordenes");
                       } else {
                           console.error("SE han producido errores al en el formato de salida");
                       }
                   } else {
                       console.error("La respuesta del servidor no tiene el formato esperado.");
                   }
               },
               error: function (error) {
                  
                   $('body').removeClass('loading');

                   console.error("Error en la llamada AJAX:", error);
               }
           });
       }

      
       $(document).ready(function () {
           $("#Button1").click(function () {
               mostrarCargando();
           });
       });
   </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="container box p-12 has-background-light">
        <div class="row">
            <div class="col-md-12">
                <h1 class="title has-text-centered has-text-success">Migración Ventas(Todas)</h1>
                <p class="title has-text-centered has-text-success"></p>
            </div>
        </div>
      <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <asp:Panel ID="Panel1" runat="server">
                <article class="message is-success">
                    <div class="message-body">
                        <div class="icon-text">
                            <span class="icon has-text-warning">
                                <i class="fas fa-exclamation-triangle"></i>
                            </span>
                            <span class="has-text-danger">Warning</span>
                        </div>
                        <b />
                        Permite Migrar la Información de a Ventas de las distintas Plataformas<br /><br />
                    </div>
                </article>
            </asp:Panel>
        </div>
        <br />
   
        
                   <div class="row">
            <div class="col-md-12" id="divMigrar">
               
                <asp:Button ID="Button1" runat="server" Text="Migrar Información"  class="button is-medium is-fullwidth" OnClientClick="mostrarCargando(); return false;" />
            </div>
            <br />
            <br />
        </div>
        
    
      <div id="divCargando" style="display:none;align-content:center;text-align:center;" >
    <label>Cargando Ordenes</label><br />
    <label>Espere Por Favor</label><br />
    <img src="../imagenes/Q0t0.gif" width="350" height="250" />
</div>

     
        <div id="divProcesoTerminado"  class="button is-medium is-fullwidth" style="display:none;">
            <h2>Proceso Terminado</h2>
        </div>

      
        <div id="resultado" style="display:none;"> </div>
    </div>    
  
</asp:Content>
