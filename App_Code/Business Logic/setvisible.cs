using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for setvisible
/// </summary>
namespace HydroProject.Code
{
    public class setvisible
    {
        public setvisible()
        {
            //
            // TODO: Add constructor logic here
            //
          

        }

        public static int setvisibility()
        {
            DataAcessLayer dal = new DataAcessLayer();
            DbCommand dbcom = BaseDb.CreateStrCommand();
            dbcom.CommandText = "select count(*) from Location";
            int i = Convert.ToInt32(BaseDb.ExecuteScalar(dbcom));
            return i;
        }


    }
}
