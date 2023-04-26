using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEmoji : MonoBehaviour {
	
	public GameObject[] emoji;
	public GameObject emojiHolder;
	
	public void showEmoji() {
		var em = Instantiate(emoji[Random.Range(0,4)]);
		em.transform.SetParent(emojiHolder.transform);
	}
}
