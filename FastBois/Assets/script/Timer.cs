using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timer;
    public bool Running;
    public float Seconds;
    public float Minutes;
    public bool Finished = false;
    private double _roundedSeconds;
    public Text timerText;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        Running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Running)
        {
            timer += Time.deltaTime;
        }
        Seconds = (timer % 60);

        Minutes = Mathf.Floor(timer / 60);
        if (Minutes == 0)
        {
            _roundedSeconds = System.Math.Round(Seconds, 3);
        }
        else
        {
            _roundedSeconds = System.Math.Round(Seconds);
        }
        if (Finished)
        {
            if (Minutes == 0)
            {
                timerText.text = ("It only took you " + _roundedSeconds + " Seconds!");
            }
            else
            {
                timerText.text = ("You finished the level in " + Minutes + " minutes and " + _roundedSeconds + " Seconds!");
            }
        }
    }

}
