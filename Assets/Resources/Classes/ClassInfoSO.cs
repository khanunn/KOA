using UnityEngine;

[CreateAssetMenu(fileName = "ClassInfoSO", menuName = "ScriptableObject/ClassInfoSO", order = 1)]
public class ClassInfoSO : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [Header("Genaral")]
    [field: SerializeField] public string DisplayName { get; private set; }
    //[field: SerializeField] public int Mana { get; private set; }

    [Header("Main Stats")]
    [SerializeField] public StatBase Constitution;
    [SerializeField] public StatBase Dexterity;
    [SerializeField] public StatBase Strength;
    [SerializeField] public StatBase Wisdom;
    [SerializeField] public StatBase Intelligent;
    [SerializeField] public StatBase Lucky;

    [Header("Secondary Stats")]
    [SerializeField] StatBase Health;
    [SerializeField] StatBase Mana;

    private void OnValidate()
    {
#if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}