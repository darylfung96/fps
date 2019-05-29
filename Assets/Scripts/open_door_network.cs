using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class open_door_network : NetworkBehaviour {

    [SyncVar]
    public bool doorOpened = false;

    public GameObject door;
    public string doorOpenAnimation;
    public string doorCloseAnimation;

    public Dictionary<Boolean, Action> doorAnimations; 
    public Dictionary<Boolean, string> helperTexts;

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

    public void triggerDoor()
    {
        doorOpened = !doorOpened;
        doorAnimations[doorOpened]();
    }

    public void playAudio()
    {
        GetComponent<AudioSource>().Play();
    }

    public void doorAction(GameObject[] actionPlayerUIs)
    {
        /*
           ActionPlayerUIs is a list of gameobject.
           [0] = the helpertext to show on the screen
           [1] = the objective to complete
           [2] = the next objective to show
        */
        actionPlayerUIs[0].GetComponent<Text>().text = helperTexts[doorOpened];
        //helperText.GetComponent<Text>().text = helperTexts[doorOpened];

        if (Input.GetButtonDown("Action"))
        {
            //doorAnimations[doorOpened]();
            //doorOpened = !doorOpened;
            //GetComponent<AudioSource>().Play();
            if (isServer)
                RpcDoorAction();
            else
                CmdDoorAction();


            if (actionPlayerUIs.Length > 1 && actionPlayerUIs[1] != null)
            {
                actionPlayerUIs[1].SetActive(true);
                actionPlayerUIs[2].SetActive(true);
            }
        }

    }

    [Command]
    public void CmdDoorAction()
    {
        Debug.Log("command door action");
        doorAnimations[doorOpened]();
        doorOpened = !doorOpened;
        GetComponent<AudioSource>().Play();
        RpcDoorAction();
    }

    [ClientRpc]
    public void RpcDoorAction()
    {
        Debug.Log("client door action");
        doorAnimations[doorOpened]();
        doorOpened = !doorOpened;
        GetComponent<AudioSource>().Play();
    }




}
