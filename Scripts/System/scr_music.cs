using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_music : MonoBehaviour 
{
	private AudioSource music;
	public AudioClip[] songs;
	void Start() 
	{
		music = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (music.isPlaying == false)
		{
			music.clip = songs[Random.Range(0,3)];
			music.Play();
		}
	}
}
