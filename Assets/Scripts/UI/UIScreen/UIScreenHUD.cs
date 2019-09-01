﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIScreenHUD : UIScreen
{
    public Text txt_TimeLeft;
    public GameObject garbageNotice;
    public Text txt_garName;
    public Image selectSlotNotice;
    public GameObject[] toolSlots;
    public Image[] toolIcons;
    public Slider pickupSlider;
    public Slider CapacitySlider;

    private float totalTime;
    private float attackedNoticeTime;
    private Camera mainCam;

    protected override void InitComponent()
    {
        mainCam = Camera.main;
        EventDispatcher.Outer.AddEventListener(EventConst.EVENT_UPDATETIMELEFT, UpdateTimeLeft);
        EventDispatcher.Outer.AddEventListener(EventConst.EVENT_OnBreakPickUp, OnPlayerBreakPickup);
        EventDispatcher.Outer.AddEventListener(EventConst.EVENT_OnStartPickUp, CallPickProcess);
        EventDispatcher.Outer.AddEventListener(EventConst.EVENT_OnLockGarbage, UpdateLockingGarbage);
        EventDispatcher.Outer.AddEventListener(EventConst.EVENT_OnBreakPickUp, CancelLockTarget);
        EventDispatcher.Outer.AddEventListener(EventConst.EVENT_OnToolSwitch, OnToolSwitch);
        EventDispatcher.Outer.AddEventListener(EventConst.EVENT_OnToolUse, OnToolUse);
        EventDispatcher.Outer.AddEventListener(EventConst.EVENT_OnPickedUp, CancelLockTarget);
        EventDispatcher.Outer.AddEventListener(EventConst.EVENT_OnCapacityChanges, OnPackgaeCapacityChange);
    }

    protected override void InitData()
    {
        totalTime = ParseDataByIndex<float>(0);
        for (int i = 0; i < toolIcons.Length; i++)
        {
            toolIcons[i].sprite = ResourceLoader.Instance.Load<Sprite>(InventoryManager.Instance.toolRepo[i].iconPath);
        }
    }

    protected override void InitView()
    {
        base.InitView();
        UIUtilities.DoFadeUI(pickupSlider.gameObject, 0, 0f, Ease.InOutBack);
        CapacitySlider.maxValue = PackageManager.Instance.maxCapacity;
        garbageNotice.SetActive(false);
    }
    
    public override void OnClose()
    {
        EventDispatcher.Outer.RemoveListener(EventConst.EVENT_UPDATETIMELEFT, UpdateTimeLeft);
        EventDispatcher.Outer.RemoveListener(EventConst.EVENT_OnBreakPickUp, OnPlayerBreakPickup);
        EventDispatcher.Outer.RemoveListener(EventConst.EVENT_OnStartPickUp, CallPickProcess);
        EventDispatcher.Outer.RemoveListener(EventConst.EVENT_OnLockGarbage, UpdateLockingGarbage);
        EventDispatcher.Outer.RemoveListener(EventConst.EVENT_OnBreakPickUp, CancelLockTarget);
        EventDispatcher.Outer.RemoveListener(EventConst.EVENT_OnToolSwitch, OnToolSwitch);
        EventDispatcher.Outer.RemoveListener(EventConst.EVENT_OnToolUse, OnToolUse);
        EventDispatcher.Outer.RemoveListener(EventConst.EVENT_OnPickedUp, CancelLockTarget);
        EventDispatcher.Outer.RemoveListener(EventConst.EVENT_OnCapacityChanges, OnPackgaeCapacityChange);
        this.gameObject.SetActive(false);
    }


    private void UpdateTimeLeft(object[] data)
    {
        float timeLeft = (float) data[0];
        int t = (int) timeLeft;
        txt_TimeLeft.text = (t / 60).ToString("00") + ":" + (t % 60).ToString("00");
        txt_TimeLeft.color = Color.Lerp(Color.red, Color.green, timeLeft / totalTime);
    }

    //通用的进度条
    public void CallPickProcess(object[] data)
    {
        float duration = (float)data[0];
        Action callBack = (Action)data[1];
        print(duration);
        StartCoroutine(PickProcess(duration, callBack));
    }

    private bool isPicking;
    private IEnumerator PickProcess(float duration, Action callBack)
    {
        float t = 0;
        //初始化进度条
        UIUtilities.DoFadeUI(pickupSlider.gameObject, 1, 0f, Ease.InOutBack);
        pickupSlider.value = 0;
        isPicking = true;
        while (t < duration)
        {
            yield return null;
            t += Time.deltaTime;
            pickupSlider.value = t / duration;
            if (!isPicking)
            {
                UIUtilities.DoFadeUI(pickupSlider.gameObject, 0, 0.2f, Ease.InOutBack);
                yield break;
            }
        }

        if (callBack != null)
            callBack.Invoke();
        UIUtilities.DoFadeUI(pickupSlider.gameObject, 0, 0.2f, Ease.InOutBack);
    }
    private void OnPlayerBreakPickup(object[] data)
    {
        isPicking = false;
    }

    private void UpdateLockingGarbage(object[] data)
    {
        Vector3 gabagePos = (Vector3)data[0];
        string garName = (string)data[1];
        txt_garName.text = garName;
        garbageNotice.SetActive(true);
        Vector2 world2ScreenPos = mainCam.WorldToScreenPoint(gabagePos);
        (garbageNotice.transform as RectTransform).anchoredPosition = world2ScreenPos;
    }

    private void CancelLockTarget(object[] data)
    {
        garbageNotice.SetActive(false);
    }

    private void OnToolSwitch(object[] data)
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            Image img = toolSlots[i].GetComponent<Image>();
            if (i == InventoryManager.Instance.currentTool)
            {
                img.color = Color.green;
            }
            else
            {
                img.color = Color.white;
            }
        }
    }

    private void OnToolUse(object[] data)
    {
        int toolIndex = (int)data[0];
        float currentClean = (float)data[1];
        float maxClean = (float)data[2];
        for (int i = 0; i < toolSlots.Length; i++)
        {
            Slider slider = toolSlots[i].GetComponentInChildren<Slider>();
            if (i == toolIndex)
            {
                slider.value = currentClean / maxClean;
            }
        }
    }

    private void OnPackgaeCapacityChange(object[] datas)
    {
        int currentCap = (int)datas[0];
        CapacitySlider.value = PackageManager.Instance.maxCapacity - currentCap;
    }
}
