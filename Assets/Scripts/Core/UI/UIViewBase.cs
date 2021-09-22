using System;
using UnityEngine;

namespace ui
{
    namespace framework
    {
        public enum UILayer
        {
            Main,
            Back,
            Top,
            DynamicMain,
        }
        public class UIViewBase : MonoBehaviour
        {
            public int zOrder = 0;
            public Action<int> Notify;
            public UILayer uiLayer;
            public void OnAwake()
            {
                RectTransform rect = this.transform as RectTransform;
                rect.localPosition = Vector3.zero;
                rect.localScale = Vector3.one;
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;
                transform.SetSiblingIndex(zOrder);
            }
        }
    }
}
