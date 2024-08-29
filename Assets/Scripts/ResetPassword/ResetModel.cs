using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetModel 
{
    public ResetModel(string username, string otp, string newpassword)
    {
        this.username = username;
        this.otp = otp;
        this.newpassword = newpassword;
    }

    public string username { get; set; }
    public string otp { get; set; }
    public string newpassword { get; set; }
}
