using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortChat : MonoBehaviour
{
// Update is called once per frame
    void Update()
    {
	    if(gameObject.transform.childCount > 5){
	    	Destroy(transform.GetChild(0).gameObject);
	    }
    }
    
	public void DestroyContent(){
		foreach (Transform child in gameObject.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}
}
