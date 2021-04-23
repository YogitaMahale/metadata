using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration; 
/// <summary>
/// Summary description for DataAcessLayer
/// </summary>
public class DataAcessLayer
{
    public string constring = ConfigurationManager.ConnectionStrings["constr"].ToString();
    SqlConnection con = new SqlConnection();
    SqlCommand cmd=new SqlCommand();
    public DataAcessLayer()
	{   
        //
		// TODO: Add constructor logic here
		//
	}
    public void connect()
    {
        con.ConnectionString = constring;
        con.Open(); 
    }
    public void Disconnect()
    {
        cmd.Dispose();
        con.Dispose(); 
    }
    public void ExecInsert(string sqlstring)
    {
        //con.Open();
        cmd.CommandText =sqlstring;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
        //con.Close(); 
         
    }
    public void ExecDelete(string sqlstring)
    {
        //con.Open();
        cmd.CommandText = sqlstring;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
        //con.Close();  
    }
    public void Execupdate(string sqlstring)
    {
        //con.Open();
        cmd.CommandText = sqlstring;
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery();
        //con.Close();
    }
    public SqlDataReader ExecSelect(string sqlstring)
    {
        cmd.CommandText=sqlstring;
        cmd.CommandType =CommandType.Text;
        SqlDataReader reader = cmd.ExecuteReader();
        return reader; 
          
    }


}
