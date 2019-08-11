using System.Collections.Generic;

//ID : 工具ID(int)
//Name : 工具名称(string)
//MaxClean : 最大清洁度(string)
//PosOffset : 绑定到手上的位置偏移(string)
//RotOffset : 旋转偏移(string)
//PrefabPath : 预制体路径(string)
//IconPath : 图标路径(string)
//AnimationTrigger : 清洁动画(string)

public class ToolData : Singleton<ToolData>
{
    public Dictionary<int, Dictionary<string, string>> data = new Dictionary<int, Dictionary<string, string>>()
    {
        {1001, new Dictionary<string, string>(){ {"Name", "Gloves"}, {"MaxClean", "8"}, {"PosOffset", "0,0,0"}, {"RotOffset", "0,0,0"}, {"PrefabPath", "CleanTools/CleanTools_Gloves"}, {"IconPath", "GetPrefabPath"}, {"AnimationTrigger", "Clean2"}, } },
        {1002, new Dictionary<string, string>(){ {"Name", "Broom"}, {"MaxClean", "5"}, {"PosOffset", "0.121,-0.072,0"}, {"RotOffset", "0,0,0"}, {"PrefabPath", "CleanTools/CleanTools_Broom"}, {"IconPath", "GetPrefabPath"}, {"AnimationTrigger", "Clean"}, } },
        {1003, new Dictionary<string, string>(){ {"Name", "Shovel"}, {"MaxClean", "2"}, {"PosOffset", "0,0,0"}, {"RotOffset", "0,0,0"}, {"PrefabPath", "CleanTools/CleanTools_Shovel"}, {"IconPath", "GetPrefabPath"}, {"AnimationTrigger", "Clean"}, } },
        {1004, new Dictionary<string, string>(){ {"Name", "Mop"}, {"MaxClean", "3"}, {"PosOffset", "0.121,-0.072,0"}, {"RotOffset", "0,0,0"}, {"PrefabPath", "CleanTools/CleanTools_Mop"}, {"IconPath", "GetPrefabPath"}, {"AnimationTrigger", "Clean"}, } },
        {1005, new Dictionary<string, string>(){ {"Name", "Rag"}, {"MaxClean", "4"}, {"PosOffset", "0,0,0"}, {"RotOffset", "0,0,0"}, {"PrefabPath", "CleanTools/CleanTools_Rag"}, {"IconPath", "GetPrefabPath"}, {"AnimationTrigger", "Clean3"}, } },
    };

}
