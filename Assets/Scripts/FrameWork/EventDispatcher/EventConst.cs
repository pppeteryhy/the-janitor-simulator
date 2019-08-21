using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventConst
{
    //< 在关卡界面选择关卡时
    public const string EVENT_LEVELSELECT = "OnLevelSelect";
    //< 剩余时间更新时
    public const string EVENT_UPDATETIMELEFT = "UpdateTimeLeft";
    //< 关卡输了
    public const string EVENT_GAMELOSE = "GameLose";
    //< 关卡通过
    public const string EVENT_LEVELPASS = "LevelPass";


    //< 当玩家开始捡垃圾时
    public const string EVENT_OnStartPickUp = "StartPickUp";
    //< 当玩家打断捡垃圾进程时
    public const string EVENT_OnBreakPickUp = "BreakPickUp";
    //<玩家检索到垃圾时
    public const string EVENT_OnLockGarbage = "LockGarbagePos";
    //<玩家捡起垃圾时
    public const string EVENT_OnPickedUp = "PickedUp";
    //<当玩家切换状态时
    public const string EVENT_OnToolSwitch = "SwitchTool";
    //<当玩家使用清洁工具时
    public const string EVENT_OnToolUse = "UseTool";
    //<当玩家的垃圾袋清洁度发生变化时
    public const string EVENT_OnCapacityChanges = "PackageCapacityChange";
}
