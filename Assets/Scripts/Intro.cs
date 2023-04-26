using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

    private Fade fade;
    [SerializeField] private GameObject intro1;
    [SerializeField] private GameObject intro2;

    // Start is called before the first frame update
    void Start() {
        fade = GameObject.Find("Fade").GetComponent<Fade>();
    }
}
