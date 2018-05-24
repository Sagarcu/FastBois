using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevel : MonoBehaviour {

    [Tooltip("The number of the level you added to the build setting.")]
    public int levelToLoad;
    LevelChanger levelChanger;

    private void Awake()
    {
        levelChanger = GameObject.Find("FadeTo_Level").GetComponent<LevelChanger>();
    }

    public void OnTriggerEnter(Collider other)
    {
        levelChanger.FadeToLevel(levelToLoad);
    }

}
