using System;
public class KillEvents
{
    public event Action<string> onMonsterKilled;
    public void MonsterKilled(string monsterID)
    {
        if(onMonsterKilled != null)
        {
            onMonsterKilled(monsterID);
        }
    }
}
