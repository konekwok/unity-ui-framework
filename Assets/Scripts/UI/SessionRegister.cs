using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionRegister : SessionRegisterBase
{
    SessionRegister(){}
    static SessionRegister s_instance;
    public static SessionRegister Instance
    {
        get
        {
            if(null == s_instance)
            {
                s_instance = new SessionRegister();
                s_instance.OnAwake();
            }
            return s_instance;
        }
    }
    protected void OnAwake()
    {
        
    }
    public void OnStart()
    {
        Register<SessionTest>((int)SessionRoute.TestWraprequestIntVal);
    }
}
