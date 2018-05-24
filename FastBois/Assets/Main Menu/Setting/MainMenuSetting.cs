using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSetting : MonoBehaviour {

    public int level;

    public void NewGame()
    {
        SceneManager.LoadScene(level);
        //Application.LoadLevel(level);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
