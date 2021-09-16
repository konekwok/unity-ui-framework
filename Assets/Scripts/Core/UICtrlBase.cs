using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UICtrlBase<T, P> : IUICtrlBase where T : UIViewBase, new()
                                                        where P : UIProxyBase, new()
{
    public enum UIState
    {
        opening,
        opened,
        closing,
        closed,
    }
    private T m_uiview;
    private P m_proxy;
    private UIState m_state;
    public void Init(UIProxyBase ProxyBase, GameObject root)
    {
        m_proxy = (P)ProxyBase;
        m_uiview = root.GetComponent<T>();
        m_uiview.Notify = OnNotify;
        root.SetActive(false);
    }
    public void Destroy(bool imme = true)
    {
        if(imme)
        {
            Object.DestroyImmediate(this.View.gameObject);
        }
        else
        {
            Object.Destroy(this.View.gameObject);
        }
        Debug.Log("destory:"+this.GetType().Name);
    }
    public void Show()
    {
        this.View.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.View.gameObject.SetActive(false);
    }
    public T View
    {
        get
        {
            return m_uiview;
        }
    }
    public P Proxy
    {
        get
        {
            return m_proxy;
        }
    }
    public UIState CurState
    {
        get
        {
            return m_state;
        }
    }
    public abstract void Open(UIDataBase data);
    public abstract void Refresh();
    public abstract void Close();
    public abstract void OnDestroy();
    public abstract void OnNotify();
}
public abstract class UICtrlBase<T, C, P> : UICtrlBase<T, P> where T : UIViewBase, new()
                                                        where P : UIProxyBase, new()
                                                        where C : IUICtrlBase, new()
{
    List<C> m_childs = new List<C>();
    public List<C> Childs
    {
        get
        {
            return m_childs;
        }
    }
    public void Instantiate(GameObject obj, Transform parent, UIDataBase data = null)
    {
        var ctrl = new C();
        m_childs.Add(ctrl);
        var intObj = Object.Instantiate<GameObject>(obj);
        ctrl.Init(Proxy, intObj);
        intObj.transform.SetParent(parent, false);
        ctrl.Open(data);
    }
    public void Instantiate(C ctrl, GameObject obj, Transform parent, UIDataBase data = null)
    {
        m_childs.Add(ctrl);
        var intObj = Object.Instantiate<GameObject>(obj);
        ctrl.Init(Proxy, intObj);
        intObj.transform.SetParent(parent, false);
        ctrl.Open(data);
    }
}
