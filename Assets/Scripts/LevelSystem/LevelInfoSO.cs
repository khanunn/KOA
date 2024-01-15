using System;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelInfoSO", menuName = "ScriptableObject/LevelInfoSO", order = 1)]
public class LevelInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }
    [Header("AnimationCurve")]
    public AnimationCurve animationCurve;
    public int MaxLevel;
    public int MaxRequireExp;

    private void OnValidate()
    {
        //Debug.Log("OnValidate");
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public int GetRequiredExp(int level)
    {
        int requireExperience = Mathf.RoundToInt(animationCurve.Evaluate(Mathf.InverseLerp(0, MaxLevel, level)) * MaxRequireExp);
        return requireExperience;
    }
}