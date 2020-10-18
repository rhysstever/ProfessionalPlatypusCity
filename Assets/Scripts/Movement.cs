using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float moveSpeed;
	public float turnSpeed;
	public float jumpAmount;
	public float gravity;
	Vector3 acceleration;

	// Start is called before the first frame update
	void Start()
    {
		moveSpeed = 10.0f;
		turnSpeed = 100.0f;
		jumpAmount = 15.0f;
		acceleration = new Vector3();
	}

    // Update is called once per frame
    void FixedUpdate()
	{
		BasicMovement();
		VerticalMovement();
	}

	void BasicMovement()
	{
		// Foward / Backward movement
		float movement = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
		transform.Translate(0, 0, movement);

		// Turning 
		float turning = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;
		transform.Rotate(0, turning, 0);
	}

	void VerticalMovement()
	{
		if(gameObject.transform.position.y > 0.85f)
		{
			acceleration.y -= gravity * Time.deltaTime;
		}
		else if(gameObject.transform.position.y <= 0.85f)
		{
			gameObject.transform.position = new Vector3(
				gameObject.transform.position.x,
				0.85f,
				gameObject.transform.position.z);

			acceleration = new Vector3();

			float jumpAccel = Input.GetAxis("Jump") * Time.deltaTime * jumpAmount;
			acceleration.y += jumpAccel;
			//transform.Translate(0, jump, 0);
		}

		transform.Translate(acceleration);
	}
}
