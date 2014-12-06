using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed;

	public int nr;

	public Vector2 inputVector;


	// Use this for initialization
	void Start ()
	{
		inputVector = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update ()
	{
		inputVector.x = Input.GetAxis("Horizontal P" + nr);
		inputVector.y = -Input.GetAxis("Vertical P" + nr);

//		print("x " + inputVector.x + " y " + inputVector.y);

		Vector2 targetVelocity = (inputVector.magnitude == 0) ? Vector2.zero : inputVector.normalized * speed;

//		Vector2 currentVelocity = rigidbody2D.velocity;
//		float currentSpeed = currentVelocity.magnitude;

		gameObject.rigidbody2D.velocity = targetVelocity;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ball")
		{
			GameState gameState = GameObject.Find("camera").GetComponent<GameState>();
			gameState.OnCatch(this, collision.gameObject.GetComponent<Ball>());
		}
	}
}
