using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlTypes;

/// <summary>
/// Summary description for usermasterentity
/// </summary>
public class usermasterentity
{
    SqlDateTime from_dt, to_dt, created_dt, updated_dt;
    int srno;
    string userid,username,password,emailid,status, created_by, updated_by;
	public usermasterentity()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public SqlDateTime Getfrom_dt
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
    public SqlDateTime Getcreated_dt
    {
        get
        {
            return created_dt;
        }
        set
        {
            created_dt = value;
        }
    }
    public SqlDateTime Getupdated_dt
    {
        get
        {
            return updated_dt;
        }
        set
        {
            updated_dt = value;
        }
    }
    public SqlDateTime Getto_dt
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

    public  string Getuserid
    {
        get
        {
            return userid ;
        }
        set
        {
            userid = value;
        }
    }
    public string Getcreated_by
    {
        get
        {
            return created_by;
        }
        set
        {
            created_by = value;
        }
    }
    public string Getupdated_by
    {
        get
        {
            return updated_by;
        }
        set
        {
            updated_by = value;
        }
    }
    public string Getusername
    {
        get
        {
            return username;
        }
        set
        {
            username = value;
        }

    }
    public string Getpassword
    {
        get
        {
            return password;
        }
        set
        {
            password = value;
        }
    }
    public string Getemailid
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
    public string Getstatus
    {
        get
        {
            return status;
        }
        set
        {
            status = value;
        }
    }
    public int Getsrno
    {
        get
        {
            return srno;
        }
        set
        {
            srno = value;
        }
    }
}
