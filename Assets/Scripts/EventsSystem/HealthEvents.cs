
using System;

public class HealthEvents
{
    public event Action<int> onHealthChange;
    public void HealthChange(int health)
    {
        if (onHealthChange != null)
        {
            onHealthChange(health);
        }
    }
    public event Action<int> onHealthGained;
    public void HealthGained(int health)
    {
        if (onHealthGained != null)
        {
            onHealthGained(health);
        }
    }
}