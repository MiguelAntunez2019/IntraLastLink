<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimePdf.aspx.cs" Inherits="ProyectoLastLink.ImprimePdf" %>

<!DOCTYPE html>
<html lang="en">

<head>
    
       <meta charset="UTF-8">
 <meta name="viewport" content="width=device-width, initial-scale=1.0">
 <title>Etiqueta Personalizada</title>
 <script src="https://cdn.rawgit.com/davidshimjs/qrcodejs/gh-pages/qrcode.min.js"></script>
 <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <style>
    

        .container {
            
            display: flex;
            align-items: center;
        }

        img {
            width: 100px;
            height: auto;
            margin-right: 10px;
        }

       

        hr {
            margin-top: 20px;
        }

        .codigo {
            text-align: center;
            font-weight: bold;
            color: black;
            font-size: 24px;
            margin-top: 10px;
        }

        .qrtiger a {
            height: 50px;
            width: 50px;
            color: #ffffff;
            background-color: #ff8c2a;
        }

        .camion-container {
            display: flex;
            align-items: center;
        }

        .camion {
            width: 50px;
            height: 50px;
            margin-right: 10px;
        }

        .cuadro {
             width: 40px;
            height: 40px;
            border: 1px solid black;
            margin-left:42px;
        }
        .Volver {
            color:blue;
            font-family:Arial;
            background-color:blue;
        }
    </style>
 <script>

    

     function obtenerDatosDesdeServidor() {
         var urlCompleta = window.location.href;

      
         var valorPedido = obtenerValorParametro(urlCompleta, 'Pedido');
         $.ajax({
             url: "../ImprimePdf.aspx/MandaImprimir",
             type: "POST",
             data: JSON.stringify({ id_pedido: valorPedido }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var responsedata = data.d.Result;
                var nombremostrar = responsedata.nombre;
                var direccionmostrar = responsedata.direccion;
                var comunamostrar = responsedata.comuna;
                var telefonomostrar = responsedata.telefono;
                var idproductomostrar = responsedata.idproducto;
                var proveedormostrar = responsedata.proveedor;
                var zonamostrar = responsedata.zona;

                $('#nombre').text(nombremostrar);
                $('#direccion').text(direccionmostrar);
                $('#comuna').text(comunamostrar);
                $('#telefono').text(telefonomostrar);
                $('#idproducto').text(idproductomostrar);
                $('#proveedor').text(proveedormostrar);
                $('#zonamostrar').text(zonamostrar);

               
                window.print();
                window.history.back;
              
                setTimeout(verificarImpresionCompletada, 1000); 
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Error en la solicitud Ajax:", textStatus, errorThrown);
            }
        });
     }

     function obtenerValorParametro(url, nombreParametro) {
         var parametros = url.split('?')[1].split('&');

         for (var i = 0; i < parametros.length; i++) {
             var parametro = parametros[i].split('=');
             if (parametro[0] === nombreParametro) {
                 return parametro[1];
             }
         }
         return null;
     }

     function verificarImpresionCompletada() {
        
         var contenidoInicial = document.body.innerHTML;
         setTimeout(function () {
             if (contenidoInicial !== document.body.innerHTML) {
                
                 console.log('Impresión completada');
               
             } else {

                 setTimeout(verificarImpresionCompletada, 1000); 
             }
         }, 1000);
     }

     $(document).ready(function () {
         obtenerDatosDesdeServidor();
     });
 </script>
  
</head>

<body  >
   <form action="#" method="Post" runat="server">
   
       <div>
    <div class="container">
        <div class="camion-container">
            <img class="camion" src="imagenes/LOGO1.png" />
        
        </div>
        <div >
            <div style="font-size:15px; color:blue; font-family:Franklin Gothic Demi, sans-serif;"><strong>Lastlink</strong></div>
            <div style="font-size:10px">Avda Ventisquero</div>
             <div style="font-size:10px">Workcenter Miraflores - Renca</div>
            <div style="font-size:10px">transporte@lastlink.cl</div>
        </div>
        <div>
                <div class="cuadro">
                    <div style="font-size:8px;text-align:center;">  Zona</div>
                    <div id="zonamostrar" style="font-size:20px;text-align:center;"></div>
                </div>
        </div>
    </div>
    <hr />
    <div style="margin-left:3px;font-weight: bold;font-family:Arial;font-size:25px;">Datos de Cliente</div>
    <div id="nombre" style="margin-left:3px;font-size:15px;">Miguel Antunez Villablanca</div>
    <div id="direccion" style="margin-left:3px;font-size:15px;">La quebrada del quillay 5120</div>
       <div id="comuna" style="margin-left:3px;font-size:15px;"></div>
    <div id="telefono" style="margin-left:3px;font-size:15px;">telefono</div>

    <div style="margin-top:-120px">
        <div id="idproducto" style="margin-top:150px; margin-left:3px; font-weight: bold;font-family:Arial;font-size:25px">mpriaakakl1</div>
 
        <div style="margin-top:-40px;"></div>
       <canvas style="margin-top:20px;margin-left:200px;width:70px;height:70px" id="codigo-qr"></canvas>
   
    </div>
        <div style="margin-top:-120px;margin-left:3px;visibility:hidden;">Bulto: □ de □</div>
     
    <div style="margin-top: 130px; margin-left: 3px;">
        <img src="imagenes/bulto.png" style="height:25px;width:145px;" /> </div>

    </div>

       </form>
    <div>
        
       <script type="text/javascript">
           

        </script>
        
        <script src="Scripts/qrious.min.js"></script>
       <script>
           const canvas = document.getElementById('codigo-qr');
          
         
           const idproducto = document.getElementById('idproducto').innerText;
          

           const qr = new QRious({
               element: canvas,
               value: idproducto,
           });
       </script>
            </div>

    <div class="qrtiger"></div>
  

</body>

</html>