using System;
using System.Collections.Generic;


public class EventDispatcher
{
    public abstract class IListener
    {
        // public virtual void Do(){}
        // public virtual void Do<T>(T t1){}
        // public virtual void Do<T1,T2>(T1 t1, T2 t2){}
        // public virtual void Do<T1,T2,T3>(T1 t1, T2 t2, T3 t3){}
        // public virtual void Do<T1,T2,T3,T4>(T1 t1, T2 t2, T3 t3, T4 t4){}
    }
    public class Listener:IListener
    {
        public Action handler;
        public Listener(Action cb)
        {
            handler = cb;
        }
        public void Do()
        {
            handler?.Invoke();
        }
    }
    public class Listener<T> : IListener
    {
        public Action<T> handler;
        public Listener(Action<T> cb)
        {
            handler = cb;
        }
        public void Do(T t1)
        {
            handler.Invoke(t1);
        }
    }
    public class Listener<T1, T2> : IListener
    {
        public Action<T1, T2> handler;
        public Listener(Action<T1, T2> cb)
        {
            handler = cb;
        }
        public void Do(T1 t1, T2 t2)
        {
            handler.Invoke(t1, t2);
        }
    }
    public class Listener<T1, T2, T3> : IListener
    {
        public Action<T1, T2, T3> handler;
        public Listener(Action<T1, T2, T3> cb)
        {
            handler = cb;
        }
        public void Do(T1 t1, T2 t2, T3 t3)
        {
            handler.Invoke(t1, t2, t3);
        }
    }
    public class Listener<T1, T2, T3, T4> : IListener
    {
        public Action<T1, T2, T3, T4> handler;
        public Listener(Action<T1, T2, T3, T4> cb)
        {
            handler = cb;
        }
        public void Do(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            handler.Invoke(t1, t2, t3, t4);
        }
    }
    static Dictionary<string, List<IListener>> s_listeners = new Dictionary<string, List<IListener>>();
    public static void AddListener(string eventname, Action handler)
    {
        List<IListener> lst;
        if(!s_listeners.TryGetValue(eventname, out lst))
        {
            lst = new List<IListener>();
            s_listeners.Add(eventname, lst);
        }
#if !APP_RELEASE
        else
        {
            for (int i = 0; i < lst.Count; i++)
            {
                var listener = (Listener)lst[i];
                if(listener.handler == handler)
                {
                    UnityEngine.Debug.LogError("duplicate eventname:" + eventname + " funcname:" + handler.Method.Name);
                    return;
                }
            }
        }
#endif
        var lis = new Listener(handler);
        lst.Add(lis);
    }
    public static void RemoveListener(string eventname, Action handler)
    {
        List<IListener> list;
        if(!s_listeners.TryGetValue(eventname, out list))
        {
            return;
        }
        for(int i=0;i<list.Count;i++)
        {
            var listener = (Listener)list[i];
            if(listener.handler == handler)
            {
                list.RemoveAt(i);
                return;
            }
        }
    }
    public static void Notify(string eventname)
    {
        List<IListener> lst;
        if(!s_listeners.TryGetValue(eventname, out lst))
        {
            return;
        }
        for(int i = lst.Count - 1; i >= 0 && i< lst.Count; i--)
        {
            IListener pair = lst[i];
            try
            {
                ((Listener)pair).Do();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("Event::notify: event=" + eventname + "\n" + e.ToString());
            }
        }
    }
    public static void AddListener<T1>(string eventname, Action<T1> handler)
    {
        List<IListener> lst;
        if(!s_listeners.TryGetValue(eventname, out lst))
        {
            lst = new List<IListener>();
            s_listeners.Add(eventname, lst);
        }
#if !APP_RELEASE
        else
        {
            for (int i = 0; i < lst.Count; i++)
            {
                var listener = (Listener<T1>)lst[i];
                if(listener.handler == handler)
                {
                    UnityEngine.Debug.LogError("duplicate eventname:" + eventname + " funcname:" + handler.Method.Name);
                    return;
                }
            }
        }
#endif
        var lis = new Listener<T1>(handler);
        lst.Add(lis);
        // AddListener(eventname, handler.Target, handler.Method.Name);
    }
    public static void RemoveListener<T>(string eventname, Action<T> handler)
    {
        List<IListener> list;
        if(!s_listeners.TryGetValue(eventname, out list))
        {
            return;
        }
        for(int i=0;i<list.Count;i++)
        {
            var listener = (Listener<T>)list[i];
            if(listener.handler == handler)
            {
                list.RemoveAt(i);
                return;
            }
        }
    }
    public static void Notify<T>(string eventname, T param)
    {
        List<IListener> lst;
        if(!s_listeners.TryGetValue(eventname, out lst))
        {
            return;
        }
        for(int i = lst.Count - 1; i >= 0 && i< lst.Count; i--)
        {
            IListener pair = lst[i];
            try
            {
                ((Listener<T>)pair).Do(param);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("Event::notify: event=" + eventname + "\n" + e.ToString());
            }
        }
    }
    public static void AddListener<T1, T2>(string eventname, Action<T1, T2> handler)
    {
        List<IListener> lst;
        if(!s_listeners.TryGetValue(eventname, out lst))
        {
            lst = new List<IListener>();
            s_listeners.Add(eventname, lst);
        }
#if !APP_RELEASE
        else
        {
            for (int i = 0; i < lst.Count; i++)
            {
                var listener = (Listener<T1,T2>)lst[i];
                if (listener.handler == handler)
                {
                    UnityEngine.Debug.LogError("duplicate eventname:" + eventname + " funcname:" + handler.Method.Name);
                    return;
                }
            }
        }
#endif
        var lis = new Listener<T1, T2>(handler);
        lst.Add(lis);
    }
    public static void RemoveListener<T1, T2>(string eventname, Action<T1, T2> handler)
    {
        List<IListener> list;
        if(!s_listeners.TryGetValue(eventname, out list))
        {
            return;
        }
        for(int i=0;i<list.Count;i++)
        {
            var listener = (Listener<T1,T2>)list[i];
            if (listener.handler == handler)
            {
                list.RemoveAt(i);
                return;
            }
        }
    }
    public static void Notify<T1, T2>(string eventname, T1 param01, T2 param02)
    {
        List<IListener> lst;
        if(!s_listeners.TryGetValue(eventname, out lst))
        {
            return;
        }
        for(int i = lst.Count - 1; i >= 0 && i< lst.Count; i--)
        {
            IListener pair = lst[i];
            try
            {
                ((Listener<T1, T2>)pair).Do(param01, param02);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("Event::notify: event=" + eventname + "\n" + e.ToString());
            }
        }
    }
    public static void AddListener<T1, T2, T3>(string eventname, Action<T1, T2, T3> handler)
    {
        List<IListener> lst;
        if(!s_listeners.TryGetValue(eventname, out lst))
        {
            lst = new List<IListener>();
            s_listeners.Add(eventname, lst);
        }
#if !APP_RELEASE
        else
        {
            for (int i = 0; i < lst.Count; i++)
            {
                var listener = (Listener<T1,T2,T3>)lst[i];
                if (listener.handler == handler)
                {
                    UnityEngine.Debug.LogError("duplicate eventname:" + eventname + " funcname:" + handler.Method.Name);
                    return;
                }
            }
        }
#endif
        var lis = new Listener<T1, T2, T3>(handler);
        lst.Add(lis);
    }
    public static void RemoveListener<T1, T2, T3>(string eventname, Action<T1, T2, T3> handler)
    {
        List<IListener> list;
        if(!s_listeners.TryGetValue(eventname, out list))
        {
            return;
        }
        for(int i=0;i<list.Count;i++)
        {
            var listener = (Listener<T1,T2,T3>)list[i];
            if (listener.handler == handler)
            {
                list.RemoveAt(i);
                return;
            }
        }
    }
    public static void Notify<T1, T2, T3>(string eventname, T1 param01, T2 param02, T3 param03)
    {
        List<IListener> lst;
        if(!s_listeners.TryGetValue(eventname, out lst))
        {
            return;
        }
        for(int i = lst.Count - 1; i >= 0 && i< lst.Count; i--)
        {
            IListener pair = lst[i];
            try
            {
                ((Listener<T1, T2, T3>)pair).Do(param01, param02, param03);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("Event::notify: event=" + eventname + "\n" + e.ToString());
            }
        }
    }
    public static void AddListener<T1, T2, T3, T4>(string eventname, Action<T1, T2, T3, T4> handler)
    {
        List<IListener> lst;
        if(!s_listeners.TryGetValue(eventname, out lst))
        {
            lst = new List<IListener>();
            s_listeners.Add(eventname, lst);
        }
#if !APP_RELEASE
        else
        {
            for (int i = 0; i < lst.Count; i++)
            {
                var listener = (Listener<T1,T2,T3,T4>)lst[i];
                if (listener.handler == handler)
                {
                    UnityEngine.Debug.LogError("duplicate eventname:" + eventname + " funcname:" + handler.Method.Name);
                    return;
                }
            }
        }
#endif
        var lis = new Listener<T1, T2, T3, T4>(handler);
        lst.Add(lis);
    }
    public static void RemoveListener<T1, T2, T3, T4>(string eventname, Action<T1, T2, T3, T4> handler)
    {
        List<IListener> list;
        if(!s_listeners.TryGetValue(eventname, out list))
        {
            return;
        }
        for(int i=0;i<list.Count;i++)
        {
            var listener = (Listener<T1,T2,T3,T4>)list[i];
            if (listener.handler == handler)
            {
                list.RemoveAt(i);
                return;
            }
        }
    }
    public static void Notify<T1, T2, T3, T4>(string eventname, T1 param01, T2 param02, T3 param03, T4 param04)
    {
        List<IListener> lst;
        if(!s_listeners.TryGetValue(eventname, out lst))
        {
            return;
        }
        for(int i = lst.Count - 1; i >= 0 && i< lst.Count; i--)
        {
            IListener pair = lst[i];
            try
            {
                ((Listener<T1, T2, T3, T4>)pair).Do(param01, param02, param03, param04);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("Event::notify: event=" + eventname + "\n" + e.ToString());
            }
        }
    }
}
