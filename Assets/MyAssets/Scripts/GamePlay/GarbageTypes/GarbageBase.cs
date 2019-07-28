using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBase : MonoBehaviour {

    public GarbageType _garbageType;
    public int cleaningValueCost;
    public float cleaningTimeNeeded;
    public int pacCapcityCost;
    public bool needPackage;

    private List<Outline> _outlines;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _outlines = new List<Outline>();
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0, c= meshes.Length; i < c; i++)
        {
            var outline = meshes[i].gameObject.AddComponent<Outline>();
            outline.OutlineColor = GameSetting.GARBAGE_OUTLINE_COLOR;
            outline.OutlineWidth = GameSetting.GARBAGE_OUTLINE_WIDTH;
            _outlines.Add(outline);
        }
        DisableOutlineColor();
    }

    public void BeginClean()
    {
        Invoke("CleanFinished", cleaningTimeNeeded);
    }

    private void CleanFinished()
    {
        if(needPackage)
            PackageManager.Instance.CurrentCapcity += pacCapcityCost;

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
}

public enum GarbageType
{
    Paper = 0,
    Food = 1,
    ChewingGum = 2,
    Stain = 3
}
