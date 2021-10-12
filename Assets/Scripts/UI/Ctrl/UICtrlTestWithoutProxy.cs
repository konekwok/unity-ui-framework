using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlTestWithoutProxy : ui.framework.UICtrlBase<UIViewTestWithoutProxy>
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

    public override void OnViewNotify(int state)
    {
        // throw new System.NotImplementedException();
    }

    public override void Open(ui.framework.UIDataBase data)
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
