using System;
using UnityEngine;

public class StatContainer
{
    public Stat Constitution;
    public Stat Dexterity;
    public Stat Strength;
    public Stat Wisdom;
    public Stat Intelligent;
    public Stat Lucky;

    public Stat Health;
    public Stat Mana;

    public StatContainer(ClassInfoSO classInfoSO)
    {
        Constitution = new Stat(StatKey.Constitution, classInfoSO.Constitution.BaseStatValue);
        Dexterity = new Stat(StatKey.Dexterity, classInfoSO.Dexterity.BaseStatValue);
        Strength = new Stat(StatKey.Strength, classInfoSO.Strength.BaseStatValue);
        Wisdom = new Stat(StatKey.Wisdom, classInfoSO.Wisdom.BaseStatValue);
        Intelligent = new Stat(StatKey.Intelligent, classInfoSO.Intelligent.BaseStatValue);
        Lucky = new Stat(StatKey.Lucky, classInfoSO.Lucky.BaseStatValue);
    }
    public Stat getStat(StatKey statKey)
    {
        var fields = typeof(StatContainer).GetFields();
        foreach (var item in fields)
        {
            Stat value = (Stat)item.GetValue(this);
            if (value.statKey == statKey)
            {
                return value;
            }
        }
        return null;
    }
}