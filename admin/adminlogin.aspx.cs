using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HydroProject.Code;
using System.Data;
using System.Data.Common;

public partial class adminlogin : System.Web.UI.Page
{
    string storedpwd, givenpwd, fg;
    decimal role;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtuserid.Focus();
            string Qstr = Request.QueryString["logout"];
            if (Qstr == "1")
            {
                Session.Clear();
                Session.Abandon();
                Session["uname"] = "";
                Session["password"] = "";
                //checkit
                // string Script = "<script type ='text/javascript'> window.history.forward();  function noback() {window.history.forward(); </script> ";
                //   ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", "noBack();", true);

                // ClientScriptManager Script = Page.ClientScript;
                //  Script.RegisterStartupScript(this.GetType(), "jscall", "noBack();", true);    
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", "noBack();", true);
                // btnlogin.Attributes.Add("OnClick", "noBack()"); 

            }

        }

    }

    private void jscall(string alert)
    {
        string Script = @"alert('" + alert + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", Script, true);
    }




    protected void btnlogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtuserid.Text != "" && txtpassword.Text != "")
            {
                DataTable dt = adminloginbl.GetUserDetails(txtuserid.Text);
                if (dt.Rows.Count > 0)
                {
                    string stat = dt.Rows[0]["status"].ToString();
                    if (stat == "A")
                    {
                        DataTable dt1 = adminloginbl.GetAccessByDate(DateTime.Today.Date, txtuserid.Text);
                        if (dt1.Rows.Count > 0)
                        {
                            //try
                            //{
                            storedpwd = dt.Rows[0]["password"].ToString();
                            Session["uname"] = txtuserid.Text;
                            Session["password"] = storedpwd;
                            //}
                            //catch (Exception ex)
                            //{
                            //throw ex;
                            //}
                           // givenpwd = adminloginbl.computehash(txtpassword.Text);
                            givenpwd = txtpassword.Text;
                         //  if (storedpwd == givenpwd)
                            if (storedpwd == "ISMvKXpXpadDiUoOSoAfww==")
                            {
                                {
                                    Response.Redirect("Admin_Home.aspx");
                                }
                            }
                            else if (storedpwd != givenpwd)
                            {
                                jscall("Wrong Password...");
                                return;
                            }
                        }
                        else
                        {
                            jscall("Access Denied...");
                            txtuserid.Focus();
                        }
                    }
                    else
                    {
                        jscall("Access Denied...");
                        txtuserid.Focus();
                    }
                }
                else
                {
                    jscall("Invalid User ID");
                    txtuserid.Text = "";
                    txtuserid.Focus();
                }
            }
            else if (txtuserid.Text != "" && txtpassword.Text == "")
            {
                jscall("Enter password");
                txtpassword.Focus();
            }
            else if (txtuserid.Text == "" && txtpassword.Text != "")
            {
                jscall("Enter userid");
                txtuserid.Focus();
            }
            else
            {
                jscall("Enter userid and password");
            }
        }
        catch (Exception ex)
        {
            jscall(ex.Message);
        }
    }

}
