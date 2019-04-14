using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class open_door : MonoBehaviour {

    public GameObject textDisplay;
    public float distanceToPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        distanceToPlayer = player_casting.distanceToTarget;
        if (distanceToPlayer <= 2)
        {
            textDisplay.GetComponent<Text>().text = "Open Door";
        }
    }

    private void OnMouseExit()
    {
            textDisplay.GetComponent<Text>().text = "";
    }

}
