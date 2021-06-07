using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour 
{
	private Rigidbody2D birdBody;

	private SpriteRenderer sr;
	private float moveSpeed = 3.5f;
	private bool goLeft;

	private Animator anim;

	private float firstJumpForce = 5f, secondJumpForce = 7f;
	private bool firstJump, secondJump;

	void Awake() 
	{
		birdBody = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	void Start() 
	{
		
	}

	void Update() 
	{

		if (GameplayController.instance.playGame) 
		{
			Move();

			if (Input.GetMouseButtonDown(0)) 
			{
				JumpFunc();
			}
		}
	}

	void Move() 
	{
		if (goLeft) 
		{
			birdBody.velocity = new Vector2(-moveSpeed, birdBody.velocity.y);
		} 
		else 
		{
			birdBody.velocity = new Vector2(moveSpeed, birdBody.velocity.y);
		}
	}

	void JumpFunc() 
	{
		if (firstJump) 
		{
			firstJump = false;
			birdBody.velocity = new Vector2(birdBody.velocity.x, firstJumpForce);

			anim.SetTrigger(TagManager.FLY_TRIGGER);

			SoundManager.instance.PlayJumpSound ();
		} 
		else if (secondJump) 
		{
			
			secondJump = false;
			birdBody.velocity = new Vector2(birdBody.velocity.x, secondJumpForce);

			anim.SetTrigger(TagManager.FLY_TRIGGER);

			SoundManager.instance.PlayJumpSound ();
		}
	}

	void OnCollisionEnter2D(Collision2D target) 
	{
		if (target.gameObject.tag == TagManager.BORDER_TAG) 
		{
			goLeft = !goLeft;
			sr.flipX = goLeft;
		}

		if (target.gameObject.tag == TagManager.GROUND_TAG) 
		{
			if (birdBody.velocity.y <= 1f) 
			{
				firstJump = true;
				secondJump = true;
			}
		}

		if (target.gameObject.tag == TagManager.DOG_TAG) 
		{
			GameplayController.instance.GameOver();

			birdBody.velocity = new Vector2(0f, 0f);
			anim.Play(TagManager.DEAD_ANIMATION);

			Spawner.instance.CancelWarningSpawner ();
		}
	}

	void OnTriggerEnter2D(Collider2D target) 
	{
		if (target.tag == TagManager.SCORE_TAG) 
		{	
			GameplayController.instance.DisplayScore(1, 0);

			target.gameObject.SetActive(false);
		}

		if (target.tag == TagManager.DIAMOND_TAG) 
		{
			GameplayController.instance.DisplayScore(0, 1);

			target.gameObject.SetActive(false);

			SoundManager.instance.PlayDiamondSound();
		}
	}
}