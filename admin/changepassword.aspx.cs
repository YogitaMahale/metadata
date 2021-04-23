using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HydroProject.Code;

public partial class admin_changepassword : System.Web.UI.Page
{
    string oldpwd;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label obj_lbl = Master.FindControl("lbltitle") as Label;
        obj_lbl.Text = "Change Password";
        if (!IsPostBack)
        {
            if (Session["uname"] != null || Session["password"] != null)
            {

            }
            else
            {
                Response.Redirect("adminlogin.aspx");
            }
        }
        txtoldpwd.Focus();

    }
    protected void btnchangepwd_Click(object sender, EventArgs e)
    {

        if (Session["uname"] == null || Session["password"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }
        else
        {
            if (txtoldpwd.Text == "")
            {
                lblmsg.Text = "Enter Required fields";
                lblmsg.Visible = true;
            }

            else if (txtnewpwd.Text == "" || txtconfirmnewpwd.Text == "")
            {
                lblmsg.Text = "Enter Required fields";
                lblmsg.Visible = true;
                jscall("Enter required fields...");
            }

            else if (txtconfirmnewpwd.Text == "" && txtnewpwd.Text == "")
            {
                lblmsg.Text = "Enter Required fields";
                lblmsg.Visible = true;
                jscall("Enter required fields...");
            }
            else
            {
                DataTable dt = changepasswordbl.GetPassword(Session["uname"].ToString());

                if (dt.Rows.Count > 0)
                {
                    oldpwd = dt.Rows[0]["password"].ToString();

                }
                string enteredpwd = changepasswordbl.computehash(txtoldpwd.Text);

                if (oldpwd == enteredpwd)
                {
                    if (txtnewpwd.Text != txtconfirmnewpwd.Text)
                    {
                        lblmsg.Text = "New Password and Confirm New Password should be same";
                        lblmsg.Visible = true;
                    }
                    else
                    {
                        lblmsg.Text = "";
                        lblmsg.Visible = false;
                        usermasterentity objm = new usermasterentity();
                        string newpswd = changepasswordbl.computehash(txtnewpwd.Text);
                        objm.Getpassword = newpswd;
                        objm.Getuserid = Session["uname"].ToString();
                        changepasswordbl.updatepassword(objm);
                        jscall1("Password changed successfully");
                    }
                }
                else
                {
                    lblmsg.Text = "Old Password does not match.Re-enter";
                    lblmsg.Visible = true;
                    // jscall("Old Password doesn't match");
                    //return;
                    //  txtoldpwd.Text = "";
                    // txtoldpwd.Focus();
                }
            }
        }
    }


    //else
    //    {
    //                    DataTable dt = changepasswordbl.GetPassword(userid);
    //            if (dt.Rows.Count > 0)
    //            {
    //                storedpwd = dt.Rows[0]["password"].ToString();
    //            }
    //            givenpwd = changepasswordbl.computehash(txtoldpwd.Text);
    //            if (storedpwd == givenpwd)
    //            {
                    
    //                if (txtnewpwd.Text != txtconfirmnewpwd.Text)
    //                {
    //                    //jscall1("Enter Confirm New Password...");
    //                    lblerror.Visible = true;
    //                }
    //                else
    //                {
    //                    changepasswordentity objm = new changepasswordentity();
    //                    updatedpwd = changepasswordbl.computehash(txtnewpwd.Text);
    //                    objm.Getpwd = updatedpwd;
    //                    objm.Getuserid = userid;
    //                    changepasswordbl.UpdatePassword(objm);
    //                    jscall1("Password Changed Successfully...");
    //                }
    //            }
    //            else if (storedpwd != givenpwd)
    //            {
    //                jscall("Wrong Current Password...");
    //                return;
    //            }
    //        }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        txtoldpwd.Text = "";
        txtnewpwd.Text = "";
        txtconfirmnewpwd.Text = "";
    }
    protected void linklogut_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminlogin.aspx?logout=1&Id='" + Session.SessionID + "'");
    }
    private void jscall(string alert)
    {
        string Script = @"alert('" + alert + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", Script, true);
    }

    private void jscall1(string confirm)
    {
        string Script = @"confirm('" + confirm + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", Script, true);
    } 

}
