using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIScreenLevelSelection : UIScreen
{
    [SerializeField]
    private float circleRadius;
    [SerializeField]
    private float yOffset;
    [SerializeField]
    private Vector3 centerPoint;
    [SerializeField]
    private Vector3 axis;

    public GameObject[] BtnsLevel;
    public Button btnLeft;
    public Button btnRight;
    public Button btnClose;
    public Button btnStart;

    public Text txt_LevelName;
    public Text txt_LevelDescription;
    public Text txt_TimeLimit;

    private int currentLevelIndex = 0;

    private IEnumerator StartRotate(float t, bool left)
    {
        int c = Mathf.FloorToInt(t / Time.fixedDeltaTime);
        for (int k = 0; k < c; k++)
        {
            for (int i = 0; i < BtnsLevel.Length; i++)
            {
                BtnsLevel[i].transform.RotateAround(centerPoint, axis, (left ? 120f : -120f) /c);
                BtnsLevel[i].transform.rotation = Quaternion.identity;
            }
            yield return new WaitForFixedUpdate();
        }
        txt_LevelName.text = LevelInfoModel.Instance.GetLevelNameByIndex(currentLevelIndex);
        txt_LevelDescription.text = LevelInfoModel.Instance.GetLevelDescriptionByIndex(currentLevelIndex);
        txt_TimeLimit.text = "Time Limit: " + LevelInfoModel.Instance.GetTimeLimitByIndex(currentLevelIndex).ToString();

    }
    
    protected override void InitComponent()
    {
        btnClose.onClick.AddListener(OnClickCloseButton);
        btnStart.onClick.AddListener(OnClickStartButton);
        btnLeft.onClick.AddListener(() => OnClickLevelSelectionButton(true));
        btnRight.onClick.AddListener(() => OnClickLevelSelectionButton(false));
    }

    protected override void InitData()
    {

    }

    protected override void InitView()
    {
        base.InitView();
        ResetView();
        txt_LevelName.text = LevelInfoModel.Instance.GetLevelNameByIndex(0);
        txt_LevelDescription.text = LevelInfoModel.Instance.GetLevelDescriptionByIndex(0);
        txt_TimeLimit.text = "Time Limit: " + LevelInfoModel.Instance.GetTimeLimitByIndex(0).ToString();
    }

    public override void OnClose()
    {
        base.OnClose();
        btnClose.onClick.RemoveAllListeners();
        btnStart.onClick.RemoveAllListeners();
        btnLeft.onClick.RemoveAllListeners();
        btnRight.onClick.RemoveAllListeners();
    }

    private void OnClickLevelSelectionButton(bool left)
    {
        if (left)
        {
            if(currentLevelIndex == 0)
            {
                currentLevelIndex += BtnsLevel.Length;
            }
            currentLevelIndex--;
        }
        else
        {
            currentLevelIndex++;
            currentLevelIndex = currentLevelIndex % BtnsLevel.Length;
        }

        StartCoroutine(StartRotate(0.5f, left));
    }

    private void OnClickStartButton()
    {
        UIManager.Instance.Pop(UIDepthConst.MiddleDepth);
        UIManager.Instance.Pop(UIDepthConst.BottomDepth);
        UIManager.Instance.Push<UIScreenLoading>(UIDepthConst.TopDepth, true);
        JSGameManager.currentLevelID = LevelInfoModel.Instance.GetIdByIndex(currentLevelIndex);
        SceneManager.LoadSceneAsync(LevelInfoModel.Instance.GetSceneNameByIndex(currentLevelIndex)).completed += delegate
        {
            UIManager.Instance.Pop(UIDepthConst.TopDepth);
            UIManager.Instance.Push<UIScreenHUD>(UIDepthConst.MiddleDepth, true, LevelInfoModel.Instance.GetTimeLimit(JSGameManager.currentLevelID));
            JSGameManager.Instance.LevelStart();
        };
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.Pop(UIDepthConst.MiddleDepth);
    }

    [ContextMenu("Reset View")]
    public void ResetView()
    {
        float zOffset = Mathf.Sqrt(Mathf.Pow(circleRadius, 2) - Mathf.Pow(yOffset, 2));
        centerPoint = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z + zOffset);
        axis = Vector3.Cross(Vector3.right, transform.position - centerPoint).normalized;
        Debug.DrawRay(centerPoint, Vector3.right, Color.blue, 20f);
        Debug.DrawRay(centerPoint, transform.position - centerPoint, Color.blue, 20f);
        Debug.DrawRay(centerPoint, axis, Color.white, 20f);
        for (int i = 0; i < BtnsLevel.Length; i++)
        {
            BtnsLevel[i].transform.RotateAround(centerPoint, axis, 120 * i);
            BtnsLevel[i].transform.rotation = Quaternion.identity;
            BtnsLevel[i].transform.Find("Mask/Img_ScreenShot").GetComponent<Image>().sprite = ResourceLoader.Instance.Load<Sprite>(LevelInfoModel.Instance.GetLevelScreenShotByIndex(i));
        }
    }
}
