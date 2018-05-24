using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    private float timer;
    public bool Running;
    public float Seconds;
    private double _roundedSeconds;
    public Text timerText;

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
        _roundedSeconds = System.Math.Round(Seconds, 3);

        timerText.text = ("Time: " + _roundedSeconds);
    }

}
