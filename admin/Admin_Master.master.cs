using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Design_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        //if (Session["uname"] == null || Session["passsword"] == null)
        //{
        //    Response.Redirect("adminlogin.aspx");
        //}
        //else
        //{
            lblwhois.Text = Session["uname"].ToString();
        //}        
    }
    
        protected void linklogut_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminlogin.aspx?logout=1&Id='" + Session.SessionID + "'");
    }
    
}
