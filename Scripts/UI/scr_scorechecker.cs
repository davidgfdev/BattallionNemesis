using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_scorechecker : MonoBehaviour 
{
	private float score;
	private Text scoreText;

    void Start() 
	{
		scoreText = GetComponent<Text>();
		scoreText.text = "Score: ";	
	}

	void Update()
	{
		scoreText.text = "Score: " + GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().score;
	}
}
