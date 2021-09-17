using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UIMangerCore
{
    static UIManager s_instance;
    public static UIManager Instance
    {
        get
        {
            if(null == s_instance)
            {
                s_instance = new UIManager();
                s_instance.OnAwake();
            }
            return s_instance;
        }
    }
    public void OnAwake()
    {
        
    }
    public void OnStart()
    {
        Register<UICtrlTest, UITestProxy>();
        Register<UICtrlTestWrap, UITestWrapProxy>();
    }
    public void OpenUI<T>(UIDataBase database = null)where T : IUICtrlBase, new()
    {
        var typename = typeof(T).Name;
        if(m_uiMap.TryGetValue(typename, out IUICtrlBase ctrlbase))
        {
            ctrlbase.Refresh();
            return;
        }
        var ctrl = new T();
        var resobj = Resources.Load<GameObject>(typename);
        if(null != resobj)
        {
            GameObject obj = Object.Instantiate(resobj);
            ctrl.Init(GetProxy(typename), obj);
        }
        m_uiMap.Add(typename, ctrl);
        ctrl.Open(database);
    }
    public GameObject Load<T>()
    {
        var typename = typeof(T).Name;
        GameObject obj = Resources.Load<GameObject>(typename);
        return obj;
    }
    public void CloseUI<T>()
    {
        var typename = typeof(T).Name;
        if(m_uiMap.TryGetValue(typename, out IUICtrlBase ctrl))
        {
            m_uiMap.Remove(typename);
            ctrl.Close();
        }
    }
}
