using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTips : MonoBehaviour {

    public Text tooltip;
    public Image Panel;
    private bool _seen = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (_seen == false)
        {
            Panel.enabled = true;
            tooltip.enabled = true;
            _seen = true;
        }

    }


    public void OnTriggerExit(Collider other)
    {
        tooltip.enabled = false;
        Panel.enabled = false;
    }
}