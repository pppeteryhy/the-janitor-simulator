using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBase : MonoBehaviour, IOutline {

    public GarbageType _garbageType;
    [HideInInspector]
    public string garbageName;
    [HideInInspector]
    public int cleaningValueCost;
    [HideInInspector]
    public float cleaningTimeNeeded;
    [HideInInspector]
    public int pacCapcityCost;
    [HideInInspector]
    public bool needPackage;
    [HideInInspector]
    public ToolType toolType;

    private List<QuickOutline> _outlines;

    private void Start()
    {
        InitOutline();
        ParseGarbageData();
    }

    private void ParseGarbageData()
    {
        garbageName = GarbageModel.Instance.GetName((int)_garbageType);
        cleaningValueCost = GarbageModel.Instance.GetCleaningValueCost((int)_garbageType);
        cleaningTimeNeeded = GarbageModel.Instance.GetCleaningValueCost((int)_garbageType);
        pacCapcityCost = GarbageModel.Instance.GetPacCapcityCost((int)_garbageType);
        needPackage = GarbageModel.Instance.GetNeedPackage((int)_garbageType);
        toolType = GarbageModel.Instance.GetToolNeed((int)_garbageType);
    }

    public void InitOutline()
    {
        _outlines = new List<QuickOutline>();
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0, c= meshes.Length; i < c; i++)
        {
            var outline = meshes[i].gameObject.AddComponent<QuickOutline>();
            outline.OutlineColor = GameSetting.GARBAGE_OUTLINE_COLOR;
            outline.OutlineWidth = GameSetting.GARBAGE_OUTLINE_WIDTH;
            _outlines.Add(outline);
        }
        DisableOutlineColor();
    }

    public void OnCleaned()
    {
        if(needPackage)
            PackageManager.Instance.CurrentCapacity += pacCapcityCost;

        Destroy(this.gameObject);
    }

    public void EnableOutlineColor()
    {
        for (int i = 0, c = _outlines.Count; i < c; i++)
        {
            _outlines[i].enabled = true;
        }
    }

    public void DisableOutlineColor()
    {
        for (int i = 0, c = _outlines.Count; i < c; i++)
        {
            _outlines[i].enabled = false;
        }
    }

    public string GetName()
    {
        if (this != null)
            return garbageName;
        return null;
    }

    public Transform GetTransform()
    {
        if(this != null)
            return this.transform;
        return null;
    }
}

public enum GarbageType
{
    Paper = 1001,
    CardboardBox = 1002,
    ChewingGum = 1003,
    Stain = 1004,
    MetalStuff = 1005,
    GarbageBag = 1006
    
}
