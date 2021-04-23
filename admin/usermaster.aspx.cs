using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using HydroProject.Code;


public partial class admin_usermaster : System.Web.UI.Page
{
    int savebtn, backbtn, searchbtn;
    string srno, username,username1, userid, status, strt_dt, clos_dt;
    string dat = dateconvert.Convertdt(HttpContext.Current.Server.UrlDecode(DateTime.Now.ToString()));

    protected void Page_Load(object sender, EventArgs e)
    {
        Label obj_lbl = Master.FindControl("lbltitle") as Label;
        obj_lbl.Text = "User Master";
        username1 = (string)Session["uname"];
        srno = (string)Session["useridusermaster"];
        lblmsg.Visible = false;
        lblreqfldmsg.Visible = false;
        addupdate();

        if (!IsPostBack)
        {
            
            if (Session["uname"] != null || Session["password"] != null)
            {
                txtuserid.Focus();
                if (srno != "")
                {
                    txtclosedt.Focus();
                    txtuserid.Text = userid;
                    txtusername.Text = username;
                    txtstartdt.Text = strt_dt;
                    txtclosedt.Text = clos_dt;
                    if (status == "Active")
                    {
                        ddstatus.SelectedValue = "A";
                    }
                    else if (status == "Inactive")
                    {
                        ddstatus.SelectedValue = "I";

                    }
                    ddstatus.SelectedItem.Text = status;
                }
                if (txtclosedt.Text != "")           // Close date once entered cannot be edited
                {
                    txtclosedt.ReadOnly = true;
                    txtclosedt.Enabled = false;
                    ImageButton4.Visible = false;
                }
                else
                {
                    txtclosedt.ReadOnly = false;
                    txtclosedt.Enabled = true;
                    ImageButton4.Visible = true;
                }
            }
            else if (Session["uname"] == null || Session["password"] == null)
            {
               Response.Redirect("adminlogin.aspx");
            }
        }
        
          //if (srno == "" || srno==null )
          //{
          //    updatecol.Visible = false;
          //    savecol.Visible = true;
          //    txtpassword.Visible = true;
          //    lblpassword.Visible = true;
          //    txtstartdt.ReadOnly = false;
          //    txtuserid.ReadOnly = false;
          //    //txtuserid.Text = "";
          //    //txtusername.Text = "";
          //    //txtstartdt.Text = "";
          //    //txtclosedt.Text = "";
             
          //}
          //else
          //{
          //    savecol.Visible = false;
          //    updatecol.Visible = true;
          //    txtuserid.ReadOnly = true;
          //    txtuserid.Enabled = false;
          //    txtusername.ReadOnly = true;
          //    txtusername.Enabled = false;
          //    txtpassword.Visible = false;
          //    lblpassword.Visible = false;
          //    txtstartdt.ReadOnly = true;
          //    txtstartdt.Enabled = false;
          //    txtclosedt.Focus();
          //    displayrecord(srno);
          //}

    }
    public void addupdate()
    {
        if (srno == "" || srno == null)
        {
            updatecol.Visible = false;
            savecol.Visible = true;
            txtpassword.Visible = true;
            lblpassword.Visible = true;
            txtstartdt.ReadOnly = false;
            txtuserid.ReadOnly = false;
            txtusername.ReadOnly = false;
        }
        else if (srno != "")
        {
            savecol.Visible = false;
            updatecol.Visible = true;
            txtuserid.ReadOnly = true;
            txtuserid.Enabled = false;
            txtusername.ReadOnly = true;
            txtusername.Enabled = false;
            txtpassword.Visible = false;
            lblpassword.Visible = false;
            txtstartdt.ReadOnly = true;
            txtstartdt.Enabled = false;
            ImageButton3.Visible = false;
            RequiredFieldValidator6.Visible = false;
            txtclosedt.Focus();
            displayrecord(srno);
        }
    }
    private void jscall(string alert)
    {
        string Script = @"alert('" + alert + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", Script, true);
    }

    private void jscall1(string confirm)
    {
        string Script = @"confirm('" + confirm + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall1", Script, true);
    } 
    public  void displayrecord(string srno)
    {
            DataTable dt=new DataTable();
            dt = usermasterbl.showdata(srno);
            if (dt.Rows.Count>0)
            {
                userid = dt.Rows[0]["userid"].ToString();
                username = dt.Rows[0]["username"].ToString();
                strt_dt = dt.Rows[0]["start_dt"].ToString();
                clos_dt = dt.Rows[0]["close_dt"].ToString();                
                //txtstartdt.Text = Convert.ToString(Convert.ToDateTime( dt.Rows[0]["start_dt"].ToString()).Date );
                //txtclosedt.Text = Convert.ToString(Convert.ToDateTime(dt.Rows[0]["close_dt"].ToString()).Date);
                status = dt.Rows[0]["STATUS"].ToString();
                
                
            }
    
    }
    public void clearall()
    {
        txtuserid.Text = "";
        txtusername.Text = "";
        txtpassword.Text = "";        
        txtstartdt.Text = "";
        txtclosedt.Text = "";
        ddstatus.SelectedIndex = 0;
        
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (Session["uname"] == null || Session["password"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }
        else
        {
            if(txtuserid.Text == "" || txtusername.Text == "" || txtpassword.Text == "" || txtstartdt.Text == "" )
            {
                lblreqfldmsg.Visible = true;
                jscall("Enter required fields...");
                return;
            }
            else if (txtuserid.Text == "" && txtusername.Text == "" && txtpassword.Text == "" && txtstartdt.Text == "")
            {
                lblreqfldmsg.Visible = true;
                jscall("Enter required fields...");
                return;
            }
            else
            {
                Page.Validate();
                if (Page.IsValid == true)
                {
                    try
                    {
                        usermasterentity objm = new usermasterentity();

                        objm.Getuserid = txtuserid.Text;
                        objm.Getusername = txtusername.Text;

                        objm.Getpassword = usermasterbl.computehash(txtpassword.Text);
                        objm.Getstatus = ddstatus.SelectedValue;
                        if (txtclosedt.Text == "")
                        {
                            SqlDateTime date1 = SqlDateTime.Null;
                            objm.Getto_dt = date1;

                        }
                        else
                        {
                            objm.Getto_dt = (SqlDateTime)(DateTime.Parse(txtclosedt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").DateTimeFormat));
                        }

                        if (txtstartdt.Text == "")
                        {
                            SqlDateTime date1 = SqlDateTime.Null;
                            objm.Getfrom_dt = date1;

                        }
                        else
                        {
                            objm.Getfrom_dt = (SqlDateTime)(DateTime.Parse(txtstartdt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").DateTimeFormat));
                        }
                        objm.Getcreated_by = username1;
                        objm.Getcreated_dt = SqlDateTime.Parse(dat);
                        int i = usermasterbl.insertusermaster(objm);
                        if (i == 1)
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "User created successfully";
                            Session["savebtn"] = 1;
                            Session["backbtn"] = 0;
                            Session["searchbtn"] = 0;
                        }
                        else
                        {
                            lblmsg.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw (ex);
                        lblmsg.Visible = true;
                        lblmsg.Text = "User ID already exists...";                        
                        txtuserid.Text = "";
                        txtuserid.Focus();                        
                        return;
                    }
                    clearall();
                    txtuserid.Focus();
                    lblmsgvalid.Visible = false;
                }

                else
                {
                    lblmsgvalid.Text = "Check Validations!";
                    return;
                }

            }
        }       
                
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        if (Session["uname"] == null || Session["password"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }
        else
        {
            Session["backbtn"] = 1;
            Session["searchbtn"] = 0;
            Session["savebtn"] = 0;
            Session["lblmessage"] = null;
            Response.Redirect("usermasterlist.aspx");
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (Session["uname"] == null || Session["password"] == null)
        {
            Response.Redirect("adminlogin.aspx");
        }
        else
        {
                try
                {
                    usermasterentity objm = new usermasterentity();
                    objm.Getsrno = Convert.ToInt16(Session["useridusermaster"].ToString());

                    if (txtclosedt.Text == "")
                    {
                        SqlDateTime date1 = SqlDateTime.Null;
                        objm.Getto_dt = date1;

                    }
                    else
                    {
                        //objm.Getto_dt = Convert.ToDateTime(txtclosedt.Text.ToString());
                        objm.Getto_dt = (SqlDateTime)(DateTime.Parse(txtclosedt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").DateTimeFormat));
                    }
                    objm.Getupdated_by = username1;
                    objm.Getupdated_dt = SqlDateTime.Parse(dat);
                    objm.Getstatus = ddstatus.SelectedValue;
                    usermasterbl.updateusermaster(objm);
                    lblmsg.Visible = true;
                    lblmsg.Text = "Data Updated successfully";
                    Session["savebtn"] = 1;
                    Session["backbtn"] = 0;
                    Session["searchbtn"] = 0;
                    txtclosedt.Enabled=false;
                }
                catch (Exception ex)
                {
                    throw (ex);

                }
                lblmsgvalid.Visible = false;            

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
    protected void txtclosedt_TextChanged(object sender, EventArgs e)
    {
        if (txtclosedt.Text != "")
        {
            try
            {
            string[] dt = txtclosedt.Text.Split(' ');
            string[] dtary = dt[0].Split('/');
            string dat = dtary[1].ToString() + "/" + dtary[0].ToString() + "/" + dtary[2].ToString();
            DateTime dat1 = Convert.ToDateTime(dat);

            string[] sdt = txtstartdt.Text.Split(' ');
            string[] sdtary = sdt[0].Split('/');
            string sdat = sdtary[1].ToString() + "/" + sdtary[0].ToString() + "/" + sdtary[2].ToString();
            DateTime sdat1 = Convert.ToDateTime(sdat);

            if (dat1 <= sdat1)
            {
                //jscall1("Close date should be greated than start date");
                lblmsg.Visible = true;
                lblmsg.Text = "Close Date should be greater than Start Date";
                txtclosedt.Text = "";
                txtclosedt.Focus();
                return;
            }
            else
                if (srno != "")
                {
                    if (DateTime.Today.Date >dat1)
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Close Date should be greater than or equal to Current Date";
                        txtclosedt.Focus();
                        txtclosedt.Text = "";
                        return;
                    }
                }
                else if (srno == "")
                {
                    if (DateTime.Today.Date >= dat1)
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Close Date should be greater than Current Date";
                        txtclosedt.Focus();
                        txtclosedt.Text = "";
                        return;
                    }
                }
                
                
            }
        
            catch
            {
                jscall("Invalid Date");
                txtclosedt.Text = "";
                txtclosedt.Focus();
            }
        }

    }
   
    protected void txtstartdt_TextChanged(object sender, EventArgs e)
    {        
        if (txtstartdt.Text == "")
        {
        }
        else
        {
            try
            {
                string[] sdt = txtstartdt.Text.Split(' ');
                string[] sdtary = sdt[0].Split('/');
                string sdat = sdtary[1].ToString() + "/" + sdtary[0].ToString() + "/" + sdtary[2].ToString();
                DateTime sdat1 = Convert.ToDateTime(sdat);

            }
            catch
            {
                jscall("Invalid Date");
                txtstartdt.Text = "";
                txtstartdt.Focus();
            }
        }
    }
}
