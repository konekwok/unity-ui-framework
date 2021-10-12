using UnityEngine.UI;

public class UIViewTest : ui.framework.UIViewBase
{
    public enum NotifyState
    {
        test,
        clicktest,
    }
    public Text test;
    void FixedUpdate()
    {
        Notify((int)NotifyState.test);
    }
    public void OnClickTest()
    {
        Notify((int)NotifyState.clicktest);
    }
}
