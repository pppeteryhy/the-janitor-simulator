using System.Collections.Generic;

//ID : 关卡ID(int)
//LevelName : 关卡名称(string)
//SceneName : 场景名称(string)
//Description : 关卡描述(string)
//TimeLimit : 通关限定时间（秒）(float)
//ScreenShot : 关卡封面路径(string)
//UnLockLevel : 解锁关卡(string)
//TimeLeftForEvaluation : 获得不同星级的评价所需要的剩余时间(string)

public class LevelInfoData : Singleton<LevelInfoData>
{
    public Dictionary<int, Dictionary<string, string>> data = new Dictionary<int, Dictionary<string, string>>()
    {
        {10001, new Dictionary<string, string>(){ {"LevelName", "PlayerGround"}, {"SceneName", "Level1"}, {"Description", "The enemy from the east declared war to the republic few days ago. Although the government promised that Combo city would not be occupied, the city meets the invaders soon after the rout of the troops at the front line."}, {"TimeLimit", "200"}, {"ScreenShot", "LevelScreenShots/Level1"}, {"UnLockLevel", "10002"}, {"TimeLeftForEvaluation", "100,60,20"}, } },
        {10002, new Dictionary<string, string>(){ {"LevelName", "HallWay"}, {"SceneName", "Level2"}, {"Description", "Is was lucky for Mike to escape from Combo. However, enemies are now attacking Newaustin, the railway terminal of the east republic. Mike needs to get to another train station before republic lost it."}, {"TimeLimit", "150"}, {"ScreenShot", "LevelScreenShots/Level2"}, {"UnLockLevel", "10003"}, {"TimeLeftForEvaluation", "80,50,30"}, } },
        {10003, new Dictionary<string, string>(){ {"LevelName", "ClassRoom"}, {"SceneName", "Level3"}, {"Description", "Mike finaly returned the capital of the city, the most important battle in this war are hapenning. Get to the other side of the city, to the direction where reinforcements will come, leave the city before it completely turned in to battle field of iron and bombs!"}, {"TimeLimit", "250"}, {"ScreenShot", "LevelScreenShots/Level3"}, {"UnLockLevel", "10001"}, {"TimeLeftForEvaluation", "100,70,20"}, } },
    };

}
