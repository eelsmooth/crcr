using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoReference : MonoBehaviour
{
    // Start is called before the first frame update
	void Start()
    {
	    GetComponent<VideoPlayer>().url = 
		    System.IO.Path.Combine 
		    (Application.streamingAssetsPath,"rain.mp4"); 
    }

}
