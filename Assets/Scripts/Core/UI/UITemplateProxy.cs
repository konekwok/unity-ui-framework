
using System;

public class UITemplateProxy : ui.framework.UIProxyBase
{
    public UITemplateProxy()
    {
        //如果需要在打开UI时，通过外部给UI传递数据，请实现下现相关逻辑
        // m_uidatas = new Dictionary<string, ui.framework.UIDataBase>();
        // RegisterUIData<UITemplateData>();
        
        //add event
    }
    //和别的ui Proxy进行通信，发起方
    public override void Request<D>(int sessionId, Action<int, D> action)
    {
        
    }
    //和别的ui Proxy建立连接， 响应方
    public override void Respond<D>(int sessionId, Action<int, D> action)
    {
        
    }
}
