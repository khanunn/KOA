using System;
using UnityEngine;

[Serializable]
public class StatBase
{
    [SerializeField] public string StatID;
    [field: SerializeField] public int BaseStatValue { get; private set; }
    [field: SerializeField] public AnimationCurve BaseStatModifier { get; private set; }
}