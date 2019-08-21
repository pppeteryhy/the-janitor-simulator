﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelInfoModel : Singleton<LevelInfoModel> {

    public Dictionary<int, Dictionary<string, string>> data;

    public LevelInfoModel()
    {
        data = LevelInfoData.Instance.data;
    }

    public int GetIdByIndex(int index)
    {
        List<int> ids = data.Keys.ToList();
        if(index < ids.Count)
        {
            return ids[index];
        }
        else
        {
            Debug.LogError("按钮数量超过了关卡数量！请重新配置");
            return -1;
        }

    }

    ///<<<<<ID
    public string GetLevelName(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["LevelName"];
        }
        return res;
    }

    public string GetSceneName(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["SceneName"];
        }
        return res;
    }

    public string GetLevelDescription(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["Description"];
        }
        return res;
    }

    public float GetTimeLimit(int id)
    {
        float res = 0;
        if (data.ContainsKey(id))
        {
            res = float.Parse(data[id]["TimeLimit"]);
        }
        return res;
    }

    public string GetLevelScreenShot(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["ScreenShot"];
        }
        return res;
    }

    public string GetUnlockLevel(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["UnLockLevel"];
        }
        return res;
    }

    public string GetLevelResultWord(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["ResultWord"];
        }
        return res;
    }

    public int GetEvaluationByTimeLeft(int id, float timeLeft)
    {
        int res = 0;
        if (data.ContainsKey(id))
        {
            string[] strs = data[id]["TimeLeftForEvaluation"].Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
                if(timeLeft > float.Parse(strs[i]))
                {
                    return 3 - i;
                }
            }
        }
        return res;
    }
    

    ///<<<<<INDEX
    public string GetLevelNameByIndex(int index)
    {
        int id = GetIdByIndex(index);
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["LevelName"];
        }
        return res;
    }

    public string GetSceneNameByIndex(int index)
    {
        string res = "";
        int id = GetIdByIndex(index);
        if (data.ContainsKey(id))
        {
            res = data[id]["SceneName"];
        }
        return res;
    }

    public string GetLevelDescriptionByIndex(int index)
    {
        int id = GetIdByIndex(index);
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["Description"];
        }
        return res;
    }

    public float GetTimeLimitByIndex(int index)
    {
        int id = GetIdByIndex(index);
        float res = 0;
        if (data.ContainsKey(id))
        {
            res = float.Parse(data[id]["TimeLimit"]);
        }
        return res;
    }

    public string GetLevelScreenShotByIndex(int index)
    {
        int id = GetIdByIndex(index);
        return GetLevelScreenShot(id);
    }

    public string GetLevelResultWordByIndex(int index)
    {
        int id = GetIdByIndex(index);
        return GetLevelResultWord(id);
    }
}
