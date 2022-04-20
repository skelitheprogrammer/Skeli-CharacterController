using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformReference : MonoBehaviour
{
    [field: SerializeField] public Transform Transform { get; private set; }
}
