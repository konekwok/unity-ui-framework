using System.Collections;
using UnityEngine;

namespace ui
{
    namespace framework
    {
        public class AsyncLoadUIBundleData
        {
            public IUICtrlBase ctrl = null;
            public bool IsFinished;
            public Object retObj;
            public void Reset()
            {
                IsFinished = false;
                retObj = null;
                ctrl = null;
            }
        }
        public abstract class UIDataBase
        {
            public abstract void Reset();
        }
        public interface IUICtrlBase
        {
            void Open(UIDataBase data);
            IEnumerator AysncOpen(UIDataBase data, AsyncLoadUIBundleData aysncData);
            void Refresh();
            void Show();
            void Hide();
            void Close();
            void OnDestroy();
            void Destroy(bool imme = true);
            void Attach(UIProxyBase ProxyBase, GameObject root);
            void OnAwake();
            UILayer Layer{get;}
            bool AndroidBackPressed();
        }
    }
}