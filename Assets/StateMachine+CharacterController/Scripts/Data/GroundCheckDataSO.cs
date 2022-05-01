using UnityEngine;

[CreateAssetMenu(menuName = "Data/GroundCheckData")]
public class GroundCheckDataSO : ScriptableObject
{
    [field: SerializeField] public GroundCheckData Data { get; private set; }
}

[System.Serializable]
public class GroundCheckData
{
    [field: SerializeField] public Vector3 RayOffset { get; private set; }
    [field: SerializeField] public Vector3 SphereOffset { get; private set; }
    [field: SerializeField] public float Length { get; private set; }
    [field: SerializeField] public float Radius { get; private set; }

}


