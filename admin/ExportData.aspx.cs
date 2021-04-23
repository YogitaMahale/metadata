using System;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Configuration;
public partial class admin_ExportData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        string query = "select SRNO, REALSTAT, DATATYPEID FROM [Hydro_Testing3].[dbo].[AVLBLDATADET];";
        query += "select SRNO, REALSTAT, DATATYPEID, STATNAME   from [Hydro_Testing3].[dbo].[LOCATION]";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);

                        //Set Name of DataTables.
                        ds.Tables[0].TableName = "Customers";
                        ds.Tables[1].TableName = "Employees";

                        try
                        {


                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                foreach (DataTable dt in ds.Tables)
                                {
                                    //Add DataTable as Worksheet.
                                    wb.Worksheets.Add(dt);
                                }

                                //Export the Excel file.
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=DataSet.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                        catch (Exception obj)
                        {
                        }
                    }
                }
            }
        }
    }
}