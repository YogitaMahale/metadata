using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MemberEntity
/// </summary>
/// 
namespace HydroProject.Code
{
    public class MemberEntity
    {
        DateTime entrydate;
        string org;
        string name;
        int pincode;
        string address;
        string state;
        string city;
        string emailid;
        int fax;
        string sector;
        string objective;
        string purpose;
        string nature;
        string frequency;

        public MemberEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DateTime GetEntryDate
        {
            get
            {
                return entrydate;

            }
            set
            {
                entrydate = value;

            }

        }
        public string GetOrganization
        {
            get
            {
                return org;
            }
            set
            {

                org = value;
            }
        }
        public string GetName
        {
            get
            {
                return name;
            }

            set
            {

                name = value;
            }

        }

        public string GetAddress
        {
            get
            {

                return address;
            }

            set
            {

                address = value;
            }
        }

        public string GetState
        {
            get
            {
                return state;
            }

            set
            {

                state = value;
            }
        }

        public string GetCity
        {

            get
            {

                return city;

            }
            set
            {
                city = value;
            }
        }
        public int GetPinCode
        {
            get
            {
                return pincode;
            }
            set
            {

                pincode = value;
            }

        }
        public string GetEmailId
        {
            get
            {
                return emailid;
            }
            set
            {

                emailid = value;
            }
        }

        public int GetFax
        {
            get
            {
                return fax;
            }
            set
            {
                fax = value;
            }

        }

        public string GetSector
        {

            get
            {
                return sector;
            }

            set
            {
                sector = value;
            }
        }

        public string GetObjective
        {

            get
            {

                return objective;
            }
            set
            {

                objective = value;
            }
        }
        public string GetPurpose
        {

            get
            {
                return purpose;

            }
            set
            {
                purpose = value;
            }
        }
        public string GetNature
        {
            get
            {
                return nature;
            }
            set
            {
                nature = value;
            }
        }
        public string GetFrequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
            }
        }
    }
}