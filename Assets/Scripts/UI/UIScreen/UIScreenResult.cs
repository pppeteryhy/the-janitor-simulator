using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScreenResult : UIScreen
{
    public Button btnMainMenu;
    public Button btnTryAgain;
    public Button btnNextLevel;
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
        btnNextLevel.onClick.AddListener(OnNextLevelBtnClicked);
    }

    protected override void InitData()
    {
        base.InitData();
        timeLeft = ParseDataByIndex<float>(1);
        startCount = LevelInfoModel.Instance.GetEvaluationByTimeLeft(JSGameManager.currentLevelID, timeLeft);
        isWin = startCount == 0 ? false : true;
    }

    protected override void InitView()
    {
        base.InitView();
        for (int i = stars.Length; i > startCount; i--)
        {
            stars[i].SetActive(false);
        }
        txtTimeLeft.text = timeLeft.ToString();
        txtEvaluation.text = LevelInfoModel.Instance.GetLevelResultWord(JSGameManager.currentLevelID);
        txtResultWord.text = LevelInfoModel.Instance.GetLevelResultWord(JSGameManager.currentLevelID);
    }

    public override void OnClose()
    {
        base.OnClose();
        btnMainMenu.onClick.RemoveAllListeners();
        btnTryAgain.onClick.RemoveAllListeners();
        btnNextLevel.onClick.RemoveAllListeners();
    }

    private void OnMainMenuBtnClicked()
    {

    }

    private void OnTryAgainBtnClicked()
    {

    }

    private void OnNextLevelBtnClicked()
    {

    }
}
