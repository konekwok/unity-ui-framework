
// public class UITemplateData : ui.framework.UIDataBase
// {
//     public override void Reset()
//     {
        
//     }
// }
public class UICtrlTemplate : ui.framework.UICtrlBase<UIViewTemplate, UITemplateProxy>
{
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
    public override void OnViewNotify(int state)
    {
        // switch((UIViewTemplate.ViewNotify)state)
        // {
        //     case UIViewTemplate.ViewNotify.none:
        //         break;
        //     default:   
        //         break;
        // }
    }
    //proxy层事件接收器
    public override void OnProxyNotify(int state)
    {
        // switch((UITemplateProxy.ProxyNotify)state)
        // {
        //     case UITemplateProxy.ProxyNotify.none:
        //         break;
        //     default:
        //         break;
        // }
    }
}
