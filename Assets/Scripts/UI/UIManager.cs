using UnityEngine;

public class UIManager : ui.framework.UIMangerCore
{
    UIManager(){}
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
    protected void OnAwake()
    {
        
    }
    public void OnStart()
    {
        Register<UICtrlTest, UITestProxy>();
        Register<UICtrlTestWrap, UITestWrapProxy>();
    }
    public void OpenUI<T>(ui.framework.UIDataBase database = null)where T : ui.framework.IUICtrlBase, new()
    {
        var typename = typeof(T).Name;
        if(m_uiMap.TryGetValue(typename, out ui.framework.IUICtrlBase ctrlbase))
        {
            ctrlbase.Refresh();
            return;
        }
        var ctrl = new T();
        var resobj = Resources.Load<GameObject>(typename);
        if(null != resobj)
        {
            GameObject obj = Object.Instantiate(resobj);
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            var proxy = GetProxy(typename);
            proxy.SessionRegister = SessionRegister.Instance;
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
        if(m_uiMap.TryGetValue(typename, out ui.framework.IUICtrlBase ctrl))
        {
            m_uiMap.Remove(typename);
            ctrl.Close();
        }
    }
    public override void Tick()
    {
        base.Tick();
    }
}
