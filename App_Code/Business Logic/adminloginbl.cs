using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;

/// <summary>
///summary
/// Summary description for adminloginbl
/// </summary>
namespace HydroProject.Code
{
    public class adminloginbl
    {
        public adminloginbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string computehash(string txttohash)
        {
            MD5CryptoServiceProvider md5algo = new MD5CryptoServiceProvider();
            byte[] bytevalue = System.Text.Encoding.UTF8.GetBytes(txttohash);
            byte[] bytehash = md5algo.ComputeHash(bytevalue);
            md5algo.Clear();
            return Convert.ToBase64String(bytehash);
        }

        public static DataTable GetUserDetails(string userid)
        {
            DbCommand dbComm = BaseDb.CreateStrCommand();
            dbComm.CommandText = "select * from usermaster where userid= '" + userid + "'";
            DataTable fieldData;
            //try
            //{
            fieldData = BaseDb.ExecuteSelect(dbComm);
            //}
            //catch (Exception exc)
            //{
            //throw exc;
            //}
            return fieldData;
        }

        public static Int32 validateuserid(string user)
        {
            DbCommand cmd = BaseDb.CreateSqlCommand();
            cmd.CommandText = "select count(*) from usermaster where ltrim(rtrim(userid))='" + user + "'";
            int cnt = Convert.ToInt32(BaseDb.ExecuteScalar(cmd).ToString());
            return (cnt);

        }

        public static DataTable GetAccessByDate(DateTime tdate, string uname)
        {
            DbCommand dbComm = BaseDb.CreateStrCommand();
            dbComm.CommandText = "SELECT * from usermaster where userid='" + uname + "' AND status='A' AND ((close_dt IS NULL) OR (close_dt >= '" + tdate + "'))";
            DataTable fieldData;
            //try
            //{
                fieldData = BaseDb.ExecuteSelect(dbComm);


            //}
            //catch (Exception exc)
            //{

                //throw exc;
            //}
            return fieldData;
        }

        public static DataTable GetwebPassword(string userid)
        {
            DbCommand dbComm = BaseDb.CreateSqlCommand();
            dbComm.CommandText = "select password from viewusermaster1 where userid= '" + userid + "' and access_for_data_upload=1";
            DataTable fieldData;
            try
            {
                fieldData = BaseDb.ExecuteSelect(dbComm);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return fieldData;
        }


        public static DataTable Getpasswordwithoutaccess(string userid)
        {
            DbCommand dbComm = BaseDb.CreateSqlCommand();
            dbComm.CommandText = "select password from viewusermaster1 where userid= '" + userid + "' ";
            DataTable fieldData;
            try
            {
                fieldData = BaseDb.ExecuteSelect(dbComm);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return fieldData;
        }


        public static DataTable GetRoleId(string userid)
        {
            DbCommand dbComm = BaseDb.CreateStrCommand();
            dbComm.CommandText = "select PASSWORD as password from USERMASTER where USERID='" + userid + "'";
            DataTable fieldData;
            try
            {
                fieldData = BaseDb.ExecuteSelect(dbComm);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return fieldData;
        }
    }
}
