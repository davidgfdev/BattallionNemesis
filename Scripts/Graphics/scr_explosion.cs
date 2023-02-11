using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_explosion : MonoBehaviour 
{
	public float explosionTime;

	void Start()
	{
		StartCoroutine(DestroyGameObject());
		transform.position.Set(transform.position.x, transform.position.y, -5);
	}

	IEnumerator DestroyGameObject()
	{
		yield return new WaitForSeconds(explosionTime);
		Destroy(this.gameObject);
	}
}
