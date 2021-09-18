using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewTest : UIViewBase
{
    void FixedUpdate()
    {
        // Notify(-1);
    }
    public void OnClickTest()
    {
        Notify(1);
    }
}
