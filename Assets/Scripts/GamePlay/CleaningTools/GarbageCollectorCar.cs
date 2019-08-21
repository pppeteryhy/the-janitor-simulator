using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollectorCar : MonoBehaviour, IOutline {


    private List<QuickOutline> _outlines;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitOutline()
    {
        _outlines = new List<QuickOutline>();
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0, c = meshes.Length; i < c; i++)
        {
            var outline = meshes[i].gameObject.AddComponent<QuickOutline>();
            outline.OutlineColor = GameSetting.GARBAGE_OUTLINE_COLOR;
            outline.OutlineWidth = GameSetting.GARBAGE_OUTLINE_WIDTH;
            _outlines.Add(outline);
        }
        DisableOutlineColor();
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
        return "Garbage Collector Car";
    }

    public Transform GetTransform()
    {
        if (this != null)
            return this.transform;
        return null;
    }
}
