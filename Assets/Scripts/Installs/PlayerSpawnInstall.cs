using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerSpawnInstall : MonoInstaller
{
	[SerializeField] private GameObject _playerWrapperPrefab;

	public override void InstallBindings()
	{
		Container.BindFactory<Transform, PlayerSpawner.PlayerFactory>()
			.FromComponentInNewPrefab(_playerWrapperPrefab)
			.AsCached()
			.NonLazy();
	}
}
