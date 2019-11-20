using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons
{
	public abstract class Weapon : MonoBehaviour
	{
		public float projectileSpeed;
		public float attackRange;
		[Range(0.02f, 2f)]
		public float attackInterval;
		float nextAttackInterval;
		public GameObject firePoint;

		private void Update()
		{
			nextAttackInterval += Time.deltaTime;
		}
		public virtual void Attack()
		{
			//if (nextAttackInterval >= attackInterval)
			Shoot();
		}

		public virtual void Shoot()
		{
			nextAttackInterval = 0;
		}
	}

}
