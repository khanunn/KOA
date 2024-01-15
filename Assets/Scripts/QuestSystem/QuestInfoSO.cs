using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObject/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("Genaral")]
    public string displayName;

    [Header("Requirements")]
    public int levelRequirements;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefab;

    [Header("Rewards")]
    public int goldReward;
    public int rubyReward;
    public int expReward;


    private void OnValidate()
    {
        //Debug.Log("OnValidate");
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
