using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class player_casting : NetworkBehaviour {

    public static float distanceToTarget;
    public float toTarget;
    public Camera playerCamera;

    // ui
    public GameObject helperText;
    public GameObject Objective1Complete;
    public GameObject objective2;
    public GameObject objective2complete;
    public GameObject objective3;

    // guns
    public GameObject realM9;
    public GameObject realMP5K;
    public GameObject gunText;

    private Ray ray;
    private int maxDistance = 2;

    private Dictionary<string, Action<RaycastHit, GameObject[]>> actions;
    private Dictionary<string, GameObject[]> actionObjects;
    private GameObject[] playerUIs;

	// Use this for initialization
	void Start () {
        actions = new Dictionary<string, Action<RaycastHit, GameObject[]>>()
        {
            {"EntryDoor", (RaycastHit hit, GameObject[] ActionplayerUIs) => { doorAction(hit, ActionplayerUIs); } },
            {"M9", (RaycastHit hit, GameObject[] ActionplayerUIs) => {gunAction(hit, ActionplayerUIs);  }},
            {"MP5K", (RaycastHit hit, GameObject[] ActionplayerUIs) => {gunAction(hit, ActionplayerUIs);  }}

        };

        actionObjects = new Dictionary<string, GameObject[]>()
        {
            {"EntryDoor", new GameObject[] {helperText, Objective1Complete, objective2} },
            {"M9", new GameObject[] {helperText, objective2complete, objective3, gunText, realM9} },
            {"MP5K", new GameObject[] {helperText, objective2complete, objective3, gunText, realMP5K} },

        };


    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer) return;

        RaycastHit hit;

        ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
                actions[hit.transform.name](hit, actionObjects[hit.transform.name]);
        }
        else
        {
            helperText.GetComponent<Text>().text = "";
        }


    }


    /* action starts here */


   // door action //
   [Command]
    private void CmdDoorAction(NetworkInstanceId id)
    {
        GameObject door = NetworkServer.FindLocalObject(id);
        open_door_network doorScript = door.GetComponent<open_door_network>();

        doorScript.playAudio();
        doorScript.triggerDoor();
        doorScript.doorAnimations[doorScript.doorOpened]();
        RpcDoorAction(id);
    }

    [ClientRpc]
    private void RpcDoorAction(NetworkInstanceId id)
    {
        GameObject door = ClientScene.FindLocalObject(id);
        open_door_network doorScript = door.GetComponent<open_door_network>();

        doorScript.playAudio();
        doorScript.triggerDoor();
        doorScript.doorAnimations[doorScript.doorOpened]();
    }

    private void doorAction(RaycastHit hit, GameObject[] ActionplayerUIs)
    /*
    ActionPlayerUIs is a list of gameobject.
    [0] = the helpertext to show on the screen
    [1] = the objective to complete
    [2] = the next objective to show
    */
    {
        NetworkInstanceId id = hit.transform.GetComponent<NetworkIdentity>().netId;
        open_door_network door = hit.transform.GetComponent<open_door_network>();
        helperText.GetComponent<Text>().text = door.helperTexts[door.doorOpened];
        if (Input.GetButtonDown("Action"))
        {
            if (isServer)
                RpcDoorAction(id);
            else
                CmdDoorAction(id);
        }
    }

    private void gunAction(RaycastHit hit, GameObject[] ActionplayerUIs)
    {
        /*
   ActionPlayerUIs is a list of gameobject.
   [0] = the helpertext to show on the screen
   [1] = the objective to complete
   [2] = the next objective to show
   [3] = the player gun  
   */
        pickup_gun_network gunScript = (pickup_gun_network)hit.transform.GetComponent(typeof(pickup_gun_network));
        if (gunScript)
            gunScript.gunAction(hit, ActionplayerUIs);

    }




}
