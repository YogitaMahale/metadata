using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Data.SqlClient;  
using System.Configuration;
using HydroProject.Code;
using System.Globalization;
using System.Threading;
using System.IO;



/// <summary>
/// Summary description for metadatasavebl
/// </summary>

namespace HydroProject.Code
{
    public class metadatasavebl
    {
        public metadatasavebl()
        {
            //
            // TODO: Add constructor logic here
            //
        }
            
            

            #region preparing data
        public static void preparedata(string filename, MetaDataEntity objm,string type)
        {
            string COND;
            CultureInfo cultinf = Thread.CurrentThread.CurrentCulture;
            TextInfo txt = cultinf.TextInfo;
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+ filename);
            conn.Open();
            DataTable mdbdtlocation = new DataTable();
            OleDbDataReader rdr = null;

            try
            {
                #region Getting data from Location table From mdb
                try
                {
                    OleDbDataReader rdr1;

                    //  OleDbCommand cmd1 = new OleDbCommand("SELECT distinct realstat,(IIf((datatype) like 'M%','Metereological',IIf((datatype) like 'H%','Hydrological'))  ) as datatypeid FROM Series ", conn);
                    // rdr1 = cmd1.ExecuteReader();


                    OleDbCommand cmd1 = new OleDbCommand("select a.datatypeid,b.* from location b,(SELECT distinct realstat,(IIf((datatype) like 'M%','Meteorological',IIf((datatype) like 'H%','Hydrological'))  ) as datatypeid FROM Series) a where a.datatypeid is not null and a.realstat=b.realstat", conn);
                    rdr1 = cmd1.ExecuteReader();

                    DataTable datatypewisetable = new DataTable();

                    datatypewisetable.Load(rdr1);
                    OleDbCommand cmd = new OleDbCommand("select * from Location", conn);

                    rdr = cmd.ExecuteReader();
                    DataSet ds = new DataSet();

                    mdbdtlocation.Load(rdr);
                    int i = 0;
                    DataTable sqldtlocation = new DataTable();
                    DbCommand sqlcmd = BaseDb.CreateStrCommand();
                    sqlcmd.CommandText = "select realstat,datatypeid from Location where datatypeid in ('Hydrological','Meteorological')";
                    sqldtlocation = BaseDb.ExecuteSelect(sqlcmd);

                    IEnumerable<DataRow> query1 = from loc in sqldtlocation.AsEnumerable()
                                                  select loc;

                    IEnumerable<DataRow> query3 = from dt in datatypewisetable.AsEnumerable()
                                                  where dt.Field<string>("datatypeid") != null
                                                  select dt;

                    int cnt3 = query1.Count();
                    DataTable qry1 = new DataTable();

                    if (cnt3 > 0)
                    {
                        qry1 = query1.CopyToDataTable();
                    }
                    else
                    {

                        qry1.Columns.Add("realstat");
                        qry1.Columns.Add("datatypeid");
                    }
                    DataTable qry3 = query3.CopyToDataTable();


                    var rowsonlyindt1 = qry3.AsEnumerable().Where(r => qry1.AsEnumerable().Any(r2 => r["realstat"].ToString().Trim().ToUpper() == r2["realstat"].ToString().Trim().ToUpper() &&
                                    r["datatypeid"].ToString().Trim().ToUpper() == r2["datatypeid"].ToString().Trim().ToUpper()));

                    int cnt4 = rowsonlyindt1.Count();
                    if (cnt4>0)
                    {

                        DataTable rowindt1 = rowsonlyindt1.CopyToDataTable();



                        //var rows1 = from r in qry3.AsEnumerable()
                        //            where !qry1.AsEnumerable().Any(r2 => r["realstat"].ToString().Trim().ToUpper() == r2["realstat"].ToString().Trim().ToUpper() &&
                        //           r["datatypeid"].ToString().Trim().ToUpper()==r2["datatypeid"].ToString().Trim().ToUpper())
                        //           select r;

                        //DataTable dtrows1=rows1.CopyToDataTable();

                        //var nonintersect=datatypewisetable.AsEnumerable().Select( r=>r.Field<string>("realstat"),r.Field<string>("datatypeid")).Except(sqldtlocation.AsEnumerable().Select(r=>r.Field<string>("realstat"),r.Field<string>("datatypeid")));


                        //var sqlquery1=mdbdtlocation.AsEnumerable().Select(a=>new {ID=a["realstat"].ToString()});
                        //var sqlquery2=sqldtlocation.AsEnumerable().Select(b=>new{ID=b["realstat"].ToString()});
                        //var q = sqlquery1.Except(sqlquery2);


                        //int cnt=q.Count();
                        int cnt = rowindt1.Rows.Count;

                        var q = from r in rowindt1.AsEnumerable() select r;
                        if (cnt > 0)
                        {
                            //DataTable dt = (from a in datatypewisetable.AsEnumerable() join ab in q on a["realstat"].ToString() equals ab.Field<string>("realstat") select a).CopyToDataTable();


                            //DataSet ds1 = new DataSet();
                            //ds1.Tables.Add(rowindt1);
                            //ds1.Tables.Add(mdbdtlocation);
                            //ds1.Relations.Add(rowindt1.Columns["realstat"], mdbdtlocation.Columns["realstat"]);



                            for (i = 0; i < cnt; i++)
                            {
                                SqlDateTime startdt, enddt;
                                if ((rowindt1.Rows[i]["STARTDATE"].ToString()) != "")
                                {
                                    startdt = DateTime.Parse(rowindt1.Rows[i]["STARTDATE"].ToString()).Date;

                                }
                                else
                                {

                                    SqlDateTime dtate1 = SqlDateTime.Null;
                                    startdt = (SqlDateTime)dtate1;
                                }

                                if ((rowindt1.Rows[i]["ENDDATE"].ToString()) != "")
                                {
                                    enddt = DateTime.Parse(rowindt1.Rows[i]["ENDDATE"].ToString()).Date;

                                }
                                else
                                {


                                    SqlDateTime dtate1 = SqlDateTime.Null;
                                    enddt = (SqlDateTime)dtate1;
                                }

                                DbCommand tempcmd = BaseDb.CreatetmpCommand();
                                if ((rowindt1.Rows[i]["ENDDATE"].ToString()) == "" && (rowindt1.Rows[i]["STARTDATE"].ToString()) == "")
                                {
                                    tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,LOCALXCOORD,LOCALYCOORD,querytype)values " +
                                        "('" + rowindt1.Rows[i]["datatypeid"] + "','" + txt.ToTitleCase(rowindt1.Rows[i]["realstat"].ToString()) + "','" + rowindt1.Rows[i]["statname"].ToString() + "','" + rowindt1.Rows[i]["country"] + "','" + rowindt1.Rows[i]["province"] + "','" + rowindt1.Rows[i]["subdistrict"] + "','" + rowindt1.Rows[i]["basin"] + "','" + rowindt1.Rows[i]["rivername"] + "','" + rowindt1.Rows[i]["tributary"] + "','" + rowindt1.Rows[i]["catch"] + "','" + rowindt1.Rows[i]["latitude"] + "','" + rowindt1.Rows[i]["longitude"] + "','" + rowindt1.Rows[i]["altitude"].ToString() + "','" + rowindt1.Rows[i]["catarea"] + "','" + rowindt1.Rows[i]["toposheet"].ToString() + "','" + rowindt1.Rows[i]["agency"].ToString() + "','" + rowindt1.Rows[i]["regoffice"].ToString() + "','" + rowindt1.Rows[i]["circoffice"].ToString() + "','" + rowindt1.Rows[i]["divoffice"].ToString() + "','" + rowindt1.Rows[i]["subdivoffice"].ToString() + "','" + rowindt1.Rows[i]["sectoffice"].ToString() + "','" + rowindt1.Rows[i]["remarks"].ToString() + "','" + rowindt1.Rows[i]["owneragency"].ToString() + "'," + Convert.ToInt32(rowindt1.Rows[i]["localxcoord"].ToString()) + "," + Convert.ToInt32(rowindt1.Rows[i]["localycoord"].ToString()) + ",'insert')";

                                }
                                else if ((rowindt1.Rows[i]["ENDDATE"].ToString()) == "")
                                {
                                    tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,LOCALXCOORD,LOCALYCOORD,querytype)values " +
                                        "('" + rowindt1.Rows[i]["datatypeid"] + "','" + txt.ToTitleCase(rowindt1.Rows[i]["realstat"].ToString()) + "','" + rowindt1.Rows[i]["statname"].ToString() + "','" + rowindt1.Rows[i]["country"] + "','" + rowindt1.Rows[i]["province"] + "','" + rowindt1.Rows[i]["subdistrict"] + "','" + rowindt1.Rows[i]["basin"] + "','" + rowindt1.Rows[i]["rivername"] + "','" + rowindt1.Rows[i]["tributary"] + "','" + rowindt1.Rows[i]["catch"] + "','" + rowindt1.Rows[i]["latitude"] + "','" + rowindt1.Rows[i]["longitude"] + "','" + rowindt1.Rows[i]["altitude"].ToString() + "','" + rowindt1.Rows[i]["catarea"] + "','" + rowindt1.Rows[i]["toposheet"].ToString() + "','" + rowindt1.Rows[i]["agency"].ToString() + "','" + rowindt1.Rows[i]["regoffice"].ToString() + "','" + rowindt1.Rows[i]["circoffice"].ToString() + "','" + rowindt1.Rows[i]["divoffice"].ToString() + "','" + rowindt1.Rows[i]["subdivoffice"].ToString() + "','" + rowindt1.Rows[i]["sectoffice"].ToString() + "','" + rowindt1.Rows[i]["remarks"].ToString() + "','" + rowindt1.Rows[i]["owneragency"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(startdt)) + "'," + Convert.ToInt32(rowindt1.Rows[i]["localxcoord"].ToString()) + "," + Convert.ToInt32(rowindt1.Rows[i]["localycoord"].ToString()) + ",'insert')";

                                }
                                else if ((rowindt1.Rows[i]["STARTDATE"].ToString()) == "")
                                {
                                    tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,ENDDATE,LOCALXCOORD,LOCALYCOORD,querytype)values " +
                                        "('" + rowindt1.Rows[i]["datatypeid"] + "','" + txt.ToTitleCase(rowindt1.Rows[i]["realstat"].ToString()) + "','" + rowindt1.Rows[i]["statname"].ToString() + "','" + rowindt1.Rows[i]["country"] + "','" + rowindt1.Rows[i]["province"] + "','" + rowindt1.Rows[i]["subdistrict"] + "','" + rowindt1.Rows[i]["basin"] + "','" + rowindt1.Rows[i]["rivername"] + "','" + rowindt1.Rows[i]["tributary"] + "','" + rowindt1.Rows[i]["catch"] + "','" + rowindt1.Rows[i]["latitude"] + "','" + rowindt1.Rows[i]["longitude"] + "','" + rowindt1.Rows[i]["altitude"].ToString() + "','" + rowindt1.Rows[i]["catarea"] + "','" + rowindt1.Rows[i]["toposheet"].ToString() + "','" + rowindt1.Rows[i]["agency"].ToString() + "','" + rowindt1.Rows[i]["regoffice"].ToString() + "','" + rowindt1.Rows[i]["circoffice"].ToString() + "','" + rowindt1.Rows[i]["divoffice"].ToString() + "','" + rowindt1.Rows[i]["subdivoffice"].ToString() + "','" + rowindt1.Rows[i]["sectoffice"].ToString() + "','" + rowindt1.Rows[i]["remarks"].ToString() + "','" + rowindt1.Rows[i]["owneragency"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(enddt)) + "'," + Convert.ToInt32(rowindt1.Rows[i]["localxcoord"].ToString()) + "," + Convert.ToInt32(rowindt1.Rows[i]["localycoord"].ToString()) + ",'insert')";
                                }
                                else
                                {
                                    tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,ENDDATE,LOCALXCOORD,LOCALYCOORD,querytype)values " +
                                        "('" + rowindt1.Rows[i]["datatypeid"] + "','" + txt.ToTitleCase(rowindt1.Rows[i]["realstat"].ToString()) + "','" + rowindt1.Rows[i]["statname"].ToString() + "','" + rowindt1.Rows[i]["country"] + "','" + rowindt1.Rows[i]["province"] + "','" + rowindt1.Rows[i]["subdistrict"] + "','" + rowindt1.Rows[i]["basin"] + "','" + rowindt1.Rows[i]["rivername"] + "','" + rowindt1.Rows[i]["tributary"] + "','" + rowindt1.Rows[i]["catch"] + "','" + rowindt1.Rows[i]["latitude"] + "','" + rowindt1.Rows[i]["longitude"] + "','" + rowindt1.Rows[i]["altitude"].ToString() + "','" + rowindt1.Rows[i]["catarea"] + "','" + rowindt1.Rows[i]["toposheet"].ToString() + "','" + rowindt1.Rows[i]["agency"].ToString() + "','" + rowindt1.Rows[i]["regoffice"].ToString() + "','" + rowindt1.Rows[i]["circoffice"].ToString() + "','" + rowindt1.Rows[i]["divoffice"].ToString() + "','" + rowindt1.Rows[i]["subdivoffice"].ToString() + "','" + rowindt1.Rows[i]["sectoffice"].ToString() + "','" + rowindt1.Rows[i]["remarks"].ToString() + "','" + rowindt1.Rows[i]["owneragency"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(startdt)) + "','" + dateconvert.Convertdt(Convert.ToString(enddt)) + "'," + Convert.ToInt32(rowindt1.Rows[i]["localxcoord"].ToString()) + "," + Convert.ToInt32(rowindt1.Rows[i]["localycoord"].ToString()) + ",'insert')";
                                }
                                BaseDb.ExecuteNonQuery(tempcmd);
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {


                }
                #endregion

                #region Getting data from Series table (for parameter)
                try
                {
                    OleDbCommand seriescmd = new OleDbCommand("select b.parameter as parameter,min(a.starttime) as starttime from series a,(select  distinct DATATYPE as parameter from series  where Tablename is not null or Tablename<>'' ) b where a.datatype=b.parameter group by b.parameter", conn);

                    OleDbDataReader rdr1 = null;

                    rdr1 = seriescmd.ExecuteReader();

                    DataTable dtseries = new DataTable();
                    dtseries.Load(rdr1);

                    int i = 0;
                    DataTable sqldtparameter = new DataTable();
                    DbCommand sqlcmd = BaseDb.CreateStrCommand();
                    sqlcmd.CommandText = "select parameterid from dataparameter where type='" + type + "'";
                    sqldtparameter = BaseDb.ExecuteSelect(sqlcmd);

                    var sqlquery1 = dtseries.AsEnumerable().Select(a => new { ID = a["parameter"].ToString() });
                    var sqlquery2 = sqldtparameter.AsEnumerable().Select(b => new { ID = b["parameterid"].ToString() });
                    var q = sqlquery1.Except(sqlquery2);

                    int cnt = q.Count();

                    DataTable dt = new DataTable();
                    if (cnt > 0)
                    {
                        dt = (from a in dtseries.AsEnumerable() join ab in q on a["parameter"].ToString() equals ab.ID select a).CopyToDataTable();



                        SqlDateTime startdt;
                        if (dt.Rows.Count > 0)
                        {
                            for (i = 0; i < cnt; i++)
                            {
                                if ((dt.Rows[i]["STARTTIME"].ToString()) != "")
                                {
                                    //startdt = DateTime.Parse(dtseries.Rows[i]["STARTTIME"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").DateTimeFormat).Date;
                                    startdt = DateTime.Parse(dtseries.Rows[i]["STARTTIME"].ToString()).Date;

                                    //startdt = dateconvert.converttommddyy((dt.Rows[i]["STARTTIME"].ToString()));
                                }
                                else
                                {

                                    SqlDateTime date1 = SqlDateTime.Null;
                                    startdt = date1;
                                }
                                DbCommand tempcmd = BaseDb.CreatetmpCommand();
                                string DATATYPEIDVal = "null";
                                string strchar = dt.Rows[i]["parameter"].ToString().ToUpper().Trim().Substring(0, 1);
                                if (strchar == "M")
                                {
                                    DATATYPEIDVal = "Meteorological";
                                }
                                else if (strchar == "H")
                                {
                                    DATATYPEIDVal = "Hydrological";
                                }
                                else if (strchar == "Q")
                                {
                                    DATATYPEIDVal = "waterquality";
                                }

                                if ((dt.Rows[i]["STARTTIME"].ToString()) != "")
                                {
                                    tempcmd.CommandText = "insert into dataparameter (parameterid,datafromdt,type,Querytype,DATATYPEID) values('" + dt.Rows[i]["parameter"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(startdt)) + "','" + type + "','insert','"+ DATATYPEIDVal + "')";
                                }
                                else
                                {
                                    tempcmd.CommandText = "insert into dataparameter (parameterid,type,Querytype,DATATYPEID) values('" + dt.Rows[i]["parameter"].ToString() + "','" + type + "','insert','"+ DATATYPEIDVal + "')";
                                }

                                BaseDb.ExecuteNonQuery(tempcmd);
                            }
                        }
                    }

                    var qry1 = sqlquery1.Intersect(sqlquery2);
                    int cnt1 = qry1.Count();

                    if (cnt1 > 0)
                    {
                        dt = (from a in dtseries.AsEnumerable() join ab in qry1 on a["parameter"].ToString() equals ab.ID select a).CopyToDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            for (i = 0; i < dt.Rows.Count; i++)
                            {
                                DbCommand cmd = BaseDb.CreateStrCommand();
                                cmd.CommandText = "select datafromdt,datauptodt from dataparameter where parameterid='" + dt.Rows[i]["parameter"] + "' and type='" + type + "'";
                                DataTable dt1 = BaseDb.ExecuteSelect(cmd);
                                // DateTime DT = DateTime.Parse(dt1.Rows[0]["datafromdt"].ToString()).Date;
                                if (DateTime.Parse(dt1.Rows[0]["datafromdt"].ToString()).Date > DateTime.Parse(dt.Rows[i]["starttime"].ToString()).Date)
                                {
                                    DbCommand tmpcmd = BaseDb.CreatetmpCommand();
                                    string ss = dateconvert.Convertdt(dt1.Rows[0]["datafromdt"].ToString());

                                    string DATATYPEIDVal = "null";
                                    string strchar = dt.Rows[i]["parameter"].ToString().ToUpper().Trim().Substring(0, 1);
                                    if(strchar=="M")
                                    {
                                        DATATYPEIDVal = "Meteorological";
                                    }
                                    else if (strchar == "H")
                                    {
                                        DATATYPEIDVal = "Hydrological";
                                    }
                                    else if (strchar == "Q")
                                    {
                                        DATATYPEIDVal = "waterquality";
                                    }

                                    tmpcmd.CommandText = "insert into dataparameter (parameterid,datafromdt,type,querytype,DATATYPEID) values('" + dt.Rows[i]["parameter"] + "','" + ss + "','" + type + "','update','"+ DATATYPEIDVal + "')";
                                    // tmpcmd.CommandText = "insert into dataparameter (parameterid,datafromdt,type,querytype) values('" + dt.Rows[i]["parameter"] + "','" + DateTime.Parse(dt1.Rows[0]["datafromdt"].ToString()).Date + "','" + type + "','update')";
                                    BaseDb.ExecuteNonQuery(tmpcmd);
                                }
                            }

                        }
                    }
                }
                catch (Exception EX)
                {

                    throw EX;
                }
                finally
                {

                };

                #endregion


                #region getting data from series for AVLBLDATADET and MISNGDATADET
                try
                {
                    //OleDbCommand seriescmd = new OleDbCommand("SELECT realstat,datatype,min(starttime),max(endtime) FROM Series where tablename is not null group by realstat,datatype;", conn);
                    OleDbCommand seriescmd = new OleDbCommand("select REALSTAT,DATATYPE as parameterid,min(STARTTIME) as from_date,max(ENDTIME)as to_date,TIUNIT,DIVIDER from series  group by realstat,datatype,TIUNIT,DIVIDER", conn);//ADDED TIUNIT AND DIVIDER FOR FREQUENCY CONSIDERATION
                    rdr = null;
                    //datatype='HZA' and realstat='Kopargaon'
                    rdr = seriescmd.ExecuteReader();
                    DataTable dtseries = new DataTable();
                    dtseries.Load(rdr);
                    DataTable sqldtavlbl = new DataTable();
                    DbCommand sqlcmd = BaseDb.CreateStrCommand();
                    sqlcmd.CommandText = "select realstat,parameterid,from_date,to_date,TIUNIT,DIVIDER from avlbldatadet where type='" + type + "' and datatypeid in('Hydrological','Meteorological')";
                    //and parameterid='HZA' and realstat='Kopargaon'
                    sqldtavlbl = BaseDb.ExecuteSelect(sqlcmd);
                    int i = 0;
                    DbCommand tmpcmd = BaseDb.CreatetmpCommand();

                    DataTable newdata = new DataTable();
                    newdata.Columns.Add("realstat");
                    newdata.Columns.Add("parameterid");
                    newdata.Columns.Add("from_date");
                    newdata.Columns.Add("to_date");
                    newdata.Columns.Add("tiunit");
                    newdata.Columns.Add("divider");
                    newdata.Columns.Add("frequency");

                    DataTable alreadypresentrecord = new DataTable();
                    alreadypresentrecord.Columns.Add("realstat");
                    alreadypresentrecord.Columns.Add("parameterid");
                    alreadypresentrecord.Columns.Add("from_date");
                    alreadypresentrecord.Columns.Add("to_date");
                    alreadypresentrecord.Columns.Add("tiunit");
                    alreadypresentrecord.Columns.Add("divider");
                    alreadypresentrecord.Columns.Add("frequency");

                    string[] maptiunit = { "Yearly", "Monthly", "Daily", "Hourly" };
                    string[] mapdivider = { " ", "twice", "thrice", "four times", "five times", "six times", "seven times", "eight times", "nine times", "ten times" };
                    if (dtseries.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtseries.Rows)
                        {
                            if (dr["from_date"].ToString() != "" || dr["to_date"].ToString() != "")
                            {
                                string freq = ""; //DSB 26.6.2012
                                int tunit = Convert.ToInt32(dr["tiunit"].ToString());
                                int div = Convert.ToInt32(dr["divider"].ToString());
                                string frequency = maptiunit[tunit - 1].ToString() + " " + mapdivider[div - 1].ToString();
                                sqlcmd.CommandText = "select count(*) from avlbldatadet where parameterid='" + dr["parameterid"] + "' and upper(realstat)='" + dr["realstat"].ToString().ToUpper() + "' and type='" + type + "' and tiunit=" + dr["tiunit"] + " and divider=" + dr["divider"];
                                int cnt = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd).ToString());
                                if (cnt == 0)
                                {
                                    //tmpcmd.CommandText = "insert into AVLBLDATADET (realstat,parameterid,tiunit,divider,frequency,from_date,to_date,type,querytype) values('" + txt.ToTitleCase(dr["realstat"].ToString()) + "','" + dr["parameterid"] + "'," + dr["tiunit"] + "," + dr["divider"] + ",'" + frequency + "','" + dateconvert.Convertdt(dr["from_date"].ToString()) + "','" + dateconvert.Convertdt(dr["to_date"].ToString()) + "','" + type + "','insert')";  //commented by DSB 26.6.2012
                                    DataRow row1 = newdata.NewRow();
                                    row1[0] = dr["realstat"];
                                    row1[1] = dr["parameterid"];
                                    //row1[2] = dr["from_date"].ToString();
                                    //row1[3] = dr["to_date"].ToString();
                                    row1[2] = dateconvert.Convertdt(dr["from_date"].ToString());
                                    row1[3] = dateconvert.Convertdt(dr["to_date"].ToString());
                                    row1[4] = dr["tiunit"];
                                    row1[5] = dr["divider"];
                                    //DSB 26.6.2012
                                    if (frequency == "Daily twice")
                                    {
                                        freq = frequency.Replace("Daily twice", "Twice Daily");
                                        row1[6] = freq;
                                    }
                                    else
                                    {
                                        row1[6] = frequency;
                                    }
                                    //
                                    //row1[6] = frequency;
                                    newdata.Rows.Add(row1);
                                    //added by DSB 26.6.2012
                                    if (frequency == "Daily twice")
                                    {
                                        tmpcmd.CommandText = "insert into AVLBLDATADET (realstat,parameterid,tiunit,divider,frequency,from_date,to_date,type,querytype) values('" + txt.ToTitleCase(dr["realstat"].ToString()) + "','" + dr["parameterid"] + "'," + dr["tiunit"] + "," + dr["divider"] + ",'" + freq + "','" + dateconvert.Convertdt(dr["from_date"].ToString()) + "','" + dateconvert.Convertdt(dr["to_date"].ToString()) + "','" + type + "','insert')";
                                    }
                                    else
                                    {
                                        tmpcmd.CommandText = "insert into AVLBLDATADET (realstat,parameterid,tiunit,divider,frequency,from_date,to_date,type,querytype) values('" + txt.ToTitleCase(dr["realstat"].ToString()) + "','" + dr["parameterid"] + "'," + dr["tiunit"] + "," + dr["divider"] + ",'" + frequency + "','" + dateconvert.Convertdt(dr["from_date"].ToString()) + "','" + dateconvert.Convertdt(dr["to_date"].ToString()) + "','" + type + "','insert')";
                                    }
                                    //
                                    BaseDb.ExecuteNonQuery(tmpcmd);

                                }
                                else
                                {

                                    DataRow row1 = alreadypresentrecord.NewRow();
                                    row1[0] = dr["realstat"];
                                    row1[1] = dr["parameterid"];
                                    //row1[2] = dr["from_date"];
                                    //row1[3] = dr["to_date"];
                                    row1[2] = dateconvert.Convertdt(dr["from_date"].ToString());
                                    row1[3] = dateconvert.Convertdt(dr["to_date"].ToString());
                                    row1[4] = dr["tiunit"];
                                    row1[5] = dr["divider"];
                                    //DSB 26.6.2012
                                    if (frequency == "Daily twice")
                                    {
                                        freq = frequency.Replace("Daily twice", "Twice Daily");
                                        row1[6] = freq;
                                    }
                                    else
                                    {
                                        row1[6] = frequency;
                                    }
                                    //
                                    //row1[6] = frequency;
                                    alreadypresentrecord.Rows.Add(row1);

                                    sqlcmd.CommandText = "select from_date,to_date from avlbldatadet where parameterid='" + dr["parameterid"] + "' and upper(realstat)='" + dr["realstat"].ToString().ToUpper() + "' and type='" + type + "' and tiunit=" + dr["tiunit"] + " and divider=" + dr["divider"];
                                    DataTable tmpdt = new DataTable();
                                    tmpdt = BaseDb.ExecuteSelect(sqlcmd);

                                    if (Convert.ToDateTime(dr["from_date"].ToString()).Date < Convert.ToDateTime(tmpdt.Rows[0]["from_date"].ToString()).Date || Convert.ToDateTime(dr["to_date"].ToString()).Date > Convert.ToDateTime(tmpdt.Rows[0]["to_date"].ToString()).Date)
                                    {
                                        if (frequency == "Daily twice")  //added by DSB on 26.6.2012
                                        {
                                            tmpcmd.CommandText = "insert into avlbldatadet (realstat,parameterid,tiunit,divider,frequency,from_date,to_date,type,querytype) values('" + txt.ToTitleCase(dr["realstat"].ToString()) + "','" + dr["parameterid"] + "'," + dr["tiunit"] + "," + dr["divider"] + ",'" + freq + "','" + dateconvert.Convertdt(dr["from_date"].ToString()) + "','" + dateconvert.Convertdt(dr["to_date"].ToString()) + "','" + type + "','update')";
                                        }
                                        else
                                        {
                                            tmpcmd.CommandText = "insert into avlbldatadet (realstat,parameterid,tiunit,divider,frequency,from_date,to_date,type,querytype) values('" + txt.ToTitleCase(dr["realstat"].ToString()) + "','" + dr["parameterid"] + "'," + dr["tiunit"] + "," + dr["divider"] + ",'" + frequency + "','" + dateconvert.Convertdt(dr["from_date"].ToString()) + "','" + dateconvert.Convertdt(dr["to_date"].ToString()) + "','" + type + "','update')";

                                        }
                                        //tmpcmd.CommandText = "insert into avlbldatadet (realstat,parameterid,tiunit,divider,frequency,from_date,to_date,type,querytype) values('" + txt.ToTitleCase(dr["realstat"].ToString()) + "','" + dr["parameterid"] + "'," + dr["tiunit"] + "," + dr["divider"] + ",'" + frequency + "','" + dateconvert.Convertdt(dr["from_date"].ToString()) + "','" + dateconvert.Convertdt(dr["to_date"].ToString()) + "','" + type + "','update')";  //commented by DSB on 26.6.2012
                                        BaseDb.ExecuteNonQuery(tmpcmd);
                                    }

                                }

                            }
                            else
                            { 
                            
                            }
                        }
                    }
                    updateavlbldatadet("avlbldatadet");

                    if (newdata.Rows.Count > 0)
                    {
                        foreach (DataRow dr in newdata.Rows)
                        {
                            seriescmd = new OleDbCommand("select realstat,datatype,TableName from series where realstat='" + dr["realstat"] + "' and datatype='" + dr["parameterid"] + "' and tiunit=" + dr["tiunit"] + " and divider=" + dr["divider"], conn);
                            //seriescmd = new OleDbCommand("select realstat,datatype,TableName from series where realstat='Nasik' and datatype='MPS'", conn);
                            DataTable dt = new DataTable();
                            rdr = null;
                            rdr = seriescmd.ExecuteReader();
                            dt.Load(rdr);

                            DataTable finalavlbldt = new DataTable();
                            finalavlbldt.Columns.Add("yr");
                            DataColumn[] keys = new DataColumn[1];
                            keys[0] = finalavlbldt.Columns["yr"];
                            finalavlbldt.PrimaryKey = keys;

                            foreach (DataRow rd in dt.Rows)
                            {
                                //selecting year from table where year!=-999.99
                                OleDbCommand avlbl = new OleDbCommand("select year(measdate) as yr from " + (rd["TableName"].ToString()) + " where measvalue<>-999.99 group by year(measdate)", conn);
                                DbDataReader dtrdr = avlbl.ExecuteReader();
                                DataTable avlbl1 = new DataTable();
                                avlbl1.Load(dtrdr);

                                foreach (DataRow drow in avlbl1.Rows)
                                {
                                    if (finalavlbldt.Rows.Contains(drow["yr"]))
                                    {
                                    }
                                    else
                                    {

                                        DataRow dr4 = finalavlbldt.NewRow();   //add new record of avlbl year
                                        dr4["yr"] = drow["yr"].ToString();

                                        finalavlbldt.Rows.Add(dr4);

                                    }
                                }
                            }

                            foreach (DataRow rd in dt.Rows)
                            {
                                OleDbCommand msngcmd = new OleDbCommand("select yr from (select year(measdate) as yr from " + (rd["TableName"].ToString()) + " group by year(measdate)) where yr not in ( select year(measdate) from " + (rd["TableName"].ToString()) + " where measvalue<>-999.99 group by year(measdate))", conn);
                                DbDataReader dataread = msngcmd.ExecuteReader();
                                DataTable msngdt = new DataTable();
                                msngdt.Load(dataread);      //for getting missing year present in table 



                                OleDbCommand msngcmd1 = new OleDbCommand("Select min(MeasDate) from " + (rd["TableName"].ToString()), conn); //for getting min date                      
                                DateTime mindate = Convert.ToDateTime(msngcmd1.ExecuteScalar().ToString());

                                msngcmd = new OleDbCommand("Select max(MeasDate) from " + (rd["TableName"].ToString()), conn); //for getting max date in table
                                DateTime maxdate = Convert.ToDateTime(msngcmd.ExecuteScalar().ToString());



                                string cond = "";
                                foreach (DataRow misngrow in msngdt.Rows)
                                {
                                    if (finalavlbldt.Rows.Contains(misngrow["yr"]))
                                    {
                                    }
                                    else
                                    {
                                        tmpcmd.CommandText = "select count(*) from misngdatadet where upper(realstat)='" + dr["realstat"].ToString().ToUpper() + "' and parameterid='" + dr["parameterid"] + "' and cancel=0 and year=" + Convert.ToInt32(misngrow["yr"]);
                                        int cnt1 = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
                                        if (cnt1 == 0)
                                        {
                                            tmpcmd.CommandText = "select count(*) from misngdatadet where upper(realstat)='" + dr["realstat"].ToString().ToUpper() + "' and parameterid='" + dr["parameterid"] + "' and cancel=1 and year=" + Convert.ToInt32(misngrow["yr"]);
                                            int cnt2 = Convert.ToInt32(BaseDb.ExecuteScalar(tmpcmd).ToString());
                                            if (cnt2 == 0)
                                            {
                                                tmpcmd.CommandText = "insert into MISNGDATADET(realstat,parameterid,tiunit,divider,frequency,year,cancel,TYPE,QUERYTYPE) values('" + txt.ToTitleCase(dr["realstat"].ToString()) + "','" + dr["parameterid"] + "' ," + dr["tiunit"] + "," + dr["divider"] + " ,'" + dr["frequency"] + "'," + Convert.ToInt32(misngrow["yr"]) + ",0,'" + type + "','insert')";
                                                BaseDb.ExecuteNonQuery(tmpcmd);
                                            }
                                        }

                                        cond = cond + misngrow["yr"] + ",";
                                    }
                                }
                            }

                        }
                    }

                    if (alreadypresentrecord.Rows.Count > 0)
                    {
                        foreach (DataRow dr in alreadypresentrecord.Rows)
                        {
                            seriescmd = new OleDbCommand("select realstat,datatype,TableName from series where realstat='" + dr["realstat"] + "' and datatype='" + dr["parameterid"] + "' and tiunit=" + dr["tiunit"] + " and divider=" + dr["divider"], conn);
                            // seriescmd = new OleDbCommand("select realstat,datatype,TableName from series where realstat='Nasik' and datatype='MPC'", conn);
                            DataTable dt = new DataTable();
                            rdr = null;
                            rdr = seriescmd.ExecuteReader();
                            dt.Load(rdr);

                            sqlcmd.CommandText = "select Distinct year,cancel from misngdatadet where upper(realstat)='" + dr["realstat"].ToString().ToUpper() + "' and parameterid='" + dr["parameterid"] + "' and type='" + type + "'";

                            DataTable tmpdt = new DataTable();
                            tmpdt.Clear();
                            tmpdt = BaseDb.ExecuteSelect(sqlcmd);//contains existing record of missing data with canceled field
                            DataColumn[] keys = new DataColumn[1];
                            
                            keys[0] = tmpdt.Columns["year"];
                            tmpdt.PrimaryKey = keys;
                            try
                            {
                                tmpdt.Columns.Add("new");
                            }
                            catch (Exception exc)
                            {
                                throw exc;
                            }
                            DataTable finalavlbldt = new DataTable();
                            finalavlbldt.Columns.Add("yr");
                            DataColumn[] keys1 = new DataColumn[1];
                            keys1[0] = finalavlbldt.Columns["yr"];
                            finalavlbldt.PrimaryKey = keys1;

                            foreach (DataRow rd in dt.Rows)
                            {
                                OleDbCommand avlbl = new OleDbCommand("select year(measdate) as yr from " + (rd["TableName"].ToString()) + " where measvalue<>-999.99 group by year(measdate)", conn);
                                DbDataReader dtrdr = avlbl.ExecuteReader();
                                DataTable avlbl1 = new DataTable();
                                avlbl1.Load(dtrdr);

                                foreach (DataRow drow in avlbl1.Rows)
                                {
                                    if (finalavlbldt.Rows.Contains(drow["yr"]))
                                    {
                                    }
                                    else
                                    {

                                        DataRow dr4 = finalavlbldt.NewRow();   //add new record of missing year
                                        dr4["yr"] = drow["yr"].ToString();

                                        finalavlbldt.Rows.Add(dr4);

                                    }
                                }
                            }


                            foreach (DataRow row in dt.Rows)
                            {
                                OleDbCommand msngcmd = new OleDbCommand("select yr from (select year(measdate) as yr from " + (row["TableName"].ToString()) + " group by year(measdate)) where yr not in ( select year(measdate) from " + (row["TableName"].ToString()) + " where measvalue<>-999.99 group by year(measdate))", conn);
                                DbDataReader dataread = msngcmd.ExecuteReader();
                                DataTable msngdt = new DataTable();
                                string flname = row["TableName"].ToString();

                                msngdt.Load(dataread);      //for getting missing year present in table from mdb 

                                string cond = "";
                                foreach (DataRow drow in msngdt.Rows)
                                {
                                    if (finalavlbldt.Rows.Contains(drow["yr"]))
                                    {
                                    }
                                    else
                                    {
                                        int m = Convert.ToInt32(drow["yr"].ToString());
                                        DataRow dtr = tmpdt.Rows.Find(m);  //checked for new missing year is already present or not

                                        if (dtr != null)
                                        {
                                            dtr.BeginEdit();
                                            dtr["cancel"] = 0;     //set cancel=0
                                            dtr.EndEdit();
                                        }
                                        else
                                        {
                                            DataRow dr4 = tmpdt.NewRow();   //add new record of missing year
                                            dr4["year"] = drow["yr"].ToString();
                                            dr4["cancel"] = 0;
                                            dr4["new"] = 'n';
                                            tmpdt.Rows.Add(dr4);
                                        }

                                        cond = cond + drow["yr"] + ",";


                                        OleDbCommand msngcmd1 = new OleDbCommand("Select min(MeasDate) from " + (row["TableName"].ToString()), conn); //for getting min date                      
                                        DateTime mindate = Convert.ToDateTime(msngcmd1.ExecuteScalar().ToString());

                                        msngcmd = new OleDbCommand("Select max(MeasDate) from " + (row["TableName"].ToString()), conn); //for getting max date in table
                                        DateTime maxdate = Convert.ToDateTime(msngcmd.ExecuteScalar().ToString());

                                        var sqlquery1 = msngdt.AsEnumerable().Select(a => new { ID = a["yr"].ToString() });
                                        var sqlquery2 = tmpdt.AsEnumerable().Select(b => new { ID = b["year"].ToString() });
                                        var q = sqlquery2.Except(sqlquery1);


                                        int cnt = q.Count();

                                        if (cnt > 0)
                                        {
                                            DataTable dt1 = (from a in tmpdt.AsEnumerable() join ab in q on a["year"].ToString() equals ab.ID select a).CopyToDataTable();

                                            foreach (DataRow r in dt1.Rows)
                                            {
                                                if ((Convert.ToInt32(mindate.Year) <= Convert.ToInt32(r["year"].ToString())) && (Convert.ToInt32(r["year"].ToString()) <= Convert.ToInt32(maxdate.Year)))
                                                {
                                                    int yr = Convert.ToInt32(r["year"].ToString());

                                                    DataRow rtbl = tmpdt.Rows.Find(yr);
                                                    if (rtbl != null)
                                                    {
                                                        if (rtbl["cancel"].ToString() != "1")
                                                        {
                                                            rtbl.BeginEdit();

                                                            rtbl["cancel"] = 1;  //if data present then set cancel=1;
                                                            rtbl.EndEdit();
                                                        }
                                                    }

                                                }
                                            }


                                        }


                                    }
                                }
                            }

                            foreach (DataRow drow1 in tmpdt.Rows)
                            {
                                DbCommand tmpdb = BaseDb.CreatetmpCommand();

                                if (drow1["new"].ToString() == "n")
                                {
                                    tmpdb.CommandText = "insert into misngdatadet(realstat,parameterid,tiunit,divider,frequency,year,cancel,type,querytype) values('" + txt.ToTitleCase(dr["realstat"].ToString()) + "','" + dr["parameterid"] + "'," + dr["tiunit"] + "," + dr["divider"] + ",'" + dr["frequency"] + "'," + Convert.ToInt32(drow1["year"].ToString()) + "," + Convert.ToInt32(drow1["cancel"].ToString()) + ",'" + type + "','insert')";
                                    BaseDb.ExecuteNonQuery(tmpdb);

                                }
                                else
                                {
                                    tmpdb.CommandText = "insert into misngdatadet(realstat,parameterid,tiunit,divider,frequency,year,cancel,type,querytype) values('" + txt.ToTitleCase(dr["realstat"].ToString()) + "','" + dr["parameterid"] + "'," + dr["tiunit"] + "," + dr["divider"] + ",'" + dr["frequency"] + "'," + Convert.ToInt32(drow1["year"].ToString()) + "," + Convert.ToInt32(drow1["cancel"].ToString()) + ",'" + type + "','update')";
                                    BaseDb.ExecuteNonQuery(tmpdb);
                                }


                            }
                        }
                    }

                    updateavlbldatadet("misngdatadet");



                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    


                }

                #endregion

                #region (inserting and updating dataparameter)
                try
                {

                    DbCommand dbcomm = BaseDb.CreatetmpCommand();
                    dbcomm.CommandText = "SELECT COUNT(*) FROM DATAPARAMETER WHERE PARAMETERID LIKE 'M%' ";
                    string MCOUNT = BaseDb.ExecuteScalar(dbcomm);

                    dbcomm.CommandText = "SELECT COUNT(*) FROM DATAPARAMETER WHERE PARAMETERID LIKE 'H%' ";
                    string HCOUNT = BaseDb.ExecuteScalar(dbcomm);
                    //dbcomm.CommandText = "SELECT COUNT(*) FROM DATAPARAMETER WHERE PARAMETERID LIKE 'Q%' ";
                    //string WCOUNT = BaseDb.ExecuteScalar(dbcomm);
                    dbcomm.CommandText = "SELECT COUNT(*) FROM DATAPARAMETER WHERE PARAMETERID LIKE 'S%' ";
                    string SCOUNT = BaseDb.ExecuteScalar(dbcomm);

                    if (Convert.ToInt32(MCOUNT.ToString()) != 0)
                    {
                        COND = " LIKE 'M%'";
                       string DTTYPE = "Meteorological";
                        adddatatype( DTTYPE, type);
                    }
                    if (Convert.ToInt32(HCOUNT.ToString()) != 0)
                    {
                        COND = " LIKE 'H%' ";
                       string DTTYPE = "Hydrological";
                       adddatatype( DTTYPE, type);
                    }
                    //if (Convert.ToInt32(WCOUNT.ToString()) != 0)
                    //{
                    //    COND = " LIKE 'Q%' ";
                    //    string DTTYPE = "WATERQUALITY";
                    //    adddatatype(COND, DTTYPE, objm,type);
                    //}
                    if (Convert.ToInt32(SCOUNT.ToString()) != 0)
                    {
                        COND = " LIKE 'S%' ";
                        // string DTTYPE = "Sediment";
                        //adddatatype(COND, DTTYPE, objm,type);
                    }

                    updatedataparameter();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    conn.Close();
                }


                #endregion
            }
            catch (SystemException ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
               
                string SaveLocation = "~\\Upload\\" + filename;

                if (File.Exists(SaveLocation))
                {
                    File.Delete(SaveLocation);
                }

            }

        }
        #endregion
             
            #region Inserting Data into Datatype

        public static bool adddatatype( string DTTYPE, string type)
        {
            DbCommand dbcomm = BaseDb.CreatetmpCommand();
            DbCommand localcom = BaseDb.CreateStrCommand();
           
            //dbcomm.CommandText = "SELECT MAX(DATAUPTODT) FROM DATAPARAMETER WHERE PARAMETERID "+COND ;
            // string MAXDT = BaseDb.ExecuteScalar(dbcomm);
            //dbcomm.CommandText = "SELECT MIN(DATAFROMDT) FROM DATAPARAMETER WHERE PARAMETERID " + COND;
            //string MINDT = BaseDb.ExecuteScalar(dbcomm);
            //DateTime dt = Convert.ToDateTime(MINDT.ToString());
            //localcom.CommandText = "select min(datafromdt) from dataparameter where parameterid" + COND;
            //string localdt=BaseDb.ExecuteScalar(localcom);
            //DateTime datafromdt;
            //if (localdt=="" ||localdt==null)
            //{
            //    datafromdt=dt;
            //}
            //else
            //{
            //    if(Convert.ToDateTime(localdt.ToString()) >dt)
            //    {
            //        datafromdt=dt;
            //    }
            //    else
            //    {
            //         datafromdt=Convert.ToDateTime(localdt.ToString());
            //    }
            //}

           // DateTime localdt = Convert.ToDateTime(BaseDb.ExecuteScalar(localcom).ToString());
            //if 
            localcom.CommandText = "SELECT COUNT(*) FROM DATATYPE WHERE DATATYPEID= '" + DTTYPE + "' and type='"+type+"'";

           
                int CHK = Convert.ToInt32(BaseDb.ExecuteScalar(localcom ));
            DbCommand tmpdb = BaseDb.CreatetmpCommand();
               if (CHK==0)
                           
                {
                    tmpdb.CommandText = " INSERT INTO DATATYPE(DATATYPEID,type,querytype) VALUES('" + DTTYPE + "','" + type + "','insert')";

                }
               else
               {
                   //tmpdb.CommandText = "INSERT INTO DATATYPE(SRNO,DATATYPEID,type,querytype) VALUES('" + DTTYPE + "','" + type + "','update')";
                   tmpdb.CommandText = "UPDATE DATATYPE Set querytype='update'  where DATATYPEID='" + DTTYPE + "'and type='" + type + "' ";

               }

                int result = -1;
                try
                {
                    result = BaseDb.ExecuteNonQuery(tmpdb);
                }
                catch (Exception exc)
                {

                    throw exc;
                }
                finally
                {

                }

                return (result != -1);
            
        }
#endregion

            #region Updating Datatypeid in dataparameter
        public static void updatedataparameter()
        {
            DbCommand dbComm = BaseDb.CreatetmpCommand();
            dbComm.CommandText = "Update DATAPARAMETER SET DATATYPEID='Meteorological' WHERE (PARAMETERID LIKE 'M%' OR PARAMETERID LIKE 'm%') ";
            BaseDb.ExecuteNonQuery(dbComm);
            dbComm.CommandText = "Update DATAPARAMETER SET DATATYPEID='Hydrological' WHERE (PARAMETERID LIKE 'H%' OR PARAMETERID LIKE 'h%') ";
            BaseDb.ExecuteNonQuery(dbComm);
            dbComm.CommandText = "Update DATAPARAMETER SET DATATYPEID='WaterQuality' WHERE (PARAMETERID LIKE 'Q%' OR PARAMETERID LIKE 'q%') ";
            BaseDb.ExecuteNonQuery(dbComm);
            dbComm.CommandText = "Update DATAPARAMETER SET DATATYPEID='Sediment' WHERE (PARAMETERID LIKE 'S%' OR PARAMETERID LIKE 's%') ";
            BaseDb.ExecuteNonQuery(dbComm);

        }

        #endregion

            #region Updating Datatypeid in avlbldata and misngdata
        public static void updateavlbldatadet(string tblname)
        {
            DbCommand dbComm = BaseDb.CreatetmpCommand();
            dbComm.CommandText = "Update " + tblname + " SET DATATYPEID='Meteorological' WHERE (PARAMETERID LIKE 'M%' OR PARAMETERID LIKE 'm%') ";
            BaseDb.ExecuteNonQuery(dbComm);
            dbComm.CommandText = "Update " + tblname + " SET DATATYPEID='Hydrological' WHERE (PARAMETERID LIKE 'H%' OR PARAMETERID LIKE 'h%') ";
            BaseDb.ExecuteNonQuery(dbComm);
            dbComm.CommandText = "Update " + tblname + " SET DATATYPEID='WaterQuality' WHERE (PARAMETERID LIKE 'Q%' OR PARAMETERID LIKE 'q%') ";
            BaseDb.ExecuteNonQuery(dbComm);
            dbComm.CommandText = "Update " + tblname + " SET DATATYPEID='Sediment' WHERE (PARAMETERID LIKE 'S%' OR PARAMETERID LIKE 's%') ";
            BaseDb.ExecuteNonQuery(dbComm);

        }   

        #endregion

            #region upload data
        public static int uploaddata(string uid)
        {
            try
            {
            addnewdata(uid);

            DbCommand tmpcmd=BaseDb.CreatetmpCommand();
            tmpcmd.CommandText="select * from dataparameter where querytype='update'";
            DataTable tmpdt=new DataTable();
            tmpdt=BaseDb.ExecuteSelect(tmpcmd);

            foreach (DataRow r in tmpdt.Rows)
            {
                DbCommand sqlcommand=BaseDb.CreateSqlCommand();
                DbCommand localcommand = BaseDb.CreateStrCommand();
                sqlcommand.CommandText="update dataparameter set datafromdt='"+ dateconvert.Convertdt(r["datafromdt"].ToString())  +"',datauptodt='"+ dateconvert.Convertdt(r["datauptodt"].ToString()) +"',updated_by='"+uid+"',updated_dt='"+dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) +"',verified_by='"+r["verified_by"]+"',verified_dt='"+ dateconvert.Convertdt(r["verified_dt"].ToString())  +"' where datatypeid='"+r["datatypeid"]+"' and parameterid='"+r["parameterid"]+"' and type='"+r["type"]+"'";
                BaseDb.ExecuteNonQuery(sqlcommand);
                localcommand.CommandText = "update dataparameter set datafromdt='" + dateconvert.Convertdt(r["datafromdt"].ToString()) + "',datauptodt='" + dateconvert.Convertdt(r["datauptodt"].ToString()) + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) + "',verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString())  + "' where datatypeid='" + r["datatypeid"] + "' and parameterid='" + r["parameterid"] + "' and type='" + r["type"] + "'";
                BaseDb.ExecuteNonQuery(localcommand);
            }

              tmpcmd.CommandText="select * from misngdatadet where querytype='update'";
            DataTable tmpdt1=new DataTable();
            tmpdt1=BaseDb.ExecuteSelect(tmpcmd);

            foreach (DataRow r in tmpdt1.Rows)
            {
                DbCommand sqlcommand=BaseDb.CreateSqlCommand();
                DbCommand localcommand=BaseDb.CreateStrCommand();
                sqlcommand.CommandText = "update Misngdatadet set cancel='" + r["cancel"] + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString())) + "',verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString()))  + "' where realstat='" + r["realstat"] + "' and datatypeid='" + r["datatypeid"] + "' and parameterid='" + r["parameterid"] + "' and type='" + r["type"] + "' and year=" + r["year"];
              BaseDb.ExecuteNonQuery(sqlcommand);
                localcommand.CommandText = "update Misngdatadet set cancel='" + r["cancel"] + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) + "' ,verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString())  + "' where realstat='" + r["realstat"] + "' and datatypeid='" + r["datatypeid"] + "' and parameterid='" + r["parameterid"] + "' and type='" + r["type"] + "' and year=" + r["year"];
                BaseDb.ExecuteNonQuery(localcommand);
            }

             tmpcmd.CommandText="select * from avlbldatadet where querytype='update'";
            DataTable tmpdt2=new DataTable();
            tmpdt2=BaseDb.ExecuteSelect(tmpcmd);

            foreach (DataRow r in tmpdt2.Rows)
            {
                DbCommand sqlcommand=BaseDb.CreateSqlCommand();  //
                DbCommand localcommand=BaseDb.CreateStrCommand();
                // RS 19/04/2014 ADDING CODE TO CHECK FROM DATE AND TO DATE FOR UPDATATION AS DISCUSSED WITH UG SIR
                localcommand.CommandText = "select from_date,to_date from avlbldatadet where parameterid='" + r["parameterid"] + "' and (realstat)='" + r["realstat"].ToString().ToUpper() + "' and datatypeid='" + r["datatypeid"] + "' and type='" + r["type"] + "' and tiunit=" + r["tiunit"] + " and divider=" + r["divider"];
                DataTable metadt = new DataTable();
                metadt = BaseDb.ExecuteSelect(localcommand);
                if (Convert.ToDateTime(r["from_date"].ToString()).Date < Convert.ToDateTime(metadt.Rows[0]["from_date"].ToString()).Date && Convert.ToDateTime(r["to_date"].ToString()).Date > Convert.ToDateTime(metadt.Rows[0]["to_date"].ToString()).Date)
                {
                    sqlcommand.CommandText = "update avlbldatadet set from_date='" + dateconvert.Convertdt(r["from_date"].ToString()) + "',to_date='" + dateconvert.Convertdt(r["to_date"].ToString()) + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) + "' ,verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString()) + "' where parameterid='" + r["parameterid"] + "' and (realstat)='" + r["realstat"].ToString().ToUpper() + "' and datatypeid='" + r["datatypeid"] + "' and type='" + r["type"] + "' and tiunit=" + r["tiunit"] + " and divider=" + r["divider"];
                    BaseDb.ExecuteNonQuery(sqlcommand);
                    localcommand.CommandText = "update avlbldatadet set from_date='" + dateconvert.Convertdt(r["from_date"].ToString()) + "',to_date='" + dateconvert.Convertdt(r["to_date"].ToString()) + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) + "' ,verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString()) + "' where parameterid='" + r["parameterid"] + "' and (realstat)='" + r["realstat"].ToString().ToUpper() + "' and datatypeid='" + r["datatypeid"] + "' and type='" + r["type"] + "' and tiunit=" + r["tiunit"] + " and divider=" + r["divider"];
                    BaseDb.ExecuteNonQuery(localcommand);
                }
                else if (Convert.ToDateTime(r["from_date"].ToString()).Date < Convert.ToDateTime(metadt.Rows[0]["from_date"].ToString()).Date) 
                {
                    sqlcommand.CommandText = "update avlbldatadet set from_date='" + dateconvert.Convertdt(r["from_date"].ToString()) + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) + "' ,verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString()) + "' where parameterid='" + r["parameterid"] + "' and upper(realstat)='" + r["realstat"].ToString().ToUpper() + "' and datatypeid='" + r["datatypeid"] + "' and type='" + r["type"] + "' and tiunit=" + r["tiunit"] + " and divider=" + r["divider"];
                    BaseDb.ExecuteNonQuery(sqlcommand);
                    localcommand.CommandText = "update avlbldatadet set from_date='" + dateconvert.Convertdt(r["from_date"].ToString()) + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) + "' ,verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString()) + "' where parameterid='" + r["parameterid"] + "' and upper(realstat)='" + r["realstat"].ToString().ToUpper() + "' and datatypeid='" + r["datatypeid"] + "' and type='" + r["type"] + "' and tiunit=" + r["tiunit"] + " and divider=" + r["divider"];
                    BaseDb.ExecuteNonQuery(localcommand);

                }
                else if (Convert.ToDateTime(r["to_date"].ToString()).Date > Convert.ToDateTime(metadt.Rows[0]["to_date"].ToString()).Date)
                {
                    sqlcommand.CommandText = "update avlbldatadet set to_date='" + dateconvert.Convertdt(r["to_date"].ToString()) + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) + "' ,verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString()) + "' where parameterid='" + r["parameterid"] + "' and upper(realstat)='" + r["realstat"].ToString().ToUpper() + "' and datatypeid='" + r["datatypeid"] + "' and type='" + r["type"] + "' and tiunit=" + r["tiunit"] + " and divider=" + r["divider"];
                    BaseDb.ExecuteNonQuery(sqlcommand);
                    localcommand.CommandText = "update avlbldatadet set to_date='" + dateconvert.Convertdt(r["to_date"].ToString()) + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) + "' ,verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString()) + "' where parameterid='" + r["parameterid"] + "' and upper(realstat)='" + r["realstat"].ToString().ToUpper() + "' and datatypeid='" + r["datatypeid"] + "' and type='" + r["type"] + "' and tiunit=" + r["tiunit"] + " and divider=" + r["divider"];
                    BaseDb.ExecuteNonQuery(localcommand);

                }
                // RS

                //sqlcommand.CommandText = "update avlbldatadet set from_date='" + dateconvert.Convertdt(r["from_date"].ToString()) + "',to_date='" + dateconvert.Convertdt(r["to_date"].ToString())  + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) + "' ,verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString())  + "' where realstat='" + r["realstat"] + "' and datatypeid='" + r["datatypeid"] + "' and parameterid='" + r["parameterid"] + "' and type='" + r["type"] + "' ";
                //BaseDb.ExecuteNonQuery(sqlcommand);
                //localcommand.CommandText = "update avlbldatadet set from_date='" + dateconvert.Convertdt(r["from_date"].ToString()) + "',to_date='" + dateconvert.Convertdt(r["to_date"].ToString()) + "',updated_by='" + uid + "',updated_dt='" + dateconvert.Convertdt((HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()))) + "' ,verified_by='" + r["verified_by"] + "',verified_dt='" + dateconvert.Convertdt(r["verified_dt"].ToString()) + "' where realstat='" + r["realstat"] + "' and datatypeid='" + r["datatypeid"] + "' and parameterid='" + r["parameterid"] + "' and type='" + r["type"] + "' ";
                //BaseDb.ExecuteNonQuery(localcommand);
            }

            tmpcmd.CommandText = "select * from datatype where querytype='update'";
            DataTable tmpdt3 = new DataTable();
            tmpdt3 = BaseDb.ExecuteSelect(tmpcmd);

            foreach (DataRow r1 in tmpdt3.Rows)
            {
                DbCommand sqlcommand = BaseDb.CreateSqlCommand();
                DbCommand localcommand = BaseDb.CreateStrCommand();
               // sqlcommand.CommandText = "update datatype set datafromdt='" + Convert.ToDateTime(r1["datafromdt"]) + "',updated_by='" + uid + "',updated_dt='" + Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString())) + "' ,verified_by='" + r1["verified_by"] + "',verified_dt='" + r1["verified_dt"] + "' where datatypeid='" + r1["datatypeid"] + "'  and type='" + r1["type"] + "' ";
               //BaseDb.ExecuteNonQuery(sqlcommand);
               // localcommand.CommandText = "update datatype set datafromdt='" + Convert.ToDateTime(r1["datafromdt"]) + "',updated_by='" + uid + "',updated_dt='" + Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString())) + "' ,verified_by='" + r1["verified_by"] + "',verified_dt='" + r1["verified_dt"] + "' where datatypeid='" + r1["datatypeid"] + "'  and type='" + r1["type"] + "' ";
                //BaseDb.ExecuteNonQuery(localcommand);
            }



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


                return(1);
            }
            catch(Exception ex)
            {
                throw(ex);
            }
          

        }



        public static void addnewdata( string userid)
        {
            System.Object obj1 = new System.Object();  //added by DSB to solve synchronization problem
            lock (obj1)                                   //added by DSB to solve synchronization problem
            {

                try
                {

                    String sqlconnection = ConfigurationManager.ConnectionStrings["sqlconn"].ToString();
                    String localconnection = ConfigurationManager.ConnectionStrings["constr"].ToString();

                    DbCommand dbcom = BaseDb.CreatetmpCommand();
                    dbcom.CommandText = "select srno,REALSTAT,DATATYPEID,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,ENDDATE,LOCALXCOORD,LOCALYCOORD,verified_by,verified_dt from location where querytype='insert'";
                    //dbcom.CommandTimeout = 120;
                    DataTable dt = new DataTable();
                    dt = BaseDb.ExecuteSelect(dbcom);

                    dt.Columns.Add("created_by");
                    dt.Columns.Add("created_dt", Type.GetType("System.DateTime"));
                    dt.Columns.Add("updated_by");
                    dt.Columns.Add("updated_dt");

                    foreach (DataRow dr in dt.Rows)
                    {

                        dr.BeginEdit();
                        dr["created_by"] = userid;
                        dr["created_dt"] = Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()));
                        dr.EndEdit();

                    }

                    if (dt.Rows.Count > 0)
                    {

                        //DSB 3.7.2012
                                //DbCommand dbcom1 = BaseDb.CreateSqlCommand();
                                //dbcom1.CommandText = "select * from location";
                                ////dbcom.CommandTimeout = 120;
                                //DataTable dt12 = new DataTable();
                                //dt12 = BaseDb.ExecuteSelect(dbcom1);
                        //ArrayList chkrealstat = new ArrayList();
                        //ArrayList chkdatatypid = new ArrayList();
                        //ArrayList chkrealstat1 = new ArrayList();
                        //ArrayList chkdatatypid1 = new ArrayList();
                        //foreach (DataRow drtemp in dt.Rows)
                        //{
                        //    chkrealstat.Add(drtemp["realstat"].ToString());
                        //    chkdatatypid.Add(drtemp["datatypeid"].ToString());
                        //}
                        
                        //    //chkrealstat1.Add(drmeta["realstat"].ToString());
                        //    //chkdatatypid1.Add(drmeta["datatypeid"].ToString());
                        //foreach (DataRow drtemp in dt.Rows)
                        //{
                        //    string dattp = drtemp["datatypeid"].ToString();
                        //    DbCommand dbcom1 = BaseDb.CreateSqlCommand();
                        //    dbcom1.CommandText = "select * from location where datatypeid = '"+dattp+"'";
                        //    //dbcom.CommandTimeout = 120;
                        //    DataTable dt12 = new DataTable();
                        //    dt12 = BaseDb.ExecuteSelect(dbcom1);

                        //    foreach (DataRow drmeta in dt12.Rows)
                        //    {
                        //        if (drtemp["realstat"].ToString() == drmeta["realstat"].ToString() && drtemp["datatypeid"].ToString() == drmeta["datatypeid"].ToString())
                        //        {

                        //        }
                        //        else
                        //        {
                        //            //if(dt.Rows.Contains(

                        //            //        }
                        //            //    }
                        //            //}
                        //            //}

                        //            //string realstatn = dt12.Rows[0]["realstat"].ToString();
                        //            //string datatypid = dt12.Rows[0]["datatypeid"].ToString();
                        //            //ArrayList same = new ArrayList();

                        //            //var sqlquery11 = dt.AsEnumerable().Select(a => new { ID = a["realstat"].ToString().Trim().ToUpper() });
                        //            //var sqlquery21 = dt12.AsEnumerable().Select(b => new { ID = b["realstat"].ToString().Trim().ToUpper() });
                        //            //var q1 = sqlquery11.Except(sqlquery21);
                        //            //if(sqlquery21.Contains(sqlquery11))

                        //            //if (chkrealstat1.Contains(chkrealstat))    // && chkdatatypid1.Contains(chkdatatypid)
                        //            //{
                        //            //}
                        //            //else
                        //            //{

                        //            //}


                        //            //

                                    using (SqlBulkCopy copy = new SqlBulkCopy(sqlconnection))
                                    {
                                        copy.DestinationTableName = "Location";
                                        copy.ColumnMappings.Add(0, 0);
                                        copy.ColumnMappings.Add(1, 1);
                                        copy.ColumnMappings.Add(2, 2);
                                        copy.ColumnMappings.Add(3, 3);
                                        copy.ColumnMappings.Add(4, 4);
                                        copy.ColumnMappings.Add(5, 5);
                                        copy.ColumnMappings.Add(6, 6);
                                        copy.ColumnMappings.Add(7, 7);
                                        copy.ColumnMappings.Add(8, 8);
                                        copy.ColumnMappings.Add(9, 9);
                                        copy.ColumnMappings.Add(10, 10);
                                        copy.ColumnMappings.Add(11, 11);
                                        copy.ColumnMappings.Add(12, 12);
                                        copy.ColumnMappings.Add(13, 13);
                                        copy.ColumnMappings.Add(14, 14);
                                        copy.ColumnMappings.Add(15, 15);
                                        copy.ColumnMappings.Add(16, 16);
                                        copy.ColumnMappings.Add(17, 17);
                                        copy.ColumnMappings.Add(18, 18);
                                        copy.ColumnMappings.Add(19, 19);
                                        copy.ColumnMappings.Add(20, 20);
                                        copy.ColumnMappings.Add(21, 21);
                                        copy.ColumnMappings.Add(22, 22);
                                        copy.ColumnMappings.Add(23, 23);
                                        copy.ColumnMappings.Add(24, 24);
                                        copy.ColumnMappings.Add(25, 25);
                                        copy.ColumnMappings.Add(26, 26);
                                        copy.ColumnMappings.Add(27, 27);
                                        copy.ColumnMappings.Add(30, 28);
                                        copy.ColumnMappings.Add(31, 29);
                                        copy.ColumnMappings.Add(32, 30);
                                        copy.ColumnMappings.Add(33, 31);
                                        copy.ColumnMappings.Add(28, 32);
                                        copy.ColumnMappings.Add(29, 33);
                                        try
                                        {
                                            copy.WriteToServer(dt);
                                        }
                                        catch (Exception exc)
                                        {

                                            throw exc;
                                        }

                                    }

                                    using (SqlBulkCopy copy = new SqlBulkCopy(localconnection))
                                    {
                                        copy.DestinationTableName = "Location";
                                        copy.ColumnMappings.Add(0, 0);
                                        copy.ColumnMappings.Add(1, 1);
                                        copy.ColumnMappings.Add(2, 2);
                                        copy.ColumnMappings.Add(3, 3);
                                        copy.ColumnMappings.Add(4, 4);
                                        copy.ColumnMappings.Add(5, 5);
                                        copy.ColumnMappings.Add(6, 6);
                                        copy.ColumnMappings.Add(7, 7);
                                        copy.ColumnMappings.Add(8, 8);
                                        copy.ColumnMappings.Add(9, 9);
                                        copy.ColumnMappings.Add(10, 10);
                                        copy.ColumnMappings.Add(11, 11);
                                        copy.ColumnMappings.Add(12, 12);
                                        copy.ColumnMappings.Add(13, 13);
                                        copy.ColumnMappings.Add(14, 14);
                                        copy.ColumnMappings.Add(15, 15);
                                        copy.ColumnMappings.Add(16, 16);
                                        copy.ColumnMappings.Add(17, 17);
                                        copy.ColumnMappings.Add(18, 18);
                                        copy.ColumnMappings.Add(19, 19);
                                        copy.ColumnMappings.Add(20, 20);
                                        copy.ColumnMappings.Add(21, 21);
                                        copy.ColumnMappings.Add(22, 22);
                                        copy.ColumnMappings.Add(23, 23);
                                        copy.ColumnMappings.Add(24, 24);
                                        copy.ColumnMappings.Add(25, 25);
                                        copy.ColumnMappings.Add(26, 26);
                                        copy.ColumnMappings.Add(27, 27);
                                        copy.ColumnMappings.Add(30, 28);
                                        copy.ColumnMappings.Add(31, 29);
                                        copy.ColumnMappings.Add(32, 30);
                                        copy.ColumnMappings.Add(33, 31);
                                        copy.ColumnMappings.Add(28, 32);
                                        copy.ColumnMappings.Add(29, 33);
                                        try
                                        {
                                            copy.WriteToServer(dt);
                                        }
                                        catch (Exception exc)
                                        {
                                            throw exc;

                                        }
                                    }
                        //            //
                        //        }
                        //    }
                        //}
                        ////

                    }
                    dbcom.CommandText = "select Distinct srno,datatypeid,datatypedesc,frequency,datafromdt,datauptodt,type,verified_by,verified_dt from datatype where querytype='insert'";  
                    
                    DataTable dtable = new DataTable();
                    dtable = BaseDb.ExecuteSelect(dbcom);
                    dtable.Columns.Add("created_by");
                    dtable.Columns.Add("created_dt", Type.GetType("System.DateTime"));
                    dtable.Columns.Add("updated_by");
                    dtable.Columns.Add("updated_dt");
                    foreach (DataRow dr1 in dtable.Rows)
                    {
                        dr1.BeginEdit();
                        dr1["created_by"] = userid;
                        dr1["created_dt"] = Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()));
                        dr1.EndEdit();

                    }
                    if (dtable.Rows.Count > 0)
                    {
                        using (SqlBulkCopy cpy = new SqlBulkCopy(sqlconnection))
                        {
                            cpy.DestinationTableName = "datatype";
                            cpy.ColumnMappings.Add(0, 0);   
                            cpy.ColumnMappings.Add(1, 1);
                            cpy.ColumnMappings.Add(2, 2);
                            cpy.ColumnMappings.Add(3, 3);
                            cpy.ColumnMappings.Add(4, 4);
                            cpy.ColumnMappings.Add(5, 5);
                            cpy.ColumnMappings.Add(6, 6);
                            cpy.ColumnMappings.Add(9, 7);
                            cpy.ColumnMappings.Add(10, 8);
                            cpy.ColumnMappings.Add(11, 9);
                            cpy.ColumnMappings.Add(12, 10);
                            cpy.ColumnMappings.Add(7, 11);

                            cpy.ColumnMappings.Add(8, 12);
                            cpy.WriteToServer(dtable);

                        }
                        using (SqlBulkCopy cpy = new SqlBulkCopy(localconnection))
                        {
                            cpy.DestinationTableName = "datatype";
                            //  srno,datatypeid,datatypedesc,frequency,datafromdt,datauptodt,type,verified_by,verified_dt 
                            //cpy.ColumnMappings.Add(0, 0);  
                            cpy.ColumnMappings.Add(1, 1);
                            cpy.ColumnMappings.Add(2, 2);
                            cpy.ColumnMappings.Add(3, 3);
                            cpy.ColumnMappings.Add(4, 4);
                            cpy.ColumnMappings.Add(5, 5);
                            cpy.ColumnMappings.Add(6, 6);
                            cpy.ColumnMappings.Add(9, 7);
                            cpy.ColumnMappings.Add(10, 8);
                            cpy.ColumnMappings.Add(11, 9);
                            cpy.ColumnMappings.Add(12, 10);
                            cpy.ColumnMappings.Add(7, 11);

                            cpy.ColumnMappings.Add(8, 12);



                            cpy.WriteToServer(dtable);
                        }
                    }



                    dbcom.CommandText = "select distinct srno,datatypeid,parameterid,parameterdesc,datafromdt,datauptodt,type,verified_by,verified_dt from dataparameter where querytype='insert'";  //commented by DSB on 4.6.2012
                    //dbcom.CommandText = "select distinct datatypeid,parameterid,parameterdesc,datafromdt,datauptodt,type,verified_by,verified_dt from dataparameter where querytype='insert'";   //added by DSB on 4.6.2012
                    DataTable dt1 = new DataTable();
                    dt1 = BaseDb.ExecuteSelect(dbcom);
                    dt1.Columns.Add("created_by");
                    dt1.Columns.Add("created_dt", Type.GetType("System.DateTime"));
                    dt1.Columns.Add("updated_by");
                    dt1.Columns.Add("updated_dt");



                    foreach (DataRow dr in dt1.Rows)
                    {
                        dr.BeginEdit();
                        dr["created_by"] = userid;
                        dr["created_dt"] = Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()));
                        dr.EndEdit();

                    }
                    if (dt1.Rows.Count > 0)
                    {
                        using (SqlBulkCopy copy1 = new SqlBulkCopy(sqlconnection))
                        {
                            copy1.DestinationTableName = "DATAPARAMETER";
                            copy1.ColumnMappings.Add(0, 0);  //commented by DSB on 4.6.2012
                            copy1.ColumnMappings.Add(1, 1);
                            copy1.ColumnMappings.Add(2, 2);
                            copy1.ColumnMappings.Add(3, 3);
                            copy1.ColumnMappings.Add(4, 4);
                            copy1.ColumnMappings.Add(5, 5);
                            copy1.ColumnMappings.Add(6, 6);
                            copy1.ColumnMappings.Add(9, 7);
                            copy1.ColumnMappings.Add(10, 8);
                            copy1.ColumnMappings.Add(11, 9);
                            copy1.ColumnMappings.Add(12, 10);
                            copy1.ColumnMappings.Add(7, 11);
                            copy1.ColumnMappings.Add(8, 12);

                            //changed by DSB on 4.6.2012
                            //copy1.ColumnMappings.Add(1, 0);
                            //copy1.ColumnMappings.Add(2, 1);
                            //copy1.ColumnMappings.Add(3, 2);
                            //copy1.ColumnMappings.Add(4, 3);
                            //copy1.ColumnMappings.Add(5, 4);
                            //copy1.ColumnMappings.Add(6, 5);
                            //copy1.ColumnMappings.Add(11, 6);
                            //copy1.ColumnMappings.Add(12, 7);
                            //copy1.ColumnMappings.Add(7, 8);
                            //copy1.ColumnMappings.Add(8, 9);
                            //copy1.ColumnMappings.Add(9, 10);

                            //copy1.ColumnMappings.Add(10, 11);


                            copy1.WriteToServer(dt1);
                        }
                        using (SqlBulkCopy copy1 = new SqlBulkCopy(localconnection))
                        {
                            copy1.DestinationTableName = "DATAPARAMETER";
                            copy1.ColumnMappings.Add(0, 0);  //commented by DSB on 4.6.2012
                            copy1.ColumnMappings.Add(1, 1);
                            copy1.ColumnMappings.Add(2, 2);
                            copy1.ColumnMappings.Add(3, 3);
                            copy1.ColumnMappings.Add(4, 4);
                            copy1.ColumnMappings.Add(5, 5);
                            copy1.ColumnMappings.Add(6, 6);
                            copy1.ColumnMappings.Add(9, 7);
                            copy1.ColumnMappings.Add(10, 8);
                            copy1.ColumnMappings.Add(11, 9);
                            copy1.ColumnMappings.Add(12, 10);
                            copy1.ColumnMappings.Add(7, 11);

                            copy1.ColumnMappings.Add(8, 12);

                            //changed by DSB on 4.6.2012
                            //copy1.ColumnMappings.Add(1, 0);
                            //copy1.ColumnMappings.Add(2, 1);
                            //copy1.ColumnMappings.Add(3, 2);
                            //copy1.ColumnMappings.Add(4, 3);
                            //copy1.ColumnMappings.Add(5, 4);
                            //copy1.ColumnMappings.Add(6, 5);
                            //copy1.ColumnMappings.Add(11, 6);
                            //copy1.ColumnMappings.Add(12, 7);
                            //copy1.ColumnMappings.Add(7, 8);
                            //copy1.ColumnMappings.Add(8, 9);
                            //copy1.ColumnMappings.Add(9, 10);

                            //copy1.ColumnMappings.Add(10, 11);

                            copy1.WriteToServer(dt1);
                        }

                    }
                    dbcom.CommandText = "select srno,realstat,datatypeid,parameterid,tiunit,divider,frequency,year,cancel,type,verified_by,verified_dt from MISNGDATADET where querytype='insert'";
                    DataTable dt2 = new DataTable();
                    dt2 = BaseDb.ExecuteSelect(dbcom);
                    dt2.Columns.Add("created_by");
                    dt2.Columns.Add("created_dt", Type.GetType("System.DateTime"));
                    dt2.Columns.Add("updated_by");
                    dt2.Columns.Add("updated_dt");


                    foreach (DataRow dr in dt2.Rows)
                    {
                        dr.BeginEdit();
                        dr["created_by"] = userid;
                        dr["created_dt"] = Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()));
                        dr.EndEdit();

                    }

                    if (dt2.Rows.Count > 0)
                    {
                        using (SqlBulkCopy copy2 = new SqlBulkCopy(sqlconnection))
                        {
                            copy2.DestinationTableName = "MISNGDATADET";

                            copy2.ColumnMappings.Add(0, 0);
                            copy2.ColumnMappings.Add(1, 1);
                            copy2.ColumnMappings.Add(2, 2);
                            copy2.ColumnMappings.Add(3, 3);
                            copy2.ColumnMappings.Add(4, 4);
                            copy2.ColumnMappings.Add(5, 5);
                            copy2.ColumnMappings.Add(6, 6);
                            copy2.ColumnMappings.Add(7, 7);
                            copy2.ColumnMappings.Add(8, 8);
                            copy2.ColumnMappings.Add(9, 9);
                            copy2.ColumnMappings.Add(12, 10);
                            copy2.ColumnMappings.Add(13, 11);

                            copy2.ColumnMappings.Add(14, 12);
                            copy2.ColumnMappings.Add(15, 13);
                            copy2.ColumnMappings.Add(10, 14);
                            copy2.ColumnMappings.Add(11, 15);
                            copy2.WriteToServer(dt2);
                        }

                        using (SqlBulkCopy copy2 = new SqlBulkCopy(localconnection))
                        {
                            copy2.DestinationTableName = "MISNGDATADET";


                            //copy2.ColumnMappings.Add(0, 0);
                            //copy2.ColumnMappings.Add(1, 1);
                            //copy2.ColumnMappings.Add(2, 2);
                            //copy2.ColumnMappings.Add(3, 3);
                            //copy2.ColumnMappings.Add(4, 4);
                            //copy2.ColumnMappings.Add(5, 5);
                            //copy2.ColumnMappings.Add(6, 6);
                            //copy2.ColumnMappings.Add(7, 7);
                            //copy2.ColumnMappings.Add(8, 8);
                            //copy2.ColumnMappings.Add(11, 9);
                            //copy2.ColumnMappings.Add(12, 10);
                            //copy2.ColumnMappings.Add(13, 11);

                            //copy2.ColumnMappings.Add(14, 12);
                            //copy2.ColumnMappings.Add(9, 13);
                            //copy2.ColumnMappings.Add(10, 14);

                            copy2.ColumnMappings.Add(0, 0);
                            copy2.ColumnMappings.Add(1, 1);
                            copy2.ColumnMappings.Add(2, 2);
                            copy2.ColumnMappings.Add(3, 3);
                            copy2.ColumnMappings.Add(4, 4);
                            copy2.ColumnMappings.Add(5, 5);
                            copy2.ColumnMappings.Add(6, 6);
                            copy2.ColumnMappings.Add(7, 7);
                            copy2.ColumnMappings.Add(8, 8);
                            copy2.ColumnMappings.Add(9, 9);
                            copy2.ColumnMappings.Add(12, 10);
                            copy2.ColumnMappings.Add(13, 11);

                            copy2.ColumnMappings.Add(14, 12);
                            copy2.ColumnMappings.Add(15, 13);
                            copy2.ColumnMappings.Add(10, 14);
                            copy2.ColumnMappings.Add(11, 15);


                            copy2.WriteToServer(dt2);
                        }

                    }
                    dbcom.CommandText = "select srno,realstat,datatypeid,parameterid,tiunit,divider,frequency,from_date,to_date,type,verified_by,verified_dt from avlbldatadet where querytype='insert'";
                    DataTable dt3 = new DataTable();
                    dt3 = BaseDb.ExecuteSelect(dbcom);
                    dt3.Columns.Add("created_by");
                    dt3.Columns.Add("created_dt", Type.GetType("System.DateTime"));
                    dt3.Columns.Add("updated_by");
                    dt3.Columns.Add("updated_dt");



                    foreach (DataRow dr in dt3.Rows)
                    {
                        dr.BeginEdit();
                        dr["created_by"] = userid;
                        dr["created_dt"] = Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()));
                        dr.EndEdit();

                    }

                    if (dt3.Rows.Count > 0)
                    {
                        using (SqlBulkCopy copy1 = new SqlBulkCopy(sqlconnection))
                        {
                            copy1.DestinationTableName = "avlbldatadet";

                            copy1.ColumnMappings.Add(0, 0);
                            copy1.ColumnMappings.Add(1, 1);
                            copy1.ColumnMappings.Add(2, 2);
                            copy1.ColumnMappings.Add(3, 3);
                            copy1.ColumnMappings.Add(4, 4);
                            copy1.ColumnMappings.Add(5, 5);
                            copy1.ColumnMappings.Add(6, 6);
                            copy1.ColumnMappings.Add(7, 7);
                            copy1.ColumnMappings.Add(8, 8);
                            copy1.ColumnMappings.Add(9, 9);
                            copy1.ColumnMappings.Add(12, 10);
                            copy1.ColumnMappings.Add(13, 11);

                            copy1.ColumnMappings.Add(14, 12);
                            copy1.ColumnMappings.Add(15, 13);
                            copy1.ColumnMappings.Add(10, 14);
                            copy1.ColumnMappings.Add(11, 15);
                            copy1.WriteToServer(dt3);
                        }

                        using (SqlBulkCopy copy1 = new SqlBulkCopy(localconnection))
                        {
                            copy1.DestinationTableName = "avlbldatadet";



                            //copy1.ColumnMappings.Add(0, 0);
                            //copy1.ColumnMappings.Add(1, 1);
                            //copy1.ColumnMappings.Add(2, 2);
                            //copy1.ColumnMappings.Add(3, 3);
                            //copy1.ColumnMappings.Add(4, 4);
                            //copy1.ColumnMappings.Add(5, 5);
                            //copy1.ColumnMappings.Add(6, 6);
                            //copy1.ColumnMappings.Add(7, 7);
                            //copy1.ColumnMappings.Add(8, 8);
                            //copy1.ColumnMappings.Add(11, 9);
                            //copy1.ColumnMappings.Add(12, 10);
                            //copy1.ColumnMappings.Add(13, 11);

                            //copy1.ColumnMappings.Add(14, 12);
                            //copy1.ColumnMappings.Add(9, 13);
                            //copy1.ColumnMappings.Add(10, 14);

                            copy1.ColumnMappings.Add(0, 0);
                            copy1.ColumnMappings.Add(1, 1);
                            copy1.ColumnMappings.Add(2, 2);
                            copy1.ColumnMappings.Add(3, 3);
                            copy1.ColumnMappings.Add(4, 4);
                            copy1.ColumnMappings.Add(5, 5);
                            copy1.ColumnMappings.Add(6, 6);
                            copy1.ColumnMappings.Add(7, 7);
                            copy1.ColumnMappings.Add(8, 8);
                            copy1.ColumnMappings.Add(9, 9);
                            copy1.ColumnMappings.Add(12, 10);
                            copy1.ColumnMappings.Add(13, 11);

                            copy1.ColumnMappings.Add(14, 12);
                            copy1.ColumnMappings.Add(15, 13);
                            copy1.ColumnMappings.Add(10, 14);
                            copy1.ColumnMappings.Add(11, 15);


                            copy1.WriteToServer(dt3);
                        }

                    }
                    #region updating divoffice,subdivoffice

                    DbCommand cmd = BaseDb.CreatetmpCommand();
                    cmd.CommandText = "select distinct divoffice from location";
                    DataTable divdt = BaseDb.ExecuteSelect(cmd);
                    DbCommand sqlcmd = BaseDb.CreateSqlCommand();
                    if (divdt.Rows.Count > 0)
                    {
                        for (int i = 0; i < divdt.Rows.Count; i++)
                        {
                            if (divdt.Rows[i]["divoffice"].ToString() != "" && divdt.Rows[i]["divoffice"].ToString() != null)
                            {
                                sqlcmd.CommandText = "select count(*) from divoffice where upper(ltrim(rtrim(divoffc_name)))=upper(ltrim(rtrim('" + divdt.Rows[i]["divoffice"] + "')))";
                                int cnt = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd).ToString());

                                if (cnt == 0)
                                {
                                    int divid = 0;
                                    sqlcmd.CommandText = "select count(*) from divoffice";
                                    int divcnt = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd));
                                    if (divcnt > 0)
                                    {
                                        sqlcmd.CommandText = "select max(divoffc_id) from divoffice";

                                        divid = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd).ToString());
                                    }
                                    else
                                    {
                                        divid = 0;
                                    }
                                    divid = divid + 1;
                                    sqlcmd.CommandText = "insert into divoffice(divoffc_id,divoffc_name,created_by,created_dt) values(" + divid + ",'" + divdt.Rows[i]["divoffice"] + "','" + userid + "','" + Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString())) + "')";
                                    BaseDb.ExecuteNonQuery(sqlcmd);
                                }

                                sqlcmd.CommandText = "select divoffc_id from divoffice where upper(ltrim(rtrim(divoffc_name)))=upper(ltrim(rtrim('" + divdt.Rows[i]["divoffice"] + "')))";
                                DataTable divoffdt = BaseDb.ExecuteSelect(sqlcmd);

                                cmd.CommandText = "select distinct SUBDIVOFFICE from location where divoffice='" + divdt.Rows[i]["divoffice"] + "'";
                                DataTable subdivdt = BaseDb.ExecuteSelect(cmd);
                                if (subdivdt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in subdivdt.Rows)
                                    {
                                        if (dr["subdivoffice"].ToString() != "" && dr["subdivoffice"].ToString() != null)
                                        {
                                            sqlcmd.CommandText = "select count(*) from subdivoffice where upper(ltrim(rtrim(SUBDIVOFFC_NAME)))=upper(ltrim(rtrim('" + dr["subdivoffice"] + "'))) and divoffc_id=" + Convert.ToInt32(divoffdt.Rows[0]["divoffc_id"].ToString());
                                            int count = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd).ToString());
                                            if (count == 0)
                                            {
                                                int subdivid;
                                                sqlcmd.CommandText = "select count(*) from subdivoffice";
                                                int subdivcnt = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd));
                                                if (subdivcnt > 0)
                                                {
                                                    sqlcmd.CommandText = "select max(SUBDIVOFFC_ID) from subdivoffice";
                                                    subdivid = Convert.ToInt32(BaseDb.ExecuteScalar(sqlcmd).ToString());
                                                }
                                                else
                                                {
                                                    subdivid = 0;
                                                }


                                                subdivid = subdivid + 1;
                                                sqlcmd.CommandText = "insert into subdivoffice(SUBDIVOFFC_ID,divoffc_id,subdivoffc_name,created_by,created_dt) values(" + subdivid + "," + Convert.ToInt32(divoffdt.Rows[0]["divoffc_id"].ToString()) + ",'" + dr["subdivoffice"] + "','" + userid + "','" + Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString())) + "')";
                                                BaseDb.ExecuteNonQuery(sqlcmd);
                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }



                    #endregion



                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            
        }
        #endregion
        
            #region updateverification
        public static Int32 updateverification(string userid)
        {
            int i = 0;
            try
            {
                DateTime dt = Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()));
                string verdate = dateconvert.Convertdt(Convert.ToString(dt));   
                DbCommand tmpcmd = BaseDb.CreatetmpCommand();
                tmpcmd.CommandText = "Update avlbldatadet set VERIFIED_BY='" + userid + "',verified_dt='" + verdate + "' ,verify=1";
                BaseDb.ExecuteNonQuery(tmpcmd);
                tmpcmd.CommandText = "Update misngdatadet set VERIFIED_BY='" + userid + "',verified_dt='" + verdate + "', verify=1";
                BaseDb.ExecuteNonQuery(tmpcmd);
                tmpcmd.CommandText = "Update dataparameter set VERIFIED_BY='" + userid + "',verified_dt='" + verdate + "' , verify=1";
                BaseDb.ExecuteNonQuery(tmpcmd);
                tmpcmd.CommandText = "Update datatype set VERIFIED_BY='" + userid + "',verified_dt='" + verdate + "', verify=1";
                BaseDb.ExecuteNonQuery(tmpcmd);
                tmpcmd.CommandText = "Update location set VERIFIED_BY='" + userid + "',verified_dt='" + verdate + "' , verify=1";
                BaseDb.ExecuteNonQuery(tmpcmd);
                i = 1;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return (i);
            
        }


        #endregion

            #region truncatetable
        public static Int32 truncatetable()
        {
            int i = 0;
            try
            {
                DbCommand localcmd = BaseDb.CreateStrCommand();
                DbCommand sqlcmd = BaseDb.CreateSqlCommand();
                DbCommand tmpcmd = BaseDb.CreatetmpCommand();

                sqlcmd.CommandText = "delete from avlbldatadet";
                BaseDb.ExecuteNonQuery(sqlcmd);
                localcmd.CommandText = "delete from avlbldatadet";
                BaseDb.ExecuteNonQuery(localcmd);
                tmpcmd.CommandText = "delete from avlbldatadet";
                BaseDb.ExecuteNonQuery(tmpcmd);

                sqlcmd.CommandText = "delete from misngdatadet";
                BaseDb.ExecuteNonQuery(sqlcmd);
                localcmd.CommandText = "delete from misngdatadet";
                BaseDb.ExecuteNonQuery(localcmd);
                tmpcmd.CommandText = "delete from misngdatadet";
                BaseDb.ExecuteNonQuery(tmpcmd);

                sqlcmd.CommandText = "delete from dataparameter";
                BaseDb.ExecuteNonQuery(sqlcmd);
                localcmd.CommandText = "delete from dataparameter";
                BaseDb.ExecuteNonQuery(localcmd);
                tmpcmd.CommandText = "delete from dataparameter";
                BaseDb.ExecuteNonQuery(tmpcmd);

                sqlcmd.CommandText = "delete from datatype";
                BaseDb.ExecuteNonQuery(sqlcmd);
                localcmd.CommandText = "delete from datatype";
                BaseDb.ExecuteNonQuery(localcmd);
                tmpcmd.CommandText = "delete from datatype";
                BaseDb.ExecuteNonQuery(tmpcmd);

               
                tmpcmd.CommandText = "delete from location";
                BaseDb.ExecuteNonQuery(tmpcmd);

                i = 1;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return (i);
        }
        #endregion

            #region preparing water quality data
         public static void preparewqdata(string filename, MetaDataEntity objm,string type)
         {
                SqlDateTime startdt;
             OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename);
             conn.Open();
             DataTable mdbdtlocation = new DataTable();
             CultureInfo cultinf = Thread.CurrentThread.CurrentCulture;
             TextInfo txt = cultinf.TextInfo;
                 
              OleDbDataReader rdr = null;
              #region prepare data from master-station for location table
              try
              {
                  string qry = "SELECT 'WaterQuality' as datatypeid,[Station Code],[Station Name], Agency, [Master - State].[State Name] AS country, District, [Tahsil / Taluk] AS Tahsil,Lat as Latitude, [Long] as longitude,[Ref Toposheet No] as toposheet,Altitude,[Master - basin].[Basin name] as Basin,River,Tributary,Stream as Catch,[Catchment Area] as catarea,[Regional Office] as REGOFFICE,";

                  qry += "[Circle Office] as CIRCOFFICE,[Divisional Office] as divoffice,[Sub-divisional Office] as subdivoffice,[Section Office] as SECTOFFICE,[Owner Agency] as OWNERAGENCY,[Date of Establishment],[Date of Closure] FROM [Master - Station] , [Master - State] ,[Master - Basin] where [Master - Station].State = [Master - State].[State Code] and [Master - station].[major basin]=[Master - basin].[basin code]";


                  OleDbCommand cmd = new OleDbCommand(qry, conn);//fetch data of station i.e location from mdb file


                  rdr = cmd.ExecuteReader();
                  mdbdtlocation.Load(rdr);

                  DataTable sqldtlocation = new DataTable();
                  DbCommand sqlcmd = BaseDb.CreateStrCommand();
                  sqlcmd.CommandText = "select realstat from Location where datatypeid='WaterQuality' ";
                  sqldtlocation = BaseDb.ExecuteSelect(sqlcmd);

                  var sqlquery1 = mdbdtlocation.AsEnumerable().Select(a => new { ID = a["Station Code"].ToString().Trim().ToUpper() });
                  var sqlquery2 = sqldtlocation.AsEnumerable().Select(b => new { ID = b["realstat"].ToString().Trim().ToUpper() });
                  var q = sqlquery1.Except(sqlquery2);
                         
                 
                
                  int cnt = q.Count();
                  int i;
                  if (cnt > 0)
                  {
                     
                    DataTable dt = (from a in mdbdtlocation.AsEnumerable() join ab in q on a["Station Code"].ToString().Trim().ToUpper() equals ab.ID.ToString().ToUpper() select a).CopyToDataTable();

                      for (i = 0; i < cnt; i++)
                      {
                          SqlDateTime enddt;
                          if ((dt.Rows[i]["Date of Establishment"].ToString()) != "")
                          {
                              startdt = DateTime.Parse(dt.Rows[i]["Date of Establishment"].ToString()).Date;
                          }
                          else
                          {
                              SqlDateTime dtate1 = SqlDateTime.Null;
                              startdt = (SqlDateTime)dtate1;
                          }

                          if ((dt.Rows[i]["Date of Closure"].ToString()) != "")
                          {
                              enddt = DateTime.Parse(dt.Rows[i]["Date of Closure"].ToString()).Date;
                          }
                          else
                          {
                              SqlDateTime dtate1 = SqlDateTime.Null;
                              enddt = (SqlDateTime)dtate1;
                          }

                          DbCommand tempcmd = BaseDb.CreatetmpCommand();
                          if ((dt.Rows[i]["Date of Closure"].ToString()) == "" && (dt.Rows[i]["Date of Establishment"].ToString()) == "")
                          {
                              tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,OWNERAGENCY,querytype)values " +
                                  "('"+dt.Rows[i]["datatypeid"].ToString()+"','" + txt.ToTitleCase(dt.Rows[i]["Station Code"].ToString()) + "','" + dt.Rows[i]["Station Name"].ToString() + "','" + dt.Rows[i]["country"] + "','" + dt.Rows[i]["District"] + "','" + dt.Rows[i]["Tahsil"] + "','" + dt.Rows[i]["basin"] + "','" + dt.Rows[i]["river"] + "','" + dt.Rows[i]["tributary"] + "','" + dt.Rows[i]["catch"] + "','" + dt.Rows[i]["latitude"] + "','" + dt.Rows[i]["longitude"] + "','" + dt.Rows[i]["altitude"].ToString() + "','" + dt.Rows[i]["catarea"] + "','" + dt.Rows[i]["toposheet"].ToString() + "','" + dt.Rows[i]["agency"].ToString() + "','" + dt.Rows[i]["regoffice"].ToString() + "','" + dt.Rows[i]["circoffice"].ToString() + "','" + dt.Rows[i]["divoffice"].ToString() + "','" + dt.Rows[i]["subdivoffice"].ToString() + "','" + dt.Rows[i]["sectoffice"].ToString() + "','" + dt.Rows[i]["owneragency"].ToString() + "','insert')";

                          }
                          else if ((dt.Rows[i]["Date of Closure"].ToString()) == "")
                          {
                              tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,OWNERAGENCY,STARTDATE,querytype)values " +
                                  "('" + dt.Rows[i]["datatypeid"].ToString() + "','" + txt.ToTitleCase(dt.Rows[i]["Station Code"].ToString()) + "','" + dt.Rows[i]["Station Name"].ToString() + "','" + dt.Rows[i]["country"] + "','" + dt.Rows[i]["District"] + "','" + dt.Rows[i]["Tahsil"] + "','" + dt.Rows[i]["basin"] + "','" + dt.Rows[i]["river"] + "','" + dt.Rows[i]["tributary"] + "','" + dt.Rows[i]["catch"] + "','" + dt.Rows[i]["latitude"] + "','" + dt.Rows[i]["longitude"] + "','" + dt.Rows[i]["altitude"].ToString() + "','" + dt.Rows[i]["catarea"] + "','" + dt.Rows[i]["toposheet"].ToString() + "','" + dt.Rows[i]["agency"].ToString() + "','" + dt.Rows[i]["regoffice"].ToString() + "','" + dt.Rows[i]["circoffice"].ToString() + "','" + dt.Rows[i]["divoffice"].ToString() + "','" + dt.Rows[i]["subdivoffice"].ToString() + "','" + dt.Rows[i]["sectoffice"].ToString() + "','" + dt.Rows[i]["owneragency"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(startdt)) + "','insert')";

                          }
                          else if ((dt.Rows[i]["Date of Establishment"].ToString()) == "")
                          {
                              tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,OWNERAGENCY,ENDDATE,querytype)values " +
                                  "('" + dt.Rows[i]["datatypeid"].ToString() + "','" + txt.ToTitleCase(dt.Rows[i]["Station Code"].ToString()) + "','" + dt.Rows[i]["Station Name"].ToString() + "','" + dt.Rows[i]["country"] + "','" + dt.Rows[i]["District"] + "','" + dt.Rows[i]["Tahsil"] + "','" + dt.Rows[i]["basin"] + "','" + dt.Rows[i]["river"] + "','" + dt.Rows[i]["tributary"] + "','" + dt.Rows[i]["catch"] + "','" + dt.Rows[i]["latitude"] + "','" + dt.Rows[i]["longitude"] + "','" + dt.Rows[i]["altitude"].ToString() + "','" + dt.Rows[i]["catarea"] + "','" + dt.Rows[i]["toposheet"].ToString() + "','" + dt.Rows[i]["agency"].ToString() + "','" + dt.Rows[i]["regoffice"].ToString() + "','" + dt.Rows[i]["circoffice"].ToString() + "','" + dt.Rows[i]["divoffice"].ToString() + "','" + dt.Rows[i]["subdivoffice"].ToString() + "','" + dt.Rows[i]["sectoffice"].ToString() + "','" + dt.Rows[i]["owneragency"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(enddt)) + "','insert')";
                          }
                          else
                          {
                               tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,OWNERAGENCY,STARTDATE,ENDDATE,querytype)values " +
                                  "('" + dt.Rows[i]["datatypeid"].ToString() + "','" + txt.ToTitleCase(dt.Rows[i]["Station Code"].ToString()) + "','" + dt.Rows[i]["Station Name"].ToString() + "','" + dt.Rows[i]["country"] + "','" + dt.Rows[i]["District"] + "','" + dt.Rows[i]["Tahsil"] + "','" + dt.Rows[i]["basin"] + "','" + dt.Rows[i]["river"] + "','" + dt.Rows[i]["tributary"] + "','" + dt.Rows[i]["catch"] + "','" + dt.Rows[i]["latitude"] + "','" + dt.Rows[i]["longitude"] + "','" + dt.Rows[i]["altitude"].ToString() + "','" + dt.Rows[i]["catarea"] + "','" + dt.Rows[i]["toposheet"].ToString() + "','" + dt.Rows[i]["agency"].ToString() + "','" + dt.Rows[i]["regoffice"].ToString() + "','" + dt.Rows[i]["circoffice"].ToString() + "','" + dt.Rows[i]["divoffice"].ToString() + "','" + dt.Rows[i]["subdivoffice"].ToString() + "','" + dt.Rows[i]["sectoffice"].ToString() + "','" + dt.Rows[i]["owneragency"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(startdt))   + "','" + dateconvert.Convertdt(Convert.ToString(enddt)) + "','insert')";
                          }
                          BaseDb.ExecuteNonQuery(tempcmd);
                      }
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              finally
              {
                  rdr.Close();
              }
              #endregion

              #region prepare data from master-parameter for dataparameter

              try
              {
                  OleDbCommand wqlparametercmd = new OleDbCommand("select distinct Hymos_id ,param_id,name from WQL_ParameterMaster", conn);

                  OleDbDataReader rdr1 = null;

                  rdr1 = wqlparametercmd.ExecuteReader();

                  DataTable dtparameter = new DataTable();
                  dtparameter.Load(rdr1);

                  int i = 0;
                  DataTable sqldtparameter = new DataTable();
                  DbCommand sqlcmd = BaseDb.CreateStrCommand();
                  sqlcmd.CommandText = "select parameterid from dataparameter where type='" + type + "' and Datatypeid='WaterQuality'";
                  sqldtparameter = BaseDb.ExecuteSelect(sqlcmd);

                  var sqlquery1 = dtparameter.AsEnumerable().Select(a => new { ID = a["Hymos_id"].ToString() });
                  var sqlquery2 = sqldtparameter.AsEnumerable().Select(b => new { ID = b["parameterid"].ToString() });
                  var q = sqlquery1.Except(sqlquery2);

                  int cnt = q.Count();

                  DataTable dt = new DataTable();

                  OleDbCommand cmd = new OleDbCommand("select min(Date_receipt)as startdate from WQL_Samples", conn);
                  OleDbDataReader rdr3 = null;
                  rdr3 = cmd.ExecuteReader();
                  DataTable dtdate = new DataTable();
                  dtdate.Load(rdr3);


                  if ((dtdate.Rows[0]["startdate"].ToString()) != "")
                  {
                      startdt = DateTime.Parse(dtdate.Rows[0]["startdate"].ToString()).Date;
                  }
                  else
                  {

                      SqlDateTime date1 = SqlDateTime.Null;
                      startdt = date1;
                  }

                  if (cnt > 0)
                  {
                      dt = (from a in dtparameter.AsEnumerable() join ab in q on a["Hymos_id"].ToString() equals ab.ID select a).CopyToDataTable();





                      if (dt.Rows.Count > 0)
                      {
                          for (i = 0; i < cnt; i++)
                          {

                              OleDbCommand cd=new OleDbCommand("SELECT count('"+dt.Rows[i]["param_id"].ToString()+"')FROM [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID where '"+dt.Rows[i]["param_id"].ToString()+"' is not null",conn);
                              int count= Convert.ToInt32(cd.ExecuteScalar().ToString());
                              if (count > 0)
                              {
                                  DbCommand tempcmd = BaseDb.CreatetmpCommand();
                                  if (startdt.ToString() != "")
                                  {
                                      tempcmd.CommandText = "insert into dataparameter (datatypeid,parameterid,parameterdesc,datafromdt,type,Querytype) values('WaterQuality','" + dt.Rows[i]["Hymos_Id"].ToString() + "','" + dt.Rows[i]["name"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(startdt)) + "','" + type + "','insert')";
                                  }
                                  else
                                  {
                                      tempcmd.CommandText = "insert into dataparameter (datatypeid,parameterid,parameterdesc,type,Querytype) values('WaterQuality','" + dt.Rows[i]["Hymos_Id"].ToString() + "','" + dt.Rows[i]["name"].ToString() + "','" + type + "','insert')";
                                  }

                                  BaseDb.ExecuteNonQuery(tempcmd);
                              }
                          }
                      }
                  }

                  var qry1 = sqlquery1.Intersect(sqlquery2);
                  int cnt1 = qry1.Count();

                  if (cnt1 > 0)
                  {
                      dt = (from a in dtparameter.AsEnumerable() join ab in qry1 on a["Hymos_id"].ToString() equals ab.ID select a).CopyToDataTable();

                      if (dt.Rows.Count > 0)
                      {
                          for (i = 0; i < dt.Rows.Count; i++)
                          {
                              DbCommand cmd1 = BaseDb.CreateStrCommand();
                              cmd1.CommandText = "select datafromdt,datauptodt from dataparameter where parameterid='" + dt.Rows[i]["hymos_id"] + "' ";
                              DataTable dt1 = BaseDb.ExecuteSelect(cmd1);
                              DateTime DT = DateTime.Parse(dt1.Rows[0]["datafromdt"].ToString()).Date;
                              if (DateTime.Parse(dt1.Rows[0]["datafromdt"].ToString()).Date > DateTime.Parse(startdt.ToString()).Date)
                              {
                                  DbCommand tmpcmd = BaseDb.CreatetmpCommand();
                                  //tmpcmd.CommandText = "insert into dataparameter values(parameterid,datafromdt,type,querytype) values('" + dt.Rows[i]["hymos_id"] + "','" + dateconvert.Convertdt(startdt.ToString()) + "','" + type + "','update')";   //commented by DSB on 26.6.2012
                                  tmpcmd.CommandText = "insert into dataparameter (parameterid,datafromdt,type,querytype) values('" + dt.Rows[i]["hymos_id"] + "','" + dateconvert.Convertdt(startdt.ToString()) + "','" + type + "','update')";     //added by DSB on 26.6.2012
                                  BaseDb.ExecuteNonQuery(tmpcmd);
                              }
                          }

                      }
                  }
              }
              catch (Exception EX)
              {

                  throw EX;
              }
              finally
              {

              };
              #endregion

             #region prepare availability and missing data
              try
              {
                  OleDbCommand cmd=new OleDbCommand("Select distinct [Site code] from [WQL_Sample-collection ]",conn);
                  OleDbDataReader dr = null;
                  dr = cmd.ExecuteReader();
                  DataTable dt = new DataTable();
                  dt.Load(dr);

                  if (dt.Rows.Count > 0)
                  {  int i=0;
                          for (i = 0; i < dt.Rows.Count; i++)
                          {  
                              DbCommand tmpcmd=BaseDb.CreatetmpCommand();
                              //string QRY1=" Select min(Expr1) as mindt,max(Expr1) as maxdt, from( SELECT WS.ID, Year(WC.Date_Collection) AS Expr1, WS.ph_fld, WS.EC_GEN, WS.DO, WS.Temp, WS.Colour_Cod,WS.Odour_code, WS.TDS, WS.SS, WS.[NH3-N],WS.[NO2+NO3],WS.[P-Tot],WS.[BOD3-27],WS.COD,WS.Ca,WS.Mg,WS.Na,WS.K,WS.Cl,WS.SO4,WS.CO3,WS.HCO3,WS.SiO2,WS.F,WS.[FCol-MPN],WS.[Chlf-a],WS.pH_GEN,WS.EC_GEN,WS.[ALK-TOT],WS.[Alk-Phen],WS.HAR_Total,WS.[BOD5-20],WS.[Tcol-MPN],WS.Al,WS.B,WS.Cd, WS.Cu,WS.Fe,WS.Hg,WS.Mn,WS.Ni,WS.[o-PO4-P],WS.[Org-N],WS.[DO_SAT%],WS.PAH,WS.Pb,WS.PCB,WS.SAR,WS.Se,WS.Secchi,WS.Turb, WS.Zn, WS.Phenols, WS.HAR_Ca,WS.[NO3-N], WS.[NO2-N],WS.TS,WS.BHC,WS.Endos,WS.[24D],WS.DDT,WS.Aldrin,WS.Dieldrine,WS.Dieldrin,WS.As,WS.CN,WS.TOC,WS.RSC,WS.[Na%] FROM [WQL_Sample-collection] WC INNER JOIN WQL_Samples WS ON WC.ID = WS.ID WHERE (((WC.[Site Code])='"+dt.Rows[i]["Site Code"]+"'))  ";
                              //string QRY2="GROUP BY WS.ID, Year(WC.Date_Collection),WS.ph_fld,WS.EC_GEN,WS.DO,WS.Temp,WS.Colour_Cod,WS.Odour_code,WS.TDS,WS.SS,WS.[NH3-N],WS.[NO2+NO3],WS.[P-Tot],WS.[BOD3-27],WS.COD,WS.Ca,WS.Mg,WS.Na,WS.K,WS.Cl,WS.SO4,WS.CO3,WS.HCO3,WS.SiO2,WS.F,WS.[FCol-MPN],WS.[Chlf-a],WS.pH_GEN,WS.EC_GEN,WS.[ALK-TOT],WS.[Alk-Phen],WS.HAR_Total,WS.[BOD5-20],WS.[Tcol-MPN],WS.Al,WS.B,WS.Cd,WS.Cu,WS.Fe,WS.Hg,WS.Mn,WS.Ni,WS.[o-PO4-P],WS.[Org-N],WS.[DO_SAT%],WS.PAH,WS.Pb,WS.PCB,WS.SAR,WS.Se,WS.Secchi,WS.Turb,WS.Zn,WS.Phenols,WS.HAR_Ca,WS.[NO3-N],WS.[NO2-N],WS.TS,WS.BHC,WS.Endos,WS.[24D],WS.DDT,WS.Aldrin,WS.Dieldrine,WS.Dieldrin,WS.As,WS.CN,WS.TOC, WS.RSC, WS.[Na%]) a";

                              string qry = "select min(WC.Date_collection) as mindt,max(WC.Date_collection) as maxdt from [WQL_Sample-collection] WC INNER JOIN WQL_Samples WS ON WC.ID = WS.ID WHERE (((WC.[Site Code])='" + dt.Rows[i]["Site Code"] + "')) ";
                              
                              OleDbCommand cmd1 = new OleDbCommand(qry, conn);
                              OleDbDataReader reader = cmd1.ExecuteReader();
                              DataTable qrydt = new DataTable();
                              qrydt.Load(reader);
                                                            
                              OleDbCommand cmd2 = new OleDbCommand("select Hymos_id ,param_id from WQL_ParameterMaster", conn);
                              DataTable parameterdt = new DataTable();
                                reader=cmd2.ExecuteReader();
                                parameterdt.Load(reader);

                                DataTable existingdt = new DataTable();//existingdt  for getting list of existing parameters for finding missing
                                existingdt.Columns.Add("param_id");
                                existingdt.Columns.Add("Hymos_id");

                                DataTable newdt = new DataTable();//newdt for getting list of the new parameters for finding missing
                                newdt.Columns.Add("param_id");
                                newdt.Columns.Add("Hymos_id");

                                OleDbCommand tmpcmd1 = new OleDbCommand("select * from [wql_samples] where 1<>1",conn);
                                DataTable tmpdt = new DataTable();
                                    OleDbDataReader tmprdr= tmpcmd1.ExecuteReader();
                                    tmpdt.Load(tmprdr);
                              //according to parameterdt update avlbl min year and max year for each parameter stationwise
                                if (parameterdt.Rows.Count > 0)
                                {
                                    foreach (DataRow drow in parameterdt.Rows)
                                    {
                                        if (tmpdt.Columns.Contains(drow["param_id"].ToString()))
                                        {
                                            OleDbCommand cd = new OleDbCommand("SELECT count([" + drow["param_id"].ToString() + "]) FROM [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID where [" + drow["param_id"].ToString() + "] is not null", conn);
                                            int count = Convert.ToInt32(cd.ExecuteScalar().ToString());
                                            if (count > 0)
                                            {
                                                DbCommand sqlcmd = BaseDb.CreateStrCommand();
                                                sqlcmd.CommandText = "select From_date,to_date from avlbldatadet where upper(realstat)='" + dt.Rows[i]["site code"].ToString().ToUpper() + "' and parameterid='" + drow["hymos_id"].ToString() + "'";
                                                DataTable dtable = BaseDb.ExecuteSelect(sqlcmd);
                                                if (dtable.Rows.Count > 0)
                                                {
                                                    if (Convert.ToDateTime(qrydt.Rows[0]["mindt"].ToString()) < Convert.ToDateTime(dtable.Rows[0]["from_date"].ToString()) && Convert.ToDateTime(qrydt.Rows[0]["maxdt"].ToString()) > Convert.ToDateTime(dtable.Rows[0]["to_date"].ToString()))
                                                    {
                                                        //tmpcmd.CommandText = "insert into avlbldatadet(realstat,datatypeid,parameterid,from_date,to_date,querytype,type) values('" + txt.ToTitleCase(dt.Rows[i]["site code"].ToString()) + "','WaterQuality','" + drow["hymos_id"].ToString() + "','" + dateconvert.Convertdt(qrydt.Rows[0]["mindt"].ToString()) + "','" + dateconvert.Convertdt(qrydt.Rows[0]["maxdt"].ToString()) + "','update','" + type + "'";  //commented by DSB on 26.6.2012
                                                        tmpcmd.CommandText = "insert into avlbldatadet(realstat,datatypeid,parameterid,from_date,to_date,querytype,type) values('" + txt.ToTitleCase(dt.Rows[i]["site code"].ToString()) + "','WaterQuality','" + drow["hymos_id"].ToString() + "','" + dateconvert.Convertdt(qrydt.Rows[0]["mindt"].ToString()) + "','" + dateconvert.Convertdt(qrydt.Rows[0]["maxdt"].ToString()) + "','update','" + type + "')";   //added by DSB on 26.6.2012
                                                        BaseDb.ExecuteNonQuery(tmpcmd);
                                                    }
                                                    else if (Convert.ToDateTime(qrydt.Rows[0]["mindt"].ToString()) < Convert.ToDateTime(dtable.Rows[0]["from_date"].ToString()) && Convert.ToDateTime(qrydt.Rows[0]["maxdt"].ToString()) < Convert.ToDateTime(dtable.Rows[0]["to_date"].ToString()))
                                                    {
                                                        //tmpcmd.CommandText = "insert into avlbldatadet(realstat,datatypeid,parameterid,from_date,to_date,querytype,type) values('" + txt.ToTitleCase(dt.Rows[i]["site code"].ToString()) + "','WaterQuality','" + drow["hymos_id"].ToString() + "','" + dateconvert.Convertdt(qrydt.Rows[0]["mindt"].ToString()) + "','" + dateconvert.Convertdt(dtable.Rows[0]["to_date"].ToString()) + "','update','" + type + "'";  //commented by DSB on 26.6.2012
                                                        tmpcmd.CommandText = "insert into avlbldatadet(realstat,datatypeid,parameterid,from_date,to_date,querytype,type) values('" + txt.ToTitleCase(dt.Rows[i]["site code"].ToString()) + "','WaterQuality','" + drow["hymos_id"].ToString() + "','" + dateconvert.Convertdt(qrydt.Rows[0]["mindt"].ToString()) + "','" + dateconvert.Convertdt(dtable.Rows[0]["to_date"].ToString()) + "','update','" + type + "')";   //added by DSB on 26.6.2012
                                                        BaseDb.ExecuteNonQuery(tmpcmd);
                                                    }
                                                    else if (Convert.ToDateTime(qrydt.Rows[0]["mindt"].ToString()) > Convert.ToDateTime(dtable.Rows[0]["from_date"].ToString()) && Convert.ToDateTime(qrydt.Rows[0]["maxdt"].ToString()) > Convert.ToDateTime(dtable.Rows[0]["to_date"].ToString()))
                                                    {
                                                        //tmpcmd.CommandText = "insert into avlbldatadet(realstat,datatypeid,parameterid,from_date,to_date,querytype,type) values('" + txt.ToTitleCase(dt.Rows[i]["site code"].ToString()) + "','WaterQuality','" + drow["hymos_id"].ToString() + "','" + dateconvert.Convertdt(dtable.Rows[0]["from_date"].ToString()) + "','" +  dateconvert.Convertdt(qrydt.Rows[0]["maxdt"].ToString()) + "','update','" + type + "'";  //commented by DSB on 26.6.2012
                                                        tmpcmd.CommandText = "insert into avlbldatadet(realstat,datatypeid,parameterid,from_date,to_date,querytype,type) values('" + txt.ToTitleCase(dt.Rows[i]["site code"].ToString()) + "','WaterQuality','" + drow["hymos_id"].ToString() + "','" + dateconvert.Convertdt(dtable.Rows[0]["from_date"].ToString()) + "','" + dateconvert.Convertdt(qrydt.Rows[0]["maxdt"].ToString()) + "','update','" + type + "')";   //added by DSB on 26.6.2012
                                                        BaseDb.ExecuteNonQuery(tmpcmd);

                                                    }
                                                    DataRow newrow = existingdt.NewRow();
                                                    newrow["param_id"] = drow["param_id"].ToString();
                                                    newrow["Hymos_id"] = drow["Hymos_id"].ToString();
                                                    existingdt.Rows.Add(newrow);
                                                }
                                                else
                                                {
                                                    string a = qrydt.Rows[0]["mindt"].ToString();
                                                    DateTime fromdt = Convert.ToDateTime(qrydt.Rows[0]["mindt"].ToString());
                                                    string b = qrydt.Rows[0]["maxdt"].ToString();
                                                    DateTime todt = Convert.ToDateTime(qrydt.Rows[0]["maxdt"].ToString());
                                                    tmpcmd.CommandText = "insert into avlbldatadet(realstat,datatypeid,parameterid,from_date,to_date,querytype,type) values('" + txt.ToTitleCase(dt.Rows[i]["site code"].ToString()) + "','WaterQuality','" + drow["hymos_id"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(fromdt)) + "','" + dateconvert.Convertdt(Convert.ToString(todt)) + "','insert','" + type + "')";
                                                    BaseDb.ExecuteNonQuery(tmpcmd);

                                                    DataRow newrow = newdt.NewRow();
                                                    newrow["param_id"] = drow["param_id"].ToString();
                                                    newrow["Hymos_id"] = drow["Hymos_id"].ToString();
                                                    newdt.Rows.Add(newrow);
                                                }
                                            }
                                        }
                                    }
                                   // OleDbCommand cmd4=new OleDbCommand("select 
                                }

                                if (newdt.Rows.Count > 0)
                                {
                                    int k = 0;
                                    try
                                    {
                                        for (k = 0; k < newdt.Rows.Count; k++)
                                        {
                                            string misngqry = "select yr from (select year(date_receipt) as yr from [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID where [WQL_Sample-collection].[site code]='" + dt.Rows[i]["Site Code"] + "'  group by year(date_receipt) )  where  yr not in (select  year(date_receipt) as yr from WQL_Samples where id in(   SELECT [WQL_Sample-collection].id FROM [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID where [WQL_Sample-collection].[site code]='" + dt.Rows[i]["Site Code"] + "')  and [" + newdt.Rows[k]["param_id"] + "] is not null group by year(date_receipt) order by year(date_receipt) )";
                                            OleDbCommand misngcmd = new OleDbCommand(misngqry, conn);
                                            OleDbDataReader misngrd = null;
                                            misngrd = misngcmd.ExecuteReader();
                                            DataTable misngdt = new DataTable();
                                            misngdt.Load(misngrd);

                                            if (misngdt.Rows.Count > 0)
                                            {
                                                foreach (DataRow row in misngdt.Rows)
                                                {
                                                    tmpcmd.CommandText = "insert into misngdatadet(realstat,datatypeid,parameterid,year,cancel,type,querytype) values('" + txt.ToTitleCase(dt.Rows[i]["site code"].ToString()) + "','WaterQuality','" + newdt.Rows[k]["hymos_id"] + "'," + row["yr"] + ",0,'" + type + "','insert')";
                                                    BaseDb.ExecuteNonQuery(tmpcmd);
                                                }
                                            }

                                        }
                                    }
                                    catch(Exception ex)
                                    {
                                        throw (ex);
                                    }
                                }
                                if (existingdt.Rows.Count > 0)
                                {  int j=0;
                                    for (j = 0; j < existingdt.Rows.Count; j++)
                                    {
                                        string misngqry = "select yr from (select year(date_receipt) as yr from [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID where [WQL_Sample-collection].[site code]='" + dt.Rows[i]["Site Code"] + "'  group by year(date_receipt) )  where  yr not in (select  year(date_receipt) as yr from WQL_Samples where id in(   SELECT [WQL_Sample-collection].id FROM [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID where [WQL_Sample-collection].[site code]='" + dt.Rows[i]["Site Code"] + "')  and [" + existingdt.Rows[j]["param_id"] + "] is not null group by year(date_receipt) order by year(date_receipt) )";
                                        //string misngqry = "select yr from (select year(date_receipt) as yr from [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID where [WQL_Sample-collection].[site code]='" + dt.Rows[i]["Site Code"] + "'  group by year(date_receipt) )  where  yr not in (select  year(date_receipt) as yr from WQL_Samples where id in(   SELECT [WQL_Sample-collection].id FROM [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID where [WQL_Sample-collection].[site code]='" + dt.Rows[i]["Site Code"] + "')  and [" + existingdt.Rows[k]["param_id"] + "] is not null group by year(date_receipt) order by year(date_receipt) )";
                                        OleDbCommand misngcmd = new OleDbCommand(misngqry, conn);
                                        OleDbDataReader misngrd = null;
                                        misngrd = misngcmd.ExecuteReader();
                                        DataTable mdbmisngdt = new DataTable();
                                        mdbmisngdt.Load(misngrd);

                                        DbCommand sqlcmd = BaseDb.CreateStrCommand();
                                        sqlcmd.CommandText = "select year,cancel from misngdatadet where upper(realstat)='" + dt.Rows[i]["site code"].ToString().ToUpper() + "' and parameterid='" + existingdt.Rows[j]["Hymos_id"] + "'";
                                        DataTable sqlmisngdt = new DataTable();
                                        sqlmisngdt = BaseDb.ExecuteSelect(sqlcmd);

                                        DataColumn[] keys = new DataColumn[1];
                                        keys[0] = sqlmisngdt.Columns["year"];
                                        sqlmisngdt.PrimaryKey = keys;
                                        sqlmisngdt.Columns.Add("new");


                                        //foreach (DataRow row in mdbmisngdt.Rows)
                                        //{
                                          //  OleDbCommand msngcmd = new OleDbCommand("select yr from (select year(measdate) as yr from " + (row["TableName"].ToString()) + " group by year(measdate)) where yr not in ( select year(measdate) from " + (row["TableName"].ToString()) + " where measvalue<>-999.99 group by year(measdate))", conn);
                                           // DbDataReader dataread = msngcmd.ExecuteReader();
                                           // DataTable msngdt = new DataTable();
                                           // string flname = row["TableName"].ToString();

                                           // msngdt.Load(dataread);      //for getting missing year present in table from mdb 

                                            string cond = "";
                                            foreach (DataRow drow in mdbmisngdt.Rows)
                                            {
                                                int m = Convert.ToInt32(drow["yr"].ToString());
                                                DataRow dtr = sqlmisngdt.Rows.Find(m);  //checked for new missing year is already present or not

                                                if (dtr != null)
                                                {
                                                    dtr.BeginEdit();
                                                    dtr["cancel"] = 0;     //set cancel=0
                                                    dtr.EndEdit();
                                                }
                                                else
                                                {
                                                    DataRow dr4 = sqlmisngdt.NewRow();   //add new record of missing year
                                                    dr4["year"] = drow["yr"].ToString();
                                                    dr4["cancel"] = 0;
                                                    dr4["new"] = 'n';
                                                    sqlmisngdt.Rows.Add(dr4);
                                                }

                                                cond = cond + drow["yr"] + ",";


                                                OleDbCommand msngcmd1 = new OleDbCommand("SELECT min(wql_samples.date_receipt)FROM [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID where [wql_Sample-collection].[site code]='"+dt.Rows[i]["site code"]+"'"  , conn); //for getting min date                      
                                                DateTime mindate = Convert.ToDateTime(msngcmd1.ExecuteScalar().ToString());

                                               OleDbCommand msngcmd = new OleDbCommand("Select max(wql_samples.date_receipt)  FROM [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID where [wql_Sample-collection].[site code]='" + dt.Rows[i]["site code"] + "'", conn); //for getting max date in table
                                                DateTime maxdate = Convert.ToDateTime(msngcmd.ExecuteScalar().ToString());

                                                var sqlquery1 = mdbmisngdt.AsEnumerable().Select(a => new { ID = a["yr"].ToString() });
                                                var sqlquery2 = sqlmisngdt.AsEnumerable().Select(b => new { ID = b["year"].ToString() });
                                                var q = sqlquery2.Except(sqlquery1);


                                                int cnt = q.Count();

                                                if (cnt > 0)
                                                {
                                                    DataTable dt1 = (from a in sqlmisngdt.AsEnumerable() join ab in q on a["year"].ToString() equals ab.ID select a).CopyToDataTable();

                                                    foreach (DataRow r in dt1.Rows)
                                                    {
                                                        if ((Convert.ToInt32(mindate.Year) <= Convert.ToInt32(r["year"].ToString())) && (Convert.ToInt32(r["year"].ToString()) <= Convert.ToInt32(maxdate.Year)))
                                                        {
                                                            int yr = Convert.ToInt32(r["year"].ToString());

                                                            DataRow rtbl = sqlmisngdt.Rows.Find(yr);
                                                            if (rtbl != null)
                                                            {
                                                                if (rtbl["cancel"].ToString() != "1")
                                                                {
                                                                    rtbl.BeginEdit();

                                                                    rtbl["cancel"] = 1;  //if data present then set cancel=1;
                                                                    rtbl.EndEdit();
                                                                }
                                                            }

                                                        }
                                                    }


                                                }


                                            }


                                            foreach (DataRow drow1 in sqlmisngdt.Rows)
                                        {
                                            DbCommand tmpdb = BaseDb.CreatetmpCommand();

                                            if (drow1["new"].ToString() == "n")
                                            {
                                                tmpdb.CommandText = "insert into misngdatadet(realstat,parameterid,year,cancel,type,querytype) values('" + txt.ToTitleCase(dt.Rows[i]["site code"].ToString()) + "','" + existingdt.Rows[j]["Hymos_id"] + "'," + Convert.ToInt32(drow1["year"].ToString()) + "," + Convert.ToInt32(drow1["cancel"].ToString()) + ",'" + type + "','insert')";
                                                BaseDb.ExecuteNonQuery(tmpdb);

                                            }
                                            else
                                            {
                                                tmpdb.CommandText = "insert into misngdatadet(realstat,parameterid,year,cancel,type,querytype) values('" + txt.ToTitleCase(dt.Rows[i]["site code"].ToString()) + "','" + existingdt.Rows[j]["Hymos_id"] + "'," + Convert.ToInt32(drow1["year"].ToString()) + "," + Convert.ToInt32(drow1["cancel"].ToString()) + ",'" + type + "','update')";
                                                BaseDb.ExecuteNonQuery(tmpdb);
                                            }


                                        }

                                    }
                                }


                          }
                      DbCommand dbcomm=BaseDb.CreatetmpCommand();

                      dbcomm.CommandText = "SELECT COUNT(*) FROM DATAPARAMETER WHERE PARAMETERID LIKE 'Q%' ";
                      string WCOUNT = BaseDb.ExecuteScalar(dbcomm);
                      if (Convert.ToInt32(WCOUNT.ToString()) != 0)
                        {
                           string COND = " LIKE 'Q%' ";
                            string DTTYPE = "WaterQuality";
                            adddatatype( DTTYPE, type);
                        }
                      

                  }

              }
              catch (Exception ex)
              {
                  throw (ex);
              }
              //added by DSB on 26.6.2012
              finally
              {
                  conn.Close();
                  conn.Dispose();

                  string SaveLocation = "~\\Upload\\" + filename;

                  if (File.Exists(SaveLocation))
                  {

                      File.Delete(SaveLocation);
                  }
              }
             //

             #endregion
             

             #region insert datatype
             //adddatatype("WaterQuality",type);
#endregion
         }

        #endregion                                                           


        #region preparing sediment data
         public static void preparesedimentdata(string filename, MetaDataEntity objm, string type)
         {
             OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename);
             conn.Open();
             DataTable mdbdtlocation = new DataTable();
             string[] paraname ;
             OleDbDataReader rdr = null;
             CultureInfo cultinf = Thread.CurrentThread.CurrentCulture;
             TextInfo txt = cultinf.TextInfo;
             try
             {

                 #region data for location table from sediment-location

                 string qry = "Select a.*,'Sediment' as datatypeid from location a";
                 try
                 {
                     OleDbCommand cmd = new OleDbCommand(qry, conn);

                     rdr = cmd.ExecuteReader();
                     mdbdtlocation.Load(rdr);
                     DataTable sqldtlocation = new DataTable();
                     DbCommand sqlcmd = BaseDb.CreateStrCommand();
                     sqlcmd.CommandText = "select realstat from Location where datatypeid='Sediment' ";
                     sqldtlocation = BaseDb.ExecuteSelect(sqlcmd);

                     var sqlquery1 = mdbdtlocation.AsEnumerable().Select(a => new { ID = a["realstat"].ToString().Trim().ToUpper() });
                     var sqlquery2 = sqldtlocation.AsEnumerable().Select(b => new { ID = b["realstat"].ToString().Trim().ToUpper() });
                     var q = sqlquery1.Except(sqlquery2);


                     int cnt = q.Count();
                     int i;
                     if (cnt > 0)
                     {
                         DataTable dt = (from a in mdbdtlocation.AsEnumerable() join ab in q on a["realstat"].ToString().Trim().ToUpper() equals ab.ID.ToString().ToUpper() select a).CopyToDataTable();

                         //DataSet ds1 = new DataSet();
                         //ds1.Tables.Add(rowindt1);
                         //ds1.Tables.Add(mdbdtlocation);
                         //ds1.Relations.Add(rowindt1.Columns["realstat"], mdbdtlocation.Columns["realstat"]);


                         for (i = 0; i < cnt; i++)
                         {
                             SqlDateTime startdt, enddt;
                             if ((dt.Rows[i]["STARTDATE"].ToString()) != "")
                             {
                                 startdt = DateTime.Parse(dt.Rows[i]["STARTDATE"].ToString()).Date;

                             }
                             else
                             {

                                 SqlDateTime dtate1 = SqlDateTime.Null;
                                 startdt = (SqlDateTime)dtate1;
                             }

                             if ((dt.Rows[i]["ENDDATE"].ToString()) != "")
                             {
                                 enddt = DateTime.Parse(dt.Rows[i]["ENDDATE"].ToString()).Date;

                             }
                             else
                             {

                                 SqlDateTime dtate1 = SqlDateTime.Null;
                                 enddt = (SqlDateTime)dtate1;
                             }

                             DbCommand tempcmd = BaseDb.CreatetmpCommand();
                             if ((dt.Rows[i]["ENDDATE"].ToString()) == "" && (dt.Rows[i]["STARTDATE"].ToString()) == "")
                             {
                                 tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,LOCALXCOORD,LOCALYCOORD,querytype)values " +
                                     "('" + dt.Rows[i]["datatypeid"] + "','" + txt.ToTitleCase(dt.Rows[i]["realstat"].ToString()) + "','" + dt.Rows[i]["statname"].ToString() + "','" + dt.Rows[i]["country"] + "','" + dt.Rows[i]["province"] + "','" + dt.Rows[i]["subdistrict"] + "','" + dt.Rows[i]["basin"] + "','" + dt.Rows[i]["rivername"] + "','" + dt.Rows[i]["tributary"] + "','" + dt.Rows[i]["catch"] + "','" + dt.Rows[i]["latitude"] + "','" + dt.Rows[i]["longitude"] + "','" + dt.Rows[i]["altitude"].ToString() + "','" + dt.Rows[i]["catarea"] + "','" + dt.Rows[i]["toposheet"].ToString() + "','" + dt.Rows[i]["agency"].ToString() + "','" + dt.Rows[i]["regoffice"].ToString() + "','" + dt.Rows[i]["circoffice"].ToString() + "','" + dt.Rows[i]["divoffice"].ToString() + "','" + dt.Rows[i]["subdivoffice"].ToString() + "','" + dt.Rows[i]["sectoffice"].ToString() + "','" + dt.Rows[i]["remarks"].ToString() + "','" + dt.Rows[i]["owneragency"].ToString() + "'," + Convert.ToInt32(dt.Rows[i]["localxcoord"].ToString()) + "," + Convert.ToInt32(dt.Rows[i]["localycoord"].ToString()) + ",'insert')";

                             }
                             else if ((dt.Rows[i]["ENDDATE"].ToString()) == "")
                             {
                                 tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,LOCALXCOORD,LOCALYCOORD,querytype)values " +
                                     "('" + dt.Rows[i]["datatypeid"] + "','" + txt.ToTitleCase(dt.Rows[i]["realstat"].ToString()) + "','" + dt.Rows[i]["statname"].ToString() + "','" + dt.Rows[i]["country"] + "','" + dt.Rows[i]["province"] + "','" + dt.Rows[i]["subdistrict"] + "','" + dt.Rows[i]["basin"] + "','" + dt.Rows[i]["rivername"] + "','" + dt.Rows[i]["tributary"] + "','" + dt.Rows[i]["catch"] + "','" + dt.Rows[i]["latitude"] + "','" + dt.Rows[i]["longitude"] + "','" + dt.Rows[i]["altitude"].ToString() + "','" + dt.Rows[i]["catarea"] + "','" + dt.Rows[i]["toposheet"].ToString() + "','" + dt.Rows[i]["agency"].ToString() + "','" + dt.Rows[i]["regoffice"].ToString() + "','" + dt.Rows[i]["circoffice"].ToString() + "','" + dt.Rows[i]["divoffice"].ToString() + "','" + dt.Rows[i]["subdivoffice"].ToString() + "','" + dt.Rows[i]["sectoffice"].ToString() + "','" + dt.Rows[i]["remarks"].ToString() + "','" + dt.Rows[i]["owneragency"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(startdt)) + "'," + Convert.ToInt32(dt.Rows[i]["localxcoord"].ToString()) + "," + Convert.ToInt32(dt.Rows[i]["localycoord"].ToString()) + ",'insert')";

                             }
                             else if ((dt.Rows[i]["STARTDATE"].ToString()) == "")
                             {
                                 tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,ENDDATE,LOCALXCOORD,LOCALYCOORD,querytype)values " +
                                     "('" + dt.Rows[i]["datatypeid"] + "','" + txt.ToTitleCase(dt.Rows[i]["realstat"].ToString()) + "','" + dt.Rows[i]["statname"].ToString() + "','" + dt.Rows[i]["country"] + "','" + dt.Rows[i]["province"] + "','" + dt.Rows[i]["subdistrict"] + "','" + dt.Rows[i]["basin"] + "','" + dt.Rows[i]["rivername"] + "','" + dt.Rows[i]["tributary"] + "','" + dt.Rows[i]["catch"] + "','" + dt.Rows[i]["latitude"] + "','" + dt.Rows[i]["longitude"] + "','" + dt.Rows[i]["altitude"].ToString() + "','" + dt.Rows[i]["catarea"] + "','" + dt.Rows[i]["toposheet"].ToString() + "','" + dt.Rows[i]["agency"].ToString() + "','" + dt.Rows[i]["regoffice"].ToString() + "','" + dt.Rows[i]["circoffice"].ToString() + "','" + dt.Rows[i]["divoffice"].ToString() + "','" + dt.Rows[i]["subdivoffice"].ToString() + "','" + dt.Rows[i]["sectoffice"].ToString() + "','" + dt.Rows[i]["remarks"].ToString() + "','" + dt.Rows[i]["owneragency"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(enddt)) + "'," + Convert.ToInt32(dt.Rows[i]["localxcoord"].ToString()) + "," + Convert.ToInt32(dt.Rows[i]["localycoord"].ToString()) + ",'insert')";
                             }
                             else
                             {
                                 tempcmd.CommandText = "insert into Location( datatypeid,REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,ENDDATE,LOCALXCOORD,LOCALYCOORD,querytype)values " +
                                     "('" + dt.Rows[i]["datatypeid"] + "','" + txt.ToTitleCase(dt.Rows[i]["realstat"].ToString()) + "','" + dt.Rows[i]["statname"].ToString() + "','" + dt.Rows[i]["country"] + "','" + dt.Rows[i]["province"] + "','" + dt.Rows[i]["subdistrict"] + "','" + dt.Rows[i]["basin"] + "','" + dt.Rows[i]["rivername"] + "','" + dt.Rows[i]["tributary"] + "','" + dt.Rows[i]["catch"] + "','" + dt.Rows[i]["latitude"] + "','" + dt.Rows[i]["longitude"] + "','" + dt.Rows[i]["altitude"].ToString() + "','" + dt.Rows[i]["catarea"] + "','" + dt.Rows[i]["toposheet"].ToString() + "','" + dt.Rows[i]["agency"].ToString() + "','" + dt.Rows[i]["regoffice"].ToString() + "','" + dt.Rows[i]["circoffice"].ToString() + "','" + dt.Rows[i]["divoffice"].ToString() + "','" + dt.Rows[i]["subdivoffice"].ToString() + "','" + dt.Rows[i]["sectoffice"].ToString() + "','" + dt.Rows[i]["remarks"].ToString() + "','" + dt.Rows[i]["owneragency"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(startdt)) + "','" + dateconvert.Convertdt(Convert.ToString(enddt)) + "'," + Convert.ToInt32(dt.Rows[i]["localxcoord"].ToString()) + "," + Convert.ToInt32(dt.Rows[i]["localycoord"].ToString()) + ",'insert')";
                             }
                             BaseDb.ExecuteNonQuery(tempcmd);
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 finally
                 {
                     rdr.Close();
                 }
                 #endregion

                 #region data for dataparameter from sediment table


                 try
                 {
                     string qry1 = "select * from sediment";

                     OleDbCommand cmd1 = new OleDbCommand(qry1, conn);
                     rdr = cmd1.ExecuteReader();

                     DataTable dtseries = new DataTable();
                     dtseries.Load(rdr);

                     string[] colname = (from dc in dtseries.Columns.Cast<DataColumn>() select dc.ColumnName).ToArray();

                     var indx1 = colname.Select((item, index) => new
                     {
                         ItemName = item,
                         Position = index
                     }).Where(i => i.ItemName == "DISCHARG").First().Position;


                     var indx2 = colname.Select((item, index) => new
                     {
                         ItemName = item,
                         Position = index
                     }).Where(i => i.ItemName.ToUpper() == "FLAG").First().Position;



                     string[] arry = new string[indx2 - (indx1 + 1)];
                     Array.Copy(colname, indx1 + 1, arry, 0, indx2 - (indx1 + 1));


                     int arycnt = arry.Count();
                     paraname = arry.ToArray();
                     DataTable dtparameter = new DataTable();
                     dtparameter.Columns.Add("parameterid");///list of parameters from mdb

                     if (arycnt > 0)
                     {
                         for (int k = 0; k < arycnt; k++)
                         {
                             DataRow dr = dtparameter.NewRow();
                             dr["parameterid"] = arry[k];
                             dtparameter.Rows.Add(dr);
                         }

                     }



                     int j = 0;
                     DataTable sqldtparameter = new DataTable();
                     DbCommand sqlcmd1 = BaseDb.CreateStrCommand();
                     sqlcmd1.CommandText = "select parameterid from dataparameter where type='" + type + "' and Datatypeid='Sediment'";
                     sqldtparameter = BaseDb.ExecuteSelect(sqlcmd1);

                     //var sqlqry1=arry.AsEnumerable().Select(a=>new{ID=a});



                     var sqlqry1 = dtparameter.AsEnumerable().Select(a => new { ID = a["parameterid"].ToString() });
                     var sqlqry2 = sqldtparameter.AsEnumerable().Select(b => new { ID = b["parameterid"].ToString() });
                     var q1 = sqlqry1.Except(sqlqry2);

                     int count = q1.Count();
                     SqlDateTime strtdt;
                     DataTable dtsediment = new DataTable();

                     OleDbCommand cmd2 = new OleDbCommand("select min(MeasurementDate)as startdate from Sediment", conn);
                     OleDbDataReader rdr3 = null;
                     rdr3 = cmd2.ExecuteReader();
                     DataTable dtdate = new DataTable();
                     dtdate.Load(rdr3);


                     if ((dtdate.Rows[0]["startdate"].ToString()) != "")
                     {
                         strtdt = DateTime.Parse(dtdate.Rows[0]["startdate"].ToString()).Date;
                     }
                     else
                     {

                         SqlDateTime date1 = SqlDateTime.Null;
                         strtdt = date1;
                     }

                     if (count > 0)
                     {
                         dtsediment = (from a in dtparameter.AsEnumerable() join ab in q1 on a["parameterid"].ToString() equals ab.ID select a).CopyToDataTable();


                         if (dtsediment.Rows.Count > 0)
                         {
                             for (j = 0; j < count; j++)
                             {

                                 OleDbCommand cd = new OleDbCommand("SELECT count('" + dtsediment.Rows[j]["parameterid"].ToString() + "')FROM Sediment where '" + dtsediment.Rows[j]["parameterid"].ToString() + "' is not null", conn);
                                 int count1 = Convert.ToInt32(cd.ExecuteScalar().ToString());
                                 if (count1 > 0)
                                 {
                                     DbCommand tempcmd = BaseDb.CreatetmpCommand();
                                     if (strtdt.ToString() != "")
                                     {
                                         tempcmd.CommandText = "insert into dataparameter (datatypeid,parameterid,parameterdesc,datafromdt,type,Querytype) values('Sediment','" + dtsediment.Rows[j]["parameterid"].ToString() + "','" + dtsediment.Rows[j]["parameterid"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(strtdt)) + "','" + type + "','insert')";
                                     }
                                     else
                                     {
                                         tempcmd.CommandText = "insert into dataparameter (datatypeid,parameterid,parameterdesc,type,Querytype) values('Sediment','" + dtsediment.Rows[j]["parameterid"].ToString() + "','" + dtsediment.Rows[j]["parameterid"].ToString() + "','" + type + "','insert')";
                                     }

                                     BaseDb.ExecuteNonQuery(tempcmd);
                                 }
                             }
                         }
                     }

                     var qry5 = sqlqry1.Intersect(sqlqry2);
                     int cnt1 = qry5.Count();
                     DataTable dt5 = new DataTable();
                     if (cnt1 > 0)
                     {
                         dt5 = (from a in dtparameter.AsEnumerable() join ab in qry5 on a["parameterid"].ToString() equals ab.ID select a).CopyToDataTable();

                         if (dt5.Rows.Count > 0)
                         {
                             for (j = 0; j < dt5.Rows.Count; j++)
                             {

                                 DbCommand cmd5 = BaseDb.CreateStrCommand();
                                 sqlcmd1.CommandText = "select datafromdt,datauptodt from dataparameter where parameterid='" + dt5.Rows[j]["parameterid"] + "' ";
                                 DataTable dt1 = BaseDb.ExecuteSelect(sqlcmd1);
                                 DateTime DT = DateTime.Parse(dt1.Rows[0]["datafromdt"].ToString()).Date;
                                 if (DateTime.Parse(dt1.Rows[0]["datafromdt"].ToString()).Date > DateTime.Parse(strtdt.ToString()).Date)
                                 {
                                     DbCommand tmpcmd = BaseDb.CreatetmpCommand();
                                     tmpcmd.CommandText = "insert into dataparameter (parameterid,PARAMETERDESC,datafromdt,type,querytype) values('" + dt5.Rows[j]["parameterid"] + "','" + dt5.Rows[j]["parameterid"] + "','" + dateconvert.Convertdt(strtdt.ToString()) + "','" + type + "','update')";
                                     BaseDb.ExecuteNonQuery(tmpcmd);
                                 }
                             }

                         }
                     }
                 }
                 catch (Exception EX)
                 {

                     throw EX;
                 }
                 finally
                 {

                 };

                 #endregion

                 #region data for avlbldatadet and misngdatadet from sediment table

                 OleDbCommand cmd3 = new OleDbCommand("SELECT max(measurementdate)as maxdt,min(measurementdate) as mindt,realstat from sediment group by realstat", conn);
                 rdr = cmd3.ExecuteReader();

                 DataTable dtmdbavlbl = new DataTable();
                 dtmdbavlbl.Load(rdr);
                 DbCommand cmd6 = BaseDb.CreateStrCommand();
                 DateTime frmdt, todt;
                 DbCommand tmpcmd1 = BaseDb.CreatetmpCommand();
                 DataTable newsedidata = new DataTable();
                 newsedidata.Columns.Add("realstat"); //record will be added in this datatable if station does not exist in avlbldatadet for calculating missing data
                 newsedidata.Columns.Add("parameterid");
                 newsedidata.Columns.Add("from_date");
                 newsedidata.Columns.Add("to_date");
                 //newsedidata.Columns.Add("tiunit");
                 //newsedidata.Columns.Add("divider");
                 //newsedidata.Columns.Add("frequency");

                 DataTable alreadypresentsedirecord = new DataTable(); //record will be added in this datatable if station exists in avlbldatadet for calculating missing data
                 alreadypresentsedirecord.Columns.Add("realstat");
                 alreadypresentsedirecord.Columns.Add("parameterid");
                 alreadypresentsedirecord.Columns.Add("from_date");
                 alreadypresentsedirecord.Columns.Add("to_date");

                 //finding availability
                 if (dtmdbavlbl.Rows.Count > 0)
                 {
                     foreach (DataRow dr in dtmdbavlbl.Rows)
                     {

                         cmd6.CommandText = "select * from avlbldatadet where realstat='" + dr["realstat"] + "' and datatypeid='Sediment'";

                         DataTable dtsqlcmd = BaseDb.ExecuteSelect(cmd6);
                         frmdt = Convert.ToDateTime(dr["mindt"].ToString());
                         todt = Convert.ToDateTime(dr["maxdt"].ToString());

                         if (dtsqlcmd.Rows.Count > 0)
                         {

                             foreach (DataRow sqlrow in dtsqlcmd.Rows)
                             {

                                 DateTime fromdt = Convert.ToDateTime(sqlrow["from_date"].ToString());
                                 DateTime todt1 = Convert.ToDateTime(sqlrow["to_date"].ToString());

                                 DataRow row1 = alreadypresentsedirecord.NewRow();
                                 row1[0] = sqlrow["realstat"];
                                 row1[1] = sqlrow["parameterid"];
                                 if (frmdt < fromdt && todt > todt1)
                                 {
                                     row1[2] = frmdt;
                                     row1[3] = todt;
                                     tmpcmd1.CommandText = "insert into avlbldatadet(realstat,datatypeid,parameterid,from_date,to_date,querytype,type) values('" + txt.ToTitleCase(sqlrow["realstat"].ToString()) + "','Sediment','" + sqlrow["parameterid"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(frmdt)) + "','" + dateconvert.Convertdt(Convert.ToString(todt)) + "','update','" + type + "')";
                                     BaseDb.ExecuteNonQuery(tmpcmd1);
                                 }
                                 else if (frmdt < fromdt && todt < todt1)
                                 {
                                     row1[2] = frmdt;
                                     row1[3] = todt1;
                                     tmpcmd1.CommandText = "insert into avlbldatadet(realstat,datatypeid,parameterid,from_date,to_date,querytype,type) values('" + txt.ToTitleCase(sqlrow["realstat"].ToString()) + "','Sediment','" + sqlrow["parameterid"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(frmdt)) + "','" + dateconvert.Convertdt(Convert.ToString(todt1)) + "','update','" + type + "')";
                                     BaseDb.ExecuteNonQuery(tmpcmd1);
                                 }
                                 else if (frmdt > fromdt && todt > todt1)
                                 {
                                     row1[2] = fromdt;
                                     row1[3] = todt;
                                     tmpcmd1.CommandText = "insert into avlbldatadet(realstat,datatypeid,parameterid,from_date,to_date,querytype,type) values('" + txt.ToTitleCase(sqlrow["realstat"].ToString()) + "','Sediment','" + sqlrow["parameterid"].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(fromdt)) + "','" + dateconvert.Convertdt(Convert.ToString(todt)) + "','update','" + type + "')";
                                     BaseDb.ExecuteNonQuery(tmpcmd1);

                                 }
                                 else
                                 {
                                     row1[2] = fromdt;
                                     row1[3] = todt1;
                                 }

                                 alreadypresentsedirecord.Rows.Add(row1);

                             }

                         }

                         else
                         {
                             if (paraname.Count() > 0)
                             {
                                 for (int n = 0; n < paraname.Count(); n++)
                                 {
                                     tmpcmd1.CommandText = "insert into avlbldatadet(datatypeid,realstat,parameterid,from_date,to_date,type,querytype) values('Sediment','" + dr["realstat"] + "','" + paraname[n].ToString() + "','" + dateconvert.Convertdt(Convert.ToString(frmdt)) + "','" + dateconvert.Convertdt(Convert.ToString(todt)) + "','" + type + "','insert')";

                                     DataRow row1 = newsedidata.NewRow();
                                     row1[0] = dr["realstat"];
                                     row1[1] = paraname[n].ToString();
                                     row1[2] = frmdt;
                                     row1[3] = todt;

                                     newsedidata.Rows.Add(row1);
                                     BaseDb.ExecuteNonQuery(tmpcmd1);
                                 }
                             }
                         }
                     }

                 }


                 if (newsedidata.Rows.Count > 0)
                 {

                     foreach (DataRow newdtrow in newsedidata.Rows)
                     {
                         //string q1="select yr from (SELECT year(MeasurementDate) as yr FROM sediment GROUP BY year(MeasurementDate)  ) where yr not in (select year(measurementdate) from sediment where realstat='"+newdtrow["realstat"]+"') group by yr having yr >  (select year(min(Measurementdate)) from sediment where realstat='"+newdtrow["realstat"]+"') ";
                         string q1 = "select year(measurementdate) as yr from sediment where realstat='" + newdtrow["realstat"] + "' group by year(measurementdate) ";
                         OleDbCommand seriescmd = new OleDbCommand(q1, conn);

                         rdr = seriescmd.ExecuteReader();

                         DataTable dtseries = new DataTable();
                         dtseries.Load(rdr);
                         int yr1 = Convert.ToDateTime(newdtrow["from_date"].ToString()).Year;
                         int yr2 = Convert.ToDateTime(newdtrow["to_date"].ToString()).Year;
                         int len = yr2 - yr1;
                         IEnumerable<int> Range = Enumerable.Range(yr1, len + 1);

                         //IEnumerable<DataRow> dr = from x in Range where !(from o in dtseries select o.yr).Contains(x) 

                         //                          select x;
                         DataTable misngdt = new DataTable();
                         if (len > 0)
                         {
                             DataTable dt45 = new DataTable();
                             dt45.Columns.Add("year");
                             //IEnumerable<DataRow> dr1 = from x in Range.Cast<DataRow>() select x;
                             foreach (var value in Range)
                             {
                                 DataRow dr = dt45.NewRow();
                                 dr["year"] = value;
                                 dt45.Rows.Add(dr);
                             }

                             //DataTable dt45 = dr1.AsEnumerable().CopyToDataTable();
                             var sqlquery1 = dt45.AsEnumerable().Select(a => new { ID = a["year"].ToString() });
                             var sqlquery2 = dtseries.AsEnumerable().Select(b => new { ID = b["yr"].ToString() });
                             var q = sqlquery1.Except(sqlquery2);
                             // var rowsindt45 = dt45.AsEnumerable().Where(r => !dtseries.AsEnumerable().Any(r2 => r["year"] == r2["yr"]));
                             if (q.Count() > 0)
                             {
                                 misngdt = (from a in dt45.AsEnumerable() join ab in q on a["year"].ToString() equals ab.ID select a).CopyToDataTable();

                             }


                         }
                         else
                         {
                             misngdt.Columns.Add("year");
                             DataRow r1 = misngdt.NewRow();
                             r1["year"] = dtseries.Rows[0]["yr"];
                             misngdt.Rows.Add(r1);
                         }

                         if (misngdt.Rows.Count > 0)
                         {
                             foreach (DataRow drow in misngdt.Rows)
                             {
                                 tmpcmd1.CommandText = "insert into misngdatadet (realstat,parameterid,datatypeid,year,cancel,TYPE,QUERYTYPE) values ('" + newdtrow["realstat"] + "','" + newdtrow["parameterid"] + "','Sediment'," + drow["year"] + ",0,'" + type + "','insert')";
                                 BaseDb.ExecuteNonQuery(tmpcmd1);
                             }
                         }

                     }
                 }
                 if (alreadypresentsedirecord.Rows.Count > 0)
                 {
                     foreach (DataRow dr in alreadypresentsedirecord.Rows)
                     {
                         cmd6.CommandText = "select year,cancel from misngdatadet where realstat='" + dr["realstat"] + "' and parameterid='" + dr["parameterid"] + "' and datatypeid='Sediment'";
                         DataTable msdt = BaseDb.ExecuteSelect(cmd6);

                         string q1 = "select year(measurementdate) as yr from sediment where realstat='" + dr["realstat"] + "' group by year(measurementdate) ";
                         OleDbCommand seriescmd = new OleDbCommand(q1, conn);

                         rdr = seriescmd.ExecuteReader();

                         DataTable dtseries = new DataTable();
                         dtseries.Load(rdr);
                         int yr1 = Convert.ToDateTime(dr["from_date"].ToString()).Year;
                         int yr2 = Convert.ToDateTime(dr["to_date"].ToString()).Year;
                         int len = yr2 - yr1;
                         IEnumerable<int> Range = Enumerable.Range(yr1, len + 1);

                         //IEnumerable<DataRow> dr = from x in Range where !(from o in dtseries select o.yr).Contains(x) 

                         //                          select x;
                         DataTable misngdt = new DataTable();
                         if (len > 0)
                         {
                             DataTable dt45 = new DataTable();
                             dt45.Columns.Add("year");
                             //IEnumerable<DataRow> dr1 = from x in Range.Cast<DataRow>() select x;
                             foreach (var value in Range)
                             {
                                 DataRow drow = dt45.NewRow();
                                 drow["year"] = value;
                                 dt45.Rows.Add(drow);
                             }

                             //DataTable dt45 = dr1.AsEnumerable().CopyToDataTable();
                             var sqlquery1 = dt45.AsEnumerable().Select(a => new { ID = a["year"].ToString() });
                             var sqlquery2 = dtseries.AsEnumerable().Select(b => new { ID = b["yr"].ToString() });
                             var q = sqlquery1.Except(sqlquery2);
                             // var rowsindt45 = dt45.AsEnumerable().Where(r => !dtseries.AsEnumerable().Any(r2 => r["year"] == r2["yr"]));
                             if (q.Count() > 0)
                             {
                                 misngdt = (from a in dt45.AsEnumerable() join ab in q on a["year"].ToString() equals ab.ID select a).CopyToDataTable();

                             }

                         }
                         else
                         {
                             misngdt.Columns.Add("year");
                             DataRow r1 = misngdt.NewRow();
                             r1["year"] = dtseries.Rows[0]["yr"];
                             misngdt.Rows.Add(r1);
                         }


                         int tempcnt = msdt.Rows.Count;
                         int mdbcnt = misngdt.Rows.Count;

                         var sqlqry1 = msdt.AsEnumerable().Select(a => new { ID = a["year"].ToString() });
                         var sqlqry2 = misngdt.AsEnumerable().Select(b => new { ID = b["year"].ToString() });
                         var qry1 = sqlqry1.Except(sqlqry2);
                         var qry2 = sqlqry2.Except(sqlqry1);

                         DataTable msngdtinmdb = new DataTable();
                         DataTable msnginsql = new DataTable();
                         if (qry2.Count() > 0)
                         {
                             msngdtinmdb = (from a in misngdt.AsEnumerable() join ab in qry2 on a["year"].ToString() equals ab.ID select a).CopyToDataTable();

                         }
                         if (qry1.Count() > 0)
                         {
                             msnginsql = (from a in msdt.AsEnumerable() join ab in qry1 on a["year"].ToString() equals ab.ID select a).CopyToDataTable();

                         }

                         if (msngdtinmdb.Rows.Count > 0)
                         {
                             foreach (DataRow msngrow in msngdtinmdb.Rows)
                             {
                                 tmpcmd1.CommandText = "insert into misngdatadet (realstat,parameterid,datatypeid,year,cancel,TYPE,QUERYTYPE) values ('" + dr["realstat"] + "','" + dr["parameterid"] + "','Sediment'," + msngrow["year"] + ",0,'" + type + "','insert')";
                                 BaseDb.ExecuteNonQuery(tmpcmd1);
                             }
                         }

                         if (msnginsql.Rows.Count > 0)
                         {
                             foreach (DataRow msngsqlrow in msnginsql.Rows)
                             {
                                 int yr3 = Convert.ToInt32(msngsqlrow["year"]);
                                 if (yr3 < yr2 && yr3 > yr1)
                                 {
                                     tmpcmd1.CommandText = "insert into misngdatadet (realstat,parameterid,datatypeid,year,cancel,TYPE,QUERYTYPE) values ('" + dr["realstat"] + "','" + dr["parameterid"] + "','Sediment'," + msngsqlrow["year"] + ",1,'" + type + "','update')";
                                     BaseDb.ExecuteNonQuery(tmpcmd1);
                                 }
                             }
                         }


                     }
                 }


                 #endregion


                 #region insert datatype
                 adddatatype("Sediment", type);
                 #endregion

             }
             catch (Exception ex)
             {
                 throw (ex);
             }
             finally
             {
                 conn.Close();
                 conn.Dispose();

                 string SaveLocation = "~\\Upload\\" + filename;
             
                 if (File.Exists(SaveLocation))
                 {

                     File.Delete(SaveLocation);
                 }
             }
        }


        #endregion

    }


    /*SELECT WQL_Samples.ID, Year([WQL_Sample-collection].Date_Collection) AS Expr1, WQL_Samples.ph_fld, WQL_Samples.EC_GEN, WQL_Samples.DO, WQL_Samples.Temp, WQL_Samples.Colour_Cod, WQL_Samples.Odour_code, WQL_Samples.TDS, WQL_Samples.SS, WQL_Samples.[NH3-N], WQL_Samples.[NO2+NO3], WQL_Samples.[P-Tot], WQL_Samples.[BOD3-27], WQL_Samples.COD, WQL_Samples.Ca, WQL_Samples.Mg, WQL_Samples.Na, WQL_Samples.K, WQL_Samples.Cl, WQL_Samples.SO4, WQL_Samples.CO3, WQL_Samples.HCO3, WQL_Samples.SiO2, WQL_Samples.F, WQL_Samples.[FCol-MPN], WQL_Samples.[Chlf-a], WQL_Samples.pH_GEN, WQL_Samples.EC_GEN, WQL_Samples.[ALK-TOT], WQL_Samples.[Alk-Phen], WQL_Samples.HAR_Total, WQL_Samples.[BOD5-20], WQL_Samples.[Tcol-MPN], WQL_Samples.Al, WQL_Samples.B, WQL_Samples.Cd, WQL_Samples.Cu, WQL_Samples.Fe, WQL_Samples.Hg, WQL_Samples.Mn, WQL_Samples.Ni, WQL_Samples.[o-PO4-P], WQL_Samples.[Org-N], WQL_Samples.[DO_SAT%], WQL_Samples.PAH, WQL_Samples.Pb, WQL_Samples.PCB, WQL_Samples.SAR, WQL_Samples.Se, WQL_Samples.Secchi, WQL_Samples.Turb, WQL_Samples.Zn, WQL_Samples.Phenols, WQL_Samples.HAR_Ca, WQL_Samples.[NO3-N], WQL_Samples.[NO2-N], WQL_Samples.TS, WQL_Samples.BHC, WQL_Samples.Endos, WQL_Samples.[24D], WQL_Samples.DDT, WQL_Samples.Aldrin, WQL_Samples.Dieldrine, WQL_Samples.Dieldrin, WQL_Samples.As, WQL_Samples.CN, WQL_Samples.TOC, WQL_Samples.RSC, WQL_Samples.[Na%]
FROM [WQL_Sample-collection] INNER JOIN WQL_Samples ON [WQL_Sample-collection].ID = WQL_Samples.ID
WHERE ((([WQL_Sample-collection].[Site Code])='Kardha'))
GROUP BY WQL_Samples.ID, Year([WQL_Sample-collection].Date_Collection), WQL_Samples.ph_fld, WQL_Samples.EC_GEN, WQL_Samples.DO, WQL_Samples.Temp, WQL_Samples.Colour_Cod, WQL_Samples.Odour_code, WQL_Samples.TDS, WQL_Samples.SS, WQL_Samples.[NH3-N], WQL_Samples.[NO2+NO3], WQL_Samples.[P-Tot], WQL_Samples.[BOD3-27], WQL_Samples.COD, WQL_Samples.Ca, WQL_Samples.Mg, WQL_Samples.Na, WQL_Samples.K, WQL_Samples.Cl, WQL_Samples.SO4, WQL_Samples.CO3, WQL_Samples.HCO3, WQL_Samples.SiO2, WQL_Samples.F, WQL_Samples.[FCol-MPN], WQL_Samples.[Chlf-a], WQL_Samples.pH_GEN, WQL_Samples.EC_GEN, WQL_Samples.[ALK-TOT], WQL_Samples.[Alk-Phen], WQL_Samples.HAR_Total, WQL_Samples.[BOD5-20], WQL_Samples.[Tcol-MPN], WQL_Samples.Al, WQL_Samples.B, WQL_Samples.Cd, WQL_Samples.Cu, WQL_Samples.Fe, WQL_Samples.Hg, WQL_Samples.Mn, WQL_Samples.Ni, WQL_Samples.[o-PO4-P], WQL_Samples.[Org-N], WQL_Samples.[DO_SAT%], WQL_Samples.PAH, WQL_Samples.Pb, WQL_Samples.PCB, WQL_Samples.SAR, WQL_Samples.Se, WQL_Samples.Secchi, WQL_Samples.Turb, WQL_Samples.Zn, WQL_Samples.Phenols, WQL_Samples.HAR_Ca, WQL_Samples.[NO3-N], WQL_Samples.[NO2-N], WQL_Samples.TS, WQL_Samples.BHC, WQL_Samples.Endos, WQL_Samples.[24D], WQL_Samples.DDT, WQL_Samples.Aldrin, WQL_Samples.Dieldrine, WQL_Samples.Dieldrin, WQL_Samples.As, WQL_Samples.CN, WQL_Samples.TOC, WQL_Samples.RSC, WQL_Samples.[Na%];*/

    #region (commented code)
    ///* DataTable sqldtavlbl = new DataTable();
               // DbCommand sqlcmd = BaseDb.CreateStrCommand();
               // sqlcmd.CommandText = "select realstat,parameterid,from_dt,to_dt from avlbldatadet where type='" + type + "'";
               // sqldtavlbl = BaseDb.ExecuteSelect(sqlcmd);
             
               // var sqlquery1 = dtseries.AsEnumerable().Select(a => new { ID =a["realstat"].ToString() });
               // var sqlquery2 = sqldtavlbl.AsEnumerable().Select(b => new { ID = b["parameterid"].ToString() });
               // var q = sqlquery1.Except(sqlquery2);*/
               // int I = 0;
               //// dt = (from a in dtseries.AsEnumerable() join ab in q on a["parameter"].ToString() equals ab.ID select a).CopyToDataTable();

               // while (I < dtseries.Rows.Count)
               // {
               //     MetaDataEntity objm1 = new MetaDataEntity();
               //     objm1.Getstation = dtseries.Rows[I]["REALSTAT"].ToString();
               //     objm1.getparameter = dtseries.Rows[I]["DATATYPE"].ToString();
               //     if ((dtseries.Rows[I]["STARTTIME"].ToString()) != "")
               //     {
               //         objm1.Getfromdt = DateTime.Parse(dtseries.Rows[I]["STARTTIME"].ToString()).Date;

               //     }
               //     else
               //     {

               //         SqlDateTime date1 = SqlDateTime.Null;
               //         objm1.Getfromdt = (DateTime)date1;
               //     }


               //     if ((dtseries.Rows[I]["ENDTIME"].ToString()) != "")
               //     {
               //         objm1.Gettodt = DateTime.Parse(dtseries.Rows[I]["ENDTIME"].ToString()).Date;
               //     }
               //     else
               //     {
               //         SqlDateTime date1 = SqlDateTime.Null;
               //         objm1.Gettodt = (DateTime)date1;
               //         objm1.Getuserid = objm.Getuserid.ToString();
               //         if ((dtseries.Rows[I]["TableName"].ToString()) != "")
               //         {
               //             int chk = DUPLICATEBL.chkduplicatedata(objm1,type);
               //             OleDbCommand msngcmd = new OleDbCommand("select yr from (select year(measdate) as yr from " + (dtseries.Rows[I]["TableName"].ToString()) + " group by year(measdate)) where yr not in ( select year(measdate) from " + (dtseries.Rows[I]["TableName"].ToString()) + " where measvalue<>-999.99 group by year(measdate))", conn);

               //             DbDataReader dr = msngcmd.ExecuteReader();
               //             DataTable msngdt = new DataTable();
               //             msngdt.Load(dr);
               //             if (chk == 0)
               //             {
               //                 addavlbldata(objm1);
               //                 msngcmd = new OleDbCommand("Select min(year(MeasDate)) from " + (dtseries.Rows[I]["TableName"].ToString()), conn);
               //                 string mindt = msngcmd.ExecuteScalar().ToString();
               //                 msngcmd = new OleDbCommand("Select max(year(MeasDate)) from " + (dtseries.Rows[I]["TableName"].ToString()), conn);
               //                 string maxdt = msngcmd.ExecuteScalar().ToString();

               //                 //msngdt=findmisngdata((dtseries.Rows[I]["TableName"].ToString()), objm1, mindt, maxdt,conn);
               //                 int i;
               //                 // conn.Close();
               //                 //   conn.Open();
               //                 for (i = 0; i < msngdt.Rows.Count; i++)
               //                 {

               //                     {
               //                         DbCommand tmpcmd = BaseDb.CreatetmpCommand();
                                    
               //                         int YEAR1 = Convert.ToInt32(msngdt.Rows[i]["yr"]);
               //                         OleDbCommand msngcmd1 = new OleDbCommand("Select min(MeasDate) from " + (dtseries.Rows[I]["TableName"].ToString()) + " WHERE year(measdate)=" + YEAR1, conn);
               //                         DateTime mindate = Convert.ToDateTime(msngcmd1.ExecuteScalar().ToString());
               //                         msngcmd = new OleDbCommand("Select max(MeasDate) from " + (dtseries.Rows[I]["TableName"].ToString()) + " where year(measdate)=" + YEAR1, conn);
               //                         DateTime maxdate = Convert.ToDateTime(msngcmd.ExecuteScalar().ToString());
               //                         DateTime CREATEDDT = Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()));
               //                         tmpcmd.CommandText = "insert into MISNGDATADET(realstat,parameterid,year,from_date,to_date,cancel,TYPE,QUERYTYPE) values('" + objm1.Getstation.ToString() + "','" + objm1.getparameter.ToString() + "' ," + YEAR1 + ",'" + mindate + "','" + maxdate + "',0,'" + type + "','insert')";
               //                         BaseDb.ExecuteNonQuery(tmpcmd);
               //                     }

               //                 }

               //             }
               //             else
               //             {
               //                 DbCommand cmd = BaseDb.CreateStrCommand();
               //                 cmd.CommandText = "select from_date,to_date from avlbldatadet where parameterid='" + objm1.getparameter + "' and realstat='" + objm1.Getstation + "' and type='"+type+"'";
               //                 DataTable dt1 = BaseDb.ExecuteSelect(cmd);
               //                 DateTime DT = DateTime.Parse(dt1.Rows[0]["from_date"].ToString()).Date;

               //                 if (DateTime.Parse(dt1.Rows[0]["from_date"].ToString()).Date > objm1.Getfromdt)
               //                 {
               //                     DbCommand cmd1= BaseDb.CreatetmpCommand();
               //                     cmd1.CommandText="insert into avlbldatadet (realstat,parameterid,from_date,type,querytype) values(@realstat,@parameterid,@from_date,'"+type+"','update')";

               //                     //cmd1.CommandText = "update avlbldatadet set from_date=@from_date ,updated_dt=@updated_dt,updated_by=@updated_by where parameterid =@parameterid and realstat=@realstat";
               //                     DbParameter dbparam = cmd1.CreateParameter();
               //                     dbparam.ParameterName = "@from_date";
               //                     dbparam.Value = objm1.Getfromdt;
               //                     dbparam.DbType = DbType.DateTime;
               //                     dbparam.Direction = ParameterDirection.Input;
               //                     cmd1.Parameters.Add(dbparam);

               //                     dbparam = cmd1.CreateParameter();
               //                     dbparam.ParameterName = "@parameterid";
               //                     dbparam.Value = objm1.getparameter.ToString();
               //                     dbparam.DbType = DbType.String;
               //                     dbparam.Direction = ParameterDirection.Input;
               //                     cmd1.Parameters.Add(dbparam);

               //                     dbparam = cmd1.CreateParameter();
               //                     dbparam.ParameterName = "@realstat";
               //                     dbparam.Value = objm1.Getstation.ToString();
               //                     dbparam.DbType = DbType.String;
               //                     dbparam.Direction = ParameterDirection.Input;
               //                     cmd1.Parameters.Add(dbparam);

               //                     BaseDb.ExecuteNonQuery(cmd1);
               //                 }
               //                 if (DateTime.Parse(dt1.Rows[0]["to_date"].ToString()).Date < objm1.Gettodt)
               //                 {
               //                     DbCommand cmd1 = BaseDb.CreatetmpCommand();
               //                     cmd1.CommandText = "insert into avlbldatadet (realstat,parameterid,to_date,type,querytype) values(@realstat,@parameterid,@to_date,'" + type + "','update')";
               //                     //cmd1.CommandText = "update avlbldatadet set to_date=@to_date,updated_dt=@updated_dt,updated_by=@updated_by where parameterid =@parameterid and realstat=@realstat";

               //                     DbParameter dbparam = cmd1.CreateParameter();
               //                     dbparam.ParameterName = "@to_date";
               //                     dbparam.Value = objm1.Gettodt;
               //                     dbparam.DbType = DbType.DateTime;
               //                     dbparam.Direction = ParameterDirection.Input;
               //                     cmd1.Parameters.Add(dbparam);

               //                     dbparam = cmd1.CreateParameter();
               //                     dbparam.ParameterName = "@parameterid";
               //                     dbparam.Value = objm1.getparameter.ToString();
               //                     dbparam.DbType = DbType.String;
               //                     dbparam.Direction = ParameterDirection.Input;
               //                     cmd1.Parameters.Add(dbparam);

               //                     dbparam = cmd1.CreateParameter();
               //                     dbparam.ParameterName = "@realstat";
               //                     dbparam.Value = objm1.Getstation.ToString();
               //                     dbparam.DbType = DbType.String;
               //                     dbparam.Direction = ParameterDirection.Input;
               //                     cmd1.Parameters.Add(dbparam);

                                
               //                     BaseDb.ExecuteNonQuery(cmd1);
               //                 }
               //             }


                          
               //             int cnt;
               //             string cond;
               //             cond = "";
               //             for (cnt = 0; cnt < msngdt.Rows.Count; cnt++)
               //             {
               //                 DbCommand cmd = BaseDb.CreateStrCommand();
               //                 DbCommand tmpcmd = BaseDb.CreatetmpCommand();
               //                 cmd.CommandText = "select count(*) from MISNGDATADET where realstat='" + objm1.Getstation + "' and parameterid='" + objm1.getparameter + "' and year=" + Convert.ToInt32(msngdt.Rows[cnt]["yr"].ToString())+" and type='"+type+"'";
               //                 int val = Convert.ToInt32(BaseDb.ExecuteScalar(cmd).ToString());
               //                 if (val == 0)
               //                 {
               //                     tmpcmd.CommandText = "insert into MISNGDATADET(realstat,parameterid,year,cancel,type,querytype) values ('" + objm1.Getstation + "','" + objm1.getparameter + "'," + Convert.ToInt32(msngdt.Rows[cnt]["yr"].ToString()) + ",0,'"+type+"','insert')";
               //                   //  cmd.CommandText = "insert into MISNGDATADET(realstat,parameterid,year1,cancel) values ('" + objm1.Getstation + "','" + objm1.getparameter + "'," + Convert.ToInt32(msngdt.Rows[cnt]["yr"].ToString()) + ",0)";
               //                     BaseDb.ExecuteNonQuery(tmpcmd);

               //                 }
               //                 else
               //                 {
               //                     cmd.CommandText = "select cancel from MISNGDATADET where realstat='" + objm1.Getstation + "' and parameterid='" + objm1.getparameter + "' and year=" + Convert.ToInt32(msngdt.Rows[cnt]["yr"].ToString())+" and type='"+type+"'";
               //                     int status = Convert.ToInt32(BaseDb.ExecuteScalar(cmd).ToString());
               //                     if (status == 1)
               //                     {
               //                         tmpcmd.CommandText = "insert into misngdatadet(realstat,parameterid,year,cancel,type,querytype) values ('" + objm1.Getstation + "','" + objm1.getparameter + "'," + Convert.ToInt32(msngdt.Rows[cnt]["yr"].ToString()) + ",'" + type + "','update')";
               //                         //cmd.CommandText = "update misngdatadet set cancel=0 where realstat='" + objm1.Getstation + "' and parameterid='" + objm1.getparameter + "' and year1=" + Convert.ToInt32(msngdt.Rows[cnt]["yr"].ToString());
               //                         BaseDb.ExecuteNonQuery(tmpcmd);
               //                     }
               //                     else if (status == 1)
               //                     {
               //                     }
               //                     else
               //                     {
               //                     }
               //                 }
               //                 string i;
               //                 if (cnt != (msngdt.Rows.Count) - 1)
               //                 {
               //                     i = (msngdt.Rows[cnt]["yr"].ToString());
               //                     cond = cond + i + ",";
               //                 }
               //                 else if (cnt == (msngdt.Rows.Count) - 1)
               //                 {
               //                     i = (msngdt.Rows[cnt]["yr"].ToString());
               //                     cond = cond + i;
               //                 }

               //            }

               //             // select yr from (select year(measdate) as yr from "+(dtseries.Rows[I]["TableName"].ToString())+" group by year(measdate)) where yr not in ( select year(measdate) from "+(dtseries.Rows[I]["TableName"].ToString())"+ where measvalue<>-999.99 group by year(measdate))
               //             if (msngdt.Rows.Count > 0)
               //             {
               //                 DbCommand cmd = BaseDb.CreatetmpCommand();
                             
               //                 cmd.CommandText = "update misngdatadet set cancel=1 where year1 in (select year1 from ((select year1 from Misngdatadet where year1 not in(2006,2007) and realstat='" + objm1.Getstation + "' and parameterid='" + objm1.getparameter + "')) where year1 in(" + objm1.Getfromdt.Year + "," + objm1.Gettodt.Year + " ))";
               //                 BaseDb.ExecuteNonQuery(cmd);
               //             }
               //         }
               //     }
               //     I++;

               // }

               // updateavlbldatadet("avlbldatadet");
               // updateavlbldatadet("misngdatadet");
               // rdr.Close();
            //}


        //while (i < dt.Rows.Count)
        //{
        //    MetaDataEntity objm2 = new MetaDataEntity();
        //    objm2.Getrealstationname = dt.Rows[i]["REALSTAT"].ToString();
        //    objm2.Getstation = dt.Rows[i]["STATNAME"].ToString();
        //    objm2.Getcountry = dt.Rows[i]["COUNTRY"].ToString();
        //    objm2.Getprovince = dt.Rows[i]["PROVINCE"].ToString();
        //    objm2.Getsubdistrict = dt.Rows[i]["SUBDISTRICT"].ToString();
        //    objm2.Getbasin = dt.Rows[i]["BASIN"].ToString();
        //    objm2.Getcatchname = dt.Rows[i]["CATCH"].ToString();
        //    objm2.Getrivername = dt.Rows[i]["RIVERNAME"].ToString();
        //    objm2.Gettributary = dt.Rows[i]["TRIBUTARY"].ToString();
        //    objm2.Getlatitude = dt.Rows[i]["LATITUDE"].ToString();
        //    objm2.Getlongitude = dt.Rows[i]["LONGITUDE"].ToString();
        //    objm2.Getaltitude = dt.Rows[i]["ALTITUDE"].ToString();
        //    objm2.Getcatarea = dt.Rows[i]["CATAREA"].ToString();
        //    objm2.Gettoposheet = dt.Rows[i]["TOPOSHEET"].ToString();
        //    objm2.Getagency = dt.Rows[i]["AGENCY"].ToString();
        //    objm2.Getregoffice = dt.Rows[i]["REGOFFICE"].ToString();
        //    objm2.Getciroffice = dt.Rows[i]["CIRCOFFICE"].ToString();
        //    objm2.Getdivoffice = dt.Rows[i]["DIVOFFICE"].ToString();
        //    objm2.Getsubdivoffice = dt.Rows[i]["SUBDIVOFFICE"].ToString();
        //    objm2.Getsectoffice = dt.Rows[i]["SECTOFFICE"].ToString();
        //    objm2.Getremarks = dt.Rows[i]["REMARKS"].ToString();
        //    objm2.Getowneragency = dt.Rows[i]["OWNERAGENCY"].ToString();

        //    if ((dt.Rows[i]["STARTDATE"].ToString()) != "")
        //    {
        //        objm2.Getstartdt = DateTime.Parse(dt.Rows[i]["STARTDATE"].ToString()).Date;

        //    }
        //    else
        //    {

        //        SqlDateTime dtate1 = SqlDateTime.Null;
        //        objm2.Getstartdt = (DateTime)dtate1;
        //    }

        //    if ((dt.Rows[i]["ENDDATE"].ToString()) != "")
        //    {
        //        objm2.Getenddt = DateTime.Parse(dt.Rows[i]["ENDDATE"].ToString()).Date;

        //    }
        //    else
        //    {

        //        SqlDateTime dtate1 = SqlDateTime.Null;
        //        objm2.Getenddt = (DateTime)dtate1;
        //    }

        //    objm2.Getuserid = objm.Getuserid.ToString();
        //    objm2.Getlocalxcoord = Convert.ToInt64(dt.Rows[i]["LOCALXCOORD"].ToString());
        //    objm2.Getlocalycoord = Convert.ToInt64(dt.Rows[i]["LOCALYCOORD"].ToString());

        //    int chk = DUPLICATEBL.chkduplicatelocation(objm2);
        //    if (chk == 0)
        //    {
        //        addlocation(objm2);
        //    }
        //    i++;
        //}
          
                //int I = 0;
                //while (I < dtseries.Rows.Count)
                //{
                //    MetaDataEntity objm3 = new MetaDataEntity();
                //    objm3.getparameter = dtseries.Rows[I]["DATATYPE"].ToString();
                //    if ((dtseries.Rows[I]["STARTTIME"].ToString()) != "")
                //    {
                //        objm3.Getfromdt = DateTime.Parse(dtseries.Rows[I]["STARTTIME"].ToString()).Date;

                //    }
                //    else
                //    {

                //        SqlDateTime date1 = SqlDateTime.Null;
                //        objm3.Getfromdt = date1;
                //    }
                //    objm3.Getuserid = objm.Getuserid.ToString();

                 
                //    if ((dtseries.Rows[I]["TableName"].ToString()) != "")
                //    {
                //        int chk = DUPLICATEBL.chkduplicateparameter(objm3);
                //        if (chk == 0)
                //        {
                //            addparameter(objm3);
                //        }
                //        else
                //        {
                //            DbCommand cmd = BaseDb.CreateStrCommand();
                //            cmd.CommandText = "select datafromdt,datauptodt from dataparameter where parameterid='" + objm3.getparameter + "'";
                //            DataTable dt1 = BaseDb.ExecuteSelect(cmd);
                //            DateTime DT = DateTime.Parse(dt1.Rows[0]["datafromdt"].ToString()).Date;
                //            if (DateTime.Parse(dt1.Rows[0]["datafromdt"].ToString()).Date > objm3.Getfromdt)
                //            {
                //                DbCommand cmd1 = BaseDb.CreateStrCommand();
                //                cmd1.CommandText = "update dataparameter set datafromdt='" + objm3.Getfromdt + "' where parameterid ='" + objm3.getparameter + "'";
                //                BaseDb.ExecuteNonQuery(cmd1);
                //            }

                //        }
                //    }
                //    I++;

                
               // rdr.Close();
                //DbCommand dbcomm = BaseDb.CreatetmpCommand();
                //dbcomm.CommandText = "SELECT COUNT(*) FROM DATAPARAMETER WHERE PARAMETERID LIKE 'M%' ";
                //string MCOUNT = BaseDb.ExecuteScalar(dbcomm);

                //dbcomm.CommandText = "SELECT COUNT(*) FROM DATAPARAMETER WHERE PARAMETERID LIKE 'H%' ";
                //string HCOUNT = BaseDb.ExecuteScalar(dbcomm);
                //dbcomm.CommandText = "SELECT COUNT(*) FROM DATAPARAMETER WHERE PARAMETERID LIKE 'Q%' ";
                //string WCOUNT = BaseDb.ExecuteScalar(dbcomm);
                //dbcomm.CommandText = "SELECT COUNT(*) FROM DATAPARAMETER WHERE PARAMETERID LIKE 'S%' ";
                //string SCOUNT = BaseDb.ExecuteScalar(dbcomm);

                //if (Convert.ToInt32(MCOUNT.ToString()) != 0)
                //{
                //    COND = " LIKE 'M%'";
                //    string DTTYPE = "METEOROLOGY";
                //    adddatatype(COND, DTTYPE, objm);
                //}
                //if (Convert.ToInt32(HCOUNT.ToString()) != 0)
                //{
                //    COND = " LIKE 'H%' ";
                //    string DTTYPE = "HYDROLOGY";
                //    adddatatype(COND, DTTYPE, objm);
                //}
                //if (Convert.ToInt32(WCOUNT.ToString()) != 0)
                //{
                //    COND = " LIKE 'Q%' ";
                //    string DTTYPE = "WATERQUALITY";
                //    adddatatype(COND, DTTYPE, objm);
                //}
                //if (Convert.ToInt32(SCOUNT.ToString()) != 0)
                //{
                //    COND = " LIKE 'S%' ";
                //    string DTTYPE = "SEDIMENT";
                //    adddatatype(COND, DTTYPE, objm);
                //}

                //updatedataparameter();
    //        #region inserting availability data
//           public static bool addavlbldata(MetaDataEntity objm)
//           {

//               DbCommand dbcom = BaseDb.CreateStrCommand();

//               dbcom.CommandText = "insert into avlbldatadet(realstat,parameterid,from_date,to_date,created_dt,created_by) values('" + objm.Getstation + "','" + objm.getparameter + "','" + objm.Getfromdt + "','" + objm.Gettodt + "','" + Convert.ToDateTime(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString())) + "','" + objm.Getuserid + "')";
//               #region commented code
//               /*    DbParameter dbparam = dbcom.CreateParameter();
//           dbparam.ParameterName = "@created_date";
//            dbparam.Value = DateTime.Today.Date;
//            dbparam.DbType = DbType.DateTime;
//            dbparam.Direction = ParameterDirection.Input;
//            dbcom.Parameters.Add(dbparam);

//           dbparam = dbcom.CreateParameter();
//                   dbparam.ParameterName = "@from_date";
//                   dbparam.Value =  objm.Getfromdt.Date ;
//                   dbparam.DbType = DbType.DateTime;
//                   dbparam.Direction = ParameterDirection.Input;
//                   dbcom.Parameters.Add(dbparam);

//                   dbparam = dbcom.CreateParameter();
//                   dbparam.ParameterName = "@to_date";
//                   dbparam.Value = objm.Gettodt.Date;
//                   dbparam.DbType = DbType.DateTime;
//                   dbparam.Direction = ParameterDirection.Input;
//                   dbcom.Parameters.Add(dbparam);

//                   dbparam = dbcom.CreateParameter();
//                   dbparam.ParameterName = "@parameterid";
//                   dbparam.Value = objm.getparameter.ToString();
//                   dbparam.DbType = DbType.String;
//                   dbparam.Direction = ParameterDirection.Input;
//                   dbcom.Parameters.Add(dbparam);
//                   //BaseDb.ExecuteNonQuery(dbcomm);

//                    dbparam=dbcom.CreateParameter();
//                    dbparam.ParameterName="@realstat";
//                    dbparam.Value=objm.Getstation.ToString();
//                    dbparam.DbType=DbType.String;
//                    dbparam.Direction = ParameterDirection.Input;
//                    dbcom.Parameters.Add(dbparam);
            
//            */
//               #endregion
//               int result = -1;
//               try
//               {
//                   result = BaseDb.ExecuteNonQuery(dbcom);
//               }
//               catch (Exception exc)
//               {

//                   throw exc;
//               }
//               finally
//               {

//               }
//               return (result != -1);
//           }
//#endregion

        //#region  (CODE FOR ADDING DATA TO LOCATION TABLE)
        ////public static bool addlocation(MetaDataEntity objm)
        ////{

        ////    DbCommand dbComm = BaseDb.CreateStrCommand();
        ////dbComm.CommandText = "insert into Location( REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,ENDDATE,LOCALXCOORD,LOCALYCOORD,CREATED_DT,CREATED_BY)values "+ 
        ////    "('"+objm.Getrealstationname +"','"+objm.Getstation +"','"+objm.Getcountry+"','"+objm.Getprovince+"','"+objm.Getsubdistrict+"','"+objm.Getbasin+"','"+objm.Getrivername+"','"+objm.Gettributary+"','"+objm.Getcatchname +"','"+objm.Getlatitude +"','"+objm.Getlongitude +"','"+objm.Getaltitude +"','"+objm.Getcatarea+"','"+objm.Gettoposheet+"','"+objm.Getagency+"','"+objm.Getregoffice +"','"+objm.Getciroffice +"','"+objm.Getdivoffice +"','"+objm.Getsubdivoffice +"','"+objm.Getsectoffice +"','"+objm.Getremarks +"','"+objm.Getowneragency+"','"+objm.Getstartdt+"','"+objm.Getenddt +"',"+objm.Getlocalxcoord +","+objm.Getlocalycoord +",'"+DateTime.Today.Date+"','"+objm.Getuserid+"')";//name of stored procedure
        //////   dbComm.CommandText = "insert into Location(realstat)values(@REALSTAT)";//name of stored procedure
        ////#region commented code
        /////* DbParameter dbparam = dbComm.CreateParameter();

        ////    dbparam.ParameterName = "@CREATED_DT";
        ////    dbparam.Value = DateTime.Today.Date;
        ////    dbparam.DbType = DbType.DateTime;
        ////    dbparam.Direction = ParameterDirection.Input;
        ////    dbComm.Parameters.Add(dbparam);

        ////  dbparam = dbComm.CreateParameter();
        ////    dbparam.ParameterName = "@REALSTAT";
        ////    dbparam.Size = objm.Getrealstationname.Length;
        ////    dbparam.Value = objm.Getrealstationname.Substring(0,3);
            
        ////    dbparam.DbType = DbType.String;
        ////    dbparam.Direction = ParameterDirection.Input;
        ////    dbComm.Parameters.Add(dbparam);

        ////    dbparam = dbComm.CreateParameter();
        ////    dbparam.ParameterName = "@STATNAME";
        ////    dbparam.Value = objm.Getstation ;
        ////    dbparam.DbType = DbType.String ;

                 
        ////    dbparam.Direction = ParameterDirection.Input;
        ////    dbComm.Parameters.Add(dbparam);*/

        /////*            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@COUNTRY";
        ////            dbparam.Value = objm.Getcountry ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@PROVINCE";
        ////            dbparam.Value = objm.Getprovince  ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@SUBDISTRICT";
        ////            dbparam.Value = objm.Getsubdistrict ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@BASIN";
        ////            dbparam.Value = objm.Getbasin ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@RIVERNAME";
        ////            dbparam.Value = objm.Getrivername ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@TRIBUTARY";
        ////            dbparam.Value = objm.Gettributary ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@CATCH";
        ////            dbparam.Value = objm.Getcatchname ;
        ////            dbparam.DbType = DbType.String ;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@LATITUDE";
        ////            dbparam.Value = objm.Getlatitude ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@LONGITUDE";
        ////            dbparam.Value = objm.Getlongitude ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@ALTITUDE";
        ////            dbparam.Value = objm.Getaltitude ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@CATAREA";
        ////            dbparam.Value = objm.Getcatarea ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);
       

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@TOPOSHEET";
        ////            dbparam.Value = objm.Gettoposheet ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@AGENCY";
        ////            dbparam.Value = objm.Getagency ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@REGOFFICE";
        ////            dbparam.Value = objm.Getregoffice ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@CIRCOFFICE";
        ////            dbparam.Value = objm.Getciroffice ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@DIVOFFICE";
        ////            dbparam.Value = objm.Getdivoffice ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////              dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@SUBDIVOFFICE";
        ////            dbparam.Value = objm.Getsubdivoffice  ;
        ////            dbparam.DbType = DbType.String ;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@SECTOFFICE";
        ////            dbparam.Value = objm.Getsectoffice   ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@REMARKS";
        ////            dbparam.Value = objm.Getremarks  ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@OWNERAGENCY";
        ////            dbparam.Value = objm.Getowneragency  ;
        ////            dbparam.DbType = DbType.String;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@STARTDATE";
        ////            dbparam.Value = objm.Getstartdt.Date;
        ////            dbparam.DbType = DbType.DateTime;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@ENDDATE";
        ////            dbparam.Value = objm.Getenddt.Date;
        ////            dbparam.DbType = DbType.DateTime;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

         
        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@LOCALXCOORD";
        ////            dbparam.Value = objm.Getlocalxcoord  ;
        ////            dbparam.DbType = DbType.Int64 ;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);

        ////            dbparam = dbComm.CreateParameter();
        ////            dbparam.ParameterName = "@LOCALYCOORD";
        ////            dbparam.Value = objm.Getlocalycoord  ;
        ////            dbparam.DbType = DbType.Int64 ;
        ////            dbparam.Direction = ParameterDirection.Input;
        ////            dbComm.Parameters.Add(dbparam);
        ////            */
        ////#endregion
        ////int result = -1;
        ////    try
        ////    {
        ////        result = BaseDb.ExecuteNonQuery(dbComm);
        ////    }
        ////    catch (Exception exc)
        ////    {

        ////        throw exc;
        ////    }
        ////    finally
        ////    {
        ////        dbComm.Dispose();
        ////    }
        ////    return (result != -1);

        ////}
        //#endregion

        //#region Adding data to Dataparameter
        //public static bool addparameter(MetaDataEntity objm)
        //{

        //    DbCommand dbCommand = BaseDb.CreateStrCommand();
        //    dbCommand.CommandText = "insert into dataparameter( PARAMETERID,DATAFROMDT,CREATED_DT,CREATED_BY)values('"+objm.getparameter+"','"+objm.Getfromdt+"','"+DateTime.Today.Date+"','"+objm.Getuserid+"')";//name of stored procedure
           
        //    #region commented region
        //    /*            DbParameter dbparam1 = dbCommand.CreateParameter();

        //    dbparam1.ParameterName = "@CREATED_DT";
        //    dbparam1.Value = DateTime.Today.Date;
        //    dbparam1.DbType = DbType.DateTime;
        //    dbparam1.Direction = ParameterDirection.Input;
        //    dbCommand.Parameters.Add(dbparam1);

        //    //dbparam.ParameterName = "@CREATED_BY";
        //    //dbparam.Value = userid;

        //    dbparam1 = dbCommand.CreateParameter();
        //    dbparam1.ParameterName = "@PARAMETER";
        //    dbparam1.Value = objm.getparameter;
        //    dbparam1.DbType = DbType.String;
        //    dbparam1.Direction = ParameterDirection.Input;
        //    dbCommand.Parameters.Add(dbparam1);

        //    dbparam1 = dbCommand.CreateParameter();
        //    dbparam1.ParameterName = "@DATAFROMDT";
        //    dbparam1.Value = objm.Getfromdt;
        //    dbparam1.DbType = DbType.DateTime;
        //    dbparam1.Direction = ParameterDirection.Input;
        //    dbCommand.Parameters.Add(dbparam1);

        //    //dbparam1 = dbCommand.CreateParameter();
        //    //dbparam1.ParameterName = "@DATATODT";
        //    //dbparam1.Value = objm.Gettodt;
        //    //dbparam1.DbType = DbType.DateTime;
        //    //dbparam1.Direction = ParameterDirection.Input;
        //    //dbCommand.Parameters.Add(dbparam1);

        //    */
        //    #endregion
        //    int result = -1;
        //    try
        //    {
        //        result = BaseDb.ExecuteNonQuery(dbCommand);
        //    }
        //    catch (Exception exc)
        //    {

        //        throw exc;
        //    }
        //    finally
        //    {

        //    }
        //    return (result != -1);
        //}
        //#endregion

        

        


        

        //#region find missing data
        //public static DataTable findmisngdata(string tblname,MetaDataEntity objm,string mindt,string maxdt,OleDbConnection conn)
        //{
        //    DataTable dt = new DataTable("msngdata");
        //    int i;
        //    ;
        //    dt.Columns.Add(new DataColumn("REALSTAT", Type.GetType("System.String")));
        //    dt.Columns.Add(new DataColumn("PARAMETERID", Type.GetType("System.String")));
        //   // dt.Columns.Add(new DataColumn("DATATYPEID", Type.GetType("System.String")));
        //    //dt.Columns.Add(new DataColumn("misng", Type.GetType("System.Int32")));
           
        //    dt.Columns.Add(new DataColumn("year1",Type.GetType("System.Int32")));
        //    dt.Columns.Add(new DataColumn("misng",Type.GetType("System.Int32")));
           
        //    for (i=Convert.ToInt32(mindt);i<= Convert.ToInt32(maxdt);i++)
        //    {
        //        DataRow r = dt.NewRow();
        //        r[0] = objm.Getstation;
        //        r[1] = objm.getparameter;
        //        r[2] = i;
                
        //        OleDbCommand cmd1 = new OleDbCommand("select count(*) from " + tblname + " where year(measdate)=" + i + " and measvalue <>-999.99", conn);
        //        int j = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
        //        if (j == 0)
        //        {
        //            r[3] = 1;
        //        }
        //        else
        //        {
        //            if (j > 0)
        //                r[3] = 0;
        //        }

        //        dt.Rows.Add(r);
        //    }

        // /*   DbCommand dbcmd = BaseDb.CreateStrCommand();
        //    dbcmd.CommandText="Insert into misngdatadet(realstat,parameterid,year,created_dt) select (*/

        //    return (dt);
           

           
        //}
        //#endregion

        //public static int uploaddata()
        //{
            
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        DbCommand dbcom = BaseDb.CreateStrCommand();

        //        String sqlconnection = ConfigurationManager.ConnectionStrings["sqlconn"].ToString();
             /*   dbcom.CommandText = "select * from location";

                dt = BaseDb.ExecuteSelect(dbcom);

                
                using (SqlBulkCopy copy = new SqlBulkCopy(sqlconnection))
                {
                    copy.DestinationTableName = "location";
                    copy.WriteToServer(dt);
                }

                dbcom.CommandText = "select * from Dataparameter";
                DataTable dt9 = new DataTable();
                dt9 = BaseDb.ExecuteSelect(dbcom);
                using (SqlBulkCopy copy = new SqlBulkCopy(sqlconnection))
                {
                    copy.DestinationTableName = "Dataparameter";
                    copy.WriteToServer(dt9);
                }
                dbcom.CommandText = "select * from Datatype";
                DataTable dt1 = new DataTable();
                dt1= BaseDb.ExecuteSelect(dbcom);
                using (SqlBulkCopy copy = new SqlBulkCopy(sqlconnection))
                {
                    copy.DestinationTableName = "Datatype";
                    copy.WriteToServer(dt1);
                }
                dbcom.CommandText = "select * from Avlbldatadet";
                DataTable dt2 = new DataTable();
                dt2 = BaseDb.ExecuteSelect(dbcom);
                using (SqlBulkCopy copy = new SqlBulkCopy(sqlconnection))
                {
                    copy.DestinationTableName = "Avlbldatadet";
                    copy.WriteToServer(dt2);
                }*/
        //        dbcom.CommandText = "select * from Misngdatadet";
        //        DataTable dt3 = new DataTable();
        //        dt3 = BaseDb.ExecuteSelect(dbcom);
        //        using (SqlBulkCopy copy = new SqlBulkCopy(sqlconnection))
        //        {
        //            copy.DestinationTableName = "Misngdatadet";
        //            copy.ColumnMappings.Add(0, 0);
        //            copy.ColumnMappings.Add(1, 1);
        //            copy.ColumnMappings.Add(2, 2);
        //            copy.ColumnMappings.Add(3, 3);
        //            copy.ColumnMappings.Add(4, 4);
        //            copy.ColumnMappings.Add(5, 5);
        //            copy.ColumnMappings.Add(6, 6);
        //            copy.WriteToServer(dt3);
        //        }
        //        return (1);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

//}

#endregion
    }
   





