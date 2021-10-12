using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ui
{
    namespace framework
    {
        public abstract class UICtrlBase<T> : IUICtrlBase where T : UIViewBase, new()
        {
            public enum UIState
            {
                inited,
                opening,
                opened,
                closing,
                closed,
                destroyed,
            }
            private T m_uiview;
            private UIState m_state;
            public virtual void Attach(UIProxyBase ProxyBase, GameObject root)
            {
                m_state = UIState.inited;
                m_uiview = root.GetComponent<T>();
                m_uiview.Notify = OnViewNotify;
                root.SetActive(false);
            }
            public virtual IEnumerator AysncOpen(UIDataBase data, AsyncLoadUIBundleData aysncData)
            {
                yield break;
            }
            public virtual void OnReceived(int sessionId, SessionContent content){}
            public virtual bool AndroidBackPressed(){return false;}
             public void OnAwake()
            {
                this.View.OnAwake();
            }
            public virtual void Destroy(bool imme = true)
            {
                if(imme)
                {
                    Object.DestroyImmediate(this.View.gameObject);
                }
                else
                {
                    Object.Destroy(this.View.gameObject);
                }
                m_state = UIState.destroyed;
                Debug.Log("destory:"+this.GetType().Name);
            }
            public void Show()
            {
                m_state = UIState.opened;
                this.View.gameObject.SetActive(true);
            }
            public void Hide()
            {
                this.View.gameObject.SetActive(false);
                m_state = UIState.closed;
            }
            public T View
            {
                get
                {
                    return m_uiview;
                }
            }
            public UIState CurState
            {
                get
                {
                    return m_state;
                }
            }
            public UILayer Layer
            {
                get
                {
                    return this.View.uiLayer;
                }
            }
            public abstract void Open(UIDataBase data);
            public abstract void Refresh();
            public abstract void Close();
            public abstract void OnDestroy();
            public abstract void OnViewNotify(int state);
        }
        public abstract class UICtrlBase<T, P> : UICtrlBase<T> where T : UIViewBase, new()
                                                                where P : UIProxyBase, new()
        {
            private P m_proxy;
            private UIState m_state;
            public override void Attach(UIProxyBase ProxyBase, GameObject root)
            {
                m_proxy = (P)ProxyBase;
                m_proxy.NotifyUI = OnProxyNotify;
                base.Attach(ProxyBase, root);
            }
            public override void Destroy(bool imme = true)
            {
                m_proxy.NotifyUI = null;
                base.Destroy(imme);
            }
            public P Proxy
            {
                get
                {
                    return m_proxy;
                }
            }
            public abstract void OnProxyNotify(int state);
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
                intObj.transform.SetParent(parent, false);
                ctrl.Attach(Proxy, intObj);
                ctrl.Open(data);
            }
            public void Instantiate(C ctrl, GameObject obj, Transform parent, UIDataBase data = null)
            {
                m_childs.Add(ctrl);
                var intObj = Object.Instantiate<GameObject>(obj);
                intObj.transform.SetParent(parent, false);
                ctrl.Attach(Proxy, intObj);
                ctrl.Open(data);
            }
        }
    }
}
