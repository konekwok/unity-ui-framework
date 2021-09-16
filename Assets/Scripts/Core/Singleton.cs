using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T : Singleton<T>, new() 
{
    static T s_instance;
    public static T Instance
    {
        get
        {
            if(null == s_instance)
            {
                s_instance = new T();
                s_instance.OnAwake();
            }
            return s_instance;
        }
    }
    public abstract void OnAwake();
    public abstract void OnStart();
}
