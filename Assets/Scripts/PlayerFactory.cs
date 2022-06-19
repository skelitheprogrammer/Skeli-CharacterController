using UnityEngine;

public interface IFactory<T>
{
    T Create();
}

public class PrefabFactory : IFactory<GameObject>
{
    private readonly GameObject _prefab;

    public PrefabFactory(GameObject prefab)
    {
        _prefab = prefab;
    }

    public GameObject Create()
    {
        return GameObject.Instantiate(_prefab);
    }
}

