using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

    public GameObject player;
    public GameObject pauseScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playGame()
    {
        SceneManager.LoadScene(2);
    }

    public void resumeGame()
    {
        pause_game pauseClass = (pause_game) pauseScript.GetComponent(typeof(pause_game));
        pauseClass.resumeGame();
    }

    public void respawnGame()
    {
        resumeGame();
        Debug.Log("hello");
        SceneManager.LoadScene(4);
    }
}
