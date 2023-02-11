using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_enemy_drone : MonoBehaviour 
{
	public float maxHealth;
	public Transform cannon1;
	public Transform cannon2;
	public Transform cannon3;
	public Transform cannon4;
	public GameObject bullet;
	public float fireRate;
	public float speed;
	public GameObject healthPack;
	public GameObject explosion;
	public float rotationSpeed;

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
		gameObject.transform.Rotate(new Vector3(0,0,1) * rotationSpeed);
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
		

		if (health <= 0)
		{
			float dice = Random.Range(0,4);
			if (dice == 2)
			{
				Instantiate(healthPack, gameObject.transform.position, healthPack.transform.rotation);
			}
			Instantiate(explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3), gameObject.transform.rotation);
			GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().score += 25;
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
		Instantiate(bullet, cannon1.position, cannon1.rotation);	
		Instantiate(bullet, cannon2.position, cannon2.rotation);	
		Instantiate(bullet, cannon3.position, cannon3.rotation);	
		Instantiate(bullet, cannon4.position, cannon4.rotation);	
		isAttacking = false;
	}
}
