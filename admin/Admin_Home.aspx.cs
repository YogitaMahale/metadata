using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HydroProject.Code;
using System.Data;
using System.Data.SqlTypes;


public partial class Design_Default : System.Web.UI.Page
{
    string storedpwd, givenpwd, storedpwd1;
    protected void linklogut_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminlogin.aspx?logout=1&Id='" + Session.SessionID + "'");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Label obj_lbl = Master.FindControl("lbltitle") as Label;
        obj_lbl.Text = "Admin Login";
        Session["lblmsg"] = null;
        Session["savebtn"] = null;
        Session["backbtn"] = null;
        Session["searchbtn"] = null;
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
       
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
 
       // Response.Redirect("adminlogin.aspx");
        int i = DUPLICATEBL.chkempty();
        
       
        if (i == 1)
        {
            int j = DUPLICATEBL.chkverfied();
            if (j == 1)
            {
                int k = DUPLICATEBL.chkuser();
            
                if (k < 1)
                {
                    lbllogin.Text = "Enter Administrator's userid password for accessing data";
                }
                popup.Show();
                txtuser.Text=" ";
                txtuser.Focus();
            }
            else
                jscall1("First Verify Data");
        }
         else
        {
            jscall1("No data for uploading first prepare it");
        }
    
    }

    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Save(null, null);
    }
    
    private void jscall1(string confirm)
    {
        string Script = @"alert('" + confirm + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", Script, true);
    }
    private void jscall(string confirm)
    {
        string Script = @"alert('" + confirm + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", Script, true);
    }
    protected void Save(object sender, EventArgs e)
    {
        try
        {
            int j = metadatasavebl.uploaddata("admin");

           if (j == 1)
            {
                jscall1("Data uploaded Successfully");

            }
        }
        catch (Exception ex)
        {
            jscall1("Error: " + ex.Message);
        }
       
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {


        if (txtuser.Text != "" && txtpswd.Text != "")
        {   

      
            int cnt = adminloginbl.validateuserid(txtuser.Text.Trim());
            if (cnt> 0)
            {

                try
                {

                    DataTable dt1 = adminloginbl.Getpasswordwithoutaccess(txtuser.Text.Trim());
                    if (dt1.Rows.Count > 0)
                    {
                        storedpwd1 = dt1.Rows[0]["password"].ToString();
                        DataTable dt = adminloginbl.GetwebPassword(txtuser.Text.Trim());
                        if (dt.Rows.Count > 0)
                        {
                            storedpwd = dt.Rows[0]["password"].ToString();

                        }
                        //Session["uname"] = txtuser.Text;
                        //Session["password"] = storedpwd;
                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }

                givenpwd = adminloginbl.computehash(txtpswd.Text);

                if (storedpwd1 == givenpwd)
                {
                    if (storedpwd == givenpwd)
                    {
                        logindiv.Visible = false;
                        datauploaddiv.Visible = true;
                        pnlAddEdit.Height = 93;
                        pnllogin.Visible = false;
                        pnlupload.Visible = true;
                        Session["user"] = txtuser.Text;

                        popup.Show();
                        txtuser.Focus();
                    }
                    else if (storedpwd != givenpwd)
                    {
                        jscall1("Access Denied...");
                        return;
                    }
                }
                else if (storedpwd1 != givenpwd)
                {
                    jscall1("Wrong Password...");
                    return;
                }
            }
            else
            {
                jscall("Wrong userid");
                TextBox1.Text = "";
            }
        }
        else if(txtuser.Text=="" && txtpswd.Text=="")
        {
            jscall1("please enter userid,password");
            txtuser.Focus();
        }
        else if (txtuser.Text == "" && txtpswd.Text != "")
        {
            jscall1("Please enter userid");
            txtuser.Focus();
        }
        else if (txtuser.Text != "" && txtpswd.Text == "")
        {
            jscall1("Please enter password");
        }


    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("verifydata.aspx");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        int j = DUPLICATEBL.chktruncate();
        if (j == 1)
        {
            int k = DUPLICATEBL.chkuser();

            if (k < 1)
            {
                lbllogin.Text = "Enter Administrator's userid password for accessing data";
            }
            //  popup.Show();
            Div1.Visible = true;
            truncatediv.Visible = false;
         pnltruncate.Height = 130;
            Panel1.Visible = true;

            paneltruncate.Visible = false;
            truncatepopup.Show();
            TextBox1.Focus();
        }
        else
        {
            jscall("Database is already empty");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = metadatasavebl.truncatetable();

        if (i == 1) 
        {
            jscall1("Tables truncated successfully,Now Prepare data");
        }
    }
    protected void Image2_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btntrunclogin_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "" && TextBox2.Text != "")
        {
            int cnt = adminloginbl.validateuserid(TextBox1.Text);
                if (cnt>0)
                {
                        try
                        { 
                             DataTable dt1=adminloginbl.Getpasswordwithoutaccess(TextBox1.Text);
                            if (dt1.Rows.Count>0)
                            {
                                storedpwd1 = dt1.Rows[0]["password"].ToString();
                                    DataTable dt = adminloginbl.GetwebPassword(TextBox1.Text);
                                    if (dt.Rows.Count > 0)
                                    {
                                        storedpwd = dt.Rows[0]["password"].ToString();

                                    }
                                    
                            }

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        givenpwd = adminloginbl.computehash(TextBox2.Text);

                      if (storedpwd1 == givenpwd)
                       {
                            if (storedpwd == givenpwd)
                            {
                               Div1.Visible = false;
                                truncatediv.Visible = true;
                                pnltruncate.Height = 93;
                                Panel1.Visible = false;

                                paneltruncate.Visible = true;

                                //  Session["user"] = TextBox1.Text;

                                truncatepopup.Show();
                            }
                            else if (storedpwd != givenpwd)
                            {
                                jscall1("Access Denied ...");
                                return;
                               
                            }

                       }
                      else if (storedpwd1 != givenpwd)
                      {
                        
                         jscall1("Wrong Password...");
                      }
                }
                else
                {
                    jscall1("Wrong Userid...");
                    TextBox1.Text = "";
                }


        }
        else if (TextBox1.Text == "" && TextBox2.Text == "")
        {
            jscall1("please enter userid,password");
            txtuser.Focus();
        }
        else if (TextBox1.Text == "" && TextBox2.Text != "")
        {
            jscall1("Please enter userid");
            txtuser.Focus();
        }
        else if (TextBox1.Text != "" && TextBox2.Text == "")
        {
            jscall1("Please enter password");
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Div1.Visible = true ;
        truncatediv.Visible = false;
        pnltruncate.Height = 93;
        Panel1.Visible = true;

        paneltruncate.Visible = false;
        TextBox1.Text = "";

    }
    protected void LinkButton1_Click(object sender, ImageClickEventArgs e)
    {
        Save(null, null);
    }
  
}
