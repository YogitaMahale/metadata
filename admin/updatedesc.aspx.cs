using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HydroProject.Code;
using System.Data;

public partial class admin_updatedesc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label obj_lbl = Master.FindControl("lbltitle") as Label;
        obj_lbl.Text = "Parameter description entry";
        string parameterid = (string)Session["parameterid"];
        ddlparameterid.SelectedItem.Text = parameterid;
        ddlparameterid.Enabled = false;
        txtparameterdesc.Focus();
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("updatedesclist.aspx");
    }
    private void jscall(string confirm)
    {

        string script = "alert('" + confirm + "'); ";
        //string Script = @"confirm('" + confirm + "');";
        ClientScript.RegisterStartupScript(this.GetType(), "jscall", script, true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "jscall", script, true);
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (txtparameterdesc.Text.Trim() == "")
        {
            jscall("Enter Description First");
            txtparameterdesc.Focus();
            return;
        }
        else
        {
            MetaDataEntity objm = new MetaDataEntity();
            objm.getparameter = txtparameterdesc.Text;
            string parameterid = (string)Session["parameterid"];

            int i = updatedatadesc.updatedatadescription(objm, parameterid);

            if (i == 0)
            {
                //if (Session["griddata"] != null)  //commented by DSB 0n 28.6.2012
                //{
                //    DataTable dt = (DataTable)Session["griddata"];
                //    DataColumn[] keys = new DataColumn[1];
                //    keys[0] = dt.Columns["parameterid"];
                //    dt.PrimaryKey = keys;

                //    DataRow dr = dt.Rows.Find(parameterid);
                //    if (dr != null)
                //    {
                //        dr.BeginEdit();
                //        dr["parameterdesc"] = txtparameterdesc.Text;
                //        dr.EndEdit();
                //    }
                //    Session["griddata"] = dt;
                //}

                Response.Redirect("updatedesclist.aspx");
            }
        }

    }
    protected void linklogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminlogin.aspx?logout=1&Id='" + Session.SessionID + "'");
    }
}
