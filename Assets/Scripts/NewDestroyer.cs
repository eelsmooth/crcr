using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDestroyer : MonoBehaviour
{
    public float seconds;

    void Start()
    {
        StartCoroutine(TimerDestroyer());
    }

    private IEnumerator TimerDestroyer(){
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
