using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenHUD : MonoBehaviour {

    public Text timeCounter;
    public float totalTime = 30.0f;
    private float currentTime = 0;

    public Image[] slots;

    //当前手持的装备
    private JanitorTool currentTool;


    private void Start()
    {
        timeCounter.text = "Time Left: ";
        currentTime = totalTime;
    }

    private void Update()
    {
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeCounter.text = "Time Left: " + (int)currentTime;
        }
        else
        {
            timeCounter.text = "GameOver";
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            int enumCount = System.Enum.GetNames(currentTool.GetType()).Length;
            currentTool = (JanitorTool)(((int)currentTool + 1) % enumCount);
            for (int i = 0; i < enumCount; i++)
            {
                if(i == (int)currentTool)
                {
                    slots[i].enabled = true;
                }
                else
                {
                    slots[i].enabled = false;
                }
            }
            
        }
    }


    public enum JanitorTool
    {
        Gloves = 0,
        Broom = 1,
        Shovel = 2
    }

}
