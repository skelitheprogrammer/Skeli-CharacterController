using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGravityData", menuName = "Data/Player/Gravity")]
public class PlayerGravityDataSO : ScriptableObject
{
    [field: SerializeField] public PlayerGravityData Data { get; private set; }
}

[System.Serializable]
public class PlayerGravityData
{
    [field: SerializeField] public float GroundedGravity { get; private set; } = -.05f;
}
