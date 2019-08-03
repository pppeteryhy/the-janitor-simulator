using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIScreenLevelSelection : UIScreen
{
    public Button[] BtnsLevel;
    public Button btnClose;
    public Button btnStart;

    public Text txt_LevelName;
    public Text txt_LevelDescription;
    public Text txt_TimeLimit;

    protected override void InitComponent()
    {

    }

    protected override void InitData()
    {

    }

    protected override void InitView()
    {
        OnClickLevelSelectionButton(0);
    }

    public override void OnClose()
    {

    }

    public override void OnHide()
    {

    }

    public override void OnShow()
    {

    }

    private void OnClickLevelSelectionButton(int index)
    {
        for (int i = 0, c = BtnsLevel.Length; i < c; i++)
        {
            if(i == index)
                BtnsLevel[i].transform.localScale = Vector3.one * 1.4f;
            else
                BtnsLevel[i].transform.localScale = Vector3.one;
        }
       
    }

    private void OnClickStartButton()
    {
        UIManager.Instance.Pop(UIDepthConst.MiddleDepth);
        UIManager.Instance.Pop(UIDepthConst.BottomDepth);
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.Pop(UIDepthConst.MiddleDepth);
    }

}
