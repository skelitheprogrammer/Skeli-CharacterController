using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Player/Jump Data")]
public class PlayerJumpDataSO : ScriptableObject
{
    [field: SerializeField] public PlayerJumpData Data { get; private set; }
}

[System.Serializable]
public class PlayerJumpData
{
    [field: SerializeField] public float JumpHeight { get; private set; }

}