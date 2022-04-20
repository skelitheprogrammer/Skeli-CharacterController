using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Player/Rotation Data")]
public class OriginRotationDataSO : ScriptableObject
{
    [field: SerializeField] public OriginRotationData Data { get; private set; }
}

[System.Serializable]
public class OriginRotationData
{
    [field: SerializeField] public float BotClamp { get; private set; } = -45;
    [field: SerializeField] public float TopClamp { get; private set; } = 55;
}
