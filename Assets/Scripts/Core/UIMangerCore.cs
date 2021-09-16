
using System.Collections.Generic;

public class UIMangerCore
{
    public UIMangerCore()
    {
        m_uiproxys = new Dictionary<string, UIProxyBase>();
        m_proxys = new Dictionary<string, UIProxyBase>();
        // m_uidatas = new Dictionary<string, UIDataBase>();
        m_uiMap = new Dictionary<string, IUICtrlBase>();
    }
    private Dictionary<string, UIProxyBase> m_uiproxys; 
    private Dictionary<string, UIProxyBase> m_proxys;
    // private Dictionary<string, UIDataBase> m_uidatas;
    protected Dictionary<string, IUICtrlBase> m_uiMap;
    protected void Register<T, P>() where P : UIProxyBase, new()
    {
        string ctrlkey = typeof(T).Name;
        if(m_uiproxys.TryGetValue(ctrlkey, out UIProxyBase val))
        {
            return;
        }
        P proxy = new P();
        m_uiproxys.Add(ctrlkey, proxy);
        var proxykey = typeof(P).Name;
        m_proxys.Add(proxykey, proxy);
    }
    public T GetProxy<T>() where T : UIProxyBase
    {
        var key = typeof(T).Name;
        if(m_proxys.TryGetValue(key, out UIProxyBase val))
        {
            return (T)val;
        }
        return null;
    }
    protected UIProxyBase GetProxy(string uiname)
    {
        if(m_uiproxys.TryGetValue(uiname, out UIProxyBase val))
        {
            return val;
        }
        return null;
    }
}
