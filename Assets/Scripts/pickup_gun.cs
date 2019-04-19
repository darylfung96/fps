using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickup_gun : MonoBehaviour {

    public float distanceToGun;
    public GameObject helperText;
    public GameObject fakeGun;
    public GameObject realGun;
    public GameObject gunText;
    public GameObject objectiveComplete;
    public AudioSource pickupAudio;
    public string gunName;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        distanceToGun = player_casting.distanceToTarget;
    }

    private void OnMouseOver()
    {
        distanceToGun = player_casting.distanceToTarget;
        if (distanceToGun <= 2)
        {
            helperText.GetComponent<Text>().text = "Pickup " + gunName;

            if (Input.GetButtonDown("Action"))
            {
                pickupAudio.Play();
                fakeGun.SetActive(false);
                realGun.SetActive(true);
                realGun.GetComponent<Animation>().Play("gun_reload");
                gunText.GetComponent<Text>().text = gunName;
                objectiveComplete.SetActive(true);
                helperText.GetComponent<Text>().text = "";

            }

        }
    }

    private void OnMouseExit()
    {
        helperText.GetComponent<Text>().text = "";
        }
        

}
