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
    public ToolType toolType;
    public int maxClean;
    public int currentClean;

    public JanitorTool(ToolType _toolType, int _maxClean)
    {
        toolType = _toolType;
        maxClean = _maxClean;
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
    public List<JanitorTool> toolRepo;
    [HideInInspector]
    public int currentTool = 0;

    public void Init()
    {
        toolRepo = new List<JanitorTool>()
        {
            new JanitorTool(ToolType.Broom, 5),
            new JanitorTool(ToolType.Rag, 8),
            new JanitorTool(ToolType.Shovel, 2)
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
}
