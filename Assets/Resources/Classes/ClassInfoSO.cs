using UnityEngine;

[CreateAssetMenu(fileName = "ClassInfoSO", menuName = "ScriptableObject/ClassInfoSO", order = 1)]
public class ClassInfoSO : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [Header("Genaral")]
    [field: SerializeField] public string DisplayName { get; private set; }
    //[field: SerializeField] public int Mana { get; private set; }

    [Header("Base Status")]
    [SerializeField] public StatBase Constitution;
    [SerializeField] public StatBase Dexterity;
    [SerializeField] public StatBase Strength;
    [SerializeField] public StatBase Wisdom;
    [SerializeField] public StatBase Intelligent;
    [SerializeField] public StatBase Lucky;

    [Header("Variable Status")]
    [SerializeField] public StatBase v_hp_max { get; private set; }
    [SerializeField] public StatBase v_mp_max { get; private set; }
    [SerializeField] public StatBase v_hp_recovery { get; private set; }
    [SerializeField] public StatBase v_mp_recovery { get; private set; }
    [SerializeField] public StatBase v_patk { get; private set; }
    [SerializeField] public StatBase v_matk { get; private set; }
    [SerializeField] public StatBase v_pdef { get; private set; }
    [SerializeField] public StatBase v_mdef { get; private set; }
    [SerializeField] public StatBase v_acc { get; private set; }
    [SerializeField] public StatBase v_evade { get; private set; }
    [SerializeField] public StatBase v_crit_change { get; private set; }
    [SerializeField] public StatBase v_crit_dam { get; private set; }
    [SerializeField] public StatBase v_pdam { get; private set; }
    [SerializeField] public StatBase v_mdam { get; private set; }

    private void OnValidate()
    {
#if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}