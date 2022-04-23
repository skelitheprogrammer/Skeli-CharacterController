using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player/Player Rotation Data")]
public class PlayerRotationDataSO : ScriptableObject
{
    [field: SerializeField] public PlayerRotationData Data { get; private set; }
}

[System.Serializable]
public class PlayerRotationData
{
    [field: SerializeField] public float RotationSmoothTime { get; private set; }
}