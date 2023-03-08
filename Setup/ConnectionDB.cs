using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CanteenDashboard
{
    public class ConnectionDB
    {
        MySqlConnection conn;
        static string host = "192.168.88.30";
        static string database = "netraya_canteen";
        static string userDB = "rfid_developer";
        static string password = "w(v97weP8UGe=bYd";

        //static string host = "192.168.88.253";
        //static string database = "netraya_canteen";
        //static string userDB = "dot_developer";
        //static string password = "dot";

        //static string host = "192.168.192.150";
        //static string database = "netraya_canteen";
        //static string userDB = "smt_developer";
        //static string password = "w(v97weP8UGe=bYd";
        public static string strProvider = "server=" + host + ";Database=" + database + ";User ID=" + userDB + ";Password=" + password + ";SslMode=None;Connection Timeout=30";
        public MySqlConnection connection = new MySqlConnection("server=" + host + ";Database=" + database + ";User ID=" + userDB + ";Password=" + password + ";SslMode=None;Connection Timeout=30");

        public bool Open()
        {
            try
            {
                strProvider = "server=" + host + ";Database=" + database + ";User ID=" + userDB + ";Password=" + password + ";SslMode=None;Connection Timeout=30;";
                //strProvider = "server=" + host + ";Database=" + database + ";User ID=" + userDB + ";Password=" + password + ";SslMode=None;Connection Timeout=30;allowPublicKeyRetrieval=true;";
                conn = new MySqlConnection(strProvider);
                conn.Open();
                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show("Connection Error ! " + er.Message, "Information");
            }
            return false;
        }
        public void Close()
        {
            conn.Close();
            conn.Dispose();
        }

        public DataSet ExecuteDataSet(string sql)
        {
            try
            {
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                da.Fill(ds, "result");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        public MySqlDataReader ExecuteReader(string sql)
        {
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                int affected;
                MySqlTransaction mytransaction = conn.BeginTransaction();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                affected = cmd.ExecuteNonQuery();
                mytransaction.Commit();
                return affected;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return -1;
        }
    }
}
