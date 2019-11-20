using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons
{
	[AddComponentMenu("[Game]/Weapons/PlasmaGun")]
	public class PlasmaGun : Weapon
	{
		public Plasma plasmaPrefab;

		public override void Shoot()
		{
			base.Shoot();

			Plasma plasma = Instantiate(plasmaPrefab, firePoint.transform.position, transform.rotation);
			plasma.moveSpeed = projectileSpeed;
			plasma.attackRange = attackRange;
		}
	}
}
