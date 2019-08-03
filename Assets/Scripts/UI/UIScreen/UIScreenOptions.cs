﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIScreenOptions : UIScreen
{
    public Slider slider_musicVolume;
    public Slider slider_soundVolume;
    public Button btn_Back;
    public Toggle toggle_MusicOn;
    public RectTransform rightPanel;

    protected override void InitComponent()
    {
        slider_musicVolume.onValueChanged.AddListener(OnSliderMusicColumeChanged);
        slider_soundVolume.onValueChanged.AddListener(OnSliderSoundColumeChanged);
        toggle_MusicOn.onValueChanged.AddListener(OnMusicOnToggled);
        btn_Back.onClick.AddListener(OnBtnBackChanged);
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
        slider_musicVolume.onValueChanged.RemoveAllListeners();
        slider_soundVolume.onValueChanged.RemoveAllListeners();
        toggle_MusicOn.onValueChanged.RemoveAllListeners();
        btn_Back.onClick.RemoveAllListeners();
        base.OnClose();
    }

    private void OnSliderMusicColumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    private void OnSliderSoundColumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }

    private void OnMusicOnToggled(bool value)
    {
        AudioManager.Instance.MuteAudio(value);
    }

    private void OnBtnBackChanged()
    {
        UIManager.Instance.Pop(UIDepthConst.MiddleDepth);
    }
}
