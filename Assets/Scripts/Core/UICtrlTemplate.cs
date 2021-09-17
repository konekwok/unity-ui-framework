
// public class UITemplateData : UIDataBase
// {
//     public override void Reset()
//     {
        
//     }
// }
public class UICtrlTemplate : UICtrlBase<UIViewTemplate, UITemplateProxy>
{
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

    //View层事件接收器
    public override void OnNotify(int state)
    {
        // throw new System.NotImplementedException();
    }
}
