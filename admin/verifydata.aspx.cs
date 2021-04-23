using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HydroProject.Code;

public partial class admin_verifydata : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label obj_lbl = Master.FindControl("lbltitle") as Label;
        obj_lbl.Text = "Verify Data";
        int cnt = DUPLICATEBL.chkverfied();
        if (cnt !=1)
        {
            int location = DUPLICATEBL.getlocation();
            lnklocation.Text = location.ToString();

            int parameter = DUPLICATEBL.getparameter();
            lnkparameter.Text = parameter.ToString();

            DataTable msngdt = DUPLICATEBL.getmisng();

            gridview1.DataSource = msngdt;
            gridview1.DataBind();
          
        }
        else
        {
           
            lnklocation.Text = "0";
            lnkparameter.Text = "0";
            gridview1.DataSource = null;
            gridview1.DataBind();
        }
        if (!IsPostBack)
        {
            if (Session["uname"] != null || Session["password"] != null)
            {
                //  txtfromdt.Focus();
                int cnt1 = updatedatadesc.getcount();
                if (cnt1 == 0)
                {
                    LinkButton1.Visible = false;
                }
                else
                {
                    LinkButton2.Visible = true;
                    LinkButton2.Text = "Please update description first";
                    LinkButton2.PostBackUrl = "updatedesclist.aspx";
                    jscall1("update description first");
                }
            }
            else if (Session["uname"] == null || Session["password"] == null)
            {
                Response.Redirect("adminlogin.aspx");

            }
        }


        if (Session["clr"] != null)
        {
            string clr = Session["clr"].ToString();
            if (clr == "1")
            {
                Session["clr"] = null;
                jscall("Data Cleared. Again Prepare Data");
                
            }
            else if (clr == "2")
            {
               cnt = DUPLICATEBL.chkverfied();
                Session["clr"] = null;
                if (cnt >0)
                {
                    jscall("Data verified successfully");

                }

            }
        }
        
    }

    private void jscall1(string confirm)
    {
        string Script = @"confirm('" + confirm + "');";



        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", Script, true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        int cnt2 = updatedatadesc.getcount();
        if (cnt2 == 0)
        {
            LinkButton1.Visible = false;
            int j = DUPLICATEBL.chkempty();
            if (j == 1)
            {
                string userid = Session["uname"].ToString();
                int i = metadatasavebl.updateverification(userid);
                if (i == 1)
                {
                    Session["clr"] = "2";
                    Response.Redirect("verifydata.aspx");
                    //jscall("Data Verified");
                }
            }
            else
            {
                jscall("No data for verification. First Prepare Data");
            }
        }
        else
        {
            LinkButton1.Visible = true;
            LinkButton1.Text = "Please update description first";
            LinkButton1.PostBackUrl = "updatedesclist.aspx";
            jscall1("update description first");
        }
        
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
       
       
        int i = DUPLICATEBL.chkempty();
        if (i == 1)
        {
            int j = DUPLICATEBL.chkverfied();
            if (j != 1)
            {
                popup.Show();
            }
            else
            {
                jscall("Data is already verified");
            }
        }
        else
        {
            jscall("No Data For cancellation");
        }
        //DUPLICATEBL.clearall();
        //jscall("Data Cleared. Again Prepare Data");
    }

    protected void linklogut_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminlogin.aspx?logout=1&Id='" + Session.SessionID + "'");
    }
    protected void lnklocation_Click(object sender, EventArgs e)
    {
        Session["value"] = "location";
        Session["callfrom"] = "verifydata";
        Response.Redirect("detaildisplay.aspx");

    }
    protected void lnkparameter_Click(object sender, EventArgs e)
    {
        Session["value"] = "parameter";
        Session["callfrom"] = "verifydata";

        Response.Redirect("detaildisplay.aspx");

    }
    protected void gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gd = (GridView)sender;
        gd.PageIndex = e.NewPageIndex;
        gd.DataBind();
    }
    protected void Yes(object sender, EventArgs e)
    {
        DUPLICATEBL.clearall();
       Session ["clr"] = "1";
        
        Response.Redirect ("verifydata.aspx");
       
    }
}
