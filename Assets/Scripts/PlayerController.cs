using System.Collections;
using System.Collections.Generic;
using System;
using FSM;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
	[Inject] private StateMachine _fsm;
	[Inject] private CharacterStateData _data;
	
	[Inject] private CharacterController _controller;

	private void Awake()
	{
	}

	private void Start()
	{
		
	}

	private void Update()
	{
	}
	
	
}
