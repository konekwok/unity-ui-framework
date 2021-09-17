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
        Debug.LogWarning("UICtrlTest OnNotify");
    }
}
