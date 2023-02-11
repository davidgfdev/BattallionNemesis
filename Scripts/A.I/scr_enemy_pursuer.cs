using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_enemy_pursuer : MonoBehaviour 
{
	private GameObject player;
	private float nextFire;
	private float health;

	public float speed;	
	public GameObject explosion;
	public float fireRate;
	public GameObject obj_bullet_enemy;
	public Transform cannon;
	public float maxHealth;

	void Start () 
	{
		player = GameObject.Find("obj_player");
		health = maxHealth;
	}
	
	void FixedUpdate () 
	{
		transform.up = ((Vector2) player.transform.position - (Vector2) transform.position).normalized;
		transform.position = Vector2.MoveTowards( transform.position,player.transform.position, speed);

		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(obj_bullet_enemy, cannon.position, cannon.rotation);
		}

		if (health <= 0)
		{
			Instantiate(explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3), gameObject.transform.rotation);
			GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().enemysDestroyed += 1;
			GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().score += 15;
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bullet")
		{
			//Actualizar a variable de daño del "GameController" cuando esté listo.
			health -= GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().playerDamage;
		}
	}
}
