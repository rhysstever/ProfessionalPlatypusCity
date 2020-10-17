using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float moveSpeed;
	public float turnSpeed;

	// Start is called before the first frame update
	void Start()
    {
		moveSpeed = 10.0f;
		turnSpeed = 50.0f;
	}

    // Update is called once per frame
    void FixedUpdate()
	{
		BasicMovement();
	}

	void BasicMovement()
	{
		// Foward / Backward movement
		float movement = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
		float turning = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;

		transform.Translate(0, 0, movement);
		transform.Rotate(0, turning, 0);
	}
}
