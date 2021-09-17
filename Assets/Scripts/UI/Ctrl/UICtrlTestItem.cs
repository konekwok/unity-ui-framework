
using UnityEngine;


public class UITestItemData : UIDataBase
{
    public float TestVal = 0f;
    public override void Reset()
    {
        TestVal = 0f;
    }
}
public class UICtrlTestItem : UICtrlBase<UIViewBase, UITestWrapProxy>
{
    public override void Open(UIDataBase data)
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
   public override void OnNotify(int state)
    {
        Debug.LogWarning("UICtrlTestItem OnNotify");
    }
}
