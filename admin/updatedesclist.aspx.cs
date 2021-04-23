using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HydroProject.Code;
using System.Data.SqlClient;

public partial class Admin_SectorDetails : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    string userid, cond, msg;
    int savebtn, backbtn, searchbtn;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label obj_lbl = Master.FindControl("lbltitle") as Label;
        obj_lbl.Text = "Parameter list";
        if (!IsPostBack)
        {
            if (Session["uname"] != null || Session["password"] != null)
            {
                //if (Session["griddata"] != null)  //commented by DSB 0n 28.6.2012
                //{
                //    dt = (DataTable)Session["griddata"];
                //    gvparameterdesc.DataSource = dt;
                //    gvparameterdesc.DataBind();
                    

                //}
                //else
                //{
                    dt = updatedatadesc.getgriddata();
                    if (dt.Rows.Count == 0)   //added by DSB 0n 28.6.2012
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "No data to be updated";
                    }
                    else
                    {
                        gvparameterdesc.DataSource = dt;
                        gvparameterdesc.DataBind();
                    }
                //gvparameterdesc.DataSource = dt;  //commented by DSB 0n 28.6.2012
                    //gvparameterdesc.DataBind();
                //}

            }
            else if (Session["uname"] == null || Session["password"] == null)
            {
                Response.Redirect("adminlogin.aspx");
            }
        }
        else
        {
            //if (Session["griddata"] != null)   //commented by DSB 0n 28.6.2012
            //{
            //    dt = (DataTable)Session["griddata"];
            //    gvparameterdesc.DataSource = dt;
            //    gvparameterdesc.DataBind();
            //}
            //else
            //{
                dt = updatedatadesc.getgriddata();
                if (dt.Rows.Count == 0)   //added by DSB 0n 28.6.2012
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "No data to be updated";
                }
                else
                {
                    gvparameterdesc.DataSource = dt;
                    gvparameterdesc.DataBind();
                }
            //gvparameterdesc.DataSource = dt;  //commented by DSB 0n 28.6.2012
            //gvparameterdesc.DataBind();
            //}
        }
      
    }
    private void jscall1(string alert)
    {
        string Script = @"alert('" + alert + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall1", Script, true);
    }

    //protected void btnaddnew_Click(object sender, EventArgs e)
    //{
    //    Session["srnosectordet"] = 0;
    //    Session["searchbtn"] = 0;
    //    Response.Redirect("SectorDetails_Entry.aspx");
    //}

    //protected void btnsearch_Click(object sender, EventArgs e)
    //{
    //    Session["searchbtn"] = 1;

    //    if (txtsearch.Text == "")
    //    {
    //        dt = sector_detailsbsc.ShowAllSectorDetails();
    //    }

    //    else if (ddsearchfield.SelectedIndex == 0)
    //    {            
    //        cond = " sectorid like '%" + txtsearch.Text + "%'";
    //        dt = sector_detailsbsc.ShowSectorDetails(cond);
    //        ViewState["sectordetails"] = dt;
    //    }
    //    else if (ddsearchfield.SelectedIndex == 1)
    //    {            
    //        cond = " sectordesc like '%" + txtsearch.Text + "%'";
    //        dt = sector_detailsbsc.ShowSectorDetails(cond);
    //        ViewState["sectordetails"] = dt;
    //    }
    //    else if (ddsearchfield.SelectedIndex == 2)
    //    {
    //        //cond = " sectordesc like '%" + txtsearch.Text + "%'";            
    //        dt = sector_detailsbsc.ShowSectorDetailsfrom_dt(DateTime.Parse(txtsearch.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-AU")));
    //        ViewState["sectordetails"] = dt;
    //    }
    //    else if (ddsearchfield.SelectedIndex == 3)
    //    {
    //        //cond = " sectordesc like '%" + txtsearch.Text + "%'";            
    //        dt = sector_detailsbsc.ShowSectorDetailsto_dt(DateTime.Parse(txtsearch.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-AU")));
    //        ViewState["sectordetails"] = dt;
    //    }

    //    if (dt.Rows.Count == 0)
    //    {
    //        lblmessage.Visible = true;
    //        lblmessage.Text = "No records found";            
    //        gvsectormaster.DataSource = null;
    //        gvsectormaster.DataBind();
    //        //txtsearch.Text = "";
    //        //ddsearchfield.SelectedIndex = 0;
    //        return;
    //    }
    //    else if (dt.Rows.Count > 0)
    //    {
    //        gvsectormaster.DataSource = dt;
    //        gvsectormaster.DataBind();
    //    }
    //}

    protected void linklogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminlogin.aspx?logout=1&Id='" + Session.SessionID + "'");
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Session["backbtn"] = 1;
        int cnt = updatedatadesc.getcount();
        if (cnt == 0)
        {
            Session["count"] = "0";
        }
        else
        {
            Session["count"] = "1";
        }

        Response.Redirect("displaydata.aspx");
    }
    protected void gvparameterdesc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gd = (GridView)sender;
        gd.PageIndex = e.NewPageIndex;
        gd.DataBind();
    }

    protected void btnedit_click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        Label parameterid = (Label)gvparameterdesc.Rows[index].FindControl("lblparameterid");
        Session["parameterid"] = (parameterid.Text);
        Session["griddata"] = dt;
        Response.Redirect("updatedesc.aspx");
    }
}
