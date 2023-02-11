using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_gameover : MonoBehaviour 
{
	private Text scoreText;
	private Text rankText;
	private Text highscoreText;
	public GameObject fadein;

	void Start()
	{
		scoreText = transform.GetChild(0).gameObject.GetComponent<Text>();
		rankText = transform.GetChild(1).gameObject.GetComponent<Text>();
		highscoreText = transform.GetChild(2).gameObject.GetComponent<Text>();
	}

	void Update()
	{
		scoreText.text = "Score: " + GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().score;
		rankText.text = "Rank: " + GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().rank;
		highscoreText.text = "Highscore: " + GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().highscore;
	}

	public void Menu()
	{
		StartCoroutine(ReturnToMenu());
	}

	IEnumerator ReturnToMenu()
	{
		Instantiate(fadein, fadein.transform.position, fadein.transform.rotation);
		yield return new WaitForSeconds(1);
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
	}
	
}
