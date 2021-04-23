using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data.Common;

using System.Data;



/// <summary>
/// Summary description for updatedatadesc
/// </summary>
namespace HydroProject.Code
{
    public class updatedatadesc
    {
        public updatedatadesc()
        {
            //
            // TODO: Add constructor logic here
            //
        }
         public static int updatedesc(string SaveLocation)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation);
            conn.Open();
            DataTable paradescdt = new DataTable();
            OleDbCommand cmd = new OleDbCommand("SELECT UCase(LTrim(RTrim([Data Type]))) AS parameterid,Unit.[Data Type Description] as descr FROM Unit ", conn);
             OleDbDataReader rdr = null;
             rdr = cmd.ExecuteReader();
             paradescdt.Load(rdr);
            
           try
           {
               DbCommand tmpcmd = BaseDb.CreatetmpCommand();

               if (paradescdt.Rows.Count > 0)
               {
                   for (int i = 0; i < paradescdt.Rows.Count; i++)
                   {
                       
                       tmpcmd.CommandText = "select count(*) from dataparameter where upper(ltrim(rtrim(parameterid)))='" + paradescdt.Rows [i]["parameterid"].ToString() + "'";
                       int cnt = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
                       if (cnt > 0)
                       {
                           tmpcmd.CommandText = "update dataparameter set parameterdesc='" + paradescdt.Rows[i]["descr"].ToString() + "' where upper(ltrim(rtrim(parameterid)))='" + paradescdt.Rows [i]["parameterid"].ToString() + "'";
                           BaseDb.ExecuteNonQuery(tmpcmd);
                       }

                   }

                  
                    
               }
               conn.Close();
               DbCommand cmd1 = BaseDb.CreatetmpCommand();
               cmd1.CommandText = "select count(*) from dataparameter where parameterdesc is null";
               int cnt1 = Convert.ToInt32(BaseDb.ExecuteScalar(cmd1).ToString());
               return (cnt1);

               //{
               //    rowindex = 2 + index;
               //    if ((int)rowindex > rg.Row)
               //    {
               //        break;
               //    }
               //    else
               //    {

               //        string abc = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowindex, colindex1]).Value2.ToString();
               //        string def = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowindex, colindex2]).Value2.ToString();
               //        index++;
               //        DbCommand cmd = BaseDb.CreatetmpCommand();
               //        cmd.CommandText = "select count(*) from dataparameter where parameterid='" + abc + "'";
               //        int i = Convert.ToInt32(BaseDb.ExecuteScalar(cmd).ToString());
               //        if (i > 0)
               //        {
               //            cmd.CommandText = "update dataparameter set parameterdesc='" + def + "' where parameterid='" + abc + "'";
               //            BaseDb.ExecuteNonQuery(cmd);
               //        }
               //    }
               //}

               //DbCommand cmd1 = BaseDb.CreatetmpCommand();
               //cmd1.CommandText = "select count(*) from dataparameter where parameterdesc is null";
               //int cnt = Convert.ToInt32(BaseDb.ExecuteScalar(cmd1).ToString());
               //return (cnt);
           }
           catch (Exception ex)
           {
               throw (ex);
           }

            
        


           //string strconn = "Provider=Microsoft.Jet.OLEDB.4.0 ;" +
           //               "Data Source=C:\\units1.xls;" + "Extended Properties= Excel 8.0";

        }

        public static System.Data.DataTable getgriddata()
        {
            DbCommand cmd = BaseDb.CreatetmpCommand();
            cmd.CommandText = "select srno,parameterid,parameterdesc from dataparameter where parameterdesc is null";
            System.Data.DataTable dt;
            try
            {
                dt = BaseDb.ExecuteSelect(cmd);
                return (dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static System.Data.DataTable getusergriddata()
        {
            DbCommand cmd = BaseDb.CreateStrCommand();
            cmd.CommandText = "select srno,userid,username,case when status = 'A' then 'Active' else 'Inactive' end as status from usermaster order by created_dt desc";
            System.Data.DataTable dt;
            try
            {
                dt = BaseDb.ExecuteSelect(cmd);
                return (dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static System.Data.DataTable ShowUserDetailsbycond(string cond)
        {
            DbCommand cmd = BaseDb.CreateStrCommand();
            cmd.CommandText = "select srno,userid,username,case when status = 'A' then 'Active' else 'Inactive' end as status from usermaster where " + cond + " order by created_dt desc";
            System.Data.DataTable dt;
            try
            {
                dt = BaseDb.ExecuteSelect(cmd);
                return (dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int updatedatadescription(MetaDataEntity objm,string parameterid)
        {
            DbCommand cmd = BaseDb.CreatetmpCommand();
            cmd.CommandText = "update dataparameter set parameterdesc='" + objm.getparameter + "' where parameterid='" + parameterid + "'";
            int error=-1;
            try
            {
                BaseDb.ExecuteNonQuery(cmd);
                error = 0;
                return (error);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }

        }
        public static int getcount()
        {
            DbCommand cmd = BaseDb.CreatetmpCommand();
            cmd.CommandText = "select count(*) from dataparameter where parameterdesc is null";
            int cnt = Convert.ToInt32(BaseDb.ExecuteScalar(cmd).ToString());
            return (cnt);
        }
    }
}
