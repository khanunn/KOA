using UnityEngine;

[CreateAssetMenu(fileName = "ClassInfoSO", menuName = "ScriptableObject/ClassInfoSO", order = 1)]
public class ClassInfoSO : ScriptableObject
{
    [field: SerializeField] public string Id;
    [Header("Genaral")]
    [field: SerializeField] public string DisplayName;
    //[field: SerializeField] public int Mana ;

    [Header("Base Status")]
    [SerializeField] public StatBase Constitution;
    [SerializeField] public StatBase Dexterity;
    [SerializeField] public StatBase Strength;
    [SerializeField] public StatBase Wisdom;
    [SerializeField] public StatBase Intelligent;
    [SerializeField] public StatBase Lucky;
    [Space(20)]
    [Header("Variable Status")]
    [SerializeField] public StatBase v_hp_max;
    [SerializeField] public StatBase v_mp_max;
    [SerializeField] public StatBase v_hp_recovery;
    [SerializeField] public StatBase v_mp_recovery;
    [SerializeField] public StatBase v_patk;
    [SerializeField] public StatBase v_matk;
    [SerializeField] public StatBase v_pdef;
    [SerializeField] public StatBase v_mdef;
    [SerializeField] public StatBase v_acc;
    [SerializeField] public StatBase v_evade;
    [SerializeField] public StatBase v_crit_change;
    [SerializeField] public StatBase v_crit_dam;
    [SerializeField] public StatBase v_pdam;
    [SerializeField] public StatBase v_mdam;

    private void OnValidate()
    {
#if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}