using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using HydroProject.Code;



public partial class admin_metadataupload1 : System.Web.UI.Page
{
    string userid;
    int i, chk = 0, filechk;
     static string  FileName, SaveLocation;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Label obj_lbl = Master.FindControl("lbltitle") as Label;
        obj_lbl.Text = "Prepare Metadata";
        if (!IsPostBack)
        {
            if (Session["uname"] !=null || Session["password"] != null)
            {
                ddltype.Focus();
            }
            else if(Session["uname"]==null || Session["password"]==null)
            {
                Response.Redirect("adminlogin.aspx");

            }
        }
        if (Session["count"] == null)
        {

        }
        else
        {
            if (Session["count"].ToString() == "0")
            {
                LinkButton1.Visible = true;
                LinkButton1.Text = "Please update description first";
                LinkButton1.PostBackUrl = "updatedesclist.aspx";
            }
            else
            {
                LinkButton1.Visible = false;
            }
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

   protected void btnprepare_Click(object sender, EventArgs e)
    {
      
        Page.Validate();
        if (Page.IsValid == true)
        {
            int i = DUPLICATEBL.chkempty();//function for cheking that temp database is empty or not

            //if (i == 1)
            if (i >=0)
            {

                FileName = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
                {

                    FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    SaveLocation = Server.MapPath("~/Upload") + " \\" + FileName;
                    if (File.Exists(SaveLocation))
                    {
                        //try
                        //{
                        //    //File.Delete(SaveLocation);
                        //}
                        //catch (Exception exc)
                        //{
                        //   // jscall("File is in use" + exc.Message.ToString()); 
                        //}
                        //finally
                        //{
                        //    System.Diagnostics.Process[] wordInstances = System.Diagnostics.Process.GetProcessesByName("WINWORD");
                        //    foreach (System.Diagnostics.Process wordInstance in wordInstances)
                        //    {
                        //        wordInstance.Kill();
                        //    }
                        //}
                    }
                    try
                    {
                        FileUpload1.PostedFile.SaveAs(SaveLocation);
                    }
                    catch (Exception ex)
                    {
                        throw ex; 
                    }
                   
                }

               SaveLocation = Server.MapPath("~/Upload") + "\\" + FileName;

               
 //string path1 = Server.MapPath("~/Upload") + "\\" + "data for WEB Site.mdb";
               filechk = DUPLICATEBL.chkfile(SaveLocation);

               ViewState["filechk"] = filechk;
               if (filechk == 0)
               {

                   chk = DUPLICATEBL.chknewentry(SaveLocation, ddltype.SelectedValue);
               }
               chk = 1;
               if (chk > 0)
               {
                   popup.Show();

               }
               else
               {
                   startprepare();
                   jscall("Data Prepared Successfully");

               }


           }
           else
           {
               jscall("first Upload Previous data");
           }
       }
       else
       {
           jscall1("check validations");
       }


              #region unwanted code
        // cmd.Connection = conn;
          // cmd.CommandText = "select REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,ENDDATE,LOCALXCOORD,LOCALYCOORD from "+filename1 +".Location";

      // int i = (int)cmd.ExecuteScalar();

           //OleDbDataReader rdr=null;
           // rdr = cmd.ExecuteReader();


           // while (rdr.Read())
           // {

           //     SqlConnection sqlcon = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Documents and Settings\\Administrator\\Desktop\\Hydro project\\App_Data\\Hydro.mdf;Integrated Security=True;User Instance=True");
           //     sqlcon.Open();
           //     SqlCommand cmd1 = new SqlCommand("insert into Location( REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT) values ('"+ rdr[0].ToString() + "','" + rdr[1].ToString() + "','" + rdr[2].ToString() + "','" + rdr[3].ToString() + "','" + rdr[4].ToString() + "' ) ", sqlcon);
           //     cmd1.ExecuteNonQuery();
           // }
           // rdr.Close();
      //       string a = conn.Database;
      //       //dbConn.Open();
      //       SqlConnection consql = new SqlConnection(DBConnection);
      //       SqlCommand cmdsql = new SqlCommand();
      //       consql.Open();
      //       cmdsql.Connection = consql;

      //       string b = consql.Database;
      //     cmdsql.CommandText = "insert into HYDRO.Location ( REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,ENDDATE,LOCALXCOORD,LOCALYCOORD) select REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,ENDDATE,LOCALXCOORD,LOCALYCOORD from " + a+".Location";
           
      //// cmdsql.CommandText="insert Location( REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,ENDDATE,LOCALXCOORD,LOCALYCOORD) select (REALSTAT,STATNAME,COUNTRY,PROVINCE,SUBDISTRICT,BASIN,RIVERNAME,TRIBUTARY,CATCH,LATITUDE,LONGITUDE,ALTITUDE,CATAREA,TOPOSHEET,AGENCY,REGOFFICE,CIRCOFFICE,DIVOFFICE,SUBDIVOFFICE,SECTOFFICE,REMARKS,OWNERAGENCY,STARTDATE,ENDDATE,LOCALXCOORD,LOCALYCOORD) from "+filename1+".Location";
       
      //      cmdsql.ExecuteNonQuery();

             //cmdsql.CommandText = "select count(*) from Location";
             //int i = (int)cmdsql.ExecuteScalar();



        //conn.Close();

        #endregion

    }

   public void startprepare()
   {
       MetaDataEntity objm = new MetaDataEntity();
       objm.Getuserid = Session["uname"].ToString();
       filechk = Convert.ToInt32(ViewState["filechk"].ToString());
      // filechk = 0;
       try
       {
           if (filechk == 1)
           {
               metadatasavebl.preparewqdata(SaveLocation, objm, "Hp");
           }
           else if (filechk == 2)
           {
               metadatasavebl.preparesedimentdata(SaveLocation, objm, "Hp");
           }
           else
           {
               metadatasavebl.preparedata(SaveLocation, objm, ddltype.SelectedValue);
           }
       }
       catch (DataException ex)
       {
           throw (ex);
       }
       catch (Exception ex)
       {
           throw (ex);
       }
       finally
       {

           //if (File.Exists(SaveLocation))
           //{
           //    File.Delete(SaveLocation);
           //}


       }

   }

  

   protected void Yes(object sender, EventArgs e)
   {
       startprepare();

       string  SaveLocation1 = Server.MapPath("~/Upload") + "\\" + "units1.xls";
      // int cnt = updatedatadesc.updatedesc(SaveLocation);
       int cnt = 0;
       if (cnt > 0)
       {
           jscall("please update description first");
           //Response.Redirect("updatedesc.aspx");
       }
       
       Response.Redirect("displaydata.aspx");
       //datapopup.Show();


   }
   protected void ok(object sender, EventArgs e)
   {
     
   }

   protected void TextBox2_TextChanged(object sender, EventArgs e)
   {

   }

   protected void btncancel_Click1(object sender, EventArgs e)
   {

   }
   protected void btncancel_Click(object sender, EventArgs e)
   {
      // ServerAlert.Show();
   }
   protected void LinkButton1_Click(object sender, EventArgs e)
   {

   }
   protected void linklogut_Click(object sender, EventArgs e)
   {
       Response.Redirect("adminlogin.aspx?logout=1&Id='" + Session.SessionID + "'");
   }
}
