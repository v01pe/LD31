using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static float speed = 10.0f;

	public int nr;

	private Vector2 inputVector;


	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		inputVector.x = Input.GetAxis("Horizontal P" + nr);
		inputVector.y = -Input.GetAxis("Vertical P" + nr);

		gameObject.rigidbody2D.AddForce(inputVector * speed);
	}
}
