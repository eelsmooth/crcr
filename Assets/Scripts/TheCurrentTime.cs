using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TheCurrentTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	    StartCoroutine(TheTime());
    }

    // Update is called once per frame
	IEnumerator TheTime()
	{
		while(true){
			string hour = "02";//DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
			string minute = "14";//DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
			GetComponent<TextMeshProUGUI>().text = hour+ ":" + minute;
			yield return new WaitForSeconds(1f);
			GetComponent<TextMeshProUGUI>().text = hour+ " " + minute;
			yield return new WaitForSeconds(1f);
		}
    }
}
