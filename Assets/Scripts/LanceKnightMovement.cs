using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceKnightMovement : MonoBehaviour
{

	[SerializeField]
	private Transform cameraTransform;
	
	private CharacterController characterController;

	[SerializeField]
	private GameObject pivotPoint; // active game object marker, center point of orbit

	private Vector3 orbitPoint; // saved position of the marker whilst orbiting
	private Animator animator;

	public float groundSpeed = 10; //speed
	public float airSpeed = 1; //speed
	public float orbitSpeed = 120; //speed of the orbiting???? idk what unit its in lmao please send help
	public float orbitVelocity = 10; //active speed variable


	public float weight = 1; // gravity modifier?

	// private float xSpeed; // this is more magnitude of movement ignoring y speed
	private float ySpeed;
	private float orbitAngle; // one full rotation is 1000~ i think its framerate dependant
	private bool orbiting = false;
	private float launchSpeed = 0;
	private float rotationSpeed = 200;

	private Vector3 velocity;
	private Vector3 movementDirection;
	private	float magnitude;

	private Quaternion debugAngle;
	void Start()
    {
		characterController = GetComponent<CharacterController>();

		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
    {
		/*
		// please help

		while grounded:
			normal movement
			takes 2 seconds to build up to max speed?

			jump pressed:
				take note of previous speed
				set current speed to 0
				take note of the orbit center pos
				begin orbiting (no longer grounded)
		
		jump let go:
			stop orbiting
			add momentum depending on how far the orbit has progressed:
				eariler = more y momentum
				later = more x momentum

		while orbiting:
			move player by one degree around a semi circle per frame
			air controlled movement


		*/


		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");


		if (characterController.isGrounded){
			// normal movement

			movementDirection = new Vector3(horizontalInput, 0, verticalInput);
			magnitude = Mathf.Clamp01(movementDirection.magnitude) * groundSpeed;
			// print(magnitude);
			// rotate by camera direction
			movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;


			// print("grounded");

			if (Input.GetButtonDown("Jump")) {
				// begin orbit sequence
				// launchSpeed = magnitude/10 + 1;
				launchSpeed = 0.1f + Mathf.Clamp01(movementDirection.magnitude);
				magnitude = 5;
				orbitPoint = pivotPoint.transform.position;

				// debug to show where the pivoting point is
				// Instantiate(pivotPoint, orbitPoint, transform.rotation);
				orbitVelocity = magnitude;

				orbitAngle = 0;
				orbiting = true;
			}
		} 
		else if (!orbiting) { // in air, not orbiting, falling? maybe.
			ySpeed += (Physics.gravity.y - weight) * Time.deltaTime;

			// air control
			// print(movementDirection);

			/*
				MODIFY movementDirection

				take input direction, influence movementDirection with a mag of like 0.01
			*/

			Vector3 airMovementDirection = new Vector3(horizontalInput, 0, verticalInput);
			float airMagnitude = Mathf.Clamp01(airMovementDirection.magnitude) * 0.002f;
			airMovementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * airMovementDirection;
			airMovementDirection = airMovementDirection * airMagnitude;
			print(airMovementDirection);

			movementDirection += airMovementDirection;
		}

		if (!Input.GetButton("Jump")){
			// Not Holding Jump
			orbiting = false;
			if(orbitAngle != 0)	{
				// print(movementDirection);
				// add momentum
				/*
				launchSpeed: speed they entered the jump with
				orbitAngle: what angle the orbit is currently at, eg progress of the orbit

				earlier, more yspeed,
				later, more xspeed,

				*/
				magnitude = scale(orbitAngle, 0, 500, 0, 20*launchSpeed); 
				// rotate xSpeed towards player facing (?)
				Vector3 temp = new Vector3(0, 0, 1);
				movementDirection = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * temp;

				ySpeed = scale(orbitAngle, 0, 500, 10*launchSpeed, 1);

				orbitAngle = 0; 
			}

		}

		if (orbiting) {
			ySpeed = 0;
			orbitAngle++;
			
			// give orbitSpeed velocity over time
			orbitVelocity += 2;
			if(orbitVelocity > orbitSpeed) orbitVelocity = orbitSpeed; // this feels good idk

			// // angle is 1 degree in the direction the character is facing
			// Quaternion angle = Quaternion.Euler((orbitSpeed * Time.deltaTime), 0, 0); // one degree
			// Quaternion playerFacing = Quaternion.Euler(0, -transform.rotation.y, 0);

			// // angle = angle * playerFacing;
			// // rotate it so its in the direction of the character
			// // angle = angle


			// print(angle);
			// Vector3 newPosition = RotatePointAroundPivot(transform.position, orbitPoint, angle);


			// attempt 2, making the angle something in game that would rotate with the character.
			// Quaternion angle = Quaternion.Euler((orbitPoint + pivotDist.transform.position));
			// print(angle);
			// debugAngle = angle;
			// Vector3 newPosition = RotatePointAroundPivot(transform.position, orbitPoint, angle);

			// attempt 3, please make this work

			// transform.RotateAround(target.transform.position, Vector3.forward, degreesPerSecond * Time.deltaTime);

			Vector3 distance = new Vector3(orbitVelocity * Time.deltaTime, 0, 0);
			distance = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * distance;
			Quaternion angle = Quaternion.Euler(distance);
			debugAngle = angle;

			Vector3 newPosition = RotatePointAroundPivot(transform.position, orbitPoint, angle);

			// using character controller, adding distance between points to current position
			Vector3 orbitDirection = newPosition - transform.position;
			characterController.Move(orbitDirection);
			if(characterController.collisionFlags != CollisionFlags.None){
				//held space for too long
				print("collided mid orbit");
				orbiting = false;
				orbitAngle = 0;
			}


		}

		// rotate the player to where they are facing
		if (movementDirection != Vector3.zero) {
			Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
			animator.SetBool("IsRunning", true);
		}
		else {
			animator.SetBool("IsRunning", false);
		}


		velocity = movementDirection * magnitude;
		velocity.y = ySpeed;
		characterController.Move(velocity * Time.deltaTime);

		// Quaternion debugangle = Quaternion.Euler((orbitSpeed), 0, 0); // one degree
		// Quaternion debugfacing = Quaternion.Euler(0, transform.rotation.y, 0);
		// // debugangle = debugangle * debugfacing;
		// Vector3 newangle = debugangle * transform.forward;

		Debug.DrawRay(transform.position, debugAngle * transform.forward,new Color(255,0,0));
	}

	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle)
	{
		return angle * (point - pivot) + pivot;
	}

	// p5js map function my beloved
	public float scale(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
	{
		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

		return (NewValue);
	}
}


