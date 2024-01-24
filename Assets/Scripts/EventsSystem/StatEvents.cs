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

    public event Action<StatManager> onSendStatManager;
    public void SendStatManager(StatManager stat)
    {
        if (onSendStatManager != null)
        {
            onSendStatManager(stat);
        }
    }
    public event Action<StatController> onSendStatController;
    public void SendStatController(StatController stat)
    {
        if (onSendStatController != null)
        {
            onSendStatController(stat);
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