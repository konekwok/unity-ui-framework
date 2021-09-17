using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestProxy : UIProxyBase
{
    public UITestProxy()
    {
        m_uidatas = new Dictionary<string, UIDataBase>();
        RegisterUIData<UITestData>();
        //add event
    }
    public void Test()
    {
        Debug.Log("uitestproxy test!!!");
    }
    public void TestNotifyUI()
    {
        
    }
}
