using UnityEngine;
public abstract class UIDataBase
{
    public abstract void Reset();
}
public interface IUICtrlBase
{
    void Open(UIDataBase data);
    void Refresh();
    void Show();
    void Hide();
    void Close();
    void OnDestroy();
    void Destroy(bool imme = true);
    void Init(UIProxyBase ProxyBase, GameObject root);
}
