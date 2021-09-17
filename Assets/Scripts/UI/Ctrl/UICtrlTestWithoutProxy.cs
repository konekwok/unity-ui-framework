using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlTestWithoutProxy : UICtrlBase<UIViewTestWithoutProxy>
{
    public override void Close()
    {
        //TODO 写在最后
        OnDestroy();
    }

    public override void OnDestroy()
    {
        //TODO 写在最后
        base.Destroy();
    }

    public override void OnNotify(int state)
    {
        // throw new System.NotImplementedException();
    }

    public override void Open(UIDataBase data)
    {
        //TODO 写在最后
        Refresh();
    }

    public override void Refresh()
    {
        //TODO 写在最后
        base.Show();
    }
}
