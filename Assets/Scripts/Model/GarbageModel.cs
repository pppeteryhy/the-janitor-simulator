using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageModel : Singleton<GarbageModel> {

    Dictionary<int, Dictionary<string, string>> data;

    public GarbageModel()
    {
        data = GarbageData.Instance.data;
    }

    public string GetName(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res =  data[id]["Name"];
        }
        return res;
    }

    public ToolType GetToolNeed(int id)
    {
        ToolType res = ToolType.Broom;
        if (data.ContainsKey(id))
        {
            res = (ToolType)Enum.Parse(typeof(ToolType), data[id]["ToolNeed"]);
        }
        return res;
    }

    public int GetPacCapcityCost(int id)
    {
        int res = 0;
        if (data.ContainsKey(id))
        {
            res = int.Parse(data[id]["pacCapcityCost"]);
        }
        return res;
    }

    public int GetCleaningValueCost(int id)
    {
        int res = 0;
        if (data.ContainsKey(id))
        {
            res = int.Parse(data[id]["cleaningValueCost"]);
        }
        return res;
    }

    public float GetCleaningTimeNeeded(int id)
    {
        float res = 0;
        if (data.ContainsKey(id))
        {
            res = float.Parse(data[id]["cleaningTimeNeeded"]);
        }
        return res;
    }

    public bool GetNeedPackage(int id)
    {
        bool res = false;
        if (data.ContainsKey(id))
        {
            res = bool.Parse(data[id]["needPackage"]);
        }
        return res;
    }
}
