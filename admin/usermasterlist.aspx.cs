using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HydroProject.Code;
using System.Data.SqlClient;
using System.Data;


public partial class admin_usermasterlist : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    //Session["uname"]="admin";
    //Session["password"]="admin";
    int savebtn, backbtn, searchbtn;
    string msg, cond;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label obj_lbl = Master.FindControl("lbltitle") as Label;
        obj_lbl.Text = "User Creation";
        string username = (string)Session["uname"];
        string password = (string)Session["password"];
        lblmessage.Visible = false;
        if (ViewState["usermaster"] == null)
        {
            dt = updatedatadesc.getusergriddata();
        }
        else
        {
            dt = (DataTable)ViewState["usermaster"];
        }

        gvusermaster.DataSource = dt;
        gvusermaster.DataBind();

        if (!IsPostBack)
        {
            ddlsearchby.Focus();

            if (Session["uname"] != null || Session["password"] != null)
            {
                //dt = updatedatadesc.getusergriddata();
                //gvusermaster.DataSource = dt;
                //gvusermaster.DataBind();
                if (dt == null)
                {
                    gvusermaster.DataSource = null;
                    gvusermaster.DataBind();
                }
                else
                {

                }                

            }
            else if (Session["uname"] == null || Session["password"] == null)
            {
                Response.Redirect("adminlogin.aspx");
                //dt = updatedatadesc.getusergriddata();
                //gvusermaster.DataSource = dt;
                //gvusermaster.DataBind();
            }
        }
        else
        {
            if (ddlsearchby.SelectedIndex == 2)
            {
                ddlstatus.Focus();
            }
            else if (ddlsearchby.SelectedIndex == 1)
            {
                txtuname.Focus();
            }
        }
        if (ddlsearchby.Text == "Status")
        {
            colselstatus.Visible = true;
            colsrchtext.Visible = false;
        }
        if (ddlsearchby.Text == "User name" || ddlsearchby.Text == "Choose Field")
        {
            colselstatus.Visible = false;
            colsrchtext.Visible = true;
        }
        msg = (string)Session["lblmessage"];
        if (msg != null)
        {
            if (Session["savebtn"] != null)
            {
                savebtn = (int)Session["savebtn"];
                if (savebtn != null)
                {
                    backbtn = (int)Session["backbtn"];
                    if (backbtn == 0)
                    {
                        searchbtn = (int)Session["searchbtn"];
                        if (searchbtn == 0)
                        {
                            lblmessage.Visible = true;
                            lblmessage.Text = msg;
                        }
                    }
                }
            }
        }
    }
  

    protected void btnedit_click(object sender, EventArgs e)
    {        
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        Label srno = (Label)gvusermaster.Rows[index].FindControl("lblsrno");
        Session["useridusermaster"] = srno.Text;
        //Label damid = (Label)gvdammaster.Rows[index].FindControl("lbldamid");
        //Session["damid"] = Convert.ToInt32(damid.Text);
        Response.Redirect("usermaster.aspx");
    }
   
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (Session["uname"] == null || Session["password"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }
        else
        {
            Session["searchbtn"] = 1;
            Session["savebtn"] = 0;
            Session["backbtn"] = 0;
            lblmessage.Text = null;
            if (txtuname.Text == "" && ddlsearchby.SelectedIndex == 0 || txtuname.Text == "" && ddlsearchby.SelectedIndex == 1)
            {
                dt = updatedatadesc.getusergriddata();
                ViewState["usermaster"] = null;
            }
            else
            {
                if (ddlsearchby.SelectedIndex == 2)
                {
                    cond = " status = '" + ddlstatus.SelectedValue + "'";
                }
                else if (ddlsearchby.SelectedIndex == 1)
                {
                    cond = " username like '%" + txtuname.Text + "%'";
                }
                dt = updatedatadesc.ShowUserDetailsbycond(cond);
                ViewState["usermaster"] = dt;
            }
            if (dt.Rows.Count == 0)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "No records found";
                gvusermaster.DataSource = null;
                gvusermaster.DataBind();
                //txtsearch.Text = "";
                //ddsearchfield.SelectedIndex = 0;
                return;
            }
            else if (dt.Rows.Count > 0)
            {
                gvusermaster.DataSource = dt;
                gvusermaster.DataBind();
                ViewState["usermaster"] = dt;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Session["useridusermaster"] = "";
        Response.Redirect("usermaster.aspx");
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Session["backbtn"] = 1;
        Response.Redirect("Admin_home.aspx");
    }
    protected void ddlsearchby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ( ddlsearchby.Text == "Status")
        {
            colselstatus.Visible = true;
            colsrchtext.Visible = false;
        }
        if (ddlsearchby.Text == "User name" || ddlsearchby.Text == "Choose Field")
        {  
            colselstatus.Visible = false;
            colsrchtext.Visible = true;
        }
    }
    protected void linklogut_Click(object sender, EventArgs e)
    {
        Session["searchbtn"] = null;
        Session["savebtn"] = null;
        Session["backbtn"] = null;
        Session["lblmessage"] = null;
        Response.Redirect("adminlogin.aspx?logout=1&Id='" + Session.SessionID + "'");
    }
protected void  gvusermaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
{
    GridView gd = (GridView)sender;
        gd.PageIndex = e.NewPageIndex;
        gd.DataBind();
}
}
