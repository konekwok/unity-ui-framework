using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlTestWrap : ui.framework.UICtrlBase<UIViewTestWrap, UICtrlTestItem, UITestWrapProxy>
{
    public override void Open(ui.framework.UIDataBase data)
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
        this.Proxy.RequestContent += this.GetContent;
        base.Show();
    }
    public override void Close()
    {
        this.Proxy.RequestContent -= this.GetContent;
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
    public int GetContent()
    {
        return int.Parse(this.View.test.text);
    }
}
