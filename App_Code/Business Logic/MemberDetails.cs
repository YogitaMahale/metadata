using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;   

/// <summary>
/// Summary description for MemberDetails
/// </summary>
/// 
namespace HydroProject.Code
{
    public class MemberDetails
    {

        DataAcessLayer dal = new DataAcessLayer();
        public MemberDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public void InsertMemberData(DateTime Entry_Date, string organization, string Name, string Address, string State, string City, int pinCode, string Email_Id, int Fax, string Sector, string Objective, string Purpose, string Nature, string Frequency)
        {
            dal.connect();
            dal.ExecInsert("insert into Member_Details Values('" + Entry_Date + "','" + organization + "','" + Name + "','" + Address + "','" + State + "','" + City + "','" + pinCode + "','" + Email_Id + "','" + Fax + "','" + Sector + "','" + Objective + "','" + Purpose + "','" + Nature + "','" + Frequency + "'");
            dal.Disconnect();


        }

        public void Update_HduData(string Address, string State, string City, int PinCode, int Contactno, int Fax, string RegNo)
        {
            dal.connect();
            dal.Execupdate("UPDATE   Hdu_Details SET  Address = ' " + Address + "', State = ' " + State + "', City =' " + City + "', PinCode = ' " + PinCode + "', ContactNo = ' " + Contactno + "', Fax =' " + Fax + "' where RegNo =' " + RegNo + "'");
            dal.Disconnect();


        }
        public static bool AddMemberData(MemberEntity objm)
        {
            //DateTime EntryDate, string Org,string Name,string Address,string State,string City,int PinCode,string Email,int Fax,string Sector,string Objective,string purpose,string Nature,string Frequency
           
          
            DbCommand dbComm = BaseDb.CreateStrCommand(); 
          //  dbComm.CommandText = "INSERT INTO Member_Details (Entry_Date, Organization, Name, Address, State, City, PinCode, Email_Id, Fax, Sector, Objective, Purpose, Nature, Frequency,Status)VALUES(@Entry_Date, @Organization, @Name, @Address, @State, @City, @PinCode, @Email_Id, @Fax, @Sector, @Objective, @Purpose, @Nature, @Frequency,@Status)";//name of stored procedure
            DbParameter dbparam = dbComm.CreateParameter();
            
            dbparam.ParameterName = "@Entry_Date";
            dbparam.Value = DateTime.Today.Date;
            dbparam.DbType = DbType.DateTime;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();   
            dbparam.ParameterName = "@Organization";
            dbparam.Value = objm.GetOrganization; 
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam=dbComm.CreateParameter();
            dbparam.ParameterName = "@Name";
            dbparam.Value = objm.GetName; 
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Address";
            dbparam.Value = objm.GetAddress; 
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@State";
            dbparam.Value = objm.GetState;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@City";
            dbparam.Value = objm.GetCity;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@PinCode";
            dbparam.Value = objm.GetPinCode;
            dbparam.DbType = DbType.Int32;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Email_Id";
            dbparam.Value = objm.GetEmailId;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Fax";
            dbparam.Value = objm.GetFax;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Sector";
            dbparam.Value = objm.GetState;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Objective";
            dbparam.Value = objm.GetObjective;
            dbparam.DbType =DbType.String;
            dbparam.Direction =ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Purpose";
            dbparam.Value = objm.GetPurpose;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Nature";
            dbparam.Value = objm.GetNature;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "Frequency";
            dbparam.Value = objm.GetFrequency;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "Status";
            dbparam.Value = "R";
            dbparam.DbType = DbType.String;
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
 
 //Update Member Details

        

        public static bool UpdateMemberData(int MemberId,DateTime RegDate,int RegFee,DateTime RenewDt,int RenFee,DateTime MemExpDate,string RegNo)
        {
            DbCommand dbComm = BaseDb.CreateCommand();

            dbComm.CommandText = "UpdateMember"; //name of stored procedure

            DbParameter dbparam = dbComm.CreateParameter();

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
            dbparam.ParameterName = "@MemberExpDt";
            dbparam.Value = MemExpDate;
            dbparam.DbType = DbType.Date;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@RenewFee";
            dbparam.Value = RenFee;
            dbparam.DbType = DbType.Int32;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);


            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@RegNo";
            dbparam.Value = RegNo;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "Member_Id";
            dbparam.Value = MemberId;
            dbparam.DbType = DbType.Int32;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Status";
            dbparam.Value = "A";
            dbparam.DbType = DbType.String; 
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
        public static DataTable GetMemberDetails()
        {
            DbCommand dbComm = BaseDb.CreateStrCommand();
            dbComm.CommandText = "select * from Member_Details where Status='R' ";
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
        public static DataTable GetHduDetails()
        {
            DbCommand dbComm = BaseDb.CreateStrCommand();
            dbComm.CommandText = "select * from Hdu_Details";
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

        public static DataTable GetHduInfoByReg(string RegNo)
        {
            DbCommand dbComm = BaseDb.CreateStrCommand();
           // dbComm.CommandText = "select * from Hdu_Details where RegNo='" + RegNo + "'";
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

        public static DataTable GetMemInfo(string name)
        {
            DbCommand dbComm = BaseDb.CreateStrCommand();
           // dbComm.CommandText = "select * from Member_Details where Name='" + name + "'";
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

        public static DataTable GetHduInfo(string name)
        {
            DbCommand dbComm = BaseDb.CreateStrCommand();
            dbComm.CommandText = "select * from HDUMASTER where Name='" + name + "'";
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
        public static bool UpdateHduData(string Address,string State,string City,int PinCode,int Contactno,int Fax,string RegNo)
        { 
        
            DbCommand dbComm = BaseDb.CreateCommand();
           // dbComm.CommandText = "UpdateHduDetails"; //name of stored procedure
          
            DbParameter dbparam = dbComm.CreateParameter();

            dbparam.ParameterName = "@Address";
            dbparam.Value = Address;
            dbparam.DbType = DbType.String;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);


            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@State";
            dbparam.Value = State ;
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
            dbparam.Value = PinCode;
            dbparam.DbType = DbType.Int32; 
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);


            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@contactNo";
            dbparam.Value = Contactno;
            dbparam.DbType = DbType.Int32; 
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);



            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@Fax";
            dbparam.Value = Fax;
            dbparam.DbType = DbType.Int32; 
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);


            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@RegNo";
            dbparam.Value = RegNo;
            dbparam.DbType = DbType.String;
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

        public static bool AddHduInfo(string MemberCode, string Organization, string Name, string Address, String State, String City, int Pincode, int ContactNo, int MobileNo, string EmailId, int Fax, string RegNo, string Sector, string Objective, string Purpose, string Nature, string Frequency, DateTime RegDate, int RegFee, DateTime RenewDt, int RenFee, DateTime MemberExpDt, string CreatedBy, DateTime CreatedDate, string UpdatedBy, DateTime UpdatedDate)
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
            dbparam.ParameterName = "@ContactNo";
            dbparam.Value = ContactNo;
            dbparam.DbType = DbType.Int32;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            dbparam = dbComm.CreateParameter();
            dbparam.ParameterName = "@MobileNo";
            dbparam.Value = MobileNo;
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
            dbparam.ParameterName = "@RegNo";
            dbparam.Value = RegNo;
            dbparam.DbType = DbType.String; 
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
            dbparam.ParameterName = "@Frequency";
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
            dbparam.Value = MemberExpDt;
            dbparam.DbType = DbType.Date;
            dbparam.Direction = ParameterDirection.Input;
            dbComm.Parameters.Add(dbparam);

            //dbparam = dbComm.CreateParameter();
            //dbparam.ParameterName = "@EntrDt";
            //dbparam.Value = EntryDate;
            //dbparam.DbType = DbType.Date;
            //dbparam.Direction = ParameterDirection.Input;
            //dbComm.Parameters.Add(dbparam);

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