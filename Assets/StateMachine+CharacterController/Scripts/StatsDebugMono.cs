using UnityEngine;
using Zenject;

public class StatsDebugMono : MonoBehaviour
{
    [Inject][SerializeField] private CharacterStateData _data;

    [Inject(Id = "RotateOrigin")][SerializeField] private Transform _origin;
}
