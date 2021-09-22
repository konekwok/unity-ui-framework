using System.Collections;
using System.Collections.Generic;
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
    Dictionary<ui.framework.UILayer, GameObject> m_canvasLayers;
    private List<string> m_asyncLoadingList;
    protected void OnAwake()
    {
        m_canvasLayers = new Dictionary<ui.framework.UILayer, GameObject>();
        m_asyncLoadingList = new List<string>();
    }
    public void OnStart()
    {
        m_canvasLayers.Add(ui.framework.UILayer.Main, GameObject.FindGameObjectWithTag("maincanvas"));
        m_canvasLayers.Add(ui.framework.UILayer.Back, GameObject.FindGameObjectWithTag("backcanvas"));
        m_canvasLayers.Add(ui.framework.UILayer.Top, GameObject.FindGameObjectWithTag("topcanvas"));
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
            var proxy = GetProxy(typename);
            if(null != proxy)
            {
                proxy.SessionRegister = SessionRegister.Instance;
            }
            ctrl.Attach(proxy, obj);
            if(m_canvasLayers.TryGetValue(ctrl.Layer, out GameObject parent))
            {
                obj.transform.SetParent(parent.transform);
            }
            ctrl.OnAwake();
        }
        m_uiMap.Add(typename, ctrl);
        ctrl.Open(database);
    }
    bool IsAsyncLoading(string typename)
    {
        
        for(int i=0; i<m_asyncLoadingList.Count;i++)
        {
            if(m_asyncLoadingList[i].Equals(typename))
            {
                return true;              
            }
        }
        return false;
    }
    public IEnumerator AysncOpenUI<T>(ui.framework.AsyncLoadUIBundleData asyncData, ui.framework.UIDataBase database = null)where T : ui.framework.IUICtrlBase, new()
    {
        var typename = typeof(T).Name;
        ui.framework.IUICtrlBase tmpctrl;
        if (m_uiMap.TryGetValue(typename, out tmpctrl))
        {
            tmpctrl.Refresh();
            asyncData.IsFinished = true;
            asyncData.ctrl = tmpctrl;
            yield break;
        }
        if(IsAsyncLoading(typename))
        {
            asyncData.IsFinished = true;
            yield break;
        }
        m_asyncLoadingList.Add(typename);
        var asyncReq = Resources.LoadAsync<GameObject>(typename);
        while(true)
        {
            if(asyncReq.isDone)
            {
                break;
            }
            yield return asyncReq;
        }
        m_asyncLoadingList.Remove(typename);
        if(null == asyncReq.asset)
        {
            yield break;
        }
        else
        {
            if (m_uiMap.TryGetValue(typename, out tmpctrl))
            {
                tmpctrl.Refresh();
                asyncData.IsFinished = true;
                asyncData.retObj = null;
                asyncData.ctrl = tmpctrl;
                yield break;
            }
            GameObject obj = Object.Instantiate(asyncReq.asset) as GameObject;
            var ctrl = new T();
            var proxy = GetProxy(typename);
            if(null != proxy)
            {
                proxy.SessionRegister = SessionRegister.Instance;
            }
            ctrl.Attach(proxy, obj);
            if(m_canvasLayers.TryGetValue(ctrl.Layer, out GameObject parent))
            {
                obj.transform.SetParent(parent.transform);
            }
            ctrl.OnAwake();
            m_uiMap.Add(typename, ctrl);
            asyncData.Reset();
            var ie = ctrl.AysncOpen(database, asyncData);
            while (true)
            {
                ie.MoveNext();
                if (asyncData.IsFinished)
                {
                    break;
                }
                yield return ie.Current;
            }
            asyncData.ctrl = ctrl;
            asyncData.IsFinished = true;
            yield break;
        }
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
