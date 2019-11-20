
using UnityEngine;

using Game.Weapons;

namespace Game.Characters.BaseCharacter
{
	public abstract class Controller : MonoBehaviour
	{
		public Weapon weapon;
		public bool canMove = true;
		public bool isMoving;
		protected Rigidbody rb;
	}
}
