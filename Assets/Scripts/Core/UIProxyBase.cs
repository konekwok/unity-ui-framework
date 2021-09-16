using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProxyBase
{
    protected Dictionary<string, UIDataBase> m_uidatas;
    public void RegisterUIData<T>() where T : UIDataBase, new()
    {
        string ctrlkey = typeof(T).Name;
        if(m_uidatas.TryGetValue(ctrlkey, out UIDataBase val))
        {
            return;
        }
        T proxy = new T();
        m_uidatas.Add(ctrlkey, proxy);
    }
    public T GetUIData<T>() where T : UIDataBase
    {
        string ctrlkey = typeof(T).Name;
        if(m_uidatas.TryGetValue(ctrlkey, out UIDataBase val))
        {
            return (T)val;
        }
        return null;
    }
}
