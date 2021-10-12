using UnityEngine;

public class UITestData : ui.framework.UIDataBase
{
    public int tmpVal = 0;
    public override void Reset()
    {
        tmpVal = 0;
    }
}
public class UICtrlTest : ui.framework.UICtrlBase<UIViewTest, UITestProxy>
{
    UITestData m_uidata;
    public override void Open(ui.framework.UIDataBase data)
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
                this.View.test.text = valint.val.ToString();
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
    public override void OnViewNotify(int state)
    {
        switch((UIViewTest.NotifyState)state)
        {
            case UIViewTest.NotifyState.test:
                Debug.LogWarning("UICtrlTest OnNotify");
                break;
            case UIViewTest.NotifyState.clicktest:
                OnClickBtn();
                break;
            default:
                break;
        }
    }
    //proxy层事件接收器
    public override void OnProxyNotify(int state)
    {
        //
    }
}
