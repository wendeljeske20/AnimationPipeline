using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public abstract class Projectile : MonoBehaviour
	{
		public float moveSpeed;
		public float attackRange = 10;
		Vector3 spawnPosition;

		public void Start()
		{
			spawnPosition = transform.position;
		}

		void Update()
		{
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

			float distance = (transform.position - spawnPosition).sqrMagnitude;

			if (distance >= attackRange * attackRange)
			{
				Destroy(gameObject);
			}

		}

	
	}
}
