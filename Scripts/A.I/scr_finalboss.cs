using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_finalboss : MonoBehaviour 
{
	public float maxHealth;
	public Transform cannon1;
	public Transform cannon2;
	public Transform cannon3;
	public Transform cannon4;
	public GameObject ring;
	public GameObject bullet;
	public float fireRate;
	public float speed;
	public float tpRate;
	public Image bossBar;
	public float rotateSpeed;
	public GameObject body;

	private float health;
	private GameObject player;
	private Transform target;
	private float directionX;
	private float directionY;

	void Start()
	{
		health = maxHealth;
		player = GameObject.FindGameObjectWithTag("Player");
		target = player.transform;
		StartCoroutine(Teleport());
		StartCoroutine(Shoot());
		ChangeDirection();
	}

	void Update()
	{
		ring.transform.Rotate(new Vector3(0,0,1) * rotateSpeed);
		bossBar.fillAmount = health/maxHealth;
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5f);
		
		GetComponent<Rigidbody2D>().velocity = new Vector2(directionX, directionY) * speed;
		
		body.transform.up = ((Vector2) target.position - (Vector2) transform.position).normalized;

		if (health <= 0)
		{
			GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().WinGame();
			Destroy(this.gameObject);
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (!other.gameObject.CompareTag("Bullet"))
		{
			ChangeDirection();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			health -= GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().playerDamage;
		}
	}

	void ChangeDirection()
	{
		directionX = Random.Range(0,2);
		directionY = Random.Range(0,2);

		if (directionX == 0)
		{
		directionX = -1;
		}
		if (directionY == 0)
		{
			directionY = -1;
		}
	}

	IEnumerator Shoot()
	{
		while (true)
		{
			bullet.GetComponent<scr_bullet>().range = 10;	
			bullet.GetComponent<scr_bullet>().damage = 20;
			Instantiate(bullet, cannon1.position, cannon1.rotation);	
			Instantiate(bullet, cannon2.position, cannon2.rotation);	
			Instantiate(bullet, cannon3.position, cannon3.rotation);	
			Instantiate(bullet, cannon4.position, cannon4.rotation);
			yield return new WaitForSeconds(fireRate);
		}
	}

	IEnumerator Teleport()
	{
		while(true)
		{
			gameObject.transform.position = new Vector2(Random.Range(-25,25),Random.Range(-13,13));
			yield return new WaitForSeconds(tpRate);
		}
	}
}
