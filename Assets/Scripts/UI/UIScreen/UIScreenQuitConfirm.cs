using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenQuitConfirm : UIScreen {

    public Button btn_Confirm;
    public Button btn_Cancel;
    public Button btn_BG;


    protected override void InitComponent()
    {
        btn_Confirm.onClick.AddListener(OnConfirmBtnClicked);
        btn_Cancel.onClick.AddListener(OnCancelBtnClicked);
        btn_BG.onClick.AddListener(OnCancelBtnClicked);
    }

    public override void OnClose()
    {
        base.OnClose();
        btn_Confirm.onClick.RemoveAllListeners();
        btn_Cancel.onClick.RemoveAllListeners();
        btn_BG.onClick.RemoveAllListeners();
    }

    private void OnConfirmBtnClicked()
    {
        //TODO: 动效后退出
        Application.Quit();
    }

    private void OnCancelBtnClicked()
    {
        UIManager.Instance.Pop(UIDepthConst.TopDepth);
    }
}
