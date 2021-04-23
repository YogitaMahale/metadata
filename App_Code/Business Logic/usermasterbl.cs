using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;

/// <summary>
/// Summary description for usermasterbl
/// </summary>
namespace HydroProject.Code
{
    public class usermasterbl
    {
        public usermasterbl()
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
        public static int insertusermaster(usermasterentity objm)
        {
            DbCommand dbcom = BaseDb.CreateStrCommand();
            try

            {
                
                string abc = objm.Getfrom_dt.ToString();
                string def = objm.Getto_dt.ToString();

                if (objm.Getto_dt.ToString() != "Null"  && objm.Getfrom_dt.ToString()!="Null")
                {
                    dbcom.CommandText = "insert into usermaster(userid,username,password,start_dt,close_dt,status,created_by,created_dt) values('" + objm.Getuserid + "','" + objm.Getusername + "','" + objm.Getpassword + "','" + Convert.ToDateTime(objm.Getfrom_dt.ToString()).Date + "','" + dateconvert.Convertdt(objm.Getto_dt.ToString()) + "','" + objm.Getstatus + "','" + objm.Getcreated_by + "','" + dateconvert.Convertdt(objm.Getcreated_dt.ToString()) + "')";
                }
                else if (objm.Getto_dt.ToString() == "Null")
                {
                    dbcom.CommandText = "insert into usermaster(userid,username,password,start_dt,status,created_by,created_dt) values('" + objm.Getuserid + "','" + objm.Getusername + "','" + objm.Getpassword + "','" + dateconvert.Convertdt(objm.Getfrom_dt.ToString()) + "','" + objm.Getstatus + "','" + objm.Getcreated_by + "','" + dateconvert.Convertdt(objm.Getcreated_dt.ToString()) + "')";
                }
                BaseDb.ExecuteNonQuery(dbcom);
                return (1);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static DataTable showdata(string srno)
        {
            try
            {
            DbCommand dbcom = BaseDb.CreateStrCommand();
            dbcom.CommandText = "select userid,username,convert(varchar,start_dt,103) as start_dt,convert(varchar,close_dt,103) as close_dt,case when status = 'A' then 'Active' else 'Inactive' end as status from usermaster where srno=" + Convert.ToInt16(srno);
            DataTable dt=new DataTable();
            dt=BaseDb.ExecuteSelect(dbcom);
            return(dt);
            }
            catch(Exception ex)
            {
                throw(ex);
            }

        }

        public static void updateusermaster(usermasterentity objm)
        {
            try
            {
                DbCommand dbcom = BaseDb.CreateStrCommand();
                if (objm.Getto_dt.ToString() != "Null")
                {
                    dbcom.CommandText = "update usermaster set close_dt='" + objm.Getto_dt + "',status='" + objm.Getstatus + "',updated_by='" + objm.Getupdated_by + "',updated_dt='" + objm.Getupdated_dt + "' where srno=" + objm.Getsrno;
                }
                else if (objm.Getto_dt.ToString() == "Null")
                {
                    dbcom.CommandText = "update usermaster set status='" + objm.Getstatus + "',updated_by='" + objm.Getupdated_by + "',updated_dt='" + objm.Getupdated_dt + "' where srno=" + objm.Getsrno;
                }
                BaseDb.ExecuteNonQuery(dbcom);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
