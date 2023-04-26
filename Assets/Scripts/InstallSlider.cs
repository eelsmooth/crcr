using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstallSlider : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
	    if(gameObject.name.Equals("sliderMusic2")){
			GetComponent<Slider>().value = 
				GameObject.Find("- Story").GetComponent<Story>().volume_mus_amb;
		}else{
			GetComponent<Slider>().value = 
				GameObject.Find("- Story").GetComponent<Story>().volume_sfx;
		}
    }
    
	void Update(){
		if(gameObject.name.Equals("sliderMusic2")){
			GameObject.Find("- Story").GetComponent<Story>().
				volume_mus_amb = GetComponent<Slider>().value;
		}else{
			GameObject.Find("- Story").GetComponent<Story>().
				volume_sfx = GetComponent<Slider>().value;
		}
	}
}
