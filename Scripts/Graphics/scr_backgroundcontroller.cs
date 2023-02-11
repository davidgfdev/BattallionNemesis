using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_backgroundcontroller : MonoBehaviour 
{
	public Sprite[] backgrounds;
	void Update () 
	{
		GetComponent<SpriteRenderer>().sprite = backgrounds[GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().Chapter];
	}
}
