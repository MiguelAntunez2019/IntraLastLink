using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ProyectoLastLink.Pages;
using System.Net.Mail;


namespace ProyectoLastLink.clases
{
    public class combos
    {
        private string sConnStr;
        public void Inicio()
        {
            sConnStr = ConfigurationManager.ConnectionStrings["conexion"].ToString();
        }
        public DataSet getHoraFinReserva()
        {
            Inicio();
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            int idEstado = 1;
            try
            {
                //1) conexion a BD
                conn = new SqlConnection(this.sConnStr);
                conn.Open();
                //configura SP a llamar
                SqlCommand cmd = new SqlCommand("sp_Perfil", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@idEstado", idEstado);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                oda.Fill(ds, "Tabla");
                conn.Close();
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw (ex);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet getHoraFinReserva2()
        {
            Inicio();
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            //int idEstado = 1;
            try
            {
                //1) conexion a BD
                conn = new SqlConnection(this.sConnStr);
                conn.Open();
                //configura SP a llamar
                SqlCommand cmd = new SqlCommand("sp_Comuna", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
               // cmd.Parameters.AddWithValue("@idEstado", idEstado);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                oda.Fill(ds, "Tabla");
                conn.Close();
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw (ex);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet getCantidad()
        {
            Inicio();
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            //int idEstado = 1;
            try
            {
                //1) conexion a BD
                conn = new SqlConnection(this.sConnStr);
                conn.Open();
                //configura SP a llamar
                SqlCommand cmd = new SqlCommand("sp_Cantidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                // cmd.Parameters.AddWithValue("@idEstado", idEstado);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                oda.Fill(ds, "Tabla");
                conn.Close();
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw (ex);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds;
        }




        public DataSet getOrigen()
        {
            Inicio();
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            //int idEstado = 1;
            try
            {
                //1) conexion a BD
                conn = new SqlConnection(this.sConnStr);
                conn.Open();
                //configura SP a llamar
                SqlCommand cmd = new SqlCommand("sp_combo_Origen", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                // cmd.Parameters.AddWithValue("@idEstado", idEstado);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                oda.Fill(ds, "Tabla");
                conn.Close();
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw (ex);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds;
        }



      




        public DataSet getComboSeller()
        {
            Inicio();
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            //int idEstado = 1;
            try
            {
                //1) conexion a BD
                conn = new SqlConnection(this.sConnStr);
                conn.Open();
                //configura SP a llamar
                SqlCommand cmd = new SqlCommand("sp_combo_seller", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                // cmd.Parameters.AddWithValue("@idEstado", idEstado);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                oda.Fill(ds, "Tabla");
                conn.Close();
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw (ex);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet getNombreProducto(string Seller)
        {
            Inicio();
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            //int idEstado = 1;
            try
            {
                //1) conexion a BD
                conn = new SqlConnection(this.sConnStr);
                conn.Open();
                //configura SP a llamar
                SqlCommand cmd = new SqlCommand("sp_datos_Producto_Seller", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Busqueda", Seller);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                oda.Fill(ds, "Tabla");
                conn.Close();
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw (ex);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds;
        }





        public DataSet getComboPedido()
        {
            Inicio();
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            //int idEstado = 1;
            try
            {
                //1) conexion a BD
                conn = new SqlConnection(this.sConnStr);
                conn.Open();
                //configura SP a llamar
                SqlCommand cmd = new SqlCommand("sp_combo_pedido", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                // cmd.Parameters.AddWithValue("@idEstado", idEstado);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                oda.Fill(ds, "Tabla");
                conn.Close();
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw (ex);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet getComboMes()
        {
            Inicio();
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            //int idEstado = 1;
            try
            {
                //1) conexion a BD
                conn = new SqlConnection(this.sConnStr);
                conn.Open();
                //configura SP a llamar
                SqlCommand cmd = new SqlCommand("sp_combo_mes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                // cmd.Parameters.AddWithValue("@idEstado", idEstado);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                oda.Fill(ds, "Tabla");
                conn.Close();
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw (ex);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds;
        }



    }
}