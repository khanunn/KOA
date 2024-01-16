using System;
using UnityEngine;

public class StatContainer
{
    public Stat Constitution { get; private set; }
    public Stat Dexterity { get; private set; }
    public Stat Strength { get; private set; }
    public Stat Wisdom { get; private set; }
    public Stat Intelligent { get; private set; }
    public Stat Lucky { get; private set; }

    public Stat v_hp_max { get; private set; }
    public Stat v_mp_max { get; private set; }
    public Stat v_hp_recovery { get; private set; }
    public Stat v_mp_recovery { get; private set; }
    public Stat v_patk { get; private set; }
    public Stat v_matk { get; private set; }
    public Stat v_pdef { get; private set; }
    public Stat v_mdef { get; private set; }
    public Stat v_acc { get; private set; }
    public Stat v_evade { get; private set; }
    public Stat v_crit_change { get; private set; }
    public Stat v_crit_dam { get; private set; }
    public Stat v_pdam { get; private set; }
    public Stat v_mdam { get; private set; }

    public StatContainer(ClassInfoSO classInfoSO)
    {
        Constitution = new Stat(StatKey.Constitution, classInfoSO.Constitution.BaseStatValue);
        Dexterity = new Stat(StatKey.Dexterity, classInfoSO.Dexterity.BaseStatValue);
        Strength = new Stat(StatKey.Strength, classInfoSO.Strength.BaseStatValue);
        Wisdom = new Stat(StatKey.Wisdom, classInfoSO.Wisdom.BaseStatValue);
        Intelligent = new Stat(StatKey.Intelligent, classInfoSO.Intelligent.BaseStatValue);
        Lucky = new Stat(StatKey.Lucky, classInfoSO.Lucky.BaseStatValue);

        v_hp_max = new Stat(StatKey.v_hp_max, classInfoSO.v_hp_max.BaseStatValue);
        v_mp_max = new Stat(StatKey.v_mp_max, classInfoSO.v_mp_max.BaseStatValue);
        v_hp_recovery = new Stat(StatKey.v_hp_recovery, classInfoSO.v_hp_recovery.BaseStatValue);
        v_mp_recovery = new Stat(StatKey.v_mp_recovery, classInfoSO.v_mp_recovery.BaseStatValue);
        v_patk = new Stat(StatKey.v_patk, classInfoSO.v_patk.BaseStatValue);
        v_matk = new Stat(StatKey.v_matk, classInfoSO.v_matk.BaseStatValue);
        v_pdef = new Stat(StatKey.v_pdef, classInfoSO.v_pdef.BaseStatValue);
        v_mdef = new Stat(StatKey.v_mdef, classInfoSO.v_mdef.BaseStatValue);
        v_acc = new Stat(StatKey.v_acc, classInfoSO.v_acc.BaseStatValue);
        v_evade = new Stat(StatKey.v_evade, classInfoSO.v_evade.BaseStatValue);
        v_crit_change = new Stat(StatKey.v_crit_change, classInfoSO.v_crit_change.BaseStatValue);
        v_crit_dam = new Stat(StatKey.v_crit_dam, classInfoSO.v_crit_dam.BaseStatValue);
        v_pdam = new Stat(StatKey.v_pdam, classInfoSO.v_pdam.BaseStatValue);
        v_mdam = new Stat(StatKey.v_mdam, classInfoSO.v_mdam.BaseStatValue);
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