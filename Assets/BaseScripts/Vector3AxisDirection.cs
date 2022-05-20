using UnityEngine;

[System.Serializable]
public struct Axis3Direction
{
    public enum Axis { Back, Down, Forward, Left, Right, Up }

    [field: SerializeField] public Axis Direction { get; private set; }

    private static readonly Vector3[] vectorAxes = new Vector3[]
    {
        Vector3.back,
        Vector3.down,
        Vector3.forward,
        Vector3.left,
        Vector3.right,
        Vector3.up,
    };

    public Vector3 GetAxis() => vectorAxes[(int)Direction];
}

[System.Serializable]
public struct AxisMoveDirection
{
    public enum Axis { Forward, Left, Right, Back }
    [field: SerializeField] public Axis Direction { get; private set; }

    private static readonly Vector3[] moveAxes = new Vector3[]
    {
        Vector3.forward,
        Vector3.left,
        Vector3.right,
        Vector3.back,
    };

    public Vector3 GetAxis() => moveAxes[(int)Direction];

}

[System.Serializable]
public struct DirectionSpeed
{
    public float speed;
    public AxisMoveDirection direction;
}