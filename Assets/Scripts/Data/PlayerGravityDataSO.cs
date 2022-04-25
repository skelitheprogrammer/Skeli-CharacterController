using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player/Player Gravity Data")]
public class PlayerGravityDataSO : ScriptableObject
{
    [field: SerializeField] public PlayerGravityData Data { get; private set; }
}

[System.Serializable]
public class PlayerGravityData
{
    [field: SerializeField] public float GroundedGravity { get; private set; } = -.15f;
    [field: SerializeField] public float GravityClamp { get; private set; } = -20f;
}
