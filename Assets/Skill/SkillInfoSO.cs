using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RequirementWeapon
{
    None,Sword
}

public enum SkillType
{
    Healing, Damage, Buff, Debuff
}
[CreateAssetMenu(fileName = "SkillInfoSO", menuName = "ScriptableObject/SkillInfoSO", order = 1)]
public class SkillInfoSO : ScriptableObject
{
    [field: Header("Genaral")]
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int SkillId { get; private set; }
    [field: SerializeField] public int SkillLevel  { get; private set; } //using to calculated Damage base on skill level
    [field: SerializeField] public int SkillTier { get; private set; } //using as index in skill tree
    [field: SerializeField] public float ManaCost { get; private set; }
    [field: SerializeField] public float OutputSkill { get; private set; }
    [field: SerializeField] public float CooldownSkill { get; private set; }
    [field: SerializeField] public int LifetimeVFX { get; private set; }    
    [field: SerializeField] public SkillType SkillType { get; private set; }
    [field: SerializeField] public StatusInfoSO StatusEffect { get; private set; }

    [field: Header("Requirement Setting")]
    [field: SerializeField] public int RequirementSkillID { get; private set; }
    [field: SerializeField] public int RequirementLevel { get; private set; }
    [field: SerializeField] public RequirementWeapon RequirementWeapon { get; private set; }

    public void UpgradeSkill()
    {
        SkillLevel += 1;
    }
    private void OnValidate()
    {
        //Debug.Log("OnValidate");
#if UNITY_EDITOR
        this.name = SkillId.ToString();
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }      
}
