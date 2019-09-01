using System.Collections.Generic;

//ID : 关卡ID(int)
//LevelName : 关卡名称(string)
//SceneName : 场景名称(string)
//Description : 关卡描述(string)
//TimeLimit : 通关限定时间（秒）(float)
//ScreenShot : 关卡封面路径(string)
//UnLockLevel : 解锁关卡(string)
//TimeLeftForEvaluation : 获得不同星级的评价所需要的剩余时间(string)
//ResultWord : 结束语(string)

public class LevelInfoData : Singleton<LevelInfoData>
{
    public Dictionary<int, Dictionary<string, string>> data = new Dictionary<int, Dictionary<string, string>>()
    {
        {10001, new Dictionary<string, string>(){ {"LevelName", "PlayGround"}, {"SceneName", "Level1"}, {"Description", "You are a cleaner of a school. One day you enter the school and you found that there is a lot of rubbish on the playground. At the same time, you have a time limit because the student are going to enter the school and begin their classes."}, {"TimeLimit", "200"}, {"ScreenShot", "LevelScreenShots/Level1"}, {"UnLockLevel", "10002"}, {"TimeLeftForEvaluation", "100,60,20"}, {"ResultWord", "Good Job!"}, } },
        {10002, new Dictionary<string, string>(){ {"LevelName", "HallWay"}, {"SceneName", "Level2"}, {"Description", "Now you walk into the building and you find that there is still a lot of garbage in the hallway and you have to clean it or you will be fired by the school."}, {"TimeLimit", "200"}, {"ScreenShot", "LevelScreenShots/Level2"}, {"UnLockLevel", "10003"}, {"TimeLeftForEvaluation", "120,60,20"}, {"ResultWord", "Good Job!"}, } },
        {10003, new Dictionary<string, string>(){ {"LevelName", "ClassRoom"}, {"SceneName", "Level3"}, {"Description", "The final place you are going to is the classroom and you have to clean all of them in a short time or the students will not have a good environment of studying."}, {"TimeLimit", "250"}, {"ScreenShot", "LevelScreenShots/Level3"}, {"UnLockLevel", "10001"}, {"TimeLeftForEvaluation", "100,70,20"}, {"ResultWord", "Good Job!"}, } },
    };

}
