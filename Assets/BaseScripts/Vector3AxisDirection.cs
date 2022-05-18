using UnityEngine;

namespace Scripts
{
    [System.Serializable]
    public struct Vector3AxisDirection
    {
        public enum AxisToRotate { Back, Down, Forward, Left, Right, Up };

        [field: SerializeField] public AxisToRotate Direction { get; private set; }

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
}