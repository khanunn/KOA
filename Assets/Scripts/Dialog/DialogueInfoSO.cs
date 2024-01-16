using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueInfoSO", menuName = "ScriptableObject/DialogueInfoSO", order = 1)]
public class DialogueInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("Genaral")]
    [field: SerializeField] public List<string> nameText = new List<string>();
    [field: SerializeField] public List<string> messageText = new List<string>();

    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}