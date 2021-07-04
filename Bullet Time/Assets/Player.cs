using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float jumpVelocity = 12f;
	public float moveSpeed = 6f;
	public float midAirControl = 3f;
	private Rigidbody2D rigidbody2d;
	public LayerMask platformLayerMask;
	private BoxCollider2D boxCollider2d;
	private bool isFacingLeft;
	public bool spawnFacingLeft = false;
	private Vector2 facingLeft;
	private Vector2 facingRight;
	void Start()
	{
		rigidbody2d = transform.GetComponent<Rigidbody2D>();
		boxCollider2d = transform.GetComponent<BoxCollider2D>();
		facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
		facingRight = new Vector2(transform.localScale.x, transform.localScale.y);
		platformLayerMask = LayerMask.GetMask("Platform");

		if (spawnFacingLeft)
		{
			transform.localScale = facingLeft;
			isFacingLeft = true;
		}
		else
		{
			transform.localScale = facingRight;
			isFacingLeft = false;
		}
	}

	void Update()
	{
		if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
		{
			rigidbody2d.velocity = Vector2.up * jumpVelocity;
		}

		handleMovement();
	}

	void FixedUpdate()
	{
	}

	private void Flip()
	{
		if (isFacingLeft)
		{
			transform.localScale = facingRight;
		}
		else
		{
			transform.localScale = facingLeft;
		}
		isFacingLeft = !isFacingLeft;
	}

	private void handleMovement()
	{
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			if (!isFacingLeft)
			{
				Flip();
			}
			if (IsGrounded())
			{
				rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
			}
			else
			{
				rigidbody2d.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
				rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
			}
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			if (isFacingLeft)
			{
				Flip();
			}
			if (IsGrounded())
			{
				rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
			}
			else
			{
				rigidbody2d.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
				rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
			}
		}
		else
		{
			if (IsGrounded())
			{
				rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
			}
		}
	}

	private bool IsGrounded()
	{
		RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
		return raycastHit2d.collider != null;
	}
}
