using System;
using UnityEngine;
public enum StatKey
{
    Constitution, Dexterity, Strength, Wisdom, Intelligent, Lucky,
    v_hp_max, v_mp_max, v_hp_recovery, v_mp_recovery, v_patk, v_matk, v_pdef, v_mdef, v_acc, v_evade, v_crit_change, v_crit_dam, v_pdam, v_mdam
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