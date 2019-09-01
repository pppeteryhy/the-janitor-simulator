using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSGameManager : MonoSingleton<JSGameManager> {

    public static int currentLevelID;
    public GameState gameState = GameState.End;

    private float timeLeft;

    private int garbageCountLeft;
    public int GarbageCountLeft
    {
        get
        {
            return garbageCountLeft;
        }
        set
        {
            garbageCountLeft = value;
            if(garbageCountLeft == 0)
            {
                //win the game!
                gameState = GameState.End;
                UIManager.Instance.Push<UIScreenResult>(UIDepthConst.TopDepth, true, timeLeft);
            }
        }
    }

    public void LevelStart()
    {
        StartCoroutine(Timer(LevelInfoModel.Instance.GetTimeLimit(currentLevelID)));
        gameState = GameState.InGame;
        garbageCountLeft = GameObject.FindObjectsOfType<GarbageBase>().Length;
    }

    private IEnumerator Timer(float t)
    {
        float tmp = t;
        while(tmp > 0)
        {
            yield return null;
            tmp -= Time.deltaTime;
            timeLeft = tmp;
            EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_UPDATETIMELEFT, tmp);
        }
        gameState = GameState.End;
        //lose the game
        UIManager.Instance.Push<UIScreenResult>(UIDepthConst.TopDepth, true, 0f);
    }

    private void Update()
    {
        if (gameState == GameState.InGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

public enum GameState
{
    InGame = 0,
    Pause = 1,
    End = 2
}

