using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour 
{
	public float backgroundSpeed;
	public float resetPosition;
	
	
	void Update () 
	{	
		transform.Translate(Vector2.down * backgroundSpeed);
		
		if (transform.position.y <= resetPosition)
		{
			transform.position = new Vector2(0,0);
		}
	}
}
