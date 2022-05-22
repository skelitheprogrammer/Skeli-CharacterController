using UnityEngine;

[CreateAssetMenu(menuName ="Data/Player/Origin Rotation Data")]
public class CameraDataSO : ScriptableObject
{
    [field: SerializeField] public CameraData Data { get; private set; }
}

[System.Serializable]
public class CameraData
{
    [field: SerializeField] public float Sensitivity { get; private set; } = 2;
    [field: SerializeField] public float BotClamp { get; private set; } = -45;
    [field: SerializeField] public float TopClamp { get; private set; } = 55;

    [field: SerializeField] public float MinZoomDistance { get; private set; } = 1;
    [field: SerializeField] public float MaxZoomDistance { get; private set; } = 3;
    [field: SerializeField] public float ZoomAmount { get; private set; } = 10;
    
    [field: Range(0,1)]
    [field: SerializeField] public float ZoomSmoothTime { get; private set; } = .3f;
}

