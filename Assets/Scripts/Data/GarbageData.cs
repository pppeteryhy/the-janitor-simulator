using System.Collections.Generic;

//ID : 垃圾ID(int)
//Name : 垃圾名称(string)
//ToolNeed : 需要的清洁工具(string)
//pacCapcityCost : 占用的垃圾袋容量(string)
//cleaningValueCost : 消耗的清洁工具清洁度(string)
//cleaningTimeNeeded : 清洁需要的时间(string)
//needPackage : 是否需要扔到垃圾袋里(string)

public class GarbageData : Singleton<GarbageData>
{
    public Dictionary<int, Dictionary<string, string>> data = new Dictionary<int, Dictionary<string, string>>()
    {
        {1001, new Dictionary<string, string>(){ {"Name", "Paper"}, {"ToolNeed", "Broom"}, {"pacCapcityCost", "2"}, {"cleaningValueCost", "1"}, {"cleaningTimeNeeded", "2"}, {"needPackage", "TRUE"}, } },
        {1002, new Dictionary<string, string>(){ {"Name", "Food"}, {"ToolNeed", "Gloves&Rag"}, {"pacCapcityCost", "3"}, {"cleaningValueCost", "3"}, {"cleaningTimeNeeded", "3"}, {"needPackage", "TRUE"}, } },
        {1003, new Dictionary<string, string>(){ {"Name", "ChewingGum"}, {"ToolNeed", "Shovel"}, {"pacCapcityCost", "1"}, {"cleaningValueCost", "4"}, {"cleaningTimeNeeded", "5"}, {"needPackage", "TRUE"}, } },
        {1004, new Dictionary<string, string>(){ {"Name", "Stain"}, {"ToolNeed", "Mop&Rag"}, {"pacCapcityCost", "0"}, {"cleaningValueCost", "3"}, {"cleaningTimeNeeded", "4"}, {"needPackage", "FALSE"}, } },
    };

}
