using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;

/// <summary>
/// Summary description for userloginbl
/// </summary>

namespace HydroProject.Code
{
    public class userloginbl
    {
        public userloginbl()
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

        public static DataTable GetPassword(string userid)
        {
            DbCommand dbComm = BaseDb.CreateStrCommand();
            dbComm.CommandText = "select password from hdumaster where userid= '" + userid + "'";
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