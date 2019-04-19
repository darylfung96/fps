using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class open_door : MonoBehaviour {

    public GameObject textDisplay;
    public GameObject objectiveComplete;

    public float distanceToPlayer;
    public bool doorOpened = false;
    public GameObject door;
    public string doorOpenAnimation;
    public string doorCloseAnimation;

    private Dictionary<Boolean, Action> doorAnimations; 
    private Dictionary<Boolean, string> helperTexts;

    // Use this for initialization
    void Start() {
        doorAnimations = new Dictionary<Boolean, Action>()
        {
            {false, () => door.GetComponent<Animation>().Play(doorOpenAnimation) },
            {true, () => door.GetComponent<Animation>().Play(doorCloseAnimation) }
        };

        helperTexts = new Dictionary<bool, string>()
        {
            {false, "Open Door"},
            {true, "Close Door"}
        };
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnMouseOver()
    {
        distanceToPlayer = player_casting.distanceToTarget;
        if (distanceToPlayer <= 2)
        {
            textDisplay.GetComponent<Text>().text = helperTexts[doorOpened];

            if (Input.GetButtonDown("Action"))
            {
                doorAnimations[doorOpened]();
                doorOpened = !doorOpened;
                GetComponent<AudioSource>().Play();

                if (objectiveComplete != null)
                    objectiveComplete.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
            textDisplay.GetComponent<Text>().text = "";
    }

}
