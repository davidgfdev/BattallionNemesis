using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_gamecontroller : MonoBehaviour 
{
	public int Chapter;
	public int enemysDestroyed;
	public GameObject[] Enemy;
	public GameObject[] ChapterText;
	public float playerRange;
	public float playerDamage;
	public float playerSpeed;
	public GameObject Nemesis;
	public float maxX;
	public float maxY;
	public float minX;
	public float minY;
	public bool spawnActive;
	public int Upgrades;
	public int damageLevel;
	public int rangeLevel;
	public int speedLevel;
	public int score;
	public int highscore;
	public string rank;
	public string playerName;
	public bool crosshairValue;
	public Texture2D crosshairSprite;
	public GameObject ui_victory;

	private Vector2 RandomSpawn;
	private InputField nameInput;

	void Start () 
	{
		DontDestroyOnLoad(gameObject);
		spawnActive = false;
		damageLevel = 1;
		speedLevel = 1;
		rangeLevel = 1;
		Upgrades = 0;
		score = 0;
		playerName = PlayerPrefs.GetString("playerName", "unkown");
		if (playerName == "unkown")
		{
			GameObject.Find("WelcomeText").GetComponent<scr_menuName>().OpenLogin();
		}
		highscore = PlayerPrefs.GetInt("highscore", 0);
	}
	
	void Update () 
	{
		Application.targetFrameRate = 60;
		//DEBUG
		if (Input.GetKeyDown(KeyCode.F1))
		{
			PlayerPrefs.DeleteAll();
		}
		
		if (crosshairValue == true)
		{
			Cursor.SetCursor(crosshairSprite, new Vector2(16,16), CursorMode.ForceSoftware);
		}
		else
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 

		}

		playerName = PlayerPrefs.GetString("playerName", null);
		foreach (GameObject controller in GameObject.FindGameObjectsWithTag("GameController"))
		{
			if (controller != this.gameObject)
			{
				Destroy(controller);
			}
		}
		switch (damageLevel)
		{
			case 1:
				playerDamage = 15;
			break;

			case 2:
				playerDamage = 30;
			break;

			case 3:
				playerDamage = 50;
			break;
		}

		switch (speedLevel)
		{
			case 1:
				playerSpeed = 20;
			break;

			case 2:
				playerSpeed = 30;
			break;

			case 3:
				playerSpeed = 40;
			break;
		}

		switch (rangeLevel)
		{
			case 1:
				playerRange = 0.2f;
			break;

			case 2:
				playerRange = 0.3f;
			break;

			case 3:
				playerRange = 0.4f;
			break;
		}

		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "NemesisMode")
		{
			RandomSpawn = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
			StartCoroutine(SpawnWaves());
			switch (enemysDestroyed)
			{
				case 0:
					StartCoroutine(ActiveTheSpawn());
					Chapter = 1; 
					enemysDestroyed = 1;
					Instantiate(ChapterText[Chapter], ChapterText[Chapter].transform.position, ChapterText[Chapter].transform.rotation);
					GameObject.FindGameObjectWithTag("Player").GetComponent<scr_playercontroller>().health = 100;
				break;

				case 10:
					foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
					{
						Destroy(enemy);	
					}
					spawnActive = false;
					Chapter = 2;
					Upgrades += 1;
					enemysDestroyed = 11;
					StartCoroutine(ActiveTheSpawn());
					Instantiate(ChapterText[Chapter], ChapterText[Chapter].transform.position, ChapterText[Chapter].transform.rotation);
					GameObject.Find("LevelTransition").GetComponent<Animator>().Play("anim_leveltransition");
					GameObject.FindGameObjectWithTag("Player").GetComponent<scr_playercontroller>().health = 100;
				break;

				case 20:
					foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
					{
						Destroy(enemy);	
					}
					spawnActive = false;
					Chapter = 3;
					Upgrades += 1;
					enemysDestroyed = 21;
					StartCoroutine(ActiveTheSpawn());
					Instantiate(ChapterText[Chapter], ChapterText[Chapter].transform.position, ChapterText[Chapter].transform.rotation);
					GameObject.Find("LevelTransition").GetComponent<Animator>().Play("anim_leveltransition");
					GameObject.FindGameObjectWithTag("Player").GetComponent<scr_playercontroller>().health = 100;
				break;

				case 35:
					foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
					{
						Destroy(enemy);	
					}
					spawnActive = false;
					Chapter = 4;
					Upgrades += 1;
					enemysDestroyed = 36;
					StartCoroutine(ActiveTheSpawn());
					Instantiate(ChapterText[Chapter], ChapterText[Chapter].transform.position, ChapterText[Chapter].transform.rotation);
					GameObject.Find("LevelTransition").GetComponent<Animator>().Play("anim_leveltransition");
					GameObject.FindGameObjectWithTag("Player").GetComponent<scr_playercontroller>().health = 100;
				break;

				case 45:
					foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
					{
						Destroy(enemy);	
					}
					spawnActive = false;
					Chapter = 5;
					Upgrades += 1;
					enemysDestroyed = 46;
					StartCoroutine(ActiveTheSpawn());
					Instantiate(ChapterText[Chapter], ChapterText[Chapter].transform.position, ChapterText[Chapter].transform.rotation);
					GameObject.Find("LevelTransition").GetComponent<Animator>().Play("anim_leveltransition");
					GameObject.FindGameObjectWithTag("Player").GetComponent<scr_playercontroller>().health = 100;
				break;

				case 55:
					foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
					{
						Destroy(enemy);	
					}
					Chapter = 6;
					enemysDestroyed = 56;
					GameObject.FindGameObjectWithTag("Player").GetComponent<scr_playercontroller>().health = 100;
					GameObject.Find("LevelTransition").GetComponent<Animator>().Play("anim_leveltransition");
				break;
			}
		}
		else
		{
			StopAllCoroutines();
			ResetStats();
		}

		if (score > 0)
		{
			rank = "D";
		}
		if (score > 500)
		{
			rank = "C";
		}
		if (score > 1000)
		{
			rank = "B";
		}
		if (score > 1500)
		{
			rank = "A";
		}
		if (score > 2000)
		{
			rank = "S";
		}
	}

	IEnumerator SpawnWaves()
	{
		while (spawnActive == true)
		{
			switch (Chapter)
			{
				case 1:
					if (GameObject.FindGameObjectsWithTag("Enemy").Length < 3)
					{
						Instantiate(Enemy[0], RandomSpawn, gameObject.transform.rotation);
					}
				break;

				case 2:
					if (GameObject.FindGameObjectsWithTag("Enemy").Length < 5)
					{
						Instantiate(Enemy[Random.Range(0,2)], RandomSpawn, gameObject.transform.rotation);
					}
				break;

				case 3:
					if (GameObject.FindGameObjectsWithTag("Enemy").Length < 7)
					{
						Instantiate(Enemy[Random.Range(0,3)], RandomSpawn, gameObject.transform.rotation);
					}
				break;

				case 4:
					if (GameObject.FindGameObjectsWithTag("Enemy").Length < 9)
					{
						Instantiate(Enemy[Random.Range(0,4)], RandomSpawn, gameObject.transform.rotation);
					}
				break;

				case 5:
					if (GameObject.FindGameObjectsWithTag("Enemy").Length < 12)
					{
						Instantiate(Enemy[Random.Range(0,4)], RandomSpawn, gameObject.transform.rotation);
					}
				break;

				case 6:
					if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1)
					{
						Instantiate(Nemesis, RandomSpawn, gameObject.transform.rotation);
					}
				break;
			}
			yield return new WaitForSeconds(1);
		}
	}

	IEnumerator ActiveTheSpawn()
	{
		if (spawnActive == false)
		{
			yield return new WaitForSeconds(5);
			spawnActive = true;
		}
		StopCoroutine(ActiveTheSpawn());
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void WinGame()
	{
		if (score > highscore)
			{
				highscore = score;
			}
			PlayerPrefs.SetInt("highscore", highscore);
			Instantiate(ui_victory, gameObject.transform.position, gameObject.transform.rotation);
			foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				Destroy(enemy);	
			}
			spawnActive = false;
			crosshairValue = false;
			GameObject.FindGameObjectWithTag("Player").SetActive(false);
	}

	public void DamageLevelUp()
	{
		if (Upgrades > 0 && damageLevel < 3)
		{
			damageLevel += 1;
			Upgrades -= 1;
		}
	}

	public void SpeedLevelUp()
	{
		if (Upgrades > 0 && speedLevel < 3)
		{
			speedLevel += 1;
			Upgrades -= 1;
		}
	}

	public void RangeLevelUp()
	{
		if (Upgrades > 0 && rangeLevel < 3)
		{
			rangeLevel += 1;
			Upgrades -= 1;
		}
	}

	public void ResetStats()
	{
		enemysDestroyed = 0;
		damageLevel = 1;
		speedLevel = 1;
		rangeLevel = 1;
		Upgrades = 0;
	}
}