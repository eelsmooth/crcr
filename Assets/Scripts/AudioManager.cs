using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	private AudioSource audio;
	public bool isSFX;
	public bool loadedMusic;
	private float maxValue;
	
	void Start(){
		audio = GetComponent<AudioSource>();
		if(!isSFX)
			StartCoroutine(StartAudio());
		else
			loadedMusic = true;
	}
	
	void Update(){
		if(loadedMusic){
			if(!isSFX)
				audio.volume = GameObject.Find("- Story").
				GetComponent<Story>().volume_mus_amb;
			else
				audio.volume = GameObject.Find("- Story").
				GetComponent<Story>().volume_sfx;
		}
	}
	
	public IEnumerator StartAudio(){
		//audio.clip = newClip;
		maxValue = GameObject.Find("- Story").
			GetComponent<Story>().volume_mus_amb;
		audio.Play();
		while(audio.volume < maxValue){
			audio.volume += .007f;
			yield return new WaitForSeconds(.15f);
		}
		loadedMusic = true;
	 }

	public IEnumerator StopAudio(){
		//audio.clip = newClip;
		//audio.Play();
		loadedMusic = false;
		while(audio.volume > 0 && audio != null){
			audio.volume -= .007f;
			yield return new WaitForSeconds(.15f);
		}
	}
}
