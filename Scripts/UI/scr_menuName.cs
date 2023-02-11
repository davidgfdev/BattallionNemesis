using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_menuName : MonoBehaviour 
{
	public GameObject fadein;
	public InputField nameInput;

	void Update()
	{
		GetComponent<Text>().text = "Welcome, Captain " + GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().playerName + ".";
	}

	public void PlayPressed()
	{
		StartCoroutine(StartGame());
		Instantiate(fadein, fadein.transform.position, fadein.transform.rotation);
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSecondsRealtime(1);
		GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().score = 0;
		UnityEngine.SceneManagement.SceneManager.LoadScene("NemesisMode");
	}

	public void Login()
	{
		if (nameInput.text != "")
		{
			GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().playerName = nameInput.text;
			PlayerPrefs.SetString("playerName", GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().playerName);	
			GameObject.FindGameObjectWithTag("LoginScreen").transform.GetChild(0).GetComponent<Animator>().Play("anim_loginscreen_close");
			new WaitForSeconds(1);
			GameObject.FindGameObjectWithTag("LoginScreen").transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	public void OpenLogin()
	{
		GameObject.FindGameObjectWithTag("LoginScreen").transform.GetChild(0).gameObject.SetActive(true);
		GameObject.FindGameObjectWithTag("LoginScreen").transform.GetChild(0).GetComponent<Animator>().Play("anim_loginscreen_open");
		
	}
}
