using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 20f;
	public Rigidbody2D rigidbody2d;

	void Start()
	{
		rigidbody2d.velocity = transform.right * speed;
	}

	void OnTrigger2D(Collider2D collider)
	{
		Destroy(gameObject);
	}

	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

}
