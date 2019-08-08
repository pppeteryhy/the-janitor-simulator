using System.Collections.Generic;

//ID : UI窗口ID(int)
//LevelName : 窗口名称(string)
//Depth : UIDepth(string)

public class UIScreenData : Singleton<UIScreenData>
{
    public Dictionary<int, Dictionary<string, string>> data = new Dictionary<int, Dictionary<string, string>>()
    {
        {10001, new Dictionary<string, string>(){ {"LevelName", "Combo"}, {"Depth", "10001"}, } },
        {10002, new Dictionary<string, string>(){ {"LevelName", "Newaustin"}, {"Depth", "10002"}, } },
        {10003, new Dictionary<string, string>(){ {"LevelName", "Barea"}, {"Depth", "10003"}, } },
    };

}
