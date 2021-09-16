﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        _ = UIManager.Instance;
    }
    void Start()
    {
        UIManager.Instance.OnStart();
        var data = UIManager.Instance.GetProxy<UITestProxy>().GetUIData<UITestData>();
        data.tmpVal = 2;
        UIManager.Instance.OpenUI<UICtrlTest>(data);
        UIManager.Instance.OpenUI<UICtrlTestWrap>();
    }
    public void OnClickBtn()
    {
        UIManager.Instance.CloseUI<UICtrlTest>();
        UIManager.Instance.CloseUI<UICtrlTestWrap>();
    }
}