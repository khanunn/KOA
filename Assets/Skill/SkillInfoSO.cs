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

    [field: Header("Skill Info")]
    [field: SerializeField] public float ManaCost { get; private set; }
    [field: SerializeField] public float OutputSkill { get; private set; }
    [field: SerializeField] public float CooldownSkill { get; private set; }
    [field: SerializeField] public RequirementWeapon RequirementWeapon { get; private set; }
    [field: SerializeField] public SkillType SkillType { get; private set; }

    private void OnValidate()
    {
        //Debug.Log("OnValidate");
#if UNITY_EDITOR
        this.name = SkillId.ToString();
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
