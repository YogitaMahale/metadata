using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using HydroProject.Code;
using Microsoft.Reporting.WebForms;


public partial class admin_rptavlbl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();

        //DbCommand cmd = BaseDb.CreateSqlCommand();
        //cmd.CommandText = "select realstat,datatypeid,parameterid,year from MISNGDATADET order by REALSTAT ";
        //dt = BaseDb.ExecuteSelect(cmd);
        dt = reportdata.getavailability();
        DataSet ds = new DataSet();
         ds.Clear();
        ds.Tables.Add(dt);
        ReportDataSource datasource = new ReportDataSource("avlbldataset_dtavlbl", ds.Tables[0]);
        ReportViewer1.Visible = true;

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.Refresh();


    }
}
