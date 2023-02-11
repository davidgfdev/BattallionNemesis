using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_back : MonoBehaviour 
{
	public void BACK()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
		Destroy(GameObject.FindGameObjectWithTag("GameController"));
	}
}
