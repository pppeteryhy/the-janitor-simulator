using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIScreenCredits : UIScreen
{
    public Button btnMore;
    public Button btnBack;
    public RectTransform rightPanel;


    protected override void InitComponent()
    {
        btnMore.onClick.AddListener(OnMoreButtonClicked);
        btnBack.onClick.AddListener(OnBackButtonClicked);
    }

    public override void OnShow()
    {
        base.OnShow();
        rightPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
    }

    public override void OnHide()
    {
        interactableMask.raycastTarget = true;
        rightPanel.DOAnchorPos(new Vector2(1000, 0), 0.5f).onComplete = OnClose;
    }

    public override void OnClose()
    {
        base.OnClose();
        btnMore.onClick.RemoveAllListeners();
        btnBack.onClick.RemoveAllListeners();
    }

    private void OnMoreButtonClicked()
    {
        Application.OpenURL("http://pppeteryao.strikingly.com");
    }

    private void OnBackButtonClicked()
    {
        UIManager.Instance.Pop(UIDepthConst.MiddleDepth);
    }
}
