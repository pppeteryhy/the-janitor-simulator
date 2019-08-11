using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMessage : UIScreen {


    public override void OnInit(object[] datas)
    {
        _datas = datas;
        InitComponent();
        InitData();
        InitView();
    }
    protected override void InitComponent()
    {

    }
    protected override void InitData()
    {

    }
    protected override void InitView()
    {
        OnShow();
    }
    public override void OnShow()
    {
        this.gameObject.SetActive(true);
    }
    public override void OnHide()
    {
        OnClose();
    }
    public override void OnClose()
    {
        UIManager.Instance.RemoveMessage(this.GetType());
    }
}
