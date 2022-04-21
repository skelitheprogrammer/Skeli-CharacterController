using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{	
	[Inject] private PlayerSpawner.PlayerFactory _playerFactory;
	[Inject] private PlayerSpawner.CameraFactory _cameraFactory;
	[Inject] private PlayerSpawner.VCamFactory _vCamFactory;
	 
	private void Start()	
	{
		_playerFactory.Create();
		
		_cameraFactory.Create();
		
		_vCamFactory.Create();
		
	}
	
	public class PlayerFactory : PlaceholderFactory<Transform>{}
	public class CameraFactory : PlaceholderFactory<Transform>{}
	public class VCamFactory : PlaceholderFactory<Transform>{}

}
