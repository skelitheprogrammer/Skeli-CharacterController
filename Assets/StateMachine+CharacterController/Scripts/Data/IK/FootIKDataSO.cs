using UnityEngine;

[CreateAssetMenu(menuName = "Data/IK/FootRaycastData")]
public class FootIKDataSO : ScriptableObject
{
    [field: SerializeField] public FootIKData Data { get; private set; }
}

[System.Serializable]
public class FootIKData
{
    [field: SerializeField] public Vector3 RayOffset { get; private set; }
    [field: SerializeField] public float Height { get; private set; }
    [field: SerializeField] public float Radius { get; private set; }
}


