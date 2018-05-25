using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseCanvas;
    public GameObject cam;
    private bool active = false;

	// Use this for initialization
	void Start () {
        pauseCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && active == false)
        {
            active = true;
            
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && active == true)
        {
            active = false;
        }


        if (active == true)
        {
            cam.GetComponent<CameraLook>().active = false;
            pauseCanvas.SetActive(true);
            Cursor.visible = true;
        }

        if (active == false)
        {
            cam.GetComponent<CameraLook>().active = true;
            pauseCanvas.SetActive(false);
            Cursor.visible = false;
        }


    }

    public void Quit()
    {
        Application.Quit();
    }
}
