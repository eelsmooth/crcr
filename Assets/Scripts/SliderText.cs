using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderText : MonoBehaviour
{
	public GameObject musicIntro;
	public GameObject rainIntro;

    // Update is called once per frame
    void Update()
	{
		transform.GetChild(0).gameObject.
	    GetComponent<TextMeshProUGUI>().text 
			= (transform.GetChild(1).gameObject.GetComponent<Slider>().
			value * 100).ToString("F0") + "%";
		    
		if(musicIntro != null){
			musicIntro.GetComponent<AudioSource>().volume = 
				transform.GetChild(1).gameObject.
				GetComponent<Slider>().value;
		}
		if(rainIntro != null){
			rainIntro.GetComponent<AudioSource>().volume = 
				transform.GetChild(1).gameObject.
				GetComponent<Slider>().value;
		}
    }
}
