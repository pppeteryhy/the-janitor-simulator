using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolModel : Singleton<ToolModel> {

    Dictionary<int, Dictionary<string, string>> data;

    public ToolModel()
    {
        data = ToolData.Instance.data;
    }

    public string GetName(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["Name"];
        }
        return res;
    }

    public int GetMaxClean(int id)
    {
        int res = 0;
        if (data.ContainsKey(id))
        {
            res = int.Parse(data[id]["MaxClean"]);
        }
        return res;
    }

    public ToolType GetToolType(int id)
    {
        return (ToolType)Enum.Parse(typeof(ToolType), GetName(id));
    }

    public Vector3 GetPosOffset(int id)
    {
        Vector3 res = Vector3.zero;
        if (data.ContainsKey(id))
        {
            string[] strs = data[id]["PosOffset"].Split(',');
            res.x = float.Parse(strs[0]);
            res.y = float.Parse(strs[1]);
            res.z = float.Parse(strs[2]);
        }
        return res;
    }

    public Vector3 GetRotOffset(int id)
    {
        Vector3 res = Vector3.zero;
        if (data.ContainsKey(id))
        {
            string[] strs = data[id]["RotOffset"].Split(',');
            res.x = float.Parse(strs[0]);
            res.y = float.Parse(strs[1]);
            res.z = float.Parse(strs[2]);
        }
        return res;
    }

    public string GetPrefabPath(int id)
    {
        string res ="";
        if (data.ContainsKey(id))
        {
            res = data[id]["PrefabPath"];
        }
        return res;
    }

    public string GetIconPath(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["IconPath"];
        }
        return res;
    }

    public string GetAnimationTrigger(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["AnimationTrigger"];
        }
        return res;
    }
}
