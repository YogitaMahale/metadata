using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;


/// <summary>
/// Summary description for reportdata
/// </summary>

namespace HydroProject.Code
{
    public class reportdata
    {
        public reportdata()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DataTable getavailability()
        {
            DbCommand cmd = BaseDb.CreatetmpCommand();
            cmd.CommandText = "select realstat,datatypeid,parameterid,year from MISNGDATADET  order by REALSTAT ";
            DataTable dt = BaseDb.ExecuteSelect(cmd);
            dt.Columns.Add("availability");

            DataTable dtmsngyr = new DataTable();
            DbCommand tmpcmd = BaseDb.CreatetmpCommand();
            DbCommand localcmd = BaseDb.CreateStrCommand();
            tmpcmd.CommandText = "select distinct realstat,datatypeid,parameterid from misngdatadet where cancel=0 ";
            DataTable dt1 = BaseDb.ExecuteSelect(tmpcmd);
            dt1.Columns.Add("availability");

            if (dt1.Rows.Count > 0)
            {

                foreach (DataRow dr in dt1.Rows)
                {

                    string avlbl="";

                    tmpcmd.CommandText = "select count(*) from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "'";
                    int cnt = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
                    if (cnt == 0)
                    {
                        localcmd.CommandText = "select count(*) from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "'";
                        int cnt1 = Convert.ToInt32(BaseDb.ExecuteScalar(localcmd).ToString());
                        if (cnt1 > 0)
                        {
                            localcmd.CommandText = "select convert(varchar,year(from_date)) as from_date,convert(varchar,year(to_date)) as to_date from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "'";
                            DataTable dt2 = BaseDb.ExecuteSelect(localcmd);
                            if (dt2.Rows.Count > 0)
                            {
                                avlbl = dt2.Rows[0]["from_date"] + " - " + dt2.Rows[0]["to_date"];

                            }

                        }

                    }
                    else
                    {
                        tmpcmd.CommandText = "select convert(varchar,year(from_date)) as from_date,convert(varchar,year(to_date)) as to_date from avlbldatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "'";
                        DataTable dt3 = BaseDb.ExecuteSelect(tmpcmd);
                        if (dt3.Rows.Count > 0)
                        {
                            avlbl = dt3.Rows[0]["from_date"] + " - " + dt3.Rows[0]["to_date"];

                        }

                    }
                    dr["availability"] = avlbl.ToString();

                    //tmpcmd.CommandText = "select year from misngdatadet where cancel=1 and realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "' ";
                    //DataTable dtmsngavlbl = BaseDb.ExecuteSelect(tmpcmd);
                    //string avlblyear="";
                    //if (dtmsngavlbl.Rows.Count > 0)
                    //{
                    //    foreach (DataRow msngrow in dtmsngavlbl.Rows)
                    //    {
                    //        string avlblyear1 = msngrow["year"].ToString();
                    //        avlblyear = avlblyear + avlblyear1 + ",";

                    //    }

                    //}
                    //if (avlblyear == "")
                    //{
                    //    dr["msngavlbl"] = "";
                    //}
                    //else
                    //{

                    //    dr["msngavlbl"] = avlblyear.Substring(0, avlblyear.Length - 1);
                    //}
                }

            }

            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        if (dr["realstat"].ToString() == drow["realstat"].ToString() && dr["parameterid"].ToString() == drow["parameterid"].ToString())
                        {
                            drow["availability"] = dr["availability"].ToString();
                        }
                    }

                }
            }
            return (dt);

        }
    }
}
