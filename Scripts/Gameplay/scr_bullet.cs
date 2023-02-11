using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bullet : MonoBehaviour 
{
	public float speed;
	public float damage;
	public float range;
	public GameObject explosion;
	
	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(DestroyByTime());	
	}

	void Update () 
	{
		rb.velocity = transform.up * speed;
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Border"))
		{
			Destroy(gameObject);
		}

		if (other.gameObject.CompareTag("Player") && gameObject.tag == "EnemyBullet")
		{
			Destroy(this.gameObject);
		}

		if (other.gameObject.CompareTag("Enemy") && gameObject.tag == "Bullet")
		{
			Destroy(gameObject);
			Instantiate(explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3), gameObject.transform.rotation);
		}
	}

	IEnumerator DestroyByTime()
	{
		yield return new WaitForSeconds(range);
		Destroy(gameObject);
	}
}
