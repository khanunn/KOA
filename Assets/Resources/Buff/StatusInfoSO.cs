using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Buff,Debuff
}

[CreateAssetMenu(fileName = "StatusInfoSO", menuName = "ScriptableObject/StatusInfoSO", order = 1)]
public class StatusInfoSO : ScriptableObject
{
    [field: Header("Status Genaral")]    
    [field: SerializeField] public int StatusID { get; private set; }
    [field: SerializeField] public BuffType StatusType { get; private set; }
    [field: SerializeField] public float Intensity { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public float Tier { get; private set; }

    [field: Header("Status Information")]
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public string Desciption { get; private set; }

    [field: SerializeField] public Sprite Icon { get; private set; }

    public void StackDuration(float duration)
    {
        this.Duration += duration;
    }

    public void SetDuration(float duration)
    {
        this.Duration = duration;
    }

    public void ChangeIntensity(float intensity)
    {
        this.Intensity = intensity;
    }
}

