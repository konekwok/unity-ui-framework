
using UnityEngine;


public class UITestItemData : ui.framework.UIDataBase
{
    public float TestVal = 0f;
    public override void Reset()
    {
        TestVal = 0f;
    }
}
public class UICtrlTestItem : ui.framework.UICtrlBase<UIViewTestItem, UITestWrapProxy>
{
    public override void Open(ui.framework.UIDataBase data)
    {
        var uidata = (UITestItemData)data;
        this.Proxy.TestItem(uidata.TestVal);
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
        //    Debug.Log("Destroy UICtrlTestItem");
        base.Destroy();
    }
    public override void OnViewNotify(int state)
    {
        Debug.LogWarning("UICtrlTestItem OnNotify");
    }
    //proxy层事件接收器
    public override void OnProxyNotify(int state)
    {
        //
    }
}
