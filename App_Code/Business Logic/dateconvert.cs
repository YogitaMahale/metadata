using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dateconvert
/// </summary>

namespace HydroProject.Code
{
    public class dateconvert
    {
        public dateconvert()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string Convertdt(string sdate)
        {
            string finaldt = "";
            try    //dsb 22.2.12
            {

                string[] dttm = sdate.Split(' ');
                string[] dtary = dttm[0].Split('/');
                //if (Convert.ToInt32(dtary[1].ToString()) > 12)
                //{
                //    finaldt = dtary[0] + "/" + dtary[1] + "/" + dtary[2];
                //}
                //else
                //{
                if (Convert.ToInt32(dtary[1]) > 12)
                {
                    finaldt = dtary[0] + "/" + dtary[1] + "/" + dtary[2];
                }
                else
                {
                    finaldt = dtary[1] + "/" + dtary[0] + "/" + dtary[2];
                }
                    //}
                //return finaldt;  //dsb 22.2.12
            }
            catch    //dsb 22.2.12
            {
                return finaldt;
            }
            //finally
            //{
            return finaldt;    //dsb 22.2.12
            //}
        }

        public static DateTime converttommddyy(string dt)
        {
            string[] dttm = dt.Split(' ');
            string[] dtary = dttm[0].Split('-');
            DateTime newdt = new DateTime(Convert.ToInt16(dtary[2]), Convert.ToInt16(dtary[1]), Convert.ToInt16(dtary[0]));
            string finaldt = newdt.ToShortDateString()+" "+dttm[1];
            newdt = Convert.ToDateTime(finaldt);

            return (newdt);
        }
    }
}
