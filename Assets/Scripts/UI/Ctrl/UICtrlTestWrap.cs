using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlTestWrap : UICtrlBase<UIViewTestWrap, UICtrlTestItem, UITestWrapProxy>
{
    public override void Open(UIDataBase data)
    {
        Debug.Log("open UICtrlTestWrap");
        var obj = UIManager.Instance.Load<UICtrlTestItem>();
        var uidata = this.Proxy.GetUIData<UITestItemData>();
        for(int i=0; i<10; i++)
        {
            uidata.TestVal = i;
            this.Instantiate(obj, this.View.transform, uidata);
        }
        Refresh();
    }
    public override void Refresh()
    {
        this.Proxy.RequestHeight += this.GetHeight;
        base.Show();
    }
    public override void Close()
    {
        this.Proxy.RequestHeight -= this.GetHeight;
        OnDestroy();
    }
    public override void OnDestroy()
    {
        for(int i=0; i<Childs.Count; i++)
        {
            Childs[i].Close();
        }
        Childs.Clear();
        base.Destroy();
    }
    public override void OnNotify(int state)
    {
        
    }
    public int GetHeight()
    {
        return 1000;
    }
}
