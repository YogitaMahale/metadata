using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlTypes;




/// <summary>
/// Summary description for metadataentity
/// </summary>
/// 
namespace HydroProject.Code
{
    public class MetaDataEntity
    {
         SqlDateTime from_dt,startdt,enddt;
        SqlDateTime to_dt, created_dt;
        Int64 localxcoord,localycoord;
        string stationname,realstation,state,province,subdistrict,rivername,basin,catchname,tributary;
        string regoffice,divoffice,ciroffice,subdivoffice,sectoffice,latitude,longitude,catarea;
        string toposheet,owneragency,altitude,agency,country,remarks,parameter;
        string userid;
        public MetaDataEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }  

     public SqlDateTime Getfromdt
        {
            get
            {
                return from_dt;
            }
            set
            {
                from_dt = value;
            }
        }

     public SqlDateTime Gettodt
        {
            get
            {
                return to_dt;
            }
            set
            {
                to_dt = value;
            }
        }

    

     public SqlDateTime Getenddt
        {
            get
            {
                return enddt;
            }
            set
            {
                enddt  = value;
            }
        }

     public string Getuserid
     {
         get
         {
             return userid;
         }
         set
         {
             userid = value;
         }

     }

     public string  Getstation
        {
            get
            {
                return stationname;
            }
            set
            {
                stationname = value;
            }
        }

     public string  Getrealstationname
        {
            get
            {
                return realstation;
            }
            set
            {
                realstation = value;
            }
        }

     public string Getcountry
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

    public string Getprovince
        {
            get
            {
                return province;
            }
            set
            {
                province = value;
            }
        }
               
    public string Getsubdistrict
        {
            get
            {
                return subdistrict;
            }
            set
            {
                subdistrict = value;
            }
        }
        
    public string Getbasin
        {
            get
            {
                return basin;
            }
            set
            {
                basin = value;
            }
        }

   public string Getrivername
        {
            get
            {
                return rivername;
            }
            set
            {
                rivername = value;
            }
        }
   public string Gettributary
        {
            get
            {
                return tributary;
            }
            set
            {
                tributary = value;
            }
        }

   public string Getcatchname
        {
            get
            {
                return catchname;
            }
            set
            {
                catchname = value;
            }
        }

    public string Getlatitude
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
            }
        }

    public string Getlongitude
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
            }
        }
    public string Getaltitude
        {
            get
            {
                return altitude;
            }
            set
            {
                altitude = value;
            }
        }
        public string Getcatarea
        {
            get
            {
                return catarea;
            }
            set
            {
                catarea = value;
            }
        }
        public string Gettoposheet
        {
            get
            {
                return toposheet;
            }
            set
            {
                toposheet = value;
            }
        }

        public string Getagency
        {
            get
            {
                return agency;
            }
            set
            {
                agency = value;
            }
        }
        public string Getregoffice
        {
            get
            {
                return regoffice;
            }
            set
            {
                regoffice = value;
            }
        }

        public string Getciroffice
        {
            get
            {
                return ciroffice;
            }
            set
            {
                ciroffice = value;
            }
        }
        public string Getdivoffice
        {
            get
            {
                return divoffice;
            }
            set
            {
                divoffice = value;
            }
        }
public string Getsubdivoffice
        {
            get
            {
                return subdivoffice;
            }
            set
            {
                subdivoffice = value;
            }
        }
        public string Getsectoffice
        {
            get
            {
                return sectoffice;
            }
            set
            {
                sectoffice = value;
            }
        }

        public string Getremarks
        {
            get
            {
                return remarks;
            }
            set
            {
                remarks = value;
            }
        }

        public string Getowneragency
        {
            get
            {
                return owneragency;
            }
            set
            {
                owneragency = value;
            }
        }
        public SqlDateTime  Getstartdt
        {
            get
            {
                return startdt;
            }
            set
            {
                startdt = value;
            }
        }
        public Int64  Getlocalxcoord
        {
            get
            {
                return localxcoord;
            }
            set
            {
                localxcoord = value;
            }
        }

        public Int64  Getlocalycoord
        {
            get
            {
                return localycoord;
            }
            set
            {
                localycoord = value;
            }
        }

        public string getparameter
        {
            get
            {
                return parameter;
            }
            set
            {
                parameter = value;
            }
        }




    }
}