using System;

public class CurrencyEvents
{
    public Action<int> onGoldGained;
    public void GoldGained(int gold)
    {
        if (onGoldGained != null)
        {
            onGoldGained(gold);
        }
    }
}