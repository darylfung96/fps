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

    public GameObject helperText;
    public GameObject Objective1Complete;
    public GameObject objective2;
    public GameObject objective2complete;
    public GameObject objective3;

    private Ray ray;
    private int maxDistance = 2;

    private Dictionary<string, Action<RaycastHit, GameObject[]>> actions;
    private Dictionary<string, GameObject[]> actionObjects;
    private GameObject[] playerUIs;

	// Use this for initialization
	void Start () {
        actions = new Dictionary<string, Action<RaycastHit, GameObject[]>>()
        {
            {"EntryDoor", (RaycastHit hit, GameObject[] ActionplayerUIs) => { doorAction(hit, ActionplayerUIs); } }

        };

        actionObjects = new Dictionary<string, GameObject[]>()
        {
            {"EntryDoor", new GameObject[] {helperText, Objective1Complete, objective2} }
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

    private void doorAction(RaycastHit hit, GameObject[] ActionplayerUIs)
    /*
    ActionPlayerUIs is a list of gameobject.
    [0] = the helpertext to show on the screen
    [1] = the objective to complete
    [2] = the next objective to show
    */
    {
        open_door_network doorScript = (open_door_network) hit.transform.GetComponent(typeof(open_door_network));
        if (doorScript)
            doorScript.doorAction(ActionplayerUIs);

    }




}
