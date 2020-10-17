using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public Vector3 direction;
	public Vector3 velocity;
	public Vector3 acceleration;
	public Quaternion rotation;
	public float accelerationValue;
	public float turnValue;
	public float maxSpeed;
	float angle;

	// Start is called before the first frame update
	void Start()
    {
		direction = new Vector3(0, 0, 1);
		velocity = new Vector3();
		acceleration = new Vector3();
		rotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		Rotate();
		Move();
	}

	void Move()
	{
		// Foward / Backward movement
		if(Input.GetKey(KeyCode.W))
			acceleration += accelerationValue * direction;
		else if(Input.GetKey(KeyCode.S))
			acceleration -= accelerationValue * direction;
		else
		{
			velocity *= 0.85f;
			acceleration = Vector3.zero;
		}

		velocity += acceleration;
		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
		transform.position += velocity;
		acceleration = new Vector3();
	}

	void Rotate()
	{
		// Left / Right movement
		if(Input.GetKey(KeyCode.A))
		{
			angle -= 2;
			direction = Quaternion.Euler(0, -2, 0) * direction;
		}
		else if(Input.GetKey(KeyCode.D))
		{
			angle += 2;
			direction = Quaternion.Euler(0, 2, 0) * direction;
		}

		transform.rotation = Quaternion.Euler(0, angle, 0);
	}
}
