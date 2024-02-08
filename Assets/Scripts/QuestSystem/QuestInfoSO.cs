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

    [field: SerializeField]
    public List<ItemInfoSO> Items { get; private set; } = new List<ItemInfoSO>();

    [field: Header("Dialogue")]
    [field: SerializeField] public DialogueInfoSO DialogueInfoSOStart { get; private set; }
    [field: SerializeField] public DialogueInfoSO DialogueInfoSOFinish { get; private set; }

    [field: Header("Window Dialogue")]
    [field: SerializeField] public string DialogTitle { get; private set; }
    [field: TextArea(3, 10)]
    [field: SerializeField] public string[] DialogDescription { get; private set; }


    private void OnValidate()
    {
        //Debug.Log("OnValidate");
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
