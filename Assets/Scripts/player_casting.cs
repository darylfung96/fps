using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_casting : MonoBehaviour {

    public static float distanceToTarget;
    public float toTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            toTarget = hit.distance;
            distanceToTarget = toTarget;
        }
		
	}
}
