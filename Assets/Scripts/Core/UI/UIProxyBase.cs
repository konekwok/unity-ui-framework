using System;
using System.Collections.Generic;

namespace ui
{
    namespace framework
    {
        public class UIProxyBase
        {
            public delegate SessionContent RequestUIDataHandler();
            public delegate void NotifyUIHandler(int state);
            protected NotifyUIHandler m_notifyUI;
            public NotifyUIHandler NotifyUI
            {
                get
                {
                    return m_notifyUI;
                }
                set
                {
                    m_notifyUI = value;
                }
            }
            protected Dictionary<string, UIDataBase> m_uidatas;
            SessionRegisterBase m_sessionRegister;
            public SessionRegisterBase SessionRegister
            {
                set
                {
                    m_sessionRegister = value;
                }
            }
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
            //发起方
            public virtual void Request<D>(int sessionId, Action<int, D> action) where D:SessionContent{}
            //响应方
            public virtual void Respond<D>(int sessionId, Action<int, D> action) where D:SessionContent{}
            public D Fetch<D>(int sessionId) where D : SessionContent
            {
                return m_sessionRegister.Get<D>(sessionId);
            }
        }
    }
}
