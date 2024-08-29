using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendOTPModel
{
    public string username { get; set; }

    public SendOTPModel(string username)
    {
        this.username = username;
    }
}
