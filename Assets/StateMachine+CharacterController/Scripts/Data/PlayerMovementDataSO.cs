using System.Linq;
using System;
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
    [field: SerializeField] public float MaxSpeed { get; private set; } = 6.5f;
    [field: SerializeField] public float AirMaxSpeed { get; private set; } = 2;

    [field: SerializeField] public float ForwardSpeed { get; private set; } = 4;
    [field: SerializeField] public float SideWaysSpeed { get; private set; } = 3;
    [field: SerializeField] public float BackwardsSpeed { get; private set; } = 2;
}