using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class pause_game : MonoBehaviour {

    public GameObject player;
    public GameObject pauseMenu;
    public bool pause = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            pause = !pause;


            if (pause)
            {
                pauseMenu.SetActive(true);
                player.GetComponent<FirstPersonController>().enabled = false;
                Time.timeScale = 0;
                Cursor.visible = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                player.GetComponent<FirstPersonController>().enabled = true;
                Time.timeScale = 1;
                Cursor.visible = false;
            }
        }

    }

    public void resumeGame()
    {
        pause = !pause;
        player.GetComponent<FirstPersonController>().enabled = true;
        Time.timeScale = 1;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
}

}
