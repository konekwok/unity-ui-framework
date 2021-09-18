using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestData : UIDataBase
{
    public int tmpVal = 0;
    public override void Reset()
    {
        tmpVal = 0;
    }
}
public class UICtrlTest : UICtrlBase<UIViewTest, UITestProxy>
{
    UITestData m_uidata;
    public override void Open(UIDataBase data)
    {
        this.Proxy.Test();
        m_uidata = (UITestData)data;
        Debug.Log("UICtrlTest uidata:"+m_uidata.tmpVal);
        Refresh();
    }
    public void OnClickBtn()
    {
        this.Proxy.Request<SessionContent>((int)SessionRoute.TestWraprequestIntVal, this.OnReceived);
    }
    public override void OnReceived(int sessionId, SessionContent val)
    {
        var session = (SessionRoute)(sessionId);
        switch(session)
        {
            case SessionRoute.TestWraprequestIntVal:
                var valint = (SessionTest)val;
                Debug.Log("OnReceived:"+valint.val);
                break;
            default:
                break;
        }
    }
    public override void Refresh()
    {
        base.Show();
    }
    public override void Close()
    {
        OnDestroy();
    }
    public override void OnDestroy()
    {
        base.Destroy();
    }
    public override void OnNotify(int state)
    {
        if(state == -1)
            Debug.LogWarning("UICtrlTest OnNotify");
        else if(state == 1)
            OnClickBtn();
    }
}
