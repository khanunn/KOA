using System;
public class KillEvents
{
    public event Action onMonsterKilled;
    public void MonsterKilled()
    {
        if(onMonsterKilled != null)
        {
            onMonsterKilled();
        }
    }
}
