using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterTextScript : MonoBehaviour 
{
	void Start () 
	{
		GetComponent<Animator>().Play("anim_chaptertext_fadein");
		StartCoroutine(lifetime());
	}
	IEnumerator lifetime()
	{
		yield return new WaitForSeconds(4);
		GetComponent<Animator>().Play("anim_chaptertext_fadeout");
		yield return new WaitForSeconds(3);
		Destroy(gameObject);
	}
}
