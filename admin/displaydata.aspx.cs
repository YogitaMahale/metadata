using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HydroProject.Code;
using System.Data;
using System.Data.SqlClient;


public partial class admin_displaydata : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label obj_lbl = Master.FindControl("lbltitle") as Label;
        obj_lbl.Text = "Display Prepared Data";
        int  location = DUPLICATEBL.getlocation();
        lnklocation.Text = location.ToString();
        
        int parameter = DUPLICATEBL.getparameter();
        lnkparameter.Text = parameter.ToString();

        DataTable msngdt = DUPLICATEBL.getmisng();

        gridview1.DataSource = msngdt;
        gridview1.DataBind();
        lnklocation.Focus();
        if (!IsPostBack)
        {
            string SaveLocation1 = Server.MapPath("~/Upload") + "\\" + "Units1.mdb";
            int cnt = updatedatadesc.updatedesc(SaveLocation1);
            //int cnt = 0;
            if (cnt == 0)
            {
            }
            else

                //int cnt = 0;
                //jscall1("Data Prepared Successfully");
                if (cnt > 0)
                {
                    LinkButton1.Visible = true;
                    LinkButton1.Text = "Please update description first";
                    LinkButton1.PostBackUrl = "updatedesclist.aspx";
                    jscall("update description first");

                }


        }
        
    }
    protected void gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gd = (GridView)sender;
        gd.PageIndex = e.NewPageIndex;
        gd.DataBind();
    }
    protected  void Button1_Click(object sender, EventArgs e)
    {
        // int cnt = updatedatadesc.updatedesc(SaveLocation1);

      

        string SaveLocation1 = Server.MapPath("~/Upload") + "\\" + "Units1.mdb";
       int cnt = updatedatadesc.updatedesc(SaveLocation1);
        //int cnt = 0;
       if (cnt == 0)

       {
           Response.Redirect("rptavlbl.aspx");
       }
       else

        //int cnt = 0;
        //jscall1("Data Prepared Successfully");
        if (cnt > 0)
        {
            LinkButton1.Visible = true;
            LinkButton1.Text = "Please update description first";
            LinkButton1.PostBackUrl = "updatedesclist.aspx";
            jscall("update description first");

        }
      
    }
    private void jscall1(string confirm)
    {
        string Script = @"confirm('" + confirm + "');";
        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", Script, true);
    }



    private void jscall(string confirm)
    {

        string script = "alert('" + confirm + "'); ";
        //string Script = @"confirm('" + confirm + "');";
        ClientScript.RegisterStartupScript(this.GetType(), "jscall", script, true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", script, true);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    DUPLICATEBL.clearall();
        //    jscall1("Again Prepare Data");
        //}
        //catch (Exception ex)
        //{
        //    throw (ex);
        //}

        int cnt = updatedatadesc.getcount();
        if (cnt == 0)
        {
            Response.Redirect("Admin_home.aspx");
        }
        else
        {
            LinkButton1.Visible = true;
            LinkButton1.Text = "Please update description first";
            LinkButton1.PostBackUrl = "updatedesclist.aspx";
            jscall("update description first");
        }
    }

    protected void linklogut_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminlogin.aspx?logout=1&Id='" + Session.SessionID + "'");
    }
    protected void lnklocation_Click(object sender, EventArgs e)
    {
        Session["value"] = "location";
        Session["callfrom"] = "displaydata";
        Response.Redirect("detaildisplay.aspx");
       
    }
    protected void lnkparameter_Click(object sender, EventArgs e)
    {
        Session["value"] = "parameter";
        Session["callfrom"] = "displaydata";
        Response.Redirect("detaildisplay.aspx");
       
    }
}
