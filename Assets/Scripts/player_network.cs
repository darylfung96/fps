using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class player_network : NetworkBehaviour {

    public int health = 10;
    public GameObject healthDisplay;

    public GameObject showHurt;
    public List<AudioSource> hurtSounds;

    // scope
    public GameObject sniperScope;
    public GameObject crosshairObject;
    public GameObject playerCamera;
    private bool isScope;

    private int hurtSoundsCount;

	// Use this for initialization
	void Start () {
        hurtSoundsCount = hurtSounds.Count;
	}
	
	// Update is called once per frame
	void Update () {
        healthDisplay.GetComponent<Text>().text = "health: " + health;

		if (health <= 0)
        {
            SceneManager.LoadScene(3);
        }

        if (Input.GetMouseButton(1))
            toggleScope();
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<FirstPersonController>().enabled = true;
        playerCamera.SetActive(true);
    }

    private void toggleScope()
    {
        isScope = !isScope;
        if (isScope)
        {
            playerCamera.GetComponent<Camera>().fieldOfView = 25;
            sniperScope.SetActive(true);
            crosshairObject.SetActive(false);

        }
        else
        {
            playerCamera.GetComponent<Camera>().fieldOfView = 60;
            sniperScope.SetActive(false);
            crosshairObject.SetActive(true);
        }
    }

    public void decreaseHealth(int damage)
    {
        health -= damage;
        StartCoroutine(flashScreen());

        // play hurt sound
        int index = Random.Range(1, hurtSoundsCount);
        hurtSounds[index].Play();
    }

    IEnumerator flashScreen()
    {
        showHurt.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        showHurt.SetActive(false);
    }
}
