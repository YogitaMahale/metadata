using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HydroProject.Code;

public partial class admin_detaildatadisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label obj_lbl = Master.FindControl("lbltitle") as Label;
        obj_lbl.Text = "New added data";
        string value=(string)Session["value"];
        if (value == "location")
        {
            parameterrow.Visible = false;
            DataTable dt = DUPLICATEBL.getlocationdet();
       
            gridview1.DataSource = dt;
            gridview1.DataBind();
            

        }
        else if (value == "parameter")
        {
            locationrow.Visible = false;
            DataTable dt = DUPLICATEBL.getparameterdet();
            gridview2.DataSource = dt;
            gridview2.DataBind();
        }

    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        string calfrm = (string)Session["callfrom"];
        if (calfrm == "verifydata")
        {
            Session["value"] = "";
            Session["callfrom"] = "";
            Response.Redirect("verifydata.aspx");
        }
        else
        {
            Session["value"] = "";
            Session["callfrom"] = "";
            Response.Redirect("displaydata.aspx");
        }
        
       
    }
    protected void gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gd = (GridView)sender;
        gd.PageIndex = e.NewPageIndex;
        gd.DataBind();
    }
    protected void gridview2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gd = (GridView)sender;
        gd.PageIndex = e.NewPageIndex;
        gd.DataBind();
    }
    protected void linklogut_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminlogin.aspx?logout=1&Id='" + Session.SessionID + "'");
    }
}
