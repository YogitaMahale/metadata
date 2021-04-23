using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
/// <summary>
/// Summary description for BaseDb
/// </summary>
/// 
namespace HydroProject.Code
{
    public static class BaseDb
    {

        public enum TransactionOption
        {
            Started,
            Stoped
        }

        public static SqlTransaction ConnectionTransaction = null;
        public static String DBConnection = ConfigurationManager.ConnectionStrings["constr"].ToString();
        public static String sqlconnection = ConfigurationManager.ConnectionStrings["sqlconn"].ToString();
        public static String tmpconnection = ConfigurationManager.ConnectionStrings["tmpconn"].ToString();
        public static DbCommand CreateCommand()
        {
            string cartDBConnection = DBConnection;
            //DbConnection dbcon2 = new OleDbConnection(DBConnection);
            DbConnection dbConn = SqlClientFactory.Instance.CreateConnection();
            dbConn.ConnectionString = cartDBConnection;
            DbCommand dbcomm = dbConn.CreateCommand();
            dbcomm.CommandType = CommandType.StoredProcedure;
            return dbcomm;


        }

        public static DbCommand CreateStrCommand()
        {
            string cartDBConnection = DBConnection;
            //DbConnection dbcon1 = new OleDbConnection(DBConnection);
            DbConnection dbConn = SqlClientFactory.Instance.CreateConnection();
            dbConn.ConnectionString = cartDBConnection;
            DbCommand dbcomm = dbConn.CreateCommand();
            dbcomm.CommandType = CommandType.Text;
            return dbcomm;

        }

        public static DbCommand CreateSqlCommand()
        {
            string sqlDBConnection = sqlconnection;
            DbConnection dbConn = SqlClientFactory.Instance.CreateConnection();
            dbConn.ConnectionString = sqlDBConnection;
            DbCommand dbcomm = dbConn.CreateCommand();
            dbcomm.CommandType = CommandType.Text;
            return dbcomm;

        }
        public static DbCommand CreatesqlCommand()
        {
            string sqlDBConnection = sqlconnection;
            //DbConnection dbcon2 = new OleDbConnection(DBConnection);
            DbConnection sqldbConn = SqlClientFactory.Instance.CreateConnection();
            sqldbConn.ConnectionString = sqlDBConnection;
            DbCommand dbcomm = sqldbConn.CreateCommand();
            dbcomm.CommandType = CommandType.StoredProcedure;
            return dbcomm;
        }


        public static DbCommand CreatetmpCommand()
        {
            string sqlDBConnection = tmpconnection;
            DbConnection dbConn = SqlClientFactory.Instance.CreateConnection();
            dbConn.ConnectionString = sqlDBConnection;
            DbCommand dbcomm = dbConn.CreateCommand();
            dbcomm.CommandType = CommandType.Text;
            return dbcomm;

        }
        public static DbCommand CreateTmpCommand()
        {
            string sqlDBConnection = tmpconnection;
            //DbConnection dbcon2 = new OleDbConnection(DBConnection);
            DbConnection sqldbConn = SqlClientFactory.Instance.CreateConnection();
            sqldbConn.ConnectionString = sqlDBConnection;
            DbCommand dbcomm = sqldbConn.CreateCommand();
            dbcomm.CommandType = CommandType.StoredProcedure;
            return dbcomm;
        }

        public static int ExecuteNonQuery(DbCommand dbCommand)
        {
            int affectedRows = -1;
            try
            {

                dbCommand.Connection.Open();
                affectedRows = dbCommand.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                dbCommand.Connection.Close();
            }
            return affectedRows;
        }

        public static string ExecuteScalar(DbCommand dbComand)
        {
            string returnvalue = "";
            try
            {
                dbComand.Connection.Open();
                returnvalue = dbComand.ExecuteScalar().ToString();
            }
            catch (Exception exc)
            {
                throw exc;

            }
            finally
            {
                dbComand.Connection.Close();
            }
            return returnvalue;
        }

        public static DataTable ExecuteSelect(DbCommand dbCommand)
        {

            DataTable dataTable = new DataTable();

            // try
            //{

            dbCommand.Connection.Open();
            DbDataReader dbReader = dbCommand.ExecuteReader();
            dataTable.Load(dbReader);
            dbReader.Close();

            //}
            //catch (Exception exc)
            //{
            //throw exc;

            //}

            //finally
            //{
            dbCommand.Connection.Close();
            //}
            return dataTable;
        }

    }


}