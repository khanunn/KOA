using System;
using UnityEngine;
public enum StatKey
{
    Constitution,
    Dexterity,
    Strength,
    Wisdom,
    Intelligent,
    Lucky
}
public class Stat
{
    public StatKey statKey;
    public int statValue;

    public Stat(StatKey statKey, int statValue)
    {
        this.statKey = statKey;
        this.statValue = statValue;
    }
}