﻿
using System;
using System.Collections.Generic;
using Core;

namespace ui
{
    namespace framework
    {
        public class UIMangerCore
        {
            protected UIMangerCore()
            {
                m_uiproxys = new Dictionary<string, UIProxyBase>();
                m_proxys = new Dictionary<string, UIProxyBase>();
                m_uiMap = new Dictionary<string, IUICtrlBase>();
                m_timerBuilder = new TimerBuilder();
            }
            private Dictionary<string, UIProxyBase> m_uiproxys; 
            private Dictionary<string, UIProxyBase> m_proxys;
            protected Dictionary<string, IUICtrlBase> m_uiMap;
            protected TimerBuilder m_timerBuilder;
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
            public D GetUIData<P, D>() where P : UIProxyBase
                                         where D : UIDataBase, new()
            {
                var proxy = GetProxy<P>();
                if(null != proxy)
                {
                    return proxy.GetUIData<D>();
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
            Dictionary<int, Action<SessionContent>> m_dic;
            public void Conversation<T, D>(int sessionId, Action<int, D> action) where T : UIProxyBase
                                                                            where D : SessionContent
            {
                var proxy = GetProxy<T>();
                proxy.Respond<D>(sessionId, action);
            }
            public virtual void Tick()
            {
                m_timerBuilder.OnFixedUpdate();
            }
            public Timer CreateTimer(TimerData data)
            {
                return m_timerBuilder.CreateTimer(data);
            }
            public void ForceStopTimer(Timer timer, object obj)
            {
                m_timerBuilder.ForceStopTimer(timer, obj);
            }
        }
    }
}
