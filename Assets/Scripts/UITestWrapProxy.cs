using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestWrapProxy : UIProxyBase
{
    // Start is called before the first frame update
    public UITestWrapProxy()
    {
        m_uidatas = new Dictionary<string, UIDataBase>();
        RegisterUIData<UITestItemData>();
    }
    public void TestItem(float val)
    {
        Debug.Log("uitestwrapproxy testitem!!!"+val);
    }
}
