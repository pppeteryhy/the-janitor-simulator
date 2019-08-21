using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    Gloves,  //手套
    Broom,   //扫帚
    Shovel,  //铲子
    Mop,     //拖把
    Rag      //抹布
}

public class JanitorTool
{
    public int ID;
    public ToolType toolType;
    public int maxClean;
    public int currentClean;
    public Vector3 posOffset;
    public Vector3 rotOffset;
    public string prefabPath;
    public string iconPath;
    public string animTrigger;

    public JanitorTool(int _ID)
    {
        ID = _ID;
        toolType = ToolModel.Instance.GetToolType(ID);
        maxClean = ToolModel.Instance.GetMaxClean(ID);
        posOffset = ToolModel.Instance.GetPosOffset(ID);
        rotOffset = ToolModel.Instance.GetRotOffset(ID);
        prefabPath = ToolModel.Instance.GetPrefabPath(ID);
        iconPath = ToolModel.Instance.GetIconPath(ID);
        animTrigger = ToolModel.Instance.GetAnimationTrigger(ID);
        currentClean = maxClean;
    }

    //Property 属性 一个变量，要不就是设置它的值，要不就是取它的值
    public bool IsUseable 
    {
        get
        {
            return currentClean > 0;
        }
    }

    public void ResetClean()
    {
        currentClean = maxClean;
    }
}

public class InventoryManager : MonoSingleton<InventoryManager> {

    //装备管理类

    //所有清洁工具的列表
    [HideInInspector]
    public List<JanitorTool> toolRepo;
    [HideInInspector]
    public int currentTool = 0;

    public List<int> defaultTools = new List<int>();

    public void Init()
    {
        toolRepo = new List<JanitorTool>()
        {
            new JanitorTool(defaultTools[0]),
            new JanitorTool(defaultTools[1]),
            new JanitorTool(defaultTools[2])
        };
    }

    /// <summary>
    /// 外部调用接口，切换清洁工具
    /// </summary>
    public void SwitchTool()
    {
        //0->1->2->0->1->2
        currentTool++;
        currentTool = currentTool % toolRepo.Count;
        OnToolSwitch();
    }

    public void SwitchTool(int index)
    {
        currentTool = index;
        OnToolSwitch();
    }

    public void UseTool()
    {
        JanitorTool cTool = GetCurrentTool();
        cTool.currentClean--;
        EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_OnToolUse, currentTool, (float)cTool.currentClean, (float)cTool.maxClean);
    }

    private void OnToolSwitch()
    {
        EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_OnToolSwitch);
    }

    /// <summary>
    /// 外部调用接口，获得清洁工具
    /// </summary>
    /// <returns></returns>
    public JanitorTool GetCurrentTool()
    {
        return toolRepo[currentTool];
    }

    public void CleanCurrentTools()
    {
        GetCurrentTool().ResetClean();
    }
}
