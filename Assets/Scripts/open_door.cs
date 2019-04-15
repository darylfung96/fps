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
    public Dictionary<Boolean, System.Action> doorAnimations;
    public Dictionary<Boolean, string> helperTexts;


    // Use this for initialization
    void Start() {
        doorAnimations = new Dictionary<Boolean, Action>()
        {
            {false, () => GetComponent<Animation>().Play("door_open_animation") },
            {true, () => GetComponent<Animation>().Play("door_close_animation") }
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
                objectiveComplete.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
            textDisplay.GetComponent<Text>().text = "";
    }

}
