using System;
using UnityEngine;

namespace ui
{
    namespace framework
    {
        public class UIViewBase : MonoBehaviour
        {
            public Action<int> Notify;
        }
    }
}
