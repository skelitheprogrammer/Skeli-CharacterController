using UnityEngine;
using VContainer;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private bool _disableOnSpawn = true;

    private void Awake()
    {
        Instantiate(_player);

        if (_disableOnSpawn)
        {
            gameObject.SetActive(false);
        }

    }
}
