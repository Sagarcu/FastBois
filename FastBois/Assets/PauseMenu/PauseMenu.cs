using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseCanvas;

	// Use this for initialization
	void Start () {
        pauseCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Cancel"))
        {
            pauseCanvas.SetActive(true);
        }
	}

    public void Quit()
    {
        Application.Quit();
    }
}
