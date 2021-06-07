using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSpawner : MonoBehaviour 
{
	private float spawnLeft = -2.25f, spawnRight = 2.25f;

	private SpriteRenderer sr;

	public GameObject dogPrefab;
	private float pushForce = 10f;

	void Awake() 
	{
		sr = GetComponent<SpriteRenderer>();
	}

	void Start () 
	{
		RandomPosition();

		InvokeRepeating("SpawnObstacle", Random.Range(3, 5), 5);
	}

	void RandomPosition() 
	{
		Vector3 temp = transform.position;

		if (Random.Range (0, 2) > 0) 
		{
			temp.x = spawnLeft;
			sr.flipX = true;
		} 
		else 
		{
			temp.x = spawnRight;
		}

		transform.position = temp;
	}

	void SpawnObstacle() 
	{
		GameObject obstacle = Instantiate(dogPrefab);

		Vector3 temp = transform.position;

		if (transform.position.x > 0) 
		{
			obstacle.transform.position = new Vector3(temp.x + 5f, temp.y, 0f);

			obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(-pushForce, obstacle.GetComponent<Rigidbody2D>().velocity.y);
		} 
		else 
		{
			obstacle.transform.position = new Vector3(temp.x - 5f, temp.y, 0f);

			obstacle.GetComponent<SpriteRenderer>().flipX = true;

			obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(pushForce, obstacle.GetComponent<Rigidbody2D>().velocity.y);
		}

		if (gameObject.activeInHierarchy) 
		{
			StartCoroutine(TurnOffOnSign());
		}
	}

	IEnumerator TurnOffOnSign() 
	{
		yield return new WaitForSeconds(2f);
		sr.enabled = false;

		yield return new WaitForSeconds(1f);
		sr.enabled = true;
	}
}