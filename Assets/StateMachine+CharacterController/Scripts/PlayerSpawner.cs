using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{	
	[Inject] private readonly PlayerFactory _playerFactory;
	 
	private void Start()	
	{
		var player = _playerFactory.Create();
		player.parent = null;
		player.position = transform.position;
		
	}
	
	public class PlayerFactory : PlaceholderFactory<Transform>{}

}
