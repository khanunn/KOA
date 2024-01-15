using System;

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
}