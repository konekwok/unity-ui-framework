using System;
using System.Collections.Generic;
using UnityEngine;

public class UITestWrapProxy : ui.framework.UIProxyBase
{
    // Start is called before the first frame update
    public delegate int RequestContentHandler();
    public event RequestContentHandler RequestContent;
    public UITestWrapProxy()
    {
        m_uidatas = new Dictionary<string, ui.framework.UIDataBase>();
        RegisterUIData<UITestItemData>();
    }
    public void TestItem(float val)
    {
        Debug.Log("uitestwrapproxy testitem!!!"+val);
    }
    public override void Respond<D>(int sessionId, Action<int, D> action)
    {
        var session = (SessionRoute)(sessionId);
        switch(session)
        {
            case SessionRoute.TestWraprequestIntVal:
                D val = (D)GetHeight(sessionId);
                action.Invoke(sessionId, val);
                break;
            default:
                break;
        }
    }
    public SessionContent GetHeight(int sessionId)
    {
        int val = RequestContent();
        var session = Fetch<SessionTest>(sessionId);
        session.val = val;
        return session;
    }
}
