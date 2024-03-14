using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ProyectoLastLink.Pages
{
    public partial class preBatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Gvbind();

            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

              


        private void CargarDatos2()
        {
            try
            {
                

             
                DataTable datatable = new DataTable();
                DataTable datatable2 = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("Listar_Verifica_Batch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView2.DataSource = datatable;
                GridView2.DataBind();
            }
            catch
            {
                throw;

            }

        }


        protected void Button6_Click(object sender, EventArgs e)
        {

            CargarDatos2();

            GridView2.Visible = true;


            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename = InformePreBatchBodegas.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView2.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }



        private void CargarDatos()
        {
            try
            {
                string buscar_fecha = BuscarNombre_calendario.Text.Trim();
                string buscar2_fecha = BuscarNombre2_calendario.Text.Trim();
                var fecha1 = DateTime.Now;
                var fecha2 = DateTime.Now;
                DateTime dt, dt2;
                var date1 = DateTime.TryParse(buscar_fecha, out dt);
                var date2 = DateTime.TryParse(buscar2_fecha, out dt2);
                if (date1 && date2)
                {
                    fecha1 = dt;
                    fecha2 = dt2;
                }
                //////////////////

                /// buscar por nombre
                string buscar = BuscarNombre.Text.Trim();
                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                if (buscar == "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_batch_Pre";
                }
                if (buscar != "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_Proveedor_Batch_Pre";
                }
                if (buscar == "" && buscar_fecha != "" && buscar2_fecha != "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_Batch_Pre";
                }
                string cadena = Convert.ToString(Session["cadena"]);
                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand(cadena, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (buscar != "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    cmd.Parameters.AddWithValue("@Busqueda", buscar);
                }
                if (buscar == "" && buscar_fecha != "" && buscar2_fecha != "")
                {
                    cmd.Parameters.AddWithValue("@Busqueda", fecha1);
                    cmd.Parameters.AddWithValue("@Busqueda2", fecha2);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView1.DataSource = datatable;
                GridView1.DataBind();
                if (datatable.Rows.Count == 0)
                {
                    GridView1.Visible = false;
                }
                else
                {
                    GridView1.Visible = true;
                }
            }
            catch
            {
                throw;
            }
        }


        protected void Anular_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = Convert.ToString(Session["usuario"]);
                string idplat = Id_plat.Text;
                int Estado = 10;
                string Comentario = "Anulado por Batch de Bodega";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_cambiar_estado", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = idplat;
                cmd.Parameters.Add("@Estado", System.Data.SqlDbType.Int).Value = Estado;


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cargar_estado_log(idplat, Estado, Comentario);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Pedido Dejado Pendiente por Bodega Correctamente!'})", true);
                CargarDatos();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cargar_estado_log(string idplat, int Estado, string Comentario)
        {
            try
            {
                string usuario = Convert.ToString(Session["usuario"]);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_log_estado", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = idplat;
                cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Estado;
                cmd.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar).Value = usuario;
                cmd.Parameters.Add("@Comentario", System.Data.SqlDbType.VarChar).Value = Comentario;


                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception)
            {

                throw;
            }


        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            CargarDatos();

        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CargarDatos();

        }

        protected void GridView11_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView11.PageIndex = e.NewPageIndex;
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id = GridView1.SelectedRow.Cells[1].Text;
            string Id_Plat = GridView1.SelectedRow.Cells[3].Text;
            CargarDatos_movimiento(Id_Plat);



            Origen.Text = GridView1.SelectedRow.Cells[2].Text;
            Id_plat.Text = GridView1.SelectedRow.Cells[3].Text;
            Proveedor.Text = GridView1.SelectedRow.Cells[4].Text;
            Nombre_Comprador.Text = GridView1.SelectedRow.Cells[5].Text;
            Direccion.Text = GridView1.SelectedRow.Cells[6].Text;
            //Comuna.Text = GridView1.SelectedRow.Cells[6].Text;
            // Telefono.Text = GridView1.SelectedRow.Cells[7].Text;
            Correo_electronico.Text = GridView1.SelectedRow.Cells[8].Text;
            //Fecha_Venta.Text = GridView1.SelectedRow.Cells[7].Text;

            ModalPopupExtender1.Show();
        }


        private void CargarDatos_movimiento(string Id_Plat)
        {
            try
            {

                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand("sp_datos_Detalle_Pedidopopup", con);
                cmd.Parameters.AddWithValue("@Id_Plat", Id_Plat);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView11.DataSource = datatable;
                GridView11.DataBind();
                if (datatable.Rows.Count == 0)
                {

                    GridView11.Visible = false;
                }
                else
                {

                    GridView11.Visible = true;
                }



            }
            catch
            {
                throw;

            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //Panel3 es seller
            //Panel_Fecha
            //buscar por fecha
            Panel3.Visible = false;
            Panel_Fecha.Visible = true;
            BuscarNombre.Text = "";

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //BuscarNombre por estado
            Panel3.Visible = true;
            Panel_Fecha.Visible = false;
            BuscarNombre_calendario.Text = "";
            BuscarNombre2_calendario.Text = "";

        }


        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            BuscarNombre_calendario.Text = Calendar1.SelectedDate.ToString();

        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            BuscarNombre2_calendario.Text = Calendar2.SelectedDate.ToString();

        }


        protected void ChkEmpty_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkstatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkstatus.NamingContainer;

        }

        protected void ChkHeader_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkHeader = (CheckBox)GridView1.HeaderRow.FindControl("ChkHeader");
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("ChkEmpty");
                if (chkHeader.Checked == true)
                {
                    chkrow.Checked = true;
                }
                else
                {
                    chkrow.Checked = false;
                }

            }
        }


        public int Productos_en_bodega(string seller, string id_plat)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("ver_existencia_bodega_bach", con);
            cmd.Parameters.Add("@id_plat", SqlDbType.VarChar).Value = id_plat;
            cmd.Parameters.Add("@seller", SqlDbType.VarChar).Value = seller;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //string Id_producto;
            int valor;
            if (dr.Read())
            {
                //   valor = Convert.ToInt32(dr["valor"]);
                Session["Id_producto"] = dr["valor"].ToString();


            }
            con.Close();
            valor = Convert.ToInt32(Session["Id_producto"]);
            return valor;
        }

        public int Productos_en_bodega_sin_SKU(string seller, string id_plat)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("ver_existencia_sku_Migracion_Bach", con);
            cmd.Parameters.Add("@id_plat", SqlDbType.VarChar).Value = id_plat;
            cmd.Parameters.Add("@seller", SqlDbType.VarChar).Value = seller;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //string Id_producto;
            int valor;
            if (dr.Read())
            {
                //   valor = Convert.ToInt32(dr["valor"]);
                Session["Sin_sku"] = dr["valor"].ToString();

            }
            con.Close();
            valor = Convert.ToInt32(Session["Sin_sku"]);
            return valor;
        }

        public int Productos_sin_Stock_bach(string seller, string id_plat)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("ver_existencia_Stock_bodega_bach", con);
            cmd.Parameters.Add("@id_plat", SqlDbType.VarChar).Value = id_plat;
            cmd.Parameters.Add("@seller", SqlDbType.VarChar).Value = seller;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //string Id_producto;
            int valor;
            if (dr.Read())
            {
                //   valor = Convert.ToInt32(dr["valor"]);
                Session["Stock_inicial"] = dr["Stock_inicial"].ToString();

            }
            con.Close();
            valor = Convert.ToInt32(Session["Stock_inicial"]);
            return valor;
        }

        protected void CambiarEstado_Click(object sender, EventArgs e)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("delete_preBatch", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chkdelete = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("ChkEmpty");
                if (chkdelete.Checked == true)
                {
                    int id = Convert.ToInt32(GridView1.Rows[i].Cells[1].Text);
                    string sellers = (GridView1.Rows[i].Cells[4].Text);
                    string idplat = (GridView1.Rows[i].Cells[3].Text);

                    int sin_sku;

                    sin_sku = Productos_en_bodega_sin_SKU(sellers, idplat);
                    if (sin_sku == 0)
                    {



                        int valor;
                        valor = Productos_en_bodega(sellers, idplat);
                        if (valor == 1)
                        {
                            int sin_stock;
                            sin_stock = Productos_sin_Stock_bach(sellers, idplat);

                            if (sin_stock == 0)
                            {

                                string Observaciones = "Sin Errores en Bodega para Pedido, " + idplat + " Puede Correr Batch para este Pedido";
                                Cambiar_estado_preBAtch(idplat);
                                insertar_error_validacion(Observaciones, idplat);


                            }
                            else
                            {
                                //   ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'No hay Stock en Bodega para Pedido," + idplat + " En Seller:" + sellers + " favor verifique en Bodega e Ingresar Stock!'})", true);
                                string Observaciones = " No hay Stock en Bodega para Pedido, " + idplat + " En Seller:" + sellers + " favor verifique en Bodega e Ingresar Stock!";

                                insertar_error_validacion(Observaciones, idplat);
                            }

                        }
                        else
                        {
                            //  ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Hay SKU inexistentes en Bodega para Pedido," + idplat + " En Seller:" + sellers + " favor verifique en Bodega de Seller intentelo nuevamente!'})", true);
                            string Observaciones = "Hay SKU inexistentes en Bodega para Pedido," + idplat + " En Seller:" + sellers + " favor verifique en Bodega de Seller";

                            insertar_error_validacion(Observaciones, idplat);


                        }

                    }
                    else
                    {
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Hay SKU inexistentes Al momento de Migración para Pedido," + idplat + " En Seller:" + sellers + " favor Informar a Seller que debe Ingresar SKU en su Plataforma de Ventas, SOLUCIÓN: Actualice el SKU en Opción Lista de Pedidos->Detalles-->Editar'})", true);
                        string Observaciones = "Hay SKU inexistentes Al momento de Migración para Pedido," + idplat + " En Seller:" + sellers + " favor Informar a Seller que debe Ingresar SKU en su Plataforma de Ventas, SOLUCIÓN: Actualice el SKU en Opción Lista de Pedidos->Detalles-->Editar";
                        insertar_error_validacion(Observaciones, idplat);

                    }
                }
            }
            Button6.Visible = true;
            CargarDatos();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Carga Pre Batch Realizada Correctamente'})", true);

        }


        public void Cambiar_estado_preBAtch(string idplat)
        {
            try
            {
                int Estado = 12;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_cambiar_estado", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id_Plat", System.Data.SqlDbType.VarChar).Value = idplat;
                cmd.Parameters.Add("@Estado", System.Data.SqlDbType.Int).Value = Estado;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception)
            {

                throw;
            }

        }


        private void insertar_error_validacion(string Observaciones,string idplat)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_Errores_PreBatch", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Observaciones", System.Data.SqlDbType.VarChar).Value = Observaciones;
                cmd.Parameters.Add("@idplat", System.Data.SqlDbType.VarChar).Value = idplat;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }
               
    }
}