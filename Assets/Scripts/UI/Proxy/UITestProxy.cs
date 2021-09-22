using System;
using System.Collections.Generic;
using UnityEngine;

public class UITestProxy : ui.framework.UIProxyBase
{
    public UITestProxy()
    {
        m_uidatas = new Dictionary<string, ui.framework.UIDataBase>();
        RegisterUIData<UITestData>();
        //add event
    }
    public void Test()
    {
        Debug.Log("uitestproxy test!!!");
    }
    public override void Request<D>(int sessionId, Action<int, D> action)
    {
        UIManager.Instance.Conversation<UITestWrapProxy, D>(sessionId, action);
    }
    public void TestNotifyUI()
    {
        
    }
}
