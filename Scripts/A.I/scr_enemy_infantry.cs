using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_enemy_infantry : MonoBehaviour 
{
	public float maxHealth;
	public Transform cannon;
	public GameObject bullet;
	public float fireRate;
	public float speed;
	public GameObject healthPack;
	public GameObject explosion;

	private float health;
	private GameObject player;
	private Transform target;
	private bool moving;
	private float directionX;
	private float directionY;
	private bool isAttacking;
	private bool isAwake;

	void Start()
	{
		isAwake = true;
		health = maxHealth;
		player = GameObject.FindGameObjectWithTag("Player");
		target = player.transform;
		moving = false;
	}

	void Update()
	{
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5f);
	
		if (isAttacking == false && isAwake == true)
		{
			StartCoroutine(Attack());
		}
		if (moving == false && isAwake == true)
		{
			ChangeDirection();
			moving = true;
		}
		else if (moving == true && isAwake == false)
		{
			directionX = 0;
			directionY = 0;
			moving = false;
		}

		GetComponent<Rigidbody2D>().velocity = new Vector2(directionX, directionY) * speed;
		
		transform.up = ((Vector2) target.position - (Vector2) transform.position).normalized;

		if (health <= 0)
		{
			float dice = Random.Range(0,3);
			if (dice == 2)
			{
				Instantiate(healthPack, gameObject.transform.position, healthPack.transform.rotation);
			}
			Instantiate(explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3), gameObject.transform.rotation);
			GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().score += 50;
			Destroy(this.gameObject);
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (!other.gameObject.CompareTag("Bullet") && isAwake == true)
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

	IEnumerator Attack()
	{
		isAttacking = true;
		yield return new WaitForSeconds(fireRate);
		Instantiate(bullet, cannon.position, cannon.rotation);	
		isAttacking = false;
	}
}
