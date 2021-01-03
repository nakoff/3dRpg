using UnityEngine;
using System.Collections.Generic;
using Entities;

namespace Game
{

	public class MainGame: MonoBehaviour
	{

		private InputManager inputManager;
		private EntityManager entityManager;

		private void Awake() {
			inputManager = gameObject.AddComponent<InputManager>();
			entityManager = gameObject.AddComponent<EntityManager>();
		}

		private void Start()
		{

			EntityFactory.CreatePlayer(Vector3.up * 2);
		}

		private void Update() 
		{
			var dt = Time.deltaTime;
			entityManager.OnUpdate(dt);
			inputManager.OnUpdate();
		}

	}
}
