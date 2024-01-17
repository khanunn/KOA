using System;
using UnityEngine;

public class StatEvents
{
    public event Action onShowStat;
    public void ShowStat()
    {
        if (onShowStat != null)
        {
            onShowStat();
        }
    }

    public event Action<StatManager> onSendStat;
    public void SendStat(StatManager stat)
    {
        if (onSendStat != null)
        {
            onSendStat(stat);
        }
    }

    public event Action onLevelUpStat;
    public void LevelUpStat()
    {
        if (onLevelUpStat != null)
        {
            onLevelUpStat();
        }
    }
}