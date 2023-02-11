using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_fade : MonoBehaviour 
{
	void Start () 
	{
		StartCoroutine(lifetime());
	}
	IEnumerator lifetime()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}
}
