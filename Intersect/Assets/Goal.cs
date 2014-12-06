using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public int score;
	// Use this for initialization
	void Start ()
	{
		score = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Ball")
		{
			other.GetComponent<Ball>().isInGoal = this;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Ball")
		{
			other.GetComponent<Ball>().isInGoal = null;
		}
	}

	public void Score()
	{
		score++;
	}
}
