using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMessageInGame : UIMessage {

    public GameObject msgPanel;
    public Text msgText;
    private string msg;
    private float destroyDelay;
    private Tweener showTweener;
    private Tweener hideTweener;


    protected override void InitData()
    {
        base.InitData();
        msg = ParseDataByIndex<string>(0);
        destroyDelay = ParseDataByIndex<float>(1);
    }

    protected override void InitView()
    {
        base.InitView();
        msgText.text = msg;
        msgPanel.transform.localScale = Vector3.zero;
    }

    public override void OnShow()
    {
        base.OnShow();
        showTweener = msgPanel.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutElastic);
        showTweener.onComplete = () =>
        {
            DOVirtual.DelayedCall(destroyDelay, () =>
            {
                OnHide();
            });
        };
    }

    public override void OnHide()
    {
        hideTweener = msgPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InOutElastic);
        hideTweener.onComplete = () =>
        {
            OnClose();
        };
    }

    private void OnDestroy()
    {
        if(showTweener != null)
        {
            showTweener.Kill();
        }
        if (hideTweener != null)
        {
            hideTweener.Kill();
        }
    }

}
