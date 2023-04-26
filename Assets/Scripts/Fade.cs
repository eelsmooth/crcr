using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fade : MonoBehaviour {

    public Image img;
	private float timer = 1f;

    public IEnumerator FadeIn() {
		img.color = new Color(0, 0, 0, 255);
		for (float i = 1; i >= 0f; i -= Time.deltaTime){
			img.color = new Color(0, 0, 0, i);
			yield return new WaitForSeconds(timer * Time.deltaTime);
		}
		img.color = new Color(0, 0, 0, 0);
		yield return true;
    }

    public IEnumerator FadeOut() {
		img.color = new Color(0, 0, 0, 0);
		for (float i = 0; i <= 1f; i += Time.deltaTime){
			img.color = new Color(0, 0, 0, i);
			yield return new WaitForSeconds(timer * Time.deltaTime);
		}
		img.color = new Color(0, 0, 0, 255);
		yield return true;
    }
}