using UnityEngine;
[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Data/Player/Movement Data")]
public class PlayerMovementDataSO : ScriptableObject
{
    [field: SerializeField] public PlayerMovementData Data { get; private set; }
}

[System.Serializable]
public class PlayerMovementData
{
    [field: SerializeField] public float Acceleration { get; private set; } = 200;
    [field: SerializeField] public AnimationCurve AccelerationCurve { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; } = 10;
}