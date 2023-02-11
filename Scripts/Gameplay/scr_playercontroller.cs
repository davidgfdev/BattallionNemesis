using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class scr_playercontroller : MonoBehaviour 
{
	private Rigidbody2D rb;
	private float nextFire;
	private bool isDamaging;
	private GameObject gameController;
	private int lifes;
	private bool invincible;

	private bool godMode;
	
	public float speed;
	public float health;
	public Transform Cannon;
	public GameObject obj_bullet;
	public GameObject ui_gameover;
	public float fireRate;
	public Image HealthBar;
	public Image UpgradeSpeed;
	public Image UpgradeDamage;
	public Image UpgradeRange;
	public Image FillSpeed;
	public Image FillDamage;
	public Image FillRange;
	public Image FillLife1;
	public Image FillLife2;
	public Image FillLife3;
	public GameObject explosion;
	
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		gameController = GameObject.FindGameObjectWithTag("GameController");
		speed = gameController.GetComponent<scr_gamecontroller>().playerSpeed;
		gameController.GetComponent<scr_gamecontroller>().crosshairValue = true;
		gameController.GetComponent<scr_gamecontroller>().spawnActive = false;
		lifes = 3;
		invincible = false;
		godMode = false;
	}	

	void Update () 
	{
		Debug.Log("GodMode: " + godMode);
		if (Input.GetKeyDown(KeyCode.F2))
		{
			if (godMode == false)
			{
				godMode = true;
			}
			else
			{
				godMode = false;
			}
		}
		if (gameController.GetComponent<scr_gamecontroller>().Upgrades > 0)
		{
			if (gameController.GetComponent<scr_gamecontroller>().damageLevel < 3)
			{
				UpgradeDamage.color = Color.yellow;
				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					gameController.GetComponent<scr_gamecontroller>().DamageLevelUp();
				}
			}
			
			if (gameController.GetComponent<scr_gamecontroller>().speedLevel < 3)
			{
				UpgradeSpeed.color = Color.yellow;
				if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					gameController.GetComponent<scr_gamecontroller>().SpeedLevelUp();
				}
			}
			
			if (gameController.GetComponent<scr_gamecontroller>().rangeLevel < 3)
			{
				UpgradeRange.color = Color.yellow;
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					gameController.GetComponent<scr_gamecontroller>().RangeLevelUp();
				}
			}
		}
		else
		{
			UpgradeDamage.color = Color.white;
			UpgradeSpeed.color = Color.white;
			UpgradeRange.color = Color.white;
		}
		
		switch(lifes)
		{
			case 3:
			FillLife1.fillAmount = 1;
			FillLife2.fillAmount = 1;
			FillLife3.fillAmount = 1;
			break;

			case 2:
			FillLife1.fillAmount = 1;
			FillLife2.fillAmount = 1;
			FillLife3.fillAmount = 0;
			break;

			case 1:
			FillLife1.fillAmount = 1;
			FillLife2.fillAmount = 0;
			FillLife3.fillAmount = 0;
			break;

			case 0:
			FillLife1.fillAmount = 0;
			FillLife2.fillAmount = 0;
			FillLife3.fillAmount = 0;
			break;
		}

		switch(gameController.GetComponent<scr_gamecontroller>().damageLevel)
		{
			case 1:
			FillDamage.fillAmount = 0.33f;
			break;

			case 2:
			FillDamage.fillAmount = 0.66f;
			break;

			case 3:
			FillDamage.fillAmount = 1;
			break;
		}

		switch(gameController.GetComponent<scr_gamecontroller>().speedLevel)
		{
			case 1:
			FillSpeed.fillAmount = 0.33f;
			break;

			case 2:
			FillSpeed.fillAmount = 0.66f;
			break;

			case 3:
			FillSpeed.fillAmount = 1;
			break;
		}

		switch(gameController.GetComponent<scr_gamecontroller>().rangeLevel)
		{
			case 1:
			FillRange.fillAmount = 0.33f;
			break;

			case 2:
			FillRange.fillAmount = 0.66f;
			break;

			case 3:
			FillRange.fillAmount = 1;
			break;
		}

		HealthBar.fillAmount = health/100;

		rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
		Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.up = (mouseWorldPosition - (Vector2) transform.position).normalized;	

		if (Input.GetMouseButton(0))
		{
			if (Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				obj_bullet.GetComponent<scr_bullet>().range = gameController.GetComponent<scr_gamecontroller>().playerRange;
				Instantiate(obj_bullet, Cannon.position, Cannon.rotation);
				GetComponent<AudioSource>().Play();
			}
		}

		if (lifes == 0)
		{
			if (gameController.GetComponent<scr_gamecontroller>().score > gameController.GetComponent<scr_gamecontroller>().highscore)
			{
				gameController.GetComponent<scr_gamecontroller>().highscore = gameController.GetComponent<scr_gamecontroller>().score;
			}
			PlayerPrefs.SetInt("highscore", gameController.GetComponent<scr_gamecontroller>().highscore);
			Instantiate(ui_gameover, gameObject.transform.position, gameObject.transform.rotation);
			foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				Destroy(enemy);	
			}
			gameController.GetComponent<scr_gamecontroller>().spawnActive = false;
			gameController.GetComponent<scr_gamecontroller>().crosshairValue = false;
			gameObject.SetActive(false);
		}

		if (health > 100)
		{
			health = 100;
		} 
		if (health <= 0)
		{	if (godMode == false)
			{
				health = 100;
				Instantiate(explosion, transform.position,transform.rotation);
				lifes -= 1;
				StartCoroutine(Respawn());
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("EnemyBullet"))
		{
			if(invincible == false)
			{
				health -= other.gameObject.GetComponent<scr_bullet>().damage;
			}
		}

		if (other.gameObject.CompareTag("HealthPack"))
		{
			health += 25;
			Destroy(other.gameObject);
		}
	}

	IEnumerator Respawn()
	{
		invincible = true;
		gameObject.transform.position = new Vector2(0,0);
		GetComponent<SpriteRenderer>().color = Color.black;
		yield return new WaitForSeconds(2);
		invincible = false;
		GetComponent<SpriteRenderer>().color = Color.white;
		StopCoroutine(Respawn());
	}
}
