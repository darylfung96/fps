using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class pickup_gun_network : NetworkBehaviour {

    public float distanceToGun;
    public GameObject fakeGun;
    public AudioSource audioPickup;
    public string gunName;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        distanceToGun = player_casting.distanceToTarget;
    }

    public void gunAction(RaycastHit hit, GameObject[] actionPlayerUIs)
    {
        GameObject helperText = actionPlayerUIs[0];
        helperText.GetComponent<Text>().text = "Pickup " + gunName;
        if (Input.GetButtonDown("Action"))
        {
            GameObject objectiveComplete = actionPlayerUIs[1];
            GameObject showNextObjective = actionPlayerUIs[2];
            GameObject gunText = actionPlayerUIs[3];
            GameObject realGun = actionPlayerUIs[4];

            audioPickup.Play();
            fakeGun.SetActive(false);
            realGun.SetActive(true);
            realGun.GetComponent<Animation>().Play("gun_reload");
            gunText.GetComponent<Text>().text = gunName;
            objectiveComplete.SetActive(true);
            showNextObjective.SetActive(true);
            helperText.GetComponent<Text>().text = "";

        }
    }

        

}
