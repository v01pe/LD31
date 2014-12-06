using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float speed;

	void Start ()
	{
		print("I'm a ball");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnThrow(Player thrower)
	{
		Vector2 direction = thrower.inputVector.normalized;
		float offset = 0.55f;
		Vector3 position = thrower.transform.position;
		position.x += direction.x * offset;
		position.y += direction.y * offset;
		rigidbody2D.transform.position = position;

		rigidbody2D.velocity = direction * speed;
	}
}
