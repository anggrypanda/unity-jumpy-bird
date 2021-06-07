using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	private GameObject player;

	void Start() 
	{
		
	}

	void Update() 
	{
		FollowPlayer();
	}

	void FollowPlayer() 
	{
		if (player) 
		{
			transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
		}
	}

	public void FindPlayer() 
	{
		player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
	}
}