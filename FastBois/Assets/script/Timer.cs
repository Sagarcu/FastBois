using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    private float timer;
    public bool Running;
    public float Seconds;
	// Use this for initialization
	void Start () {
        timer = 0;
        Running = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Running)
        {
            timer += Time.deltaTime;
        }
        Seconds = (timer % 60);
    }
}
