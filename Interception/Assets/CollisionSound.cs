using UnityEngine;
using System.Collections;

public class CollisionSound : MonoBehaviour
{
	public string[] soundTags;
	public bool onCollision = true;
	public bool onTrigger = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (onCollision)
			CheckObject (collision.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (onTrigger)
			CheckObject (other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (onTrigger)
			CheckObject (other.gameObject, 1);
	}

	private void CheckObject(GameObject obj, int offset=0)
	{	
		AudioSource[] sounds = GetComponents<AudioSource> ();
		
		for (int i = 0; i < soundTags.Length && i < sounds.Length; i++)
		{
			if (obj.tag == soundTags[i])
			{
				sounds[i+offset].Play();
				break;
			}
		}
	}
}
