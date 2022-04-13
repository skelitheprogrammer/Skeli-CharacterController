using UnityEngine;

[CreateAssetMenu(menuName ="Data/Player Init Setup")]
public class PlayerInitSetupSO : ScriptableObject
{
    [field: SerializeField] public GameObject Character { get; private set; }
    [field: SerializeField] public GameObject MainCamera { get; private set; }
    [field: SerializeField] public GameObject Vcam { get; private set; }

}