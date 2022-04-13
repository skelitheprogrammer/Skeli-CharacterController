using UnityEngine;

[CreateAssetMenu(fileName = "PlayerJumpingData", menuName = "Data/Player/Jumping Data")]
public class PlayerJumpingDataSO : ScriptableObject
{
    [field: SerializeField] public PlayerJumpingData Data { get; private set; }
}

[System.Serializable]
public class PlayerJumpingData
{
    [field: SerializeField] public float JumpHeight { get; private set; } = 3;
}