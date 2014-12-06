using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float speed;

	public Goal isInGoal;

	private Vector3 initialPosition;

	void Start ()
	{
		isInGoal = null;
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnThrow(Player thrower)
	{
		Vector2 direction = thrower.inputVector.normalized;
		float offset = (thrower.transform.localScale.x + transform.localScale.x) / 2f + 0.005f;
		Vector3 position = thrower.transform.position;
		position.x += direction.x * offset;
		position.y += direction.y * offset;
		rigidbody2D.transform.position = position;

		rigidbody2D.velocity = direction * speed;
	}

	public void Reset()
	{
		transform.position = initialPosition;
		rigidbody2D.velocity = Vector2.zero;
		renderer.enabled = true;
		isInGoal = null;
	}
}
