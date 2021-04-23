using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;

/// <summary>
/// Summary description for changepasswordbl
/// </summary>

namespace HydroProject.Code
{
    public class changepasswordbl
    {
        public changepasswordbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DataTable GetPassword(string userid)
        {
            DbCommand cmd = BaseDb.CreateStrCommand();
            cmd.CommandText = "select password from usermaster where userid='" + userid + "'";
            DataTable dt = BaseDb.ExecuteSelect(cmd);
            return (dt);
        }
        public static string computehash(string givenpaswd)
        {
            MD5CryptoServiceProvider md5algo = new MD5CryptoServiceProvider();
            byte[] bytevalue = System.Text.Encoding.UTF8.GetBytes(givenpaswd);
            byte[] bytehash = md5algo.ComputeHash(bytevalue);
            md5algo.Clear();
            return Convert.ToBase64String(bytehash);
        }
        public static void updatepassword(usermasterentity objm)
        {
            try
            {
                DbCommand cmd = BaseDb.CreateStrCommand();
                cmd.CommandText = "update usermaster set password='" + objm.Getpassword + "'where userid='" + objm.Getuserid + "'";
                BaseDb.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }

   
}
