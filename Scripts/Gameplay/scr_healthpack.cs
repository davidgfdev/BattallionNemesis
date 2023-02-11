using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_healthpack : MonoBehaviour 
{
	private GameObject player;
	void Start()
	{
		player = GameObject.Find("obj_player");
		StartCoroutine(DestroyGameObject());
	}

	void Update()
	{
		if (Vector2.Distance(gameObject.transform.position, player.transform.position) <= 7)
		{
			transform.position = Vector2.MoveTowards(transform.position,player.transform.position, 1);
		}
	}
	IEnumerator DestroyGameObject()
	{
		yield return new WaitForSeconds(5);
		Destroy(this.gameObject);
	}
}
