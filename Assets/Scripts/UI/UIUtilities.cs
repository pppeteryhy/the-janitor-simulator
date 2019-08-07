using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIUtilities {

    //UI的淡入淡出效果
    public static void DoFadeUI(GameObject uiObj, float endValue, float duration, Ease ease)
    {
        Image[] imgs = uiObj.GetComponentsInChildren<Image>();
        for (int i = 0; i < imgs.Length; i++)
        {
            imgs[i].DOFade(endValue, duration).SetEase(ease);
        }
        Text[] txts = uiObj.GetComponentsInChildren<Text>();
        for (int i = 0; i < txts.Length; i++)
        {
            txts[i].DOFade(endValue, duration).SetEase(ease);
        }
    }

}
