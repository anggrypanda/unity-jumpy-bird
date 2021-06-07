using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGameobject : MonoBehaviour 
{
	void Update() 
	{
		DeactivateGameObj();
	}

	void DeactivateGameObj() 
	{
		if (Camera.main.transform.position.y > transform.position.y + 9f) 
		{
			gameObject.SetActive(false);
		}
	}
}