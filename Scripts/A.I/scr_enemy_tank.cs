using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_enemy_tank : MonoBehaviour 
{
	private GameObject player;
	private float health;

	public float speed;	
	public float maxHealth;
	public GameObject explosion;

	void Start () 
	{
		player = GameObject.Find("obj_player");
		health = maxHealth;
	}
	
	void FixedUpdate () 
	{
		transform.up = ((Vector2) player.transform.position - (Vector2) transform.position).normalized;
		transform.position = Vector2.MoveTowards( transform.position,player.transform.position, speed);

		if (health <= 0)
		{
			Instantiate(explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3), gameObject.transform.rotation);
			GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_gamecontroller>().score += 100;
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
