using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using HydroProject.Code;  
//using HydroProject.Code; 

/// <summary>
/// Summary description for Hdu_Details
/// </summary>
/// 
namespace HydroProject.code
{
    public class Hdu_Details
    {
        public Hdu_Details()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool AddHduInfo(string MemberCode,string Organization,string Name,string Address,String State,String City,int Pincode,int ContactNo,int MobileNo,string EmailId,int Fax,string RegNo,string Sector,string Objective,string Purpose,string Nature,string Frequency,DateTime RegDate,int RegFee,DateTime RenewDt,int RenFee,DateTime MemberExpDt,DateTime EntryDate,string CreatedBy,DateTime CreatedDate,string UpdatedBy,DateTime UpdatedDate)
        {
            DbCommand dbComm = BaseDb.CreateCommand();

            dbComm.CommandText = "AddHduInformation"; //name of stored procedure

            DbParameter dbparam = dbComm.CreateParameter();

            dbparam.ParameterName = "@MemberCode";
            dbparam.Value = MemberCode;
            dbparam.DbType = DbType.String; 
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Organization";
            dbparam.Value = Organization;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Name";
            dbparam.Value = Name;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Address";
            dbparam.Value = Address;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@State";
            dbparam.Value = State;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@City";
            dbparam.Value = City;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@PinCode";
            dbparam.Value = Pincode;
            dbparam.DbType = DbType.Int32;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@EmailId";
            dbparam.Value = EmailId;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Fax";
            dbparam.Value = Fax;
            dbparam.DbType = DbType.Int32; 
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Sector";
            dbparam.Value = Sector;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Objective";
            dbparam.Value = Objective;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Purpose";
            dbparam.Value = Purpose;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Nature";
            dbparam.Value = Nature;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "Frequency";
            dbparam.Value = Frequency;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@RegDate";
            dbparam.Value = RegDate;
            dbparam.DbType = DbType.Date; 
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@RegFee";
            dbparam.Value = RegFee;
            dbparam.DbType = DbType.Int32;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@RenewDt";
            dbparam.Value = RenewDt;
            dbparam.DbType = DbType.Date;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@RenFee";
            dbparam.Value = RenFee;
            dbparam.DbType = DbType.Int32; 
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@MemberExpDt";
            dbparam.Value = MemberExpDt ;
            dbparam.DbType = DbType.Date;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@EntrDt";
            dbparam.Value = EntryDate;
            dbparam.DbType = DbType.Date;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@CreatedBy";
            dbparam.Value = CreatedBy;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@CreatedDt";
            dbparam.Value = CreatedDate;
            dbparam.DbType = DbType.Date;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@UpdatedBy";
            dbparam.Value = UpdatedBy;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@UpdatedDt";
            dbparam.Value = UpdatedDate;
            dbparam.DbType = DbType.Date;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);



            int result = -1;
            try
            {
                result = BaseDb.ExecuteNonQuery(dbComm);
            }
            catch (Exception exc)
            {

                throw exc;
            }
            finally
            {

            }
            return (result != -1);
        }
 
    }
}
