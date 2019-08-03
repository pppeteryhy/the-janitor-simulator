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
}
