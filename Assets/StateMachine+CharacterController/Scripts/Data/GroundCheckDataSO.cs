using UnityEngine;

[CreateAssetMenu(menuName = "Data/GroundCheckData")]
public class GroundCheckDataSO : ScriptableObject
{
    [field: SerializeField] public GroundCheckData Data { get; private set; }
}

[System.Serializable]
public class GroundCheckData
{
    [field: SerializeField] public float GroundDistance { get; private set; }

}
