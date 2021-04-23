using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.OleDb ;


/// <summary>
/// Summary description for DUPLICATEBL
/// </summary>
namespace HydroProject.Code
{
    public class DUPLICATEBL
    {
        public DUPLICATEBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static Int32 chkempty()
        {
            int result;

            DbCommand tmpdb = BaseDb.CreatetmpCommand();
            tmpdb.CommandText = "select count(*) from location";
             result = Convert.ToInt32(BaseDb.ExecuteScalar(tmpdb));
             if (result == 0)
             {
                 tmpdb.CommandText = "select count(*) from dataparameter";
                 result = Convert.ToInt32(BaseDb.ExecuteScalar(tmpdb));
                 if (result == 0)
                 {
                     tmpdb.CommandText = "select count(*) from avlbldatadet";
                     result = Convert.ToInt32(BaseDb.ExecuteScalar(tmpdb));
                     if (result==0)
                     {
                         tmpdb.CommandText = "select count(*) from misngdatadet";
                         result = Convert.ToInt32(BaseDb.ExecuteScalar(tmpdb));

                     }
                 }
             }

             if (result == 0)
             {
                 return (0);
             }
             else
             {
                 return (1);
             }
        }

        public static Int32 chkuser()
        {
            int cnt;

            DbCommand cmd = BaseDb.CreateSqlCommand();
            cmd.CommandText = "select count(*) from viewusermaster1 where ACCESS_FOR_DATA_UPLOAD=1";
            cnt = Convert.ToInt32(BaseDb.ExecuteScalar(cmd).ToString());

            return (cnt);

        }
        public static Int32 chkverfied()
        {
            int i = 0, datacnt = 0,cnt=0;
            DbCommand tmpcmd = BaseDb.CreatetmpCommand();
            tmpcmd.CommandText = "select count(*) from location ";
            datacnt = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
            if (datacnt>0)
            {
                tmpcmd.CommandText = "select count(*) from location where verify=1";
                cnt=Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
            }
             
            
                 tmpcmd.CommandText = "select count(*) from dataparameter  ";
                    datacnt = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
                    if (datacnt > 0)
                    {
                        tmpcmd.CommandText = "select count(*) from dataparameter where verify=1";
                        cnt = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
                    }
                       
                        tmpcmd.CommandText = "select count(*) from avlbldatadet ";
                            datacnt = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());

                            if (datacnt > 0)
                            {
                                tmpcmd.CommandText = "select count(*) from avlbldatadet where verify=1";
                                cnt = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
                            }
                               
                                    tmpcmd.CommandText = "select count(*) from misngdatadet ";
                                    datacnt = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
                                    if (datacnt > 0)
                                    {
                                        tmpcmd.CommandText = "select count(*) from misngdatadet where verify=1";
                                        cnt = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
                                    }
                                        if (cnt > 0)
                                        {
                                            i = 1;
                                        }
                                 
                                   
            return (i);
        }

        public static Int32 chkduplicatelocation(MetaDataEntity objm)
        {
           DbCommand  cmd = BaseDb.CreateStrCommand();
           cmd.CommandText = "select count(*) from Location where realstat='" + objm.Getrealstationname+"'";
           int chk =Convert.ToInt32( BaseDb.ExecuteScalar(cmd).ToString());

           return (chk);

        }
        public static Int32 chkduplicateparameter(MetaDataEntity objm)
        {
            DbCommand cmd = BaseDb.CreateStrCommand();
            cmd.CommandText = "select count(*) from DATAPARAMETER where parameterid='" + objm.getparameter+"'";
            int chk = Convert.ToInt32(BaseDb.ExecuteScalar(cmd));
            return (chk);
        }
        public static Int32 chkduplicatedata(MetaDataEntity objm,string type)
        {
            DbCommand cmd = BaseDb.CreateStrCommand();
            cmd.CommandText = "select count(*) from avlbldatadet where realstat='" + objm.Getstation + "' and parameterid='" + objm.getparameter + "' and type='"+type+"'";
            int chk = Convert.ToInt32(BaseDb.ExecuteScalar(cmd));
            return (chk);
        }
        public static Int32 chknewentry(string path, string type)
        {
           
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=True");
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select realstat from location ", conn);
            OleDbDataReader rdr = null;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            DataSet ds = new DataSet();
            DbCommand cmd1 = BaseDb.CreateStrCommand();
            cmd1.CommandText = "select realstat from Location ";
            DataTable dt1 = new DataTable();
            dt1 = BaseDb.ExecuteSelect(cmd1);
          
            var query1 = dt.AsEnumerable().Select(a => new { ID = a["realstat"].ToString() });
            var query2 = dt1.AsEnumerable().Select(b => new { ID = b["realstat"].ToString() });
            var exceptresult = query1.Except(query2);
         
            int i = exceptresult.Count();
         
            return (i);
             }

        public static Int32 chknewentry1(string path,string type)
        {
             OleDbConnection conn = new OleDbConnection("Provider=Microsoft.jet.OLEDB.4.0; Data Source=" + path);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select realstat from location ", conn);
            OleDbDataReader rdr = null;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            DataSet ds=new DataSet();
            DbCommand cmd1=BaseDb.CreateStrCommand();
            cmd1.CommandText="select realstat from location ";
            //"select ralstat from location where type='" + type + "'"
            DataTable dt1=new DataTable();
            dt1=BaseDb.ExecuteSelect(cmd1);
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt);
            DataView dtview=new DataView(dt1);
            int i=(dtview.FindRows(dt)).Count();
             return(i);
            //dtview.RowFilter="realstat='"+
            
        }

        public static Int32 chkfile(string path)
        {
            int cnt;
            
              try
{
              OleDbConnection conn = new OleDbConnection("Provider=Microsoft.jet.OLEDB.4.0; Data Source=" + path);
            conn.Open();
           
            
            DataTable dt1 = conn.GetSchema();
            
            DataTable dt2 = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt2.Columns[2];
            dt2.PrimaryKey = keys;
            DataRow dr = null;
           
            if (dt2.Rows.Count > 0)
            {
                dr = dt2.Rows.Find("WQL_Samples");
            }
            if (dr != null)
            {
                cnt = 1;

            }
            else
            {
                if (dt2.Rows.Count > 0)
                {
                    dr = dt2.Rows.Find("Sediment");
                }
                if (dr != null)
                {
                    cnt = 2;
                }
                else
                {
                    cnt = 0;
                }
            }
         
              
           // DataTable dt3=conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns,new Object[]{null,null,null,"COLUMN"});

          //OleDbSchemaGuid cmg = new OleDbSchemaGuid();
          //string tbl=  OleDbSchemaGuid.Tables.ToString();
          //string  tbl2=OleDbSchemaGuid.Tables_Info.ToString();
           
           return (cnt);

  }
catch(OleDbException ex)
{
   throw(ex);
}
        }
        public static Int32  getlocation()
        {
            DbCommand tmpcmd= BaseDb.CreatetmpCommand();
            tmpcmd.CommandText = "select count(distinct realstat) from location where querytype='insert'";
            int cnt =  Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());

            return cnt;

        }


        public static DataTable getlocationdet()
        {
            DbCommand tmpcmd = BaseDb.CreatetmpCommand();
            tmpcmd.CommandText = "select distinct realstat,statname from location where querytype='insert'";
            DataTable dt = BaseDb.ExecuteSelect(tmpcmd);

            return dt;

        }


        public static Int32 getparameter()
        {
            DbCommand tmpcmd = BaseDb.CreatetmpCommand();
            tmpcmd.CommandText = "select count(*) from dataparameter where querytype='insert'";
            int cnt = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
            return (cnt);

        }


        public static DataTable  getparameterdet()
        {
            DbCommand tmpcmd = BaseDb.CreatetmpCommand();
            tmpcmd.CommandText = "select parameterid,parameterdesc from dataparameter where querytype='insert'";
            DataTable dt = BaseDb.ExecuteSelect(tmpcmd);

            return dt;

        }
        public static DataTable getmisng()
        {
            DataTable dtmsngyr = new DataTable();
            DbCommand tmpcmd = BaseDb.CreatetmpCommand();
            DbCommand localcmd = BaseDb.CreateStrCommand();
            tmpcmd.CommandText = "select distinct realstat,datatypeid,parameterid,tiunit,divider,frequency from misngdatadet where cancel=0 ";
            DataTable dt = BaseDb.ExecuteSelect(tmpcmd);
            dt.Columns.Add("year");
            dt.Columns.Add("availability");
            dt.Columns.Add("msngavlbl");
            if (dt.Rows.Count > 0)
            {
               
                foreach (DataRow  dr in dt.Rows)
                {
                    string myear = "";
                    string avlbl = "";
                    string avlblyear = "";
                    dtmsngyr.Clear();
                    string tiunit = dr["tiunit"].ToString();
                    if (dr["tiunit"].ToString()==""&& dr["divider"].ToString()=="")
                    {
                        tmpcmd.CommandText = "select year from misngdatadet where cancel=0 and realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "' ";
                    }
                    else
                    {
                    tmpcmd.CommandText = "select year from misngdatadet where cancel=0 and realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "' and tiunit="+dr["tiunit"]+" and divider="+dr["divider"];
                    }
                    dtmsngyr = BaseDb.ExecuteSelect(tmpcmd);
                    if (dtmsngyr.Rows.Count > 0)
                    {
                        foreach (DataRow msngrow in dtmsngyr.Rows)
                        {
                            string myear1 = msngrow["year"].ToString();

                            myear = myear + myear1 + ", ";

                        }

                    }
                    if (myear == "")
                    {
                        dr["year"] = "";
                    }
                    else
                    {

                        dr["year"] = myear.Substring(0, myear.Length - 2);
                    }

                    if (dr["tiunit"].ToString() == "" && dr["divider"].ToString() == "")
                    {
                        tmpcmd.CommandText = "select count(*) from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "'";
                    }
                    else
                    {
                        tmpcmd.CommandText = "select count(*) from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "' and tiunit=" + dr["tiunit"] + " and divider=" + dr["divider"];
                    }
                    int cnt=Convert.ToInt32( BaseDb.ExecuteScalar(tmpcmd).ToString());
                    if (cnt == 0)
                    {
                        if (dr["tiunit"].ToString() == "" && dr["divider"].ToString() == "")
                        {
                            localcmd.CommandText = "select count(*) from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "'";
                        }
                        else
                        {
                            localcmd.CommandText = "select count(*) from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "' and tiunit=" + dr["tiunit"] + " and divider=" + dr["divider"];
                        }
                        int cnt1 = Convert.ToInt32(BaseDb.ExecuteScalar(localcmd).ToString());
                        if (cnt1 > 0)
                        {
                            if (dr["tiunit"].ToString() == "" && dr["divider"].ToString() == "")
                            {
                                localcmd.CommandText = "select convert(varchar,year(from_date)) as from_date,convert(varchar,year(to_date)) as to_date from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "'";
                            }
                            else
                            {
                                localcmd.CommandText = "select convert(varchar,year(from_date)) as from_date,convert(varchar,year(to_date)) as to_date from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "' and tiunit=" + dr["tiunit"] + " and divider=" + dr["divider"];
                            }
                            DataTable dt1 = BaseDb.ExecuteSelect(localcmd);
                            if (dt1.Rows.Count > 0)
                            {
                                avlbl = dt1.Rows[0]["from_date"] + " - " + dt1.Rows[0]["to_date"];

                            }

                        }

                    }
                    else
                    {
                        if (dr["tiunit"].ToString() == "" && dr["divider"].ToString() == "")
                        {
                            tmpcmd.CommandText = "select convert(varchar,year(from_date)) as from_date,convert(varchar,year(to_date)) as to_date from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "' ";
                        }
                        else
                        {
                            tmpcmd.CommandText = "select convert(varchar,year(from_date)) as from_date,convert(varchar,year(to_date)) as to_date from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "' and tiunit=" + dr["tiunit"] + " and divider=" + dr["divider"];
                        }
                        DataTable dt1 = BaseDb.ExecuteSelect(tmpcmd);
                        if (dt1.Rows.Count > 0)
                        {
                            avlbl = dt1.Rows[0]["from_date"] + " - " + dt1.Rows[0]["to_date"];

                        }

                    }
                    dr["availability"] = avlbl.ToString();
                    if (dr["tiunit"].ToString() == "" && dr["divider"].ToString() == "")
                    {
                        tmpcmd.CommandText = "select year from misngdatadet where cancel=1 and realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "'";
                    }
                    else
                    {
                        tmpcmd.CommandText = "select year from misngdatadet where cancel=1 and realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "' and tiunit=" + dr["tiunit"] + " and divider=" + dr["divider"];
                    }
                    DataTable dtmsngavlbl = BaseDb.ExecuteSelect(tmpcmd);
                    if (dtmsngavlbl.Rows.Count > 0)
                    {
                        foreach (DataRow msngrow in dtmsngavlbl.Rows)
                        {
                            string avlblyear1= msngrow["year"].ToString();
                            avlblyear = avlblyear + avlblyear1 + ",";

                        }

                    }
                    if (avlblyear == "")
                    {
                        dr["msngavlbl"] = "";
                    }
                    else
                    {

                        dr["msngavlbl"] = avlblyear.Substring(0, avlblyear.Length - 1);
                    }
                }
            }

           // tmpcmd.CommandText = "select distinct realstat,parameterid from misngdatadet where cancel=0 and querytype='update'";


            return (dt);
        }
        public static void clearall()
        {   
            DbCommand tmpdb = BaseDb.CreatetmpCommand();
            tmpdb.CommandText = "Delete from avlbldatadet";
            BaseDb.ExecuteNonQuery(tmpdb);
            tmpdb.CommandText = "delete from misngdatadet";
            BaseDb.ExecuteNonQuery(tmpdb);
            tmpdb.CommandText = "delete from dataparameter";
            BaseDb.ExecuteNonQuery(tmpdb);
            tmpdb.CommandText = "delete from datatype";
            BaseDb.ExecuteNonQuery(tmpdb);
            tmpdb.CommandText = "delete from location";
            BaseDb.ExecuteNonQuery(tmpdb);
        }

        public static Int32 chktruncate()
        {
            int j = 0;
            DbCommand sqlcmd = BaseDb.CreateSqlCommand();
            sqlcmd.CommandText = "select count(*) from dataparameter";
            int cnt = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd).ToString());
            if (cnt ==0)
            {
                sqlcmd.CommandText = "select count(*) from misngdatadet";
                cnt = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd).ToString());
                if (cnt == 0)
                {
                    sqlcmd.CommandText = "select count(*) from avlbldatadet";
                    cnt = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd).ToString());
                    if (cnt == 0)
                    {
                        sqlcmd.CommandText = "select count(*) from datatype";
                        cnt = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd).ToString());
                        if (cnt == 0)
                        {
                            j = 0;
                        }
                        else
                        {
                            j = 1;
                        }
                    }
                    else
                    {
                        j = 1;
                    }
                }
                else
                {
                    j = 1;
                }
            }
            else
            {
                 j = 1;
            }

            return (j);
        }

    }
}
