using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace BaiTapLon.Class
{
    class Functions
    {
        public static SqlConnection con;  
        public static string strcon;
        public static void Connect()
        {
            strcon = "Data Source=Admin;Initial Catalog=BaiTapLon;Integrated Security=True";
            con = new SqlConnection();         		   
            con.ConnectionString = strcon; 		
            con.Open();

        }
        public static void Disconnect()
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
                con = null;
            }                
        }
        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter adp = new SqlDataAdapter(sql, Functions.con);
            DataTable table = new DataTable();
            adp.Fill(table);
            return table;

        }
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Functions.con);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public static void RunSql(string sql)
        {
            SqlCommand cmd;		                
            cmd = new SqlCommand();	         
            cmd.Connection = Functions.con;	
            cmd.CommandText = sql;			 
            try
            {
                cmd.ExecuteNonQuery();		  
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        public static void RunSqlDel(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Functions.con;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Dữ liệu đang được dùng, không thể xóa...", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            cmd.Dispose();
            cmd = null;
        }
        public static bool IsDate(string d)
        {
            string[] parts = d.Split('/');
            if ((Convert.ToInt32(parts[0]) >= 1) && (Convert.ToInt32(parts[0]) <= 31) &&(Convert.ToInt32(parts[1]) >= 1) && (Convert.ToInt32(parts[1]) <= 12) && (Convert.ToInt32(parts[2]) >= 1900))
                return true;
            else
                return false;
        }
        public static string ConvertDateTime(string d)
        {
            string[] parts = d.Split('/');
            string dt = String.Format("{0}/{1}/{2}", parts[1], parts[0], parts[2]);
            return dt;
        }
        
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, Functions.con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ma = reader.GetValue(0).ToString();
            }
            reader.Close();
            return ma;
        }
        public static void Runsql(string sql)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.Connection = Functions.con;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Functions.con);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma;    
            cbo.DisplayMember = ten;    
        }

    }
}
