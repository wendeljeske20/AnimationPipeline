using UnityEngine;

namespace Game.Characters.Player
{
	[AddComponentMenu("[Game]/Characters/Player")]
	public class Controller : BaseCharacter.Controller
	{
		public bool gravity = true;
		public new Camera camera;
		public float moveSpeed = 7;
		public float rotationSpeed = 10;
		CharacterController characterController;
		public Vector2 input;
		public Vector3 velocity;
		public bool canShoot = true;
		public bool canRotate = true;



		Animator animator;
	
		protected  void Start()
		{
			animator = GetComponent<Animator>();
			characterController = GetComponent<CharacterController>();
			rb = GetComponent<Rigidbody>();


		}


		protected  void Update()
		{
			animator.SetFloat("CurrentSpeed", input.magnitude);

			if (canMove)
			{
				MoveControls();

			}

			animator.SetFloat("AttackAnimationSpeed", 1f / weapon.attackInterval);


			if (weapon && canShoot && Input.GetMouseButton(0))
			{
				LookAtMouse();
				animator.SetBool("Shooting", true);
				canMove = false;
				//Attack(); //É chamado no clip.
			}
			if (animator.GetBool("Shooting") && Input.GetMouseButtonUp(0))
			{
				canMove = true;
				animator.SetBool("Shooting", false);
			}

			float scroll = Input.GetAxis("Mouse ScrollWheel");

			if (scroll > 0)
			{
				camera.transform.Translate(Vector3.forward);
			}
			else if (scroll < 0)
			{
				camera.transform.Translate(-Vector3.forward);
			}


			if (Input.GetKeyDown(KeyCode.T))
			{
				animator.SetTrigger("Die");
				DisableControl();
				animator.SetBool("Shooting", false);
			}


		}
		public void EnableControl()
		{
			canMove = true;
			canRotate = true;
			canShoot = true;
		}

		public void DisableControl()
		{
			canMove = false;
			canRotate = false;
			canShoot = false;
		}
		
		
		void MoveControls()
		{
			input = new Vector2(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));
			if (input.magnitude > 0.5f)
			{
				input.Normalize();

			}
			if (input.magnitude > 0.1f)
			{
				Vector3 direction = new Vector3(input.normalized.x, transform.position.y, input.normalized.y);
				LookAt(direction);
			}


			if (gravity)
			{
				if (!characterController.isGrounded)
					velocity.y += Physics.gravity.y * 2 * Time.deltaTime;
				else if (characterController.isGrounded && velocity.y < 0)
					velocity.y = 0f;

			}


			characterController.Move(new Vector3(input.x * moveSpeed, velocity.y, input.y * moveSpeed) * Time.deltaTime);


		}



		void Attack()
		{
			weapon.Attack();
		}


		void LookAtMouse()
		{
			Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
			Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
			float rayLenght;
			if (groundPlane.Raycast(cameraRay, out rayLenght))
			{
				Vector3 pointToLook = new Vector3(cameraRay.GetPoint(rayLenght).x, cameraRay.GetPoint(rayLenght).y, cameraRay.GetPoint(rayLenght).z - 0.5f);

				Vector3 direction = (pointToLook - transform.position).normalized;

				LookAt(direction);

			}
		}

		void LookAt(Vector3 direction)
		{
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
		}



	}
}