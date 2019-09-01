using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScreenResult : UIScreen
{
    public Button btnMainMenu;
    public Button btnTryAgain;
    public GameObject[] stars;
    public Text txtTimeLeft;
    public Text txtEvaluation;
    public Text txtResultWord;

    private float timeLeft;
    private int startCount;
    private bool isWin;

    protected override void InitComponent()
    {
        base.InitComponent();
        btnMainMenu.onClick.AddListener(OnMainMenuBtnClicked);
        btnTryAgain.onClick.AddListener(OnTryAgainBtnClicked);
    }

    protected override void InitData()
    {
        base.InitData();
        timeLeft = ParseDataByIndex<float>(0);
        startCount = LevelInfoModel.Instance.GetEvaluationByTimeLeft(JSGameManager.currentLevelID, timeLeft);
        isWin = startCount == 0 ? false : true;
    }

    protected override void InitView()
    {
        base.InitView();
        for (int i = stars.Length -1 ; i >= startCount; i--)
        {
            stars[i].SetActive(false);
        }
        txtTimeLeft.text = timeLeft.ToString();
        txtEvaluation.text = isWin ? LevelInfoModel.Instance.GetLevelResultWord(JSGameManager.currentLevelID) : "Mission Failed";
        txtResultWord.text = isWin ? LevelInfoModel.Instance.GetLevelResultWord(JSGameManager.currentLevelID) : "Don't give up!";
    }

    public override void OnClose()
    {
        base.OnClose();
        btnMainMenu.onClick.RemoveAllListeners();
        btnTryAgain.onClick.RemoveAllListeners();
    }

    private void OnMainMenuBtnClicked()
    {
        UIManager.Instance.Push<UIScreenLoading>(UIDepthConst.TopDepth, true);
        SceneManager.LoadSceneAsync("MainMenuScene").completed += delegate
        {
            UIManager.Instance.PopToBottom();
        };
    }

    private void OnTryAgainBtnClicked()
    {
        UIManager.Instance.Pop(UIDepthConst.MiddleDepth);
        UIManager.Instance.Pop(UIDepthConst.TopDepth);
        UIManager.Instance.Push<UIScreenLoading>(UIDepthConst.TopDepth, true);
        SceneManager.LoadSceneAsync(LevelInfoModel.Instance.GetSceneName(JSGameManager.currentLevelID)).completed += delegate
        {
            UIManager.Instance.Pop(UIDepthConst.TopDepth);
            UIManager.Instance.Push<UIScreenHUD>(UIDepthConst.MiddleDepth, true, 120f);
            JSGameManager.Instance.LevelStart();
        };
    }
}
