using UnityEngine.UI;

public class UIViewTest : ui.framework.UIViewBase
{
    public Text test;
    void FixedUpdate()
    {
        Notify(-1);
    }
    public void OnClickTest()
    {
        Notify(1);
    }
}
