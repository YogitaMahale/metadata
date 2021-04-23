using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for Damdataentrybl
/// </summary>

namespace HydroProject.Code
{
    public class Damdataentrybl
    {
        public Damdataentrybl()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static DataTable GetDamName(string RegionName)
        {
            DbCommand dbComm = BaseDb.CreateStrCommand();
            dbComm.CommandText = "SELECT DAM_NAME FROM DAMMASTER WHERE (REGIONID = (SELECT REGIONID FROM REGIONMASTER WHERE (REGION_NAME = '" + RegionName + "')))";
            DataTable fieldData;
            try
            {
                fieldData = BaseDb.ExecuteSelect(dbComm);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return fieldData;
        }
    }
}